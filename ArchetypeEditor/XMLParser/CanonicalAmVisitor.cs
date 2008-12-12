using System;
using System.Collections.Generic;
using System.Text;


namespace XMLParser
{
    class CanonicalAmVisitor
    {
        public ARCHETYPE VisitArchetype(ARCHETYPE archetype)
        {
            ARCHETYPE canonicalArchetype = new XMLParser.ARCHETYPE();

            canonicalArchetype.adl_version = archetype.adl_version;
            canonicalArchetype.archetype_id = archetype.archetype_id;
            canonicalArchetype.concept = archetype.concept;

            //canonicalArchetype.definition = archetype.definition;
            canonicalArchetype.definition = VisitComplexObjectConstraint(archetype.definition);

            canonicalArchetype.invariants = archetype.invariants;

            if (archetype.is_controlledSpecified)
            {
                canonicalArchetype.is_controlled = archetype.is_controlled;
                canonicalArchetype.is_controlledSpecified = true;
            }

            canonicalArchetype.ontology = VisitOntology(archetype.ontology);

            canonicalArchetype.original_language = archetype.original_language;
            canonicalArchetype.parent_archetype_id = archetype.parent_archetype_id;
            canonicalArchetype.revision_history = archetype.revision_history;

            canonicalArchetype.translations = VisitTranslations(archetype.translations);

            canonicalArchetype.uid = archetype.uid;

            return canonicalArchetype;
        }

        C_OBJECT[] VisitChildren(C_OBJECT[] children)
        {
            C_OBJECT[] objectConstraints = null;
            if (children != null)
            {
                List<C_OBJECT> objectConstraintList = new List<C_OBJECT>();

                foreach (C_OBJECT objectConstraint in children)
                {
                    C_OBJECT newObjectConstraint;

                    C_COMPLEX_OBJECT complexObjectConstraint = objectConstraint as C_COMPLEX_OBJECT;
                    if (complexObjectConstraint != null)
                        newObjectConstraint = VisitComplexObjectConstraint(complexObjectConstraint);

                    else
                    {
                        C_PRIMITIVE_OBJECT primitiveObjectConstraint = objectConstraint as C_PRIMITIVE_OBJECT;
                        if (primitiveObjectConstraint != null)
                            newObjectConstraint = VisitPrimitiveObjectConstraint(primitiveObjectConstraint);
                        else
                        newObjectConstraint = objectConstraint;
                    }

                    System.Diagnostics.Debug.Assert(newObjectConstraint != null, "newObject must not be null");
                    objectConstraintList.Add(newObjectConstraint);
                }
                objectConstraints = objectConstraintList.ToArray();
            }

            return objectConstraints;
        }

        C_COMPLEX_OBJECT VisitComplexObjectConstraint(C_COMPLEX_OBJECT objectConstraint)
        {
            System.Diagnostics.Trace.Assert(objectConstraint != null,
                "objectConstraint must not be null");

            C_COMPLEX_OBJECT newObjectConstraint = new C_COMPLEX_OBJECT();
            CloneObjectConstraint(objectConstraint, newObjectConstraint);

            newObjectConstraint.attributes = VisitAttributes(objectConstraint.attributes);

            return newObjectConstraint;
        }

        C_PRIMITIVE_OBJECT VisitPrimitiveObjectConstraint(C_PRIMITIVE_OBJECT objectConstraint)
        {
            System.Diagnostics.Trace.Assert(objectConstraint != null,
                "objectConstraint must not be null");

            C_PRIMITIVE primitiveConstraint = VisitPrimitiveConstraint(objectConstraint.item);

            C_PRIMITIVE_OBJECT primitiveObjectConstraint = null;
            if (primitiveConstraint != objectConstraint.item)
            {
                primitiveObjectConstraint = new C_PRIMITIVE_OBJECT();
                CloneObjectConstraint(objectConstraint, primitiveObjectConstraint);
                primitiveObjectConstraint.item = primitiveConstraint;
            }
            else
                primitiveObjectConstraint = objectConstraint;

            System.Diagnostics.Debug.Assert(primitiveObjectConstraint != null,
                "primitiveObjectConstraint must not be null");
            return primitiveObjectConstraint;
        }

        C_PRIMITIVE VisitPrimitiveConstraint(C_PRIMITIVE primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null,
                "objectConstraint must not be null");

            C_PRIMITIVE resultPrimitiveConstraint = null;

