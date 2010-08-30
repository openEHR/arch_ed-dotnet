using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using EiffelKernel = EiffelSoftware.Library.Base.Kernel;
using EiffelStructures = EiffelSoftware.Library.Base.Structures;

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
                               
        protected override TermBindingSet[] CloneTermBindingSet(EiffelStructures.Table.HashTableReferenceReference o)
        {
            TermBindingSet[] result = null;
            List<TermBindingSet> termBindingSets = new List<TermBindingSet>();

            if (o != null && o.Count() > 0)
            {
                o.Start();

                for (int i = 1; i <= o.Count(); i++)
                {
                    TermBindingSet termBindingSet = new TermBindingSet();
                    termBindingSet.terminology = o.KeyForIteration().ToString();

                    EiffelStructures.Table.HashTableReferenceReference adlTerms = o.ItemForIteration() as EiffelStructures.Table.HashTableReferenceReference;
                    SortedList<string, TERM_BINDING_ITEM> localTerms = new SortedList<string, TERM_BINDING_ITEM>();
                    adlTerms.Start();

                    for (int j = 1; j <= adlTerms.Count(); j++)
                    {
                        AdlParser.CodePhrase term = adlTerms.ItemForIteration() as AdlParser.CodePhrase;

                        if (term != null)
                        {
                            TERM_BINDING_ITEM localTerm = new TERM_BINDING_ITEM();
                            localTerm.code = adlTerms.KeyForIteration().ToString();
                            CODE_PHRASE codePhrase = new CODE_PHRASE();
                            codePhrase.code_string = term.CodeString().ToString();

                            if (term.CodeString() != null)
                            {
                                TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                                terminologyId.value = term.TerminologyId().Value().ToString();
                                codePhrase.terminology_id = terminologyId;
                            }

                            localTerm.value = codePhrase;
                            localTerms.Add(localTerm.code, localTerm);
                        }

                        adlTerms.Forth();
                    }

                    termBindingSet.items = new TERM_BINDING_ITEM[localTerms.Count];
                    localTerms.Values.CopyTo(termBindingSet.items, 0);

                    if (termBindingSet.items != null)
                        termBindingSets.Add(termBindingSet);

                    o.Forth();
                }
            }

            if (termBindingSets.Count > 0)
            {
                result = new TermBindingSet[termBindingSets.Count];
                termBindingSets.CopyTo(result);
            }

            return result;
        }

        protected override void CleanUpCodeDefinitionSet(CodeDefinitionSet codeDefinitionSet)
        {
            foreach (ARCHETYPE_TERM term in codeDefinitionSet.items)
            {
                if (term.items != null)
                    foreach (StringDictionaryItem item in term.items)
                        if (item.id == "text")
                            item.Value = item.Value.Trim();
            }
        }

        protected override C_ATTRIBUTE CloneAttribute(AdlParser.CAttribute currentObject)
        {
            C_ATTRIBUTE result = base.CloneAttribute(currentObject);

            AdlParser.CComplexObject parent = currentObject.Parent() as AdlParser.CComplexObject;

            if (parent != null)
            {
                string parentTypeName = parent.RmTypeName().ToCil();
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
