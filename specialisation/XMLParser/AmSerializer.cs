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
#else
using OpenEhr.V1.Its.Xml.AM;
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
        static XmlSerializer xmlSchemaSerializer;
        static object xmlSchemaSerializerLock = new object();

        static XmlSerializer XmlSchemaSerializer
        {
            get
            {
                if (xmlSchemaSerializer == null)
                {
                    lock (xmlSchemaSerializerLock)
                    {
                        if (xmlSchemaSerializer == null)
                            xmlSchemaSerializer = new XmlSerializer(typeof(XmlSchema));
                    }
                }

                return xmlSchemaSerializer;
            }
        }

        public static XmlSchema GetSchema(string schemaName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
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
        private static volatile XmlSerializer archetypeSerialiser = null;

        static private XmlSerializer ArchetypeSerialiser
        {
            get
            {
                if (archetypeSerialiser == null)
                {
                    lock (archetypeSerialiserLock)
                    {
                        if (archetypeSerialiser == null)
                            archetypeSerialiser = new XmlSerializer(typeof(ARCHETYPE));
                    }
                }

                return archetypeSerialiser;
            }
        }

        public static void Serialize(XmlWriter writer, ARCHETYPE archetype)
        {
            ArchetypeSerialiser.Serialize(writer, archetype);
        }

        public static MemoryStream Serialize(XmlWriterSettings settings, ARCHETYPE archetype)
        {
            MemoryStream result = new MemoryStream();

            if (settings != null)
            {
                XmlWriter writer = XmlWriter.Create(result, settings);
                ArchetypeSerialiser.Serialize(writer, archetype);
            }
            else
                ArchetypeSerialiser.Serialize(result, archetype);

            if (result == null)
                throw new ApplicationException("stream must not be null");

            return result;
        }

        public static void ValidateArchetype(MemoryStream archetypeStream)
        {
            long position = archetypeStream.Position;
            archetypeStream.Position = 0;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(ArchetypeSchemaSet);
            settings.ValidationType = ValidationType.Schema;

            using (XmlReader reader = XmlReader.Create(archetypeStream, settings))
            {
                ARCHETYPE archetype = ArchetypeSerialiser.Deserialize(reader) as ARCHETYPE;

                if (archetype == null)
                    throw new ApplicationException("application must not be null");
            }

            archetypeStream.Position = position;
        }

        public static bool ValidateArchetype(ARCHETYPE archetype)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            settings.Indent = false;

            MemoryStream stream = Serialize(settings, archetype);
            AmSerializer.ValidateArchetype(stream);

            return true;
        }
    }
}
