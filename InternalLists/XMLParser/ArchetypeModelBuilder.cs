using System;
using System.Collections.Generic;
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

        public static ARCHETYPE Build(string adlFilePath)
        {
            //Check.Assert(System.IO.File.Exists(adlFilePath), "File '" + adlFilePath + "' does not exist.");

            ARCHETYPE archetypeObject;
            openehr.adl_parser.@interface.ADL_INTERFACE adlParser;
            adlParser = openehr.adl_parser.@interface.Create.ADL_INTERFACE.make();
            adlParser.open_adl_file(EiffelSoftware.Library.Base.kernel.Create.STRING_8.make_from_cil(adlFilePath));

            // check file opened successfully by checking status
            if (!adlParser.archetype_source_loaded())
                throw new ApplicationException(String.Format("{0}\n{1}",
                    adlFilePath,
                    adlParser.status().to_cil()));
            else
            {
                adlParser.parse_archetype();
                if (!adlParser.parse_succeeded())
                    throw new ApplicationException(String.Format("{0}\n{1}",
                         Path.GetFileName(adlFilePath),
                         adlParser.status().to_cil()));
                else
                {
                    openehr.adl_parser.syntax.adl.ADL_ENGINE adlEngine = adlParser.adl_engine();
                    if (!adlParser.archetype_available())
                        throw new ApplicationException(String.Format("{0}\n{1}",
                                           adlFilePath,
                                           adlParser.status().to_cil()));
                    else                                            
                        archetypeObject = Build(adlEngine.archetype());                    
                }
            }
            
            return archetypeObject;
        }

        public static ARCHETYPE Build(openehr.openehr.am.archetype.ARCHETYPE archetype)
        {
            if (archetype == null)
                throw new ArgumentNullException("archetype must not be null");

            CloneConstraintVisitor cloneVisitor = new CloneConstraintVisitor();

            //Clone eiffel ADL archetype as OpenEhr.V1.Its.Xml.AM.ARCHETYPE
            ARCHETYPE archetypeObject = cloneVisitor.CloneArchetype(archetype);

            if(archetypeObject == null)
                throw new ApplicationException("Archetype object must not be null");

            AmSerializer.ValidateArchetype(archetypeObject);
            return archetypeObject;            
        }

        public static ARCHETYPE CanonicalArchetype(ARCHETYPE archetype)
        {
            CanonicalAmVisitor visitor = new CanonicalAmVisitor();
            ARCHETYPE canonicalArchetype = visitor.VisitArchetype(archetype);

            return canonicalArchetype;
        }

        public static string ArchetypeDigest(ARCHETYPE archetype)
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            settings.Indent = false;

            System.IO.MemoryStream archetypeStream = AmSerializer.Serialize(settings, archetype);
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

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            SoapHexBinary hexEncoder = new SoapHexBinary(md5.ComputeHash(data, offset, data.Length-offset));
            string digest = hexEncoder.ToString();

            return digest;
        }
    }
}
