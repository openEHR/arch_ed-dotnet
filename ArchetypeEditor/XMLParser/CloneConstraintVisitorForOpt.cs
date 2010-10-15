using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using EiffelKernel = EiffelSoftware.Library.Base.kernel;
using EiffelStructures = EiffelSoftware.Library.Base.structures;

#if XMLParser
using XMLParser;
#else
using OpenEhr.V1.Its.Xml.AM;
#endif

namespace XMLParser.OpenEhr.V1.Its.Xml.AM
{
    public class CloneConstraintVisitorForOpt : CloneConstraintVisitor
    {
        protected override CloneConstraintVisitor NewInstance()
        {
            return new CloneConstraintVisitorForOpt();
        }
                               
        protected override TermBindingSet[] CloneTermBindingSet(EiffelSoftware.Library.Base.structures.table.HASH_TABLE_REFERENCE_REFERENCE currentObject)
        {
            List<TermBindingSet> termBindingSets = new List<TermBindingSet>();

            if (currentObject != null)
            {
                if (currentObject.count() > 0)
                {
                    currentObject.start();

                    // 0..* items TERM_BINDING_ITEM                    
                    for (int i = 1; i <= currentObject.count(); i++)
                    {
                        TermBindingSet termBindingSet = new TermBindingSet();

                        //1 terminology string
                        termBindingSet.terminology = currentObject.key_for_iteration().ToString();

                        //terms HASH table
                        EiffelStructures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE adlTerms;
                        adlTerms = currentObject.item_for_iteration() as EiffelStructures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE;
                        SortedList<string, TERM_BINDING_ITEM> localTerms = new SortedList<string, TERM_BINDING_ITEM>();

                        adlTerms.start();

                        if (adlTerms.count() > 0)
                        {
                            for (int j = 1; j <= adlTerms.count(); j++)
                            {
                                openehr.openehr.rm.data_types.text.CODE_PHRASE term = adlTerms.item_for_iteration() as openehr.openehr.rm.data_types.text.CODE_PHRASE;

                                if (term != null)
                                {
                                    TERM_BINDING_ITEM localTerm = new TERM_BINDING_ITEM();

                                    // 1 code string
                                    localTerm.code = adlTerms.key_for_iteration().ToString();

                                    // 1 value CODE_PHRASE (CODE_PHRASE to HASH TABLE)                                
                                    CODE_PHRASE codePhrase = new CODE_PHRASE();

                                    // 1 code_string string
                                    codePhrase.code_string = term.code_string().ToString();

                                    // 1 terminology_id TERMINOLOGY_ID                                
                                    if (term.code_string() != null)
                                    {
                                        TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                                        terminologyId.value = term.terminology_id().value().ToString();
                                        codePhrase.terminology_id = terminologyId;
                                    }

                                    localTerm.value = codePhrase;
                                    localTerms.Add(localTerm.code, localTerm);
                                }

                                adlTerms.forth();
                            }

                            termBindingSet.items = new TERM_BINDING_ITEM[localTerms.Count];
                            localTerms.Values.CopyTo(termBindingSet.items, 0);
                        }

                        if (termBindingSet.items != null)
                        {
                            termBindingSets.Add(termBindingSet);
                        }

                        currentObject.forth();
                    }
                }
            }

            if (termBindingSets.Count > 0)
            {
                TermBindingSet[] termBindingSetArray = new TermBindingSet[termBindingSets.Count];
                termBindingSets.CopyTo(termBindingSetArray);
                return termBindingSetArray;
            }
            else
            {
                return null;
            }
        }

        protected override void CleanUpCodeDefinitionSet(CodeDefinitionSet codeDefinitionSet)
        {
            foreach (ARCHETYPE_TERM term in codeDefinitionSet.items)
            {
                foreach (StringDictionaryItem item in term.items)
                    if (item.id == "text")
                    {
                        item.Value = item.Value.Trim();
                        break;
                    }
            }
        }

        protected override C_ATTRIBUTE CloneAttribute(openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE currentObject)
        {
            C_ATTRIBUTE result = base.CloneAttribute(currentObject);

            openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT parent = currentObject.parent() as openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT;

            if (parent != null)
            {
                string parentTypeName = parent.rm_type_name().to_cil();
                string attrName = result.rm_attribute_name;

                if ((parentTypeName == "COMPOSITION" && attrName == "context")
                 || (parentTypeName == "COMPOSITION" && attrName == "content")
                 || (parentTypeName == "EVENT_CONTEXT" && attrName == "other_context")
                 || (parentTypeName == "SECTION" && attrName == "items")
                 || (parentTypeName == "CARE_ENTRY" && attrName == "protocol")
                 || (parentTypeName == "OBSERVATION" && attrName == "protocol")
                 || (parentTypeName == "EVALUATION" && attrName == "protocol")
                 || (parentTypeName == "INSTRUCTION" && attrName == "protocol")
                 || (parentTypeName == "ACTION" && attrName == "protocol")
                 || (parentTypeName == "OBSERVATION" && attrName == "state")
                 || (parentTypeName == "INSTRUCTION" && attrName == "activities")
                 || (parentTypeName == "ISM_TRANSITION" && attrName == "careflow_step")
                 || (parentTypeName == "ITEM_TREE" && attrName == "items")
                 || (parentTypeName == "ITEM_TABLE" && attrName == "rows")
                 || (parentTypeName == "ITEM_LIST" && attrName == "items")
                 || (parentTypeName == "HISTORY" && attrName == "events")
                 || (parentTypeName == "HISTORY" && attrName == "period")
                 || (parentTypeName == "EVENT" && attrName == "state")
                 || (parentTypeName == "POINT_EVENT" && attrName == "state")
                 || (parentTypeName == "INTERVAL_EVENT" && attrName == "state")
                 || (parentTypeName == "ELEMENT" && attrName == "value")
                 || (parentTypeName == "ELEMENT" && attrName == "null_flavour")
                 || (parentTypeName == "DV_INTERVAL" && attrName == "lower")
                 || (parentTypeName == "DV_INTERVAL" && attrName == "upper")
                )
                {
                    result.existence = new IntervalOfInteger();
                    result.existence.lower = 0;
                    result.existence.upper = 1;
                    result.existence.lowerSpecified = true;
                    result.existence.upperSpecified = true;
                    result.existence.lower_included = true;
                    result.existence.upper_included = true;
                    result.existence.lower_includedSpecified = true;
                    result.existence.upper_includedSpecified = true;
                }
            }

            return result;
        }
    }
}
