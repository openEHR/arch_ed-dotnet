using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.IO;

#if XMLParser
using XMLParser;
#else
using OpenEhr.V1.Its.Xml.AM;
#endif

namespace XMLParser.OpenEhr.V1.Its.Xml.AM
{
    public static class ArchetypeModelBuilder
    {
        public const string ARCHETYPE_DIGEST_ID = "MD5-CAM-1.0.1";

        public static ARCHETYPE BuildFromAdlFile(string adlFilePath, CloneConstraintVisitor visitor)
        {
            //Check.Assert(System.IO.File.Exists(adlFilePath), "File '" + adlFilePath + "' does not exist.");

            ARCHETYPE result = null;
            AdlParser.ArchetypeParser adlParser = AdlParser.Create.ArchetypeParser.DefaultCreate();
            adlParser.ArchDir().AddAdhocItem(Eiffel.String(adlFilePath));
            AdlParser.ArchRepArchetype arch = adlParser.SelectedArchetype();

            if (arch == null || adlParser.AppRoot().Billboard().HasErrors())
                throw new ApplicationException(String.Format("{0}\n{1}", adlFilePath, adlParser.AppRoot().Billboard().Content().ToCil()));
            else
            {
                arch.Compile();

                if (arch.IsValid())
                    result = Build(arch.FlatArchetype(), visitor);
                else
                    throw new ApplicationException(String.Format("{0}\n{1}", Path.GetFileName(adlFilePath), arch.CompilationResult().ToCil()));
            }
            
            return result;
        }

        public static ARCHETYPE Build(AdlParser.Archetype archetype, CloneConstraintVisitor visitor)
        {
            if (archetype == null)
                throw new ArgumentNullException("archetype must not be null");

            if (visitor == null)
                throw new ArgumentNullException("visitor must not be null");

            ARCHETYPE result = visitor.CloneArchetype(archetype);
            AmSerializer.ValidateArchetype(result);
            return result;
        }

        public static ARCHETYPE CanonicalArchetype(ARCHETYPE archetype)
        {
            return new CanonicalAmVisitor().VisitArchetype(archetype);
        }

        public static string ArchetypeDigest(ARCHETYPE archetype)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            settings.Indent = false;

            MemoryStream archetypeStream = AmSerializer.Serialize(settings, archetype);
            AmSerializer.ValidateArchetype(archetypeStream);

//#if DEBUG
//            archetypeStream.Position = 0;
//            System.IO.StreamReader reader = new System.IO.StreamReader(archetypeStream);
//            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("CanonicalArchetype.xml", false, Encoding.UTF8))
//            {
//                writer.Write(reader.ReadToEnd());
//                writer.Close();
//                reader.Close();
//            }
//#endif

            byte[] data = archetypeStream.ToArray();

            // Remove UTF-8 BOM 
            int offset = 0;

            if (data.Length < 1)
                throw new ApplicationException("Canonical archetype model must have length greater than 0");

            if (data[0] == 239) // UTF-8 BOM: EF BB BF (239 187 191)
            {
                offset = 3;

                if (data.Length <= offset)
                    throw new ApplicationException("Canonical archetype model must have length greater than the BOM offset");
            }

            if (data[offset] != 60) // XML root element (<)
                throw new ApplicationException("Unexpected start character of canonical archetype model");

            return new SoapHexBinary(new MD5CryptoServiceProvider().ComputeHash(data, offset, data.Length - offset)).ToString();
        }
    }
}