            C_DATE dateConstraint = primitiveConstraint as C_DATE;
            if (dateConstraint != null)
                resultPrimitiveConstraint = VisitPrimitiveConstraint(dateConstraint);
            else
            {
                C_DATE_TIME dateTimeConstraint = primitiveConstraint as C_DATE_TIME;
                if (dateTimeConstraint != null)
                    resultPrimitiveConstraint = VisitPrimitiveConstraint(dateTimeConstraint);
                else
                {
                    C_TIME timeConstraint = primitiveConstraint as C_TIME;
                    if (timeConstraint != null)
                        resultPrimitiveConstraint = VisitPrimitiveConstraint(timeConstraint);
                    else
                    {
                        C_DURATION durationConstraint = primitiveConstraint as C_DURATION;
                        if (durationConstraint != null)
                            resultPrimitiveConstraint = VisitPrimitiveConstraint(durationConstraint);
                        else
                            resultPrimitiveConstraint = primitiveConstraint;
                    }
                }
            }

            System.Diagnostics.Debug.Assert(resultPrimitiveConstraint != null,
                "objectConstraint must not be null");
            return resultPrimitiveConstraint;
        }

        C_DATE VisitPrimitiveConstraint(C_DATE primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null,
                "objectConstraint must not be null");

            C_DATE dateConstraint =  new C_DATE();
            dateConstraint.assumed_value = primitiveConstraint.assumed_value;
            if (primitiveConstraint.pattern != null)
                dateConstraint.pattern = primitiveConstraint.pattern.ToUpperInvariant();
            dateConstraint.range = primitiveConstraint.range;
            dateConstraint.timezone_validity = primitiveConstraint.timezone_validity;
            dateConstraint.timezone_validitySpecified = primitiveConstraint.timezone_validitySpecified;

