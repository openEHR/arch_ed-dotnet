using System;
using System.Collections.Generic;
using System.Text;

using OpenEhr.V1.Its.Xml.AM;

[assembly: CLSCompliant(true)]


namespace XMLParser
{
    public class XmlArchetypeParser
    {
        ARCHETYPE _archetype;
        
        public XmlArchetypeParser()
        {

        }

        Ontology _ontology;

        public Ontology Ontology
        {
            get
            {
                if (_ontology == null)
                {
                    _ontology = new Ontology(_archetype);
                    _ontology.LanguageAdded += new LanguageAddedEventHandler(ontology_LanguageAdded);
                }
                return _ontology;
            }
        }

        private void ontology_LanguageAdded(object sender, string language, string defaultLanguageCodeSet)
        {
            CODE_PHRASE cp = new CODE_PHRASE();
            cp.code_string = language;
            
            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.12
            if (defaultLanguageCodeSet != "")
            {                
                TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                terminologyId.value = defaultLanguageCodeSet;
                cp.terminology_id = terminologyId;                
            }
            
            AddDescriptionItem(cp);
        }

        string _file_name;

        public string FileName
        {
            get
            {
                return _file_name;
            }
            set
            {
               _file_name = value;
            }
        }

        public bool ArchetypeAvailable
        {
            get
            {
                return _archetype != null;
            }
        }

        string _status;

        public string Status
        {
            get
            {
                return _status;
            }
        }

        public ARCHETYPE Archetype
        {
            get
            {
                return _archetype;
            }
        }

        bool _open_file_error = false;

        public bool OpenFileError
        {
            get
            {
                return _open_file_error;
            }
        }

        public void SpecialiseArchetype(string addition_to_concept_name)
        {
            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            //Increase the specialisation depth
            //int ii  = int.Parse(_archetype.ontology.specialisation_depth);
            //ii++;
            //_archetype.ontology.specialisation_depth = ii.ToString();

            //set the current archetype id to the parent value
            //_archetype.parent_archetype_id = _archetype.archetype_id; 
            if (_archetype.parent_archetype_id != null)
                _archetype.parent_archetype_id.value = _archetype.archetype_id.value; //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1

            //get the specialised concept code
            _archetype.concept = _ontology.NextSpecialisedTermId(_archetype.concept);

            //now add the addition to the concept name in the archetype ID
            string[] y = _archetype.archetype_id.value.ToString().Split(".".ToCharArray());
            _archetype.archetype_id.value = y[0] + "." + y[1] + "-" + addition_to_concept_name;
            for (int i = 2; i < y.Length; i++)
            {
                _archetype.archetype_id.value += "." + y[i];
            }

            //don't overwrite the old archetype
            _file_name = null;

        }

        public void SetDefinitionId(string a_concept_id)
        {
            if (_archetype.definition == null)
            {
                _archetype.definition = new C_COMPLEX_OBJECT();
            }
            _archetype.definition.node_id = a_concept_id;
        }

        bool _write_file_error = false;

        public bool WriteFileError
        {
            get
            {
                return _write_file_error;
            }
        }

        public string TypeName
        {
            get
            {
                if (this.ArchetypeAvailable)
                {
                    //string[] y = _archetype.archetype_id.Split("-.".ToCharArray()); //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                    string[] y = _archetype.archetype_id.value.ToString().Split("-.".ToCharArray());//JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                    return y[2];
                }
                return "";
            }
        }

        public void ResetAll()
        {
            //nothing needed as yet
        }

        public string Serialise()
        {
            string result = "";

            if(this.ArchetypeAvailable)
            {
                // HKF: 8 Dec 2008
                SetArchetypeDigest();

                // HKF: 8 Dec 2008
                //System.Xml.Serialization.XmlSerializer xmlSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(ARCHETYPE));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //xmlSerialiser.Serialize(ms, _archetype);
                ms = AmSerializer.Serialize((System.Xml.XmlWriterSettings)null, _archetype);

                ms.Position = 0;
                System.IO.StreamReader a_reader = new System.IO.StreamReader(ms);
                result = a_reader.ReadToEnd();
                a_reader.Close();
            }
            
            return result;
        }
        
