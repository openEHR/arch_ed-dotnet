using System;
using System.Collections.Generic;
using System.Text;

#if XMLParser
using XMLParser;
#else
using OpenEhr.V1.Its.Xml.AM;
#endif

namespace XMLParser.OpenEhr.V1.Its.Xml.AM
{
    class CanonicalAmVisitor
    {
        public ARCHETYPE VisitArchetype(ARCHETYPE archetype)
        {
            ARCHETYPE result = new ARCHETYPE();

            result.adl_version = archetype.adl_version;
            result.archetype_id = archetype.archetype_id;
            result.concept = archetype.concept;
            result.definition = VisitComplexObjectConstraint(archetype.definition);
            result.invariants = archetype.invariants;

            if (archetype.is_controlledSpecified)
            {
                result.is_controlled = archetype.is_controlled;
                result.is_controlledSpecified = true;
            }

            result.ontology = VisitOntology(archetype.ontology);
            result.original_language = archetype.original_language;
            result.parent_archetype_id = archetype.parent_archetype_id;
            result.revision_history = archetype.revision_history;
            result.translations = VisitTranslations(archetype.translations);
            result.uid = archetype.uid;

            return result;
        }

        C_OBJECT[] VisitChildren(C_OBJECT[] children)
        {
            C_OBJECT[] result = null;

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

                result = objectConstraintList.ToArray();
            }

            return result;
        }

        C_COMPLEX_OBJECT VisitComplexObjectConstraint(C_COMPLEX_OBJECT objectConstraint)
        {
            System.Diagnostics.Trace.Assert(objectConstraint != null, "objectConstraint must not be null");

            C_COMPLEX_OBJECT result = new C_COMPLEX_OBJECT();
            CloneObjectConstraint(objectConstraint, result);
            result.attributes = VisitAttributes(objectConstraint.attributes);

            return result;
        }

        C_PRIMITIVE_OBJECT VisitPrimitiveObjectConstraint(C_PRIMITIVE_OBJECT objectConstraint)
        {
            System.Diagnostics.Trace.Assert(objectConstraint != null, "objectConstraint must not be null");

            C_PRIMITIVE result = VisitPrimitiveConstraint(objectConstraint.item);
            C_PRIMITIVE_OBJECT primitiveObjectConstraint = null;

            if (result != objectConstraint.item)
            {
                primitiveObjectConstraint = new C_PRIMITIVE_OBJECT();
                CloneObjectConstraint(objectConstraint, primitiveObjectConstraint);
                primitiveObjectConstraint.item = result;
            }
            else
                primitiveObjectConstraint = objectConstraint;

            System.Diagnostics.Debug.Assert(primitiveObjectConstraint != null, "primitiveObjectConstraint must not be null");
            return primitiveObjectConstraint;
        }

        C_PRIMITIVE VisitPrimitiveConstraint(C_PRIMITIVE primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null, "objectConstraint must not be null");

            C_PRIMITIVE result = null;
            C_DATE dateConstraint = primitiveConstraint as C_DATE;

            if (dateConstraint != null)
                result = VisitPrimitiveConstraint(dateConstraint);
            else
            {
                C_DATE_TIME dateTimeConstraint = primitiveConstraint as C_DATE_TIME;

                if (dateTimeConstraint != null)
                    result = VisitPrimitiveConstraint(dateTimeConstraint);
                else
                {
                    C_TIME timeConstraint = primitiveConstraint as C_TIME;

                    if (timeConstraint != null)
                        result = VisitPrimitiveConstraint(timeConstraint);
                    else
                    {
                        C_DURATION durationConstraint = primitiveConstraint as C_DURATION;

                        if (durationConstraint != null)
                            result = VisitPrimitiveConstraint(durationConstraint);
                        else
                            result = primitiveConstraint;
                    }
                }
            }