            return dateConstraint;
        }

        C_DATE_TIME VisitPrimitiveConstraint(C_DATE_TIME primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null,
                "objectConstraint must not be null");

            C_DATE_TIME resultPrimitiveConstraint = new C_DATE_TIME();
            resultPrimitiveConstraint.assumed_value = primitiveConstraint.assumed_value;
            if (primitiveConstraint.pattern != null)
                resultPrimitiveConstraint.pattern = primitiveConstraint.pattern.ToUpperInvariant();
            resultPrimitiveConstraint.range = primitiveConstraint.range;
            resultPrimitiveConstraint.timezone_validity = primitiveConstraint.timezone_validity;
            resultPrimitiveConstraint.timezone_validitySpecified = primitiveConstraint.timezone_validitySpecified;

            return resultPrimitiveConstraint;
        }

        C_TIME VisitPrimitiveConstraint(C_TIME primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null,
                "objectConstraint must not be null");

            C_TIME resultPrimitiveConstraint = new C_TIME();
            resultPrimitiveConstraint.assumed_value = primitiveConstraint.assumed_value;
            if (primitiveConstraint.pattern != null)
                resultPrimitiveConstraint.pattern = primitiveConstraint.pattern.ToUpperInvariant();
            resultPrimitiveConstraint.range = primitiveConstraint.range;
            resultPrimitiveConstraint.timezone_validity = primitiveConstraint.timezone_validity;
            resultPrimitiveConstraint.timezone_validitySpecified = primitiveConstraint.timezone_validitySpecified;

            return resultPrimitiveConstraint;
        }

        C_DURATION VisitPrimitiveConstraint(C_DURATION primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null,
                "objectConstraint must not be null");

            C_DURATION resultPrimitiveConstraint = new C_DURATION();
            resultPrimitiveConstraint.assumed_value = primitiveConstraint.assumed_value;
            if (primitiveConstraint.pattern != null)
                resultPrimitiveConstraint.pattern = primitiveConstraint.pattern.ToUpperInvariant();
            resultPrimitiveConstraint.range = primitiveConstraint.range;

            return resultPrimitiveConstraint;
        }

        /// <summary>
        /// Clone C_OBJECT
        /// </summary>
        /// <param name="existingNode"></param>
        /// <param name="newNode"></param>
        void CloneObjectConstraint(C_OBJECT objectConstraint, C_OBJECT newObjectConstraint)
        {
            newObjectConstraint.node_id = objectConstraint.node_id;
            newObjectConstraint.occurrences = objectConstraint.occurrences;
            newObjectConstraint.rm_type_name = objectConstraint.rm_type_name;
        }

        //void CloneC_Defined_Object(C_DEFINED_OBJECT existingNode, C_DEFINED_OBJECT newNode)
        //{
        //    CloneC_Object(existingNode, newNode);

        //}

        C_ATTRIBUTE[] VisitAttributes(C_ATTRIBUTE[] attributes)
        {
            //System.Diagnostics.Trace.Assert(attributes == null || attributes.Length > 0, 
            //    "attributeConstraints must be null or not empty");

            C_ATTRIBUTE[] attributeConstraintArray = null;
            if (attributes != null)
            {
                SortedList<string, C_ATTRIBUTE> attributeConstraintList = new SortedList<string, C_ATTRIBUTE>();

                foreach (C_ATTRIBUTE attributeConstraint in attributes)
                {
                    C_ATTRIBUTE newAttribute;

                    C_MULTIPLE_ATTRIBUTE multipleAttributeConstraint = attributeConstraint as C_MULTIPLE_ATTRIBUTE;
                    if (multipleAttributeConstraint != null)
                        newAttribute = VisitMultipleAttributeConstraint(multipleAttributeConstraint);
                    else
                        newAttribute = VisitSingleAttributeConstraint((C_SINGLE_ATTRIBUTE)attributeConstraint);

                    System.Diagnostics.Debug.Assert(newAttribute != null, "newAttribute must not be null");
                    newAttribute.children = VisitChildren(attributeConstraint.children);

                    attributeConstraintList.Add(newAttribute.rm_attribute_name, newAttribute);
                }

                attributeConstraintArray = new C_ATTRIBUTE[attributeConstraintList.Count];
                attributeConstraintList.Values.CopyTo(attributeConstraintArray, 0);
            }

            return attributeConstraintArray;
        }

        C_MULTIPLE_ATTRIBUTE VisitMultipleAttributeConstraint(C_MULTIPLE_ATTRIBUTE attributeConstraint)
        {
            System.Diagnostics.Trace.Assert(attributeConstraint != null, 
                "existingAttribute must not be null");

            C_MULTIPLE_ATTRIBUTE newAttribute = new C_MULTIPLE_ATTRIBUTE();
            CloneAttributeConstraint(attributeConstraint, newAttribute);

            newAttribute.cardinality = attributeConstraint.cardinality;

            return newAttribute;
        }

        C_SINGLE_ATTRIBUTE VisitSingleAttributeConstraint(C_SINGLE_ATTRIBUTE attributeConstraint)
        {
            System.Diagnostics.Trace.Assert(attributeConstraint != null, 
                "existingAttribute must not be null");

            C_SINGLE_ATTRIBUTE newAttribute = new C_SINGLE_ATTRIBUTE();
            CloneAttributeConstraint(attributeConstraint, newAttribute);

            return newAttribute;
        }

        void CloneAttributeConstraint(C_ATTRIBUTE attributeConstraint, C_ATTRIBUTE newAttributeConstraint)
        {
            newAttributeConstraint.rm_attribute_name = attributeConstraint.rm_attribute_name;
            newAttributeConstraint.existence = attributeConstraint.existence;
        }

        /// <summary>
        /// canonicalise translations hash list
        /// </summary>
        /// <param name="translations"></param>
        /// <returns></returns>
        TRANSLATION_DETAILS[] VisitTranslations(TRANSLATION_DETAILS[] translations)
        {
            if (translations == null)
                return null;

            SortedDictionary<string, TRANSLATION_DETAILS> sortedTranslations = new SortedDictionary<string, TRANSLATION_DETAILS>();
            foreach (TRANSLATION_DETAILS translation in translations)
            {
                if (translation == null)
                    throw new ApplicationException("translations list must not contain null");

                TRANSLATION_DETAILS canonicalTranslation = new TRANSLATION_DETAILS();
                canonicalTranslation.accreditation = translation.accreditation;
                canonicalTranslation.language = translation.language;
                // sort translations author hash list
                canonicalTranslation.author = VisitStringDictionary(translation.author);
                // sort translations other_details hash list
                canonicalTranslation.other_details = VisitStringDictionary(translation.other_details);

                sortedTranslations.Add(translation.language.code_string, canonicalTranslation);
            }

            TRANSLATION_DETAILS[] sortedResult = new XMLParser.TRANSLATION_DETAILS[sortedTranslations.Count];
            sortedTranslations.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        ARCHETYPE_ONTOLOGY VisitOntology(ARCHETYPE_ONTOLOGY ontology)
        {
            ARCHETYPE_ONTOLOGY canonicalOntology = new XMLParser.ARCHETYPE_ONTOLOGY();

            // canonicalise ontology term_definitions
            canonicalOntology.term_definitions = VisitCodeDefinitions(ontology.term_definitions);
            // canonicalise ontology constraint_definitions
            canonicalOntology.constraint_definitions = VisitCodeDefinitions(ontology.constraint_definitions);

            // canonicalise ontology term_bindings
            canonicalOntology.term_bindings = VisitTermBindings(ontology.term_bindings);

            // canonicalise ontology constraint_bindings
            canonicalOntology.constraint_bindings = VisitConstraintBindings(ontology.constraint_bindings);

            return canonicalOntology;
        }

        CodeDefinitionSet[] VisitCodeDefinitions(CodeDefinitionSet[] codeDefinitions)
        {
            if (codeDefinitions == null)
                return null;

            SortedDictionary<string, CodeDefinitionSet> sortedCodeDefinitions = new SortedDictionary<string, CodeDefinitionSet>();
            foreach (CodeDefinitionSet codeDefinitionItem in codeDefinitions)
            {
                CodeDefinitionSet canonicalItem = new CodeDefinitionSet();

                canonicalItem.language = codeDefinitionItem.language;
                // Canonicalise code definitions items
                canonicalItem.items = VisitArchetypeTerms(codeDefinitionItem.items);

                sortedCodeDefinitions.Add(codeDefinitionItem.language, canonicalItem);
            }

            CodeDefinitionSet[] sortedResult = new CodeDefinitionSet[sortedCodeDefinitions.Count];
            sortedCodeDefinitions.Values.CopyTo(sortedResult, 0);

            return sortedResult;

        }

        ARCHETYPE_TERM[] VisitArchetypeTerms(ARCHETYPE_TERM[] archetypeTerms)
        {
            if (archetypeTerms == null)
                return null;

            SortedDictionary<string, ARCHETYPE_TERM> sortedItems = new SortedDictionary<string, ARCHETYPE_TERM>();
            foreach (ARCHETYPE_TERM termItem in archetypeTerms)
            {
                ARCHETYPE_TERM canonicalTerm = new XMLParser.ARCHETYPE_TERM();
                canonicalTerm.code = termItem.code;
                canonicalTerm.items = VisitStringDictionary(termItem.items);
                sortedItems.Add(termItem.code, canonicalTerm);
            }

            ARCHETYPE_TERM[] sortedResult = new ARCHETYPE_TERM[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        TermBindingSet[] VisitTermBindings(TermBindingSet[] termBindings)
        {
            if (termBindings == null)
                return null;

            SortedDictionary<string, TermBindingSet> sortedItems = new SortedDictionary<string, TermBindingSet>();
            foreach (TermBindingSet bindingItem in termBindings)
            {
                TermBindingSet canonicalItem = new TermBindingSet();

                canonicalItem.terminology = bindingItem.terminology;
                // sort term bindings items hash list
                canonicalItem.items = VisitTermBindingItems(bindingItem.items);

                sortedItems.Add(bindingItem.terminology, canonicalItem);
            }

            TermBindingSet[] sortedResult = new TermBindingSet[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        TERM_BINDING_ITEM[] VisitTermBindingItems(TERM_BINDING_ITEM[] bindingItems)
        {
            if (bindingItems == null)
                return null;

            SortedDictionary<string, TERM_BINDING_ITEM> sortedItems = new SortedDictionary<string, TERM_BINDING_ITEM>();
            foreach (TERM_BINDING_ITEM item in bindingItems)
                sortedItems.Add(item.code, item);

            TERM_BINDING_ITEM[] sortedResult = new TERM_BINDING_ITEM[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        ConstraintBindingSet[] VisitConstraintBindings(ConstraintBindingSet[] constraintBindings)
        {
            if (constraintBindings == null)
                return null;

            SortedDictionary<string, ConstraintBindingSet> sortedItems = new SortedDictionary<string, ConstraintBindingSet>();
            foreach (ConstraintBindingSet constraintBindingsItem in constraintBindings)
            {
                ConstraintBindingSet canonicalItem = new ConstraintBindingSet();

                canonicalItem.terminology = constraintBindingsItem.terminology;
                // sort constraint bindings items hash list
                canonicalItem.items = VisitConstraintBindingItems(constraintBindingsItem.items);

                sortedItems.Add(constraintBindingsItem.terminology, canonicalItem);
            }

            ConstraintBindingSet[] sortedResult = new ConstraintBindingSet[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        CONSTRAINT_BINDING_ITEM[] VisitConstraintBindingItems(CONSTRAINT_BINDING_ITEM[] bindingItems)
        {
            if (bindingItems == null)
                return null;

            SortedDictionary<string, CONSTRAINT_BINDING_ITEM> sortedItems = new SortedDictionary<string, CONSTRAINT_BINDING_ITEM>();
            foreach (CONSTRAINT_BINDING_ITEM item in bindingItems)
                sortedItems.Add(item.code, item);

            CONSTRAINT_BINDING_ITEM[] sortedResult = new CONSTRAINT_BINDING_ITEM[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }

        StringDictionaryItem[] VisitStringDictionary(StringDictionaryItem[] stringDictionary)
        {
            if (stringDictionary == null)
                return null;

            SortedDictionary<string, StringDictionaryItem> sortedItems = new SortedDictionary<string, StringDictionaryItem>();
            foreach (StringDictionaryItem dictionaryItem in stringDictionary)
                sortedItems.Add(dictionaryItem.id, dictionaryItem);

            StringDictionaryItem[] sortedResult = new StringDictionaryItem[sortedItems.Count];
            sortedItems.Values.CopyTo(sortedResult, 0);

            return sortedResult;
        }
    }
}
