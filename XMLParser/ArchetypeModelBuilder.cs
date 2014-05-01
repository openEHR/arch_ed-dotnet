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
            openehr.adl_parser.@interface.ADL_INTERFACE adlParser;
            adlParser = openehr.adl_parser.@interface.Create.ADL_INTERFACE.make();
            adlParser.open_adl_file(Eiffel.String(adlFilePath));

            // check file opened successfully by checking status
            if (!adlParser.archetype_source_loaded())
                throw new ApplicationException(String.Format("{0}\n{1}", adlFilePath, adlParser.status().to_cil()));
            else
            {
                adlParser.parse_archetype();

                if (!adlParser.parse_succeeded())
                    throw new ApplicationException(String.Format("{0}\n{1}", Path.GetFileName(adlFilePath), adlParser.status().to_cil()));
                else if (!adlParser.archetype_available())
                    throw new ApplicationException(String.Format("{0}\n{1}", adlFilePath, adlParser.status().to_cil()));
                else
                    result = Build(adlParser.adl_engine().archetype(), visitor);
            }
            
            return result;
        }

        public static ARCHETYPE Build(openehr.openehr.am.archetype.ARCHETYPE archetype, CloneConstraintVisitor visitor)
        {
            if (archetype == null)
                throw new ArgumentNullException("archetype must not be null");

            if (visitor == null)
                throw new ArgumentNullException("visitor must not be null");

            ARCHETYPE result = visitor.CloneArchetype(archetype);
#if XMLParser
            AmSerializer.ValidateArchetype(result);
#endif
            return result;            
        }

        public static ARCHETYPE CanonicalArchetype(ARCHETYPE archetype)
        {
            return new CanonicalAmVisitor().VisitArchetype(archetype);
        }

        public static string ArchetypeDigest(ARCHETYPE archetype)
        {
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true,
                Indent = false
            };

//#if DEBUG
//            using (var writer = XmlWriter.Create(@".\CanonicalArchetype2.xml", new XmlWriterSettings { Indent = true }))
//                AmSerializer.Serialize(writer, archetype);
//#endif

            byte[] data;

            using (MemoryStream stream = AmSerializer.Serialize(settings, archetype))
            {
#if XMLParser
                AmSerializer.ValidateArchetype(stream);
#endif
                data = stream.ToArray();
            }

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

            MD5 md5 = new MD5CryptoServiceProvider();
            SoapHexBinary hexEncoder = new SoapHexBinary(md5.ComputeHash(data, offset, data.Length-offset));
            return hexEncoder.ToString();
        }
    }
}
