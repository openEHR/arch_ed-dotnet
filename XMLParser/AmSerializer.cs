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

namespace OpenEhr.V1.Its.Xml.AM
{
    static public class AmSerializer
    {
#if XMLParser
        private const string SCHEMA_LOCATOR_PATTERN = "{0}.{1}.xsd";
#else
        private const string SCHEMA_LOCATOR_PATTERN = "{0}.Schemas.{1}.xsd";
#endif

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
                            Assembly assembly = Assembly.GetExecutingAssembly();
                            string[] archetypeSchemaNames = { "BaseTypes", "Resource", "Archetype", "OpenehrProfile" };
                            AssemblyName name = assembly.GetName();

                            XmlSchemaSet tempSchemaSet = new XmlSchemaSet();
                            foreach (string schemaName in archetypeSchemaNames)
                            {
                                string schemaPath = string.Format(SCHEMA_LOCATOR_PATTERN, name.Name, schemaName);
                                using (XmlReader schemaReader = XmlTextReader.Create(assembly.GetManifestResourceStream(schemaPath)))
                                {
                                    tempSchemaSet.Add(XmlSchema.Read(schemaReader, null));
                                }
                            }
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