        // HKF 8 Dec 2008
        void SetArchetypeDigest()
        {
            System.Diagnostics.Debug.Assert(_archetype != null, "archetype must not be null");
            System.Diagnostics.Debug.Assert(_archetype.description != null, "archetype description must not be null");

            XMLParser.ARCHETYPE canonicalArchetype = ArchetypeModelBuilder.CanonicalArchetype(_archetype);

            string archetypDigest = ArchetypeModelBuilder.ArchetypeDigest(canonicalArchetype);

            Dictionary<string, StringDictionaryItem> otherDetails = new Dictionary<string, StringDictionaryItem>();
            if (_archetype.description.other_details != null)
            {
                foreach (StringDictionaryItem item in _archetype.description.other_details)
                    otherDetails.Add(item.id, item);
            }
            if (!otherDetails.ContainsKey(ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID))
            {
                StringDictionaryItem item = new StringDictionaryItem();
                item.id = ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID;
                item.Value = archetypDigest;

                otherDetails.Add(ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID, item);
            }
            else
            {
                StringDictionaryItem item = otherDetails[ArchetypeModelBuilder.ARCHETYPE_DIGEST_ID];

                item.Value = archetypDigest;
            }

            StringDictionaryItem[] sortedResult = new StringDictionaryItem[otherDetails.Count];
            otherDetails.Values.CopyTo(sortedResult, 0);

            _archetype.description.other_details = sortedResult;
        }

        public System.Collections.ArrayList AvailableFormats
        {
            get
            {
                System.Collections.ArrayList a_list = new System.Collections.ArrayList();
                a_list.Add("xml");
                return a_list;
            }
        }

        public void NewArchetype(string an_archetypeID, string a_languageCode, string defaultLanguageCodeSet)
        {
            _archetype = new ARCHETYPE();
            
            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            //_archetype.archetype_id = an_archetypeID;            
            ARCHETYPE_ID archetypeId = new ARCHETYPE_ID();
            archetypeId.value = an_archetypeID;
            _archetype.archetype_id = archetypeId;            
            _archetype.description = new RESOURCE_DESCRIPTION();
            _archetype.original_language = new CODE_PHRASE();
            _archetype.original_language.code_string = a_languageCode;
            
            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            //_archetype.original_language.terminology_id = defaultLanguageCodeSet;
            if (defaultLanguageCodeSet != "")
            {
                TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                terminologyId.value = defaultLanguageCodeSet;
                _archetype.original_language.terminology_id = terminologyId;
            }
            
            _archetype.description.lifecycle_state = "AuthorDraft";
            _archetype.concept = "at0000";
            _archetype.definition = new C_COMPLEX_OBJECT();
            _archetype.definition.node_id = _archetype.concept;
            string[] y = an_archetypeID.Split(".-".ToCharArray());
            _archetype.definition.rm_type_name = y[2];
            _archetype.ontology = new ARCHETYPE_ONTOLOGY();
            _archetype.ontology.term_definitions = Array.CreateInstance(typeof(CodeDefinitionSet), 1) as CodeDefinitionSet[];
            _archetype.ontology.term_definitions[0] = new CodeDefinitionSet();
            _archetype.ontology.term_definitions[0].language = a_languageCode;
            _archetype.ontology.term_definitions[0].items = Array.CreateInstance(typeof(ARCHETYPE_TERM), 1) as ARCHETYPE_TERM[];
            _archetype.ontology.term_definitions[0].items[0] = new ARCHETYPE_TERM();
            _archetype.ontology.term_definitions[0].items[0].code = "at0000";
            _archetype.ontology.term_definitions[0].items[0].items = Array.CreateInstance(typeof(StringDictionaryItem), 2) as StringDictionaryItem[];
            _archetype.ontology.term_definitions[0].items[0].items[0] = new StringDictionaryItem();
            _archetype.ontology.term_definitions[0].items[0].items[0].id = "text";
            _archetype.ontology.term_definitions[0].items[0].items[0].Value = "?";
            _archetype.ontology.term_definitions[0].items[0].items[1] = new StringDictionaryItem();
            _archetype.ontology.term_definitions[0].items[0].items[1].id = "description";
            _archetype.ontology.term_definitions[0].items[0].items[1].Value = "*";

            AddDescriptionItem(_archetype.original_language);
        }

