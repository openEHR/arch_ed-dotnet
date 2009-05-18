using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;

#if XMLParser
using XMLParser;
#endif

namespace XMLParser.OpenEhr.V1.Its.Xml.AM
{
    static public class AmSerializer
    {
#if XMLParser
        private const string SCHEMA_LOCATOR_PATTERN = "{0}.{1}.xsd";
#else
        private const string SCHEMA_LOCATOR_PATTERN = "{0}.Schemas.{1}.xsd";
#endif
        static System.Xml.Serialization.XmlSerializer xmlSchemaSerializer;
        static object xmlSchemaSerializerLock = new object();

        static System.Xml.Serialization.XmlSerializer XmlSchemaSerializer
        {
            get
            {
                if (xmlSchemaSerializer == null)
                {
                    lock (xmlSchemaSerializerLock)
                    {
                        if (xmlSchemaSerializer == null)
                            xmlSchemaSerializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlSchema));
                    }
                }
                return xmlSchemaSerializer;
            }
        }

        public static XmlSchema GetSchema(string schemaName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            //string[] names = assembly.GetManifestResourceNames();
            AssemblyName name = assembly.GetName();

            string resourceName = string.Format(SCHEMA_LOCATOR_PATTERN, name.Name, schemaName);
            XmlSchema schema = null;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new ArgumentException("schemaName", "Schema resource " + resourceName + " not found in manifest");

                schema = (XmlSchema)XmlSchemaSerializer.Deserialize(new XmlTextReader(stream));
            }

            return schema;
        }

        private static object archetypeSchemaLock = new object();
        private static volatile XmlSchemaSet archetypeSchemaSet;

        internal static XmlSchemaSet ArchetypeSchemaSet
        {
            get
            {
                if (archetypeSchemaSet == null)
                {
                    lock (archetypeSchemaLock)
                    {
                        if (archetypeSchemaSet == null)
                        {
                            XmlSchema amSchema = GetSchema("OpenehrProfile");
                            amSchema.Includes.RemoveAt(0);

                            XmlSchema schema = GetSchema("Archetype");

                            foreach (XmlSchemaObject item in schema.Items)
                                amSchema.Items.Add(item);

                            XmlSchema contentSchema = GetSchema("Resource");

                            foreach (XmlSchemaObject item in contentSchema.Items)
                                amSchema.Items.Add(item);

                            XmlSchema baseTypesSchema = GetSchema("BaseTypes");

                            foreach (XmlSchemaObject item in baseTypesSchema.Items)
                                amSchema.Items.Add(item);

                            XmlSchemaSet tempSchemaSet = new XmlSchemaSet();
                            tempSchemaSet.Add(amSchema);
                            tempSchemaSet.Compile();

                            archetypeSchemaSet = tempSchemaSet;
                        }
                    }
                }
                return archetypeSchemaSet;
            }
        }

        private static object archetypeSerialiserLock = new object();
        private static volatile System.Xml.Serialization.XmlSerializer archetypeSerialiser = null;

        static private System.Xml.Serialization.XmlSerializer ArchetypeSerialiser
        {
            get
            {
                if (archetypeSerialiser == null)
                {
                    lock (archetypeSerialiserLock)
                    {
                        if (archetypeSerialiser == null)
                            archetypeSerialiser =
                                new System.Xml.Serialization.XmlSerializer(typeof(ARCHETYPE));
                    }
                }
                return archetypeSerialiser;
            }
        }

        public static void Serialize(XmlWriter writer, ARCHETYPE archetype)
        {
            ArchetypeSerialiser.Serialize(writer, archetype);
        }

        public static System.IO.MemoryStream Serialize(XmlWriterSettings settings, ARCHETYPE archetype)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            if (settings != null)
            {
                System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(stream, settings);
                ArchetypeSerialiser.Serialize(writer, archetype);
            }
            else
                ArchetypeSerialiser.Serialize(stream, archetype);

            if (stream == null)
                throw new ApplicationException("stream must not be null");

            return stream;
        }

        public static void ValidateArchetype(System.IO.MemoryStream archetypeStream)
        {
            long position = archetypeStream.Position;
            archetypeStream.Position = 0;

            System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
            settings.Schemas.Add(ArchetypeSchemaSet);
            settings.ValidationType = System.Xml.ValidationType.Schema;

            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(archetypeStream, settings))
            {
                //System.Xml.XmlDocument archetypeDoc = new System.Xml.XmlDocument();
                //    archetypeDoc.Load(reader);
                ARCHETYPE archetype = ArchetypeSerialiser.Deserialize(reader) as ARCHETYPE;
                if (archetype == null)
                    throw new ApplicationException("application must not be null");

                reader.Close();
            }

            archetypeStream.Position = position;
        }

        public static bool ValidateArchetype(ARCHETYPE archetype)
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            settings.Indent = false;

            System.IO.MemoryStream stream = Serialize(settings, archetype);
            AmSerializer.ValidateArchetype(stream);

            return true;
        }
    }
}
