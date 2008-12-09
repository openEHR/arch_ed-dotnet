using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

using AM = XMLParser;

namespace OceanInformatics.ArchetypeModel
{
    public static class ArchetypeModelBuilder
    {
        public const string ARCHETYPE_DIGEST_ID = "MD5-CAM-1.0.1";

        public static AM.ARCHETYPE Build(openehr.openehr.am.archetype.ARCHETYPE archetype)
        {
            if (archetype == null)
                throw new ArgumentNullException("archetype must not be null");

            CloneConstraintVisitor cloneVisitor = new CloneConstraintVisitor();

            //Clone eiffel ADL archetype as OpenEhr.V1.Its.Xml.AM.ARCHETYPE
            AM.ARCHETYPE archetypeObject = cloneVisitor.CloneArchetype(archetype);

            if(archetypeObject == null)
                throw new ApplicationException("Archetype object must not be null");

            XmlSerializer.ValidateArchetype(archetypeObject);
            return archetypeObject;            
        }

        public static AM.ARCHETYPE CanonicalArchetype(AM.ARCHETYPE archetype)
        {
            AM.ARCHETYPE canonicalArchetype = new XMLParser.ARCHETYPE();

            canonicalArchetype.adl_version = archetype.adl_version;
            canonicalArchetype.archetype_id = archetype.archetype_id;
            canonicalArchetype.concept = archetype.concept;
            canonicalArchetype.definition = archetype.definition;
            canonicalArchetype.invariants = archetype.invariants;

            if (archetype.is_controlledSpecified)
            {
                canonicalArchetype.is_controlled = archetype.is_controlled;
                canonicalArchetype.is_controlledSpecified = true;
            }

            canonicalArchetype.ontology = CanonicaliseOntology(archetype.ontology);

            canonicalArchetype.original_language = archetype.original_language;
            canonicalArchetype.parent_archetype_id = archetype.parent_archetype_id;
            canonicalArchetype.revision_history = archetype.revision_history;

            canonicalArchetype.translations = CanonicaliseTranslations(archetype.translations);

            canonicalArchetype.uid = archetype.uid;            

            return canonicalArchetype;
        }

        public static string ArchetypeDigest(AM.ARCHETYPE archetype)
        {
            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            settings.Indent = false;

            System.IO.MemoryStream archetypeStream = XmlSerializer.Serialize(settings, archetype);
            XmlSerializer.ValidateArchetype(archetypeStream);

            byte[] data = archetypeStream.ToArray();

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            SoapHexBinary hexEncoder = new SoapHexBinary(md5.ComputeHash(data));
            string digest = hexEncoder.ToString();

            return digest;
        }