        internal void AddDescriptionItem(CODE_PHRASE a_language)
        {
            RESOURCE_DESCRIPTION_ITEM rdi = new RESOURCE_DESCRIPTION_ITEM();
            RESOURCE_DESCRIPTION_ITEM[] descriptionItems;
            rdi.language = a_language;
            if (_archetype.description.details == null)
            {
                //create an array
                descriptionItems = Array.CreateInstance(typeof(RESOURCE_DESCRIPTION_ITEM), 1) as RESOURCE_DESCRIPTION_ITEM[];
                descriptionItems[0] = rdi;
            }
            else
            {
                //resize the array
                descriptionItems = _archetype.description.details;
                int i = descriptionItems.Length;
                Array.Resize(ref descriptionItems, i + 1);
                descriptionItems[i] = rdi;

                foreach (RESOURCE_DESCRIPTION_ITEM descItem in _archetype.description.details)
                {
                    //copy from the primary language
                    string originalLanguage = _archetype.original_language.code_string;
                    if (descItem.language.code_string == originalLanguage)
                    {
                        rdi.purpose = string.Format("{0}{1}({2})", "*", descItem.purpose, originalLanguage);
                        if (rdi.use != null && rdi.use != "")
                        {
                            rdi.use = string.Format("{0}{1}({2})", "*", descItem.use, originalLanguage);
                        }
                        if (rdi.misuse != null && rdi.misuse != "")
                        {
                            rdi.misuse = string.Format("{0}{1}({2})", "*", descItem.misuse, originalLanguage);
                        }
                        //if (rdi.original_resource_uri != null && rdi.original_resource_uri != "") //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                        if (rdi.original_resource_uri != null && rdi.original_resource_uri.ToString() != "")
                        {                            
                            rdi.original_resource_uri = descItem.original_resource_uri;
                        }
                    }
                }
            }
            _archetype.description.details = descriptionItems;
            // HKF: 8 Dec 2008
            //AddTranslation(a_language);
        }

        public void AddTranslation(CODE_PHRASE a_language)
        {
            TRANSLATION_DETAILS td = new TRANSLATION_DETAILS();
            TRANSLATION_DETAILS[] translationDetails;
            td.language = a_language;
            if (_archetype.translations == null)
            {
                //create an array
                translationDetails = Array.CreateInstance(typeof(TRANSLATION_DETAILS), 1) as TRANSLATION_DETAILS[];
                translationDetails[0] = td;
            }
            else
            {
                //resize the array
                translationDetails = _archetype.translations;
                int i = translationDetails.Length;
                Array.Resize(ref translationDetails, i + 1);
                translationDetails[i] = td;
            }
            _archetype.translations = translationDetails;    
        }

        public void ReadXml(string xmlString)
        {
            _open_file_error = false;
            _status = "";
            _archetype = null;
            _ontology = null; //reset the ontology

            try
            {
                System.IO.StringReader stringReader = new System.IO.StringReader(xmlString);
                System.Xml.XmlReader xml_reader = System.Xml.XmlReader.Create(stringReader);
                System.Xml.Serialization.XmlSerializer xmlSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(ARCHETYPE));
				ARCHETYPE new_archetype = xmlSerialiser.Deserialize(xml_reader) as ARCHETYPE;
                xml_reader.Close();

				// Reset the ontology and load the new archetype if there is no error.
				_ontology = null;
				_archetype = new_archetype;
			}
            catch (Exception e)
            {
                _open_file_error = true;
                _status = "Open file error: " + e.ToString();
				System.Diagnostics.Debug.Assert(false, e.ToString());
            }
        }