            System.Diagnostics.Debug.Assert(result != null, "objectConstraint must not be null");
            return result;
        }

        C_DATE VisitPrimitiveConstraint(C_DATE primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null, "objectConstraint must not be null");

            C_DATE result = new C_DATE();
            result.assumed_value = primitiveConstraint.assumed_value;

            if (primitiveConstraint.pattern != null)
                result.pattern = primitiveConstraint.pattern.ToUpperInvariant();

            result.range = primitiveConstraint.range;
            result.timezone_validity = primitiveConstraint.timezone_validity;
            result.timezone_validitySpecified = primitiveConstraint.timezone_validitySpecified;

            return result;
        }

        C_DATE_TIME VisitPrimitiveConstraint(C_DATE_TIME primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null, "objectConstraint must not be null");

            C_DATE_TIME result = new C_DATE_TIME();
            result.assumed_value = primitiveConstraint.assumed_value;

            if (primitiveConstraint.pattern != null)
                result.pattern = primitiveConstraint.pattern.ToUpperInvariant();

            result.range = primitiveConstraint.range;
            result.timezone_validity = primitiveConstraint.timezone_validity;
            result.timezone_validitySpecified = primitiveConstraint.timezone_validitySpecified;

            return result;
        }

        C_TIME VisitPrimitiveConstraint(C_TIME primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null, "objectConstraint must not be null");

            C_TIME result = new C_TIME();
            result.assumed_value = primitiveConstraint.assumed_value;

            if (primitiveConstraint.pattern != null)
                result.pattern = primitiveConstraint.pattern.ToUpperInvariant();

            result.range = primitiveConstraint.range;
            result.timezone_validity = primitiveConstraint.timezone_validity;
            result.timezone_validitySpecified = primitiveConstraint.timezone_validitySpecified;

            return result;
        }

        C_DURATION VisitPrimitiveConstraint(C_DURATION primitiveConstraint)
        {
            System.Diagnostics.Trace.Assert(primitiveConstraint != null,
                "objectConstraint must not be null");

            C_DURATION result = new C_DURATION();
            result.assumed_value = primitiveConstraint.assumed_value;

            if (primitiveConstraint.pattern != null)
                result.pattern = primitiveConstraint.pattern.ToUpperInvariant();

            result.range = primitiveConstraint.range;

            return result;
        }

        /// <summary>
        /// Clone C_OBJECT
        /// </summary>
        void CloneObjectConstraint(C_OBJECT objectConstraint, C_OBJECT newObjectConstraint)
        {
            newObjectConstraint.node_id = objectConstraint.node_id;
            newObjectConstraint.occurrences = objectConstraint.occurrences;
            newObjectConstraint.rm_type_name = objectConstraint.rm_type_name;
        }

        C_ATTRIBUTE[] VisitAttributes(C_ATTRIBUTE[] attributes)
        {
            C_ATTRIBUTE[] result = null;

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

                result = new C_ATTRIBUTE[attributeConstraintList.Count];
                attributeConstraintList.Values.CopyTo(result, 0);
            }

            return result;
        }

        C_MULTIPLE_ATTRIBUTE VisitMultipleAttributeConstraint(C_MULTIPLE_ATTRIBUTE attributeConstraint)
        {
            System.Diagnostics.Trace.Assert(attributeConstraint != null, "existingAttribute must not be null");

            C_MULTIPLE_ATTRIBUTE result = new C_MULTIPLE_ATTRIBUTE();
            CloneAttributeConstraint(attributeConstraint, result);
            result.cardinality = attributeConstraint.cardinality;

            return result;
        }

        C_SINGLE_ATTRIBUTE VisitSingleAttributeConstraint(C_SINGLE_ATTRIBUTE attributeConstraint)
        {
            System.Diagnostics.Trace.Assert(attributeConstraint != null, 
                "existingAttribute must not be null");

            C_SINGLE_ATTRIBUTE result = new C_SINGLE_ATTRIBUTE();
            CloneAttributeConstraint(attributeConstraint, result);

            return result;
        }

        void CloneAttributeConstraint(C_ATTRIBUTE attributeConstraint, C_ATTRIBUTE newAttributeConstraint)
        {
            newAttributeConstraint.rm_attribute_name = attributeConstraint.rm_attribute_name;
            newAttributeConstraint.existence = attributeConstraint.existence;
        }

        /// <summary>
        /// canonicalise translations hash list
        /// </summary>
        TRANSLATION_DETAILS[] VisitTranslations(TRANSLATION_DETAILS[] translations)
        {
            TRANSLATION_DETAILS[] result = null;

            if (translations != null)
            {
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

                result = new TRANSLATION_DETAILS[sortedTranslations.Count];
                sortedTranslations.Values.CopyTo(result, 0);
            }

            return result;
        }

        ARCHETYPE_ONTOLOGY VisitOntology(ARCHETYPE_ONTOLOGY ontology)
        {
            ARCHETYPE_ONTOLOGY result = new ARCHETYPE_ONTOLOGY();

            // canonicalise ontology term_definitions
            result.term_definitions = VisitCodeDefinitions(ontology.term_definitions);

            // canonicalise ontology constraint_definitions
            result.constraint_definitions = VisitCodeDefinitions(ontology.constraint_definitions);

            // canonicalise ontology term_bindings
            result.term_bindings = VisitTermBindings(ontology.term_bindings);

            // canonicalise ontology constraint_bindings
            result.constraint_bindings = VisitConstraintBindings(ontology.constraint_bindings);

            return result;
        }

        CodeDefinitionSet[] VisitCodeDefinitions(CodeDefinitionSet[] codeDefinitions)
        {
            CodeDefinitionSet[] result = null;

            if (codeDefinitions != null)
            {
                SortedDictionary<string, CodeDefinitionSet> sortedCodeDefinitions = new SortedDictionary<string, CodeDefinitionSet>();

                foreach (CodeDefinitionSet codeDefinitionItem in codeDefinitions)
                {
                    string language = codeDefinitionItem.language;

                    if (!string.IsNullOrEmpty(language))
                    {
                        CodeDefinitionSet canonicalItem = new CodeDefinitionSet();
                        canonicalItem.language = language;

                        // Canonicalise code definitions items
                        canonicalItem.items = VisitArchetypeTerms(codeDefinitionItem.items);
                        sortedCodeDefinitions.Add(language, canonicalItem);
                    }
                }

                result = new CodeDefinitionSet[sortedCodeDefinitions.Count];
                sortedCodeDefinitions.Values.CopyTo(result, 0);
            }

            return result;
        }

        ARCHETYPE_TERM[] VisitArchetypeTerms(ARCHETYPE_TERM[] archetypeTerms)
        {
            ARCHETYPE_TERM[] result = null;

            if (archetypeTerms != null)
            {
                SortedDictionary<string, ARCHETYPE_TERM> sortedItems = new SortedDictionary<string, ARCHETYPE_TERM>();

                foreach (ARCHETYPE_TERM termItem in archetypeTerms)
                {
                    ARCHETYPE_TERM canonicalTerm = new ARCHETYPE_TERM();
                    canonicalTerm.code = termItem.code;
                    canonicalTerm.items = VisitStringDictionary(termItem.items);
                    sortedItems.Add(termItem.code, canonicalTerm);
                }

                result = new ARCHETYPE_TERM[sortedItems.Count];
                sortedItems.Values.CopyTo(result, 0);
            }

            return result;
        }

        TermBindingSet[] VisitTermBindings(TermBindingSet[] termBindings)
        {
            TermBindingSet[] result = null;

            if (termBindings != null)
            {
                SortedDictionary<string, TermBindingSet> sortedItems = new SortedDictionary<string, TermBindingSet>();

                foreach (TermBindingSet bindingItem in termBindings)
                {
                    TermBindingSet canonicalItem = new TermBindingSet();
                    canonicalItem.terminology = bindingItem.terminology;

                    // sort term bindings items hash list
                    canonicalItem.items = VisitTermBindingItems(bindingItem.items);
                    sortedItems.Add(bindingItem.terminology, canonicalItem);
                }

                result = new TermBindingSet[sortedItems.Count];
                sortedItems.Values.CopyTo(result, 0);
            }

            return result;
        }

        TERM_BINDING_ITEM[] VisitTermBindingItems(TERM_BINDING_ITEM[] bindingItems)
        {
            TERM_BINDING_ITEM[] result = null;

            if (bindingItems != null)
            {
                SortedDictionary<string, TERM_BINDING_ITEM> sortedItems = new SortedDictionary<string, TERM_BINDING_ITEM>();

                foreach (TERM_BINDING_ITEM item in bindingItems)
                    sortedItems.Add(item.code, item);

                result = new TERM_BINDING_ITEM[sortedItems.Count];
                sortedItems.Values.CopyTo(result, 0);
            }

            return result;
        }

        ConstraintBindingSet[] VisitConstraintBindings(ConstraintBindingSet[] constraintBindings)
        {
            ConstraintBindingSet[] result = null;

            if (constraintBindings != null)
            {
                SortedDictionary<string, ConstraintBindingSet> sortedItems = new SortedDictionary<string, ConstraintBindingSet>();

                foreach (ConstraintBindingSet constraintBindingsItem in constraintBindings)
                {
                    ConstraintBindingSet canonicalItem = new ConstraintBindingSet();
                    canonicalItem.terminology = constraintBindingsItem.terminology;

                    // sort constraint bindings items hash list
                    canonicalItem.items = VisitConstraintBindingItems(constraintBindingsItem.items);
                    sortedItems.Add(constraintBindingsItem.terminology, canonicalItem);
                }

                result = new ConstraintBindingSet[sortedItems.Count];
                sortedItems.Values.CopyTo(result, 0);
            }

            return result;
        }

        CONSTRAINT_BINDING_ITEM[] VisitConstraintBindingItems(CONSTRAINT_BINDING_ITEM[] bindingItems)
        {
            CONSTRAINT_BINDING_ITEM[] result = null;

            if (bindingItems != null)
            {
                SortedDictionary<string, CONSTRAINT_BINDING_ITEM> sortedItems = new SortedDictionary<string, CONSTRAINT_BINDING_ITEM>();

                foreach (CONSTRAINT_BINDING_ITEM item in bindingItems)
                    sortedItems.Add(item.code, item);

                result = new CONSTRAINT_BINDING_ITEM[sortedItems.Count];
                sortedItems.Values.CopyTo(result, 0);
            }

            return result;
        }

        StringDictionaryItem[] VisitStringDictionary(StringDictionaryItem[] stringDictionary)
        {
            StringDictionaryItem[] result = null;

            if (stringDictionary != null)
            {
                SortedDictionary<string, StringDictionaryItem> sortedItems = new SortedDictionary<string, StringDictionaryItem>();

                foreach (StringDictionaryItem dictionaryItem in stringDictionary)
                    sortedItems.Add(dictionaryItem.id, dictionaryItem);

                result = new StringDictionaryItem[sortedItems.Count];
                sortedItems.Values.CopyTo(result, 0);
            }

            return result;
        }
    }
}