        /// <summary>
        /// canonicalise translations hash list
        /// </summary>
        /// <param name="translations"></param>
        /// <returns></returns>
        static AM.TRANSLATION_DETAILS[] CanonicaliseTranslations(AM.TRANSLATION_DETAILS[] translations)
        {
            if (translations == null)
                return null;

            SortedDictionary<string, AM.TRANSLATION_DETAILS> sortedTranslations = new SortedDictionary<string, AM.TRANSLATION_DETAILS>();
            foreach (AM.TRANSLATION_DETAILS translation in translations)
            {
                if (translation == null)
                    throw new ApplicationException("translations list must not contain null");

                AM.TRANSLATION_DETAILS canonicalTranslation = new AM.TRANSLATION_DETAILS();
                canonicalTranslation.accreditation = translation.accreditation;
                canonicalTranslation.language = translation.language;
                // sort translations author hash list
                canonicalTranslation.author = SortStringDictionary(translation.author);
                // sort translations other_details hash list
                canonicalTranslation.other_details = SortStringDictionary(translation.other_details);

                sortedTranslations.Add(translation.language.code_string, canonicalTranslation);
            }

            AM.TRANSLATION_DETAILS[] sortedResult = new XMLParser.TRANSLATION_DETAILS[sortedTranslations.Count]; 
            sortedTranslations.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        static AM.ARCHETYPE_ONTOLOGY CanonicaliseOntology(AM.ARCHETYPE_ONTOLOGY ontology)
        {
            AM.ARCHETYPE_ONTOLOGY canonicalOntology = new XMLParser.ARCHETYPE_ONTOLOGY();

            // canonicalise ontology term_definitions
            canonicalOntology.term_definitions = CanonicaliseCodeDefinitions(ontology.term_definitions);
            // canonicalise ontology constraint_definitions
            canonicalOntology.constraint_definitions = CanonicaliseCodeDefinitions(ontology.constraint_definitions);

            // canonicalise ontology term_bindings
            canonicalOntology.term_bindings = CanonicaliseTermBindings(ontology.term_bindings);

            // canonicalise ontology constraint_bindings
            canonicalOntology.constraint_bindings = CanonicaliseConstraintBindings(ontology.constraint_bindings);

            return canonicalOntology;
        }

        static AM.CodeDefinitionSet[] CanonicaliseCodeDefinitions(AM.CodeDefinitionSet[] codeDefinitions)
        {
            if (codeDefinitions == null)
                return null;

            SortedDictionary<string, AM.CodeDefinitionSet> sortedCodeDefinitions = new SortedDictionary<string, AM.CodeDefinitionSet>();
            foreach (AM.CodeDefinitionSet codeDefinitionItem in codeDefinitions)
            {
                AM.CodeDefinitionSet canonicalItem = new AM.CodeDefinitionSet();

                canonicalItem.language = codeDefinitionItem.language;
                // Canonicalise code definitions items
                canonicalItem.items = CanonicaliseArchetypeTerms(codeDefinitionItem.items);

                sortedCodeDefinitions.Add(codeDefinitionItem.language, canonicalItem);
            }

            AM.CodeDefinitionSet[] sortedResult = new AM.CodeDefinitionSet[sortedCodeDefinitions.Count];
            sortedCodeDefinitions.Values.CopyTo(sortedResult, 0);

            return sortedResult;

        }

        static AM.ARCHETYPE_TERM[] CanonicaliseArchetypeTerms(AM.ARCHETYPE_TERM[] archetypeTerms)
        {
            if (archetypeTerms == null)
                return null;

            SortedDictionary<string, AM.ARCHETYPE_TERM> sortedItems = new SortedDictionary<string, AM.ARCHETYPE_TERM>();
            foreach (AM.ARCHETYPE_TERM termItem in archetypeTerms)
            {
                AM.ARCHETYPE_TERM canonicalTerm = new XMLParser.ARCHETYPE_TERM();
                canonicalTerm.code = termItem.code;
                canonicalTerm.items = SortStringDictionary(termItem.items);
                sortedItems.Add(termItem.code, canonicalTerm);
            }

            AM.ARCHETYPE_TERM[] sortedResult = new AM.ARCHETYPE_TERM[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        static AM.TermBindingSet[] CanonicaliseTermBindings(AM.TermBindingSet[] termBindings)
        {
            if (termBindings == null)
                return null;

            SortedDictionary<string, AM.TermBindingSet> sortedItems = new SortedDictionary<string, AM.TermBindingSet>();
            foreach (AM.TermBindingSet bindingItem in termBindings)
            {
                AM.TermBindingSet canonicalItem = new AM.TermBindingSet();

                canonicalItem.terminology = bindingItem.terminology;
                // sort term bindings items hash list
                canonicalItem.items = SortTermBindingItems(bindingItem.items);

                sortedItems.Add(bindingItem.terminology, canonicalItem);
            }

            AM.TermBindingSet[] sortedResult = new AM.TermBindingSet[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        static AM.TERM_BINDING_ITEM[] SortTermBindingItems(AM.TERM_BINDING_ITEM[] bindingItems)
        {
            if (bindingItems == null)
                return null;

            SortedDictionary<string, AM.TERM_BINDING_ITEM> sortedItems = new SortedDictionary<string, AM.TERM_BINDING_ITEM>();
            foreach (AM.TERM_BINDING_ITEM item in bindingItems)
                sortedItems.Add(item.code, item);

            AM.TERM_BINDING_ITEM[] sortedResult = new AM.TERM_BINDING_ITEM[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        static AM.ConstraintBindingSet[] CanonicaliseConstraintBindings(AM.ConstraintBindingSet[] constraintBindings)
        {
            if (constraintBindings == null)
                return null;

            SortedDictionary<string, AM.ConstraintBindingSet> sortedItems = new SortedDictionary<string, AM.ConstraintBindingSet>();
            foreach (AM.ConstraintBindingSet constraintBindingsItem in constraintBindings)
            {
                AM.ConstraintBindingSet canonicalItem = new AM.ConstraintBindingSet();

                canonicalItem.terminology = constraintBindingsItem.terminology;
                // sort constraint bindings items hash list
                canonicalItem.items = SortConstraintBindingItems(constraintBindingsItem.items);

                sortedItems.Add(constraintBindingsItem.terminology, canonicalItem);
            }

            AM.ConstraintBindingSet[] sortedResult = new AM.ConstraintBindingSet[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        static AM.CONSTRAINT_BINDING_ITEM[] SortConstraintBindingItems(AM.CONSTRAINT_BINDING_ITEM[] bindingItems)
        {
            if (bindingItems == null)
                return null;

            SortedDictionary<string, AM.CONSTRAINT_BINDING_ITEM> sortedItems = new SortedDictionary<string, AM.CONSTRAINT_BINDING_ITEM>();
            foreach (AM.CONSTRAINT_BINDING_ITEM item in bindingItems)
                sortedItems.Add(item.code, item);

            AM.CONSTRAINT_BINDING_ITEM[] sortedResult = new AM.CONSTRAINT_BINDING_ITEM[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        static AM.StringDictionaryItem[] SortStringDictionary(AM.StringDictionaryItem[] stringDictionary)
        {
            if (stringDictionary == null)
                return null;

            SortedDictionary<string, AM.StringDictionaryItem> sortedItems = new SortedDictionary<string, AM.StringDictionaryItem>();
            foreach (AM.StringDictionaryItem dictionaryItem in stringDictionary)
                sortedItems.Add(dictionaryItem.id, dictionaryItem);

            AM.StringDictionaryItem[] sortedResult = new AM.StringDictionaryItem[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

    }
}
