using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

using XMLParser;

namespace OceanInformatics.ArchetypeModel
{
    public static class ArchetypeModelBuilder
    {
        public const string ARCHETYPE_DIGEST_ID = "MD5-CAM-1.0.1";

        public static ARCHETYPE Build(openehr.openehr.am.archetype.ARCHETYPE archetype)
        {
            if (archetype == null)
                throw new ArgumentNullException("archetype must not be null");

            CloneConstraintVisitor cloneVisitor = new CloneConstraintVisitor();

            //Clone eiffel ADL archetype as OpenEhr.V1.Its.Xml.AM.ARCHETYPE
            ARCHETYPE archetypeObject = cloneVisitor.CloneArchetype(archetype);

            if(archetypeObject == null)
                throw new ApplicationException("Archetype object must not be null");

            XmlSerializer.ValidateArchetype(archetypeObject);
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

            System.IO.MemoryStream archetypeStream = XmlSerializer.Serialize(settings, archetype);
            XmlSerializer.ValidateArchetype(archetypeStream);

#if DEBUG
            archetypeStream.Position = 0;
            System.IO.StreamReader reader = new System.IO.StreamReader(archetypeStream);
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("CanonicalArchetype.xml", false, Encoding.UTF8))
            {
                writer.Write(reader.ReadToEnd());
                writer.Close();
                reader.Close();
            }
#endif
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