        public void OpenFile(string a_file_name)
        {
            _open_file_error = false;
            _status = "";
            _archetype = null;
            _ontology = null; //reset the ontology

            try
            {
                System.Xml.XmlReader xml_reader
                    = System.Xml.XmlReader.Create(a_file_name);
                System.Xml.Serialization.XmlSerializer xmlSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(ARCHETYPE));
				ARCHETYPE new_archetype = xmlSerialiser.Deserialize(xml_reader) as ARCHETYPE;
                xml_reader.Close();

				// Reset the ontology and load the new archetype if there is no error.
				_ontology = null;
				_archetype = new_archetype;
			}
            catch (Exception e)
            {
                _open_file_error = true;
                _status = "Open file error: " + e;
                System.Diagnostics.Debug.Assert(false, e.ToString());
            }
        }

        public void WriteFile(string a_file_name)
        {
            _write_file_error = false;
            _status = "";

            try
            {
                _ontology.RemoveUnusedCodes();

                // HKF: 8 Dec 2008
                SetArchetypeDigest();

                System.Xml.XmlWriter xml_writer = System.Xml.XmlWriter.Create(a_file_name);
                // HKF: 8 Dec 2008
                //System.Xml.Serialization.XmlSerializer xmlSerialiser = new System.Xml.Serialization.XmlSerializer(typeof(ARCHETYPE));
                //xmlSerialiser.Serialize(xml_writer, _archetype);
                AmSerializer.Serialize(xml_writer, _archetype);

                xml_writer.Close();
            }
            catch (Exception e)
            {
                _write_file_error = true;
                _status = "Error writing file: " + e;
                System.Diagnostics.Debug.Assert(false, e.ToString());
            }
        }

        public System.Collections.ArrayList PhysicalPaths()
        {
            System.Collections.ArrayList all_paths = new System.Collections.ArrayList();

            get_paths(all_paths, _archetype.definition, "");
            return all_paths;
        }

        public System.Collections.ArrayList LogicalPaths(string a_language_code)
        {
           System.Collections.ArrayList logical_paths = new System.Collections.ArrayList();

            get_logical_paths(logical_paths, _archetype.definition, "", a_language_code);
            return logical_paths;        
        }

        private void get_paths(System.Collections.ArrayList list, C_COMPLEX_OBJECT node, string path)
        {
            if (path != "")
            {
                if (!string.IsNullOrEmpty(node.node_id)) 
                    path += "[" + node.node_id + "]";
                list.Add(path);
            }

            if (node.attributes != null)
            {
                foreach (C_ATTRIBUTE attrib in node.attributes)
                {
                    if (attrib.children != null)
                    {
                        if (attrib.GetType() == typeof(C_MULTIPLE_ATTRIBUTE))
                        {
                            foreach (C_OBJECT obj in attrib.children)
                            {
                                C_COMPLEX_OBJECT co = obj as C_COMPLEX_OBJECT;

                                if (co != null && !string.IsNullOrEmpty(co.node_id))
                                    get_paths(list, co, path + "/" + attrib.rm_attribute_name);
                            }
                        }
                        else //single attribute
                        {
                            C_COMPLEX_OBJECT co = attrib.children[0] as C_COMPLEX_OBJECT;

                            //if (co != null && !string.IsNullOrEmpty(co.node_id))
                            if (co != null)
                                get_paths(list, co, path + "/" + attrib.rm_attribute_name);
                        }
                    }
                }
            }
        }


        private void get_logical_paths(System.Collections.ArrayList list, C_COMPLEX_OBJECT node, string path, string a_language_code)
        {
            if (path != "")
            {
                path += "[" + getText(a_language_code, node.node_id) + "]";
                list.Add(path);
            }

            if (node.attributes != null)
            {
                foreach (C_ATTRIBUTE attrib in node.attributes)
                {
                    if (attrib.children != null)
                    {
                        if (attrib.GetType() == typeof(C_MULTIPLE_ATTRIBUTE))
                        {
                            foreach (C_OBJECT obj in attrib.children)
                            {
                                C_COMPLEX_OBJECT co = obj as C_COMPLEX_OBJECT;

                                if (co != null && !string.IsNullOrEmpty(co.node_id))
                                    get_logical_paths(list, co, path + "/" + attrib.rm_attribute_name, a_language_code);
                            }
                        }
                        else //single attribute
                        {
                            C_COMPLEX_OBJECT co = attrib.children[0] as C_COMPLEX_OBJECT;

                            if (co != null && !string.IsNullOrEmpty(co.node_id))
                                get_logical_paths(list, co, path + "/" + attrib.rm_attribute_name, a_language_code);
                        }
                    }
                }
            }
        }

        private string getText(string a_language_code, string a_node_id)
        {
            ARCHETYPE_TERM term = _ontology.TermDefinition(a_language_code, a_node_id);
            if (term != null)
            {
                //JAR: 30APR2007, AE-42 Support XML Schema 1.0.12
                foreach (StringDictionaryItem di in term.items)
                {
                    if (di.id == "text")
                        return di.Value;
                }
            }
            System.Diagnostics.Debug.Assert(false, "text field is not present");
            return "";
        }
    }

}
