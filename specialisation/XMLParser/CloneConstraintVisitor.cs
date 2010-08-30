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
    public class CloneConstraintVisitor
    {
        protected MethodInfo lastMethod;

        protected AdlParser.CObject lastObject;

        protected virtual CloneConstraintVisitor NewInstance()
        {
            return new CloneConstraintVisitor();
        }

        /// <summary>
        /// Clone an Eiffel ADL archetype as an OpenEhr.V1.Its.Xml.AM.ARCHETYPE.
        /// </summary>
        public ARCHETYPE CloneArchetype(AdlParser.Archetype adlArchetype)
        {
            ARCHETYPE result = CloneArchetypeDetails(adlArchetype);

            object rootNode = Visit(adlArchetype.Definition(), 0);
            C_COMPLEX_OBJECT rootComplexObject = rootNode as C_COMPLEX_OBJECT;
            result.definition = rootComplexObject;
            CloneTree(adlArchetype.Definition(), rootComplexObject, 0);

            return result;
        }

        protected virtual void CloneTree(AdlParser.CComplexObject adlComplexObject, C_COMPLEX_OBJECT parentComplexObject, int depth)
        {
            CloneConstraintVisitor nodeVisitor = NewInstance();

            AdlParser.CAttribute adlAttribute;
            parentComplexObject.attributes = new C_ATTRIBUTE[adlComplexObject.Attributes().Count()];

            for (int i = 1; i <= parentComplexObject.attributes.Length; i++)
            {
                adlAttribute = adlComplexObject.Attributes().ITh(i) as AdlParser.CAttribute;
                C_ATTRIBUTE attributeNode = nodeVisitor.CloneAttribute(adlAttribute);
                attributeNode.children = new C_OBJECT[adlAttribute.Children().Count()];
                parentComplexObject.attributes[i - 1] = attributeNode;

                for (int j = 1; j <= attributeNode.children.Length; j++)
                {
                    AdlParser.CObject child = adlAttribute.Children().ITh(j) as AdlParser.CObject;
                    object childNode = nodeVisitor.Visit(child, depth);
                    attributeNode.children[j - 1] = childNode as C_OBJECT;

                    if (child is AdlParser.CComplexObject)
                        CloneTree(child as AdlParser.CComplexObject, childNode as C_COMPLEX_OBJECT, ++depth);
                }
            }
        }

        protected virtual object Visit(AdlParser.CObject o, int depth)
        {
            if (o == null)
                throw new ArgumentNullException("CObject parameter must not be null.  depth = " + depth);

            try
            {
                System.Reflection.MethodInfo method = this.GetType().GetMethod("Visit",
                               System.Reflection.BindingFlags.ExactBinding | System.Reflection.BindingFlags.NonPublic
                               | System.Reflection.BindingFlags.Instance, Type.DefaultBinder,
                               new Type[] { o.GetType(), depth.GetType() }, new System.Reflection.ParameterModifier[0]);

                if (method != null)
                    // Avoid StackOverflow exceptions by executing only if the method and visitable  
                    // are different from the last parameters used.
                    if (method != lastMethod || o != lastObject)
                    {
                        lastMethod = method;
                        lastObject = o;
                        object itemObject = method.Invoke(this, new object[] { o, depth });

                        return itemObject;
                    }
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        //System.Diagnostics.Debug.WriteLine(ex.InnerException.InnerException.ToString() + Environment.NewLine);
                        //throw ex.InnerException;
                        throw new InvalidOperationException("Visit Exeception.  depth = " + depth, ex.InnerException.InnerException);
                    }
                    System.Diagnostics.Debug.WriteLine(ex.InnerException.ToString() + Environment.NewLine);
                    //throw ex.InnerException;
                    throw new InvalidOperationException("Visit Exeception", ex.InnerException);
                }
                System.Diagnostics.Debug.WriteLine(ex.ToString() + Environment.NewLine + depth);
                //throw;
                throw new InvalidOperationException("Visit Exeception", ex);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine(ex.InnerException.ToString() + Environment.NewLine);
                    //throw ex.InnerException;
                    throw new InvalidOperationException("Visit Exeception", ex.InnerException);
                }
                System.Diagnostics.Debug.WriteLine(ex.ToString() + Environment.NewLine + depth);
                //throw;
                throw new InvalidOperationException("Visit Exeception", ex);
            }

            throw new NotImplementedException("The Visitor method 'Visit' with parameter type '" + o.GetType().ToString() + "' is not implemented.");
        }

        protected virtual object Visit(AdlParser.Impl.CComplexObject o, int depth)
        {
            C_COMPLEX_OBJECT result = new C_COMPLEX_OBJECT();
            CloneC_Object(result, o);
            return result;
        }

        protected virtual object Visit(AdlParser.Impl.CPrimitiveObject o, int depth)
        {
            C_PRIMITIVE_OBJECT result = new C_PRIMITIVE_OBJECT();
            CloneC_Object(result, o);

            if (o.Item() != null)
                result.item = CloneC_Primitive(o.Item());

            return result;
        }

        protected virtual C_DATE CloneDate(AdlParser.CDate o)
        {
            C_DATE result = new C_DATE();

            if (o.HasAssumedValue())
                result.assumed_value = o.AssumedValue().ToString();

            if (o.Pattern() != null)
                result.pattern = o.Pattern().ToString();

            result.range = CloneIntervalOfDate(o.Range());

            return result;
        }

        protected virtual C_DATE_TIME CloneDateTime(AdlParser.CDateTime o)
        {
            C_DATE_TIME result = new C_DATE_TIME();

            if (o.HasAssumedValue())
                result.assumed_value = o.AssumedValue().ToString();

            if (o.Pattern() != null)
                result.pattern = o.Pattern().ToString();

            result.range = CloneIntervalOfDateTime(o.Range());

            return result;
        }

        protected virtual C_TIME CloneTime(AdlParser.CTime o)
        {
            C_TIME result = new C_TIME();

            if (o.HasAssumedValue())
                result.assumed_value = o.AssumedValue().ToString();

            if (o.Pattern() != null)
                result.pattern = o.Pattern().ToString();

            result.range = CloneIntervalOfTime(o.Range());

            return result;
        }

        protected virtual C_DURATION CloneDuration(AdlParser.CDuration o)
        {
            C_DURATION result = new C_DURATION();

            if (o.HasAssumedValue())
                result.assumed_value = o.AssumedValue().ToString();

            if (o.Pattern() != null)
                result.pattern = o.Pattern().ToString();

            result.range = CloneDurationRange(o.Range());

            return result;
        }

        protected virtual IntervalOfDuration CloneDurationRange(AdlParser.IntervalReference o)
        {
            IntervalOfDuration result = null;

            if (o != null)
            {
                result = new IntervalOfDuration();
                result.lower_unbounded = o.LowerUnbounded();

                if (!o.LowerUnbounded())
                {
                    result.lower = o.Lower().ToString();
                    result.lower_included = o.LowerIncluded();
                    result.lower_includedSpecified = true;
                }

                result.upper_unbounded = o.UpperUnbounded();

                if (!o.UpperUnbounded())
                {
                    result.upper = o.Upper().ToString();
                    result.upper_included = o.UpperIncluded();
                    result.upper_includedSpecified = true;
                }
            }

            return result;
        }

        protected virtual IntervalOfDate CloneIntervalOfDate(AdlParser.IntervalReference o)
        {
            IntervalOfDate result = null;

            if (o != null)
            {
                result = new IntervalOfDate();
                result.lower_unbounded = o.LowerUnbounded();

                if (!o.LowerUnbounded())
                {
                    result.lower = o.Lower().ToString();
                    result.lower_included = o.LowerIncluded();
                    result.lower_includedSpecified = true;
                }

                result.upper_unbounded = o.UpperUnbounded();

                if (!o.UpperUnbounded())
                {
                    result.upper = o.Upper().ToString();
                    result.upper_included = o.UpperIncluded();
                    result.upper_includedSpecified = true;
                }
            }

            return result;
        }

        protected virtual IntervalOfDateTime CloneIntervalOfDateTime(AdlParser.IntervalReference o)
        {
            IntervalOfDateTime result = null;

            if (o != null)
            {
                result = new IntervalOfDateTime();
                result.lower_unbounded = o.LowerUnbounded();

                if (!o.LowerUnbounded())
                {
                    result.lower = o.Lower().ToString();
                    result.lower_included = o.LowerIncluded();
                    result.lower_includedSpecified = true;
                }

                result.upper_unbounded = o.UpperUnbounded();

                if (!o.UpperUnbounded())
                {
                    result.upper = o.Upper().ToString();
                    result.upper_included = o.UpperIncluded();
                    result.upper_includedSpecified = true;
                }
            }

            return result;
        }

        protected virtual IntervalOfTime CloneIntervalOfTime(AdlParser.IntervalReference o)
        {
            IntervalOfTime result = null;

            if (o != null)
            {
                result = new IntervalOfTime();
                result.lower_unbounded = o.LowerUnbounded();

                if (!o.LowerUnbounded())
                {
                    result.lower = o.Lower().ToString();
                    result.lower_included = o.LowerIncluded();
                    result.lower_includedSpecified = true;
                }

                result.upper_unbounded = o.UpperUnbounded();

                if (!o.UpperUnbounded())
                {
                    result.upper = o.Upper().ToString();
                    result.upper_included = o.UpperIncluded();
                    result.upper_includedSpecified = true;
                }
            }

            return result;
        }

        protected virtual C_BOOLEAN CloneBoolean(AdlParser.CBoolean o)
        {
            C_BOOLEAN result = new C_BOOLEAN();
            result.true_valid = o.TrueValid();
            result.false_valid = o.FalseValid();

            if (o.HasAssumedValue())
            {
                result.assumed_valueSpecified = true;
                result.assumed_value = (o.AssumedValue() as EiffelKernel.BooleanRef).Item();
            }

            return result;
        }

        protected virtual C_STRING CloneString(AdlParser.CString o)
        {
            C_STRING result = new C_STRING();

            if (o.Regexp() != null)
                result.pattern = o.Regexp().ToCil();

            if (o.HasAssumedValue())
                result.assumed_value = o.AssumedValue().ToString();

            if (o.Strings() != null && o.Strings().Count() > 0)
            {
                result.list = new string[o.Strings().Count()];

                for (int i = 1; i <= result.list.Length; i++)
                    result.list[i - 1] = o.Strings().ITh(i).ToString();
            }

            if (o.IsOpen())
            {
                result.list_open = true;
                result.list_openSpecified = true;
            }

            return result;
        }

        protected virtual C_INTEGER CloneInteger(AdlParser.CInteger o)
        {
            C_INTEGER result = new C_INTEGER();

            if (o.Range() != null)
                result.range = CloneIntervalOfInteger(o.Range());

            if (o.HasAssumedValue())
            {
                result.assumed_valueSpecified = true;
                result.assumed_value = (o.AssumedValue() as EiffelKernel.Integer_32Ref).Item();
            }

            if (o.List() != null && o.List().Count() > 0)
            {
                result.list = new int[o.List().Count()];

                for (int i = 1; i <= result.list.Length; i++)
                    result.list[i - 1] = o.List().ITh(i);
            }

            return result;
        }

        protected virtual C_REAL CloneReal(AdlParser.CReal o)
        {
            C_REAL result = new C_REAL();

            if (o.Range() != null)
                result.range = CloneIntervalOfReal(o.Range());

            if (o.HasAssumedValue())
            {
                result.assumed_valueSpecified = true;
                result.assumed_value = (o.AssumedValue() as EiffelKernel.Dotnet.Real_32Ref).Item();
            }

            if (o.List() != null && o.List().Count() > 0)
            {
                result.list = new float[o.List().Count()];

                for (int i = 1; i <= result.list.Length; i++)
                    result.list[i - 1] = (float)o.List().ITh(i);
            }

            return result;
        }

        protected virtual object Visit(AdlParser.Impl.ConstraintRef o, int depth)
        {
            CONSTRAINT_REF result = new CONSTRAINT_REF();
            CloneC_Object(result, o);
            result.reference = o.Target().ToString();

            return result;
        }

        protected virtual object Visit(AdlParser.Impl.ArchetypeInternalRef o, int depth)
        {
            ARCHETYPE_INTERNAL_REF result = new ARCHETYPE_INTERNAL_REF();
            CloneC_Object(result, o);
            result.target_path = o.TargetPath().ToCil();

            return result;
        }

        protected virtual object Visit(AdlParser.Impl.ArchetypeSlot o, int depth)
        {
            ARCHETYPE_SLOT result = new ARCHETYPE_SLOT();
            CloneC_Object(result, o);

            if (o.HasIncludes())
                result.includes = CloneAssertion(o.Includes());

            if (o.HasExcludes())
                result.excludes = CloneAssertion(o.Excludes());

            return result;
        }

        protected virtual object Visit(AdlParser.Impl.CDvQuantity o, int depth)
        {
            C_DV_QUANTITY result = new C_DV_QUANTITY();
            CloneC_Object(result, o);

            if (o.AssumedValue() != null)
                result.assumed_value = CloneDvQuantity((AdlParser.Quantity)o.AssumedValue());

            if (o.Property() != null)
            {
                result.property = CloneCodePhrase(o.Property());

                if (o.List() != null && o.List().Count() > 0)
                {
                    result.list = new C_QUANTITY_ITEM[o.List().Count()];

                    for (int i = 1; i <= result.list.Length; i++)
                        result.list[i - 1] = CloneQuantityItem(o.List().ITh(i) as AdlParser.CQuantityItem);
                }
            }

            return result;
        }

        protected virtual DV_ORDINAL CloneDvOrdinal(AdlParser.Ordinal o)
        {
            DV_ORDINAL result = new DV_ORDINAL();

            // Inherits DV_ORDERED (only in Reference Model)
            // 0..1 normal_range DV_INTERVAL
            // 0..* other_reference_ranges REFERENCE_RANGE                        
            // 0..1 normal_status CODE_PHRASE            

            result.value = o.Value();

            result.symbol = new DV_CODED_TEXT();
            result.symbol.defining_code = CloneCodePhrase(o.Symbol());
            result.symbol.value = "";  // what should this be?

            return result;
        }

        protected virtual DV_QUANTITY CloneDvQuantity(AdlParser.Quantity o)
        {
            DV_QUANTITY result = new DV_QUANTITY();

            // Inherits DV_AMOUNT (only in Reference Model)
            // 0..1 normal_range DV_INTERVAL
            // 0..* other_reference_ranges REFERENCE_RANGE
            // 0..1 normal_status CODE_PHRASE
            // 0..1 magnitude_status string
            // 0..1 accuracy float
            // 0..1 accuracy_is_percent boolean

            result.magnitude = o.Magnitude();
            result.precision = o.Precision();

            if (o.Units() != null)
                result.units = o.Units().ToString();

            return result;
        }

        protected virtual object Visit(AdlParser.Impl.CDvOrdinal o, int depth)
        {
            C_DV_ORDINAL result = new C_DV_ORDINAL();
            CloneC_Object(result, o);

            if (o.AssumedValue() != null)
                result.assumed_value = CloneDvOrdinal((AdlParser.Ordinal)o.AssumedValue());

            if (!o.AnyAllowed())
            {
                EiffelStructures.List.LinkedListReference adlOrdinals = o.Items();
                result.list = new DV_ORDINAL[adlOrdinals.Count()];
                adlOrdinals.Start();

                for (int i = 0; i < result.list.Length; i++)
                {
                    result.list[i] = CloneDvOrdinal((AdlParser.Ordinal)adlOrdinals.Active().Item());
                    adlOrdinals.Forth();
                }
            }

            return result;
        }

        protected virtual object Visit(AdlParser.Impl.CCodePhrase o, int depth)
        {
            C_CODE_PHRASE result = new C_CODE_PHRASE();
            CloneC_Object(result, o);

            if (o.TerminologyId() != null)
            {
                TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                terminologyId.value = o.TerminologyId().Value().ToString();
                result.terminology_id = terminologyId;
            }

            if (o.CodeList() != null)
            {
                result.code_list = new string[o.CodeList().Count()];

                for (int i = 1; i <= result.code_list.Length; i++)
                    result.code_list[i - 1] = o.CodeList().ITh(i).ToString();
            }

            if (o.HasAssumedValue())
                result.assumed_value = CloneCodePhrase((AdlParser.CodePhrase)o.AssumedValue());

            return result;
        }

        protected virtual void CloneC_Object(C_OBJECT cloneObject, AdlParser.CObject o)
        {
            cloneObject.node_id = o.NodeId().ToCil();

            if (cloneObject.node_id.StartsWith("unknown"))  //hack for xml conversion 
                cloneObject.node_id = "";

            cloneObject.rm_type_name = o.RmTypeName().ToCil();

            if (o.Occurrences() != null)
                cloneObject.occurrences = CloneIntervalOfInteger(o.Occurrences());
            else
            {
                cloneObject.occurrences = new IntervalOfInteger();
                cloneObject.occurrences.lower = 1;
                cloneObject.occurrences.upper = 1;
                cloneObject.occurrences.lower_included = true;
                cloneObject.occurrences.upper_included = true;
                cloneObject.occurrences.lower_includedSpecified = true;
                cloneObject.occurrences.upper_includedSpecified = true;
            }
        }

        protected virtual IntervalOfReal CloneIntervalOfReal(AdlParser.IntervalReal_32 o)
        {
            IntervalOfReal result = new IntervalOfReal();

            System.Diagnostics.Debug.Assert(!result.lower_includedSpecified, "lower included specified must be false!");

            result.lower_unbounded = o.LowerUnbounded();

            if (!o.LowerUnbounded())
            {
                result.lower = o.Lower();
                result.lowerSpecified = true;
                result.lower_included = o.LowerIncluded();
                result.lower_includedSpecified = true;
            }

            result.upper_unbounded = o.UpperUnbounded();

            if (!o.UpperUnbounded())
            {
                result.upper = o.Upper();
                result.upperSpecified = true;
                result.upper_included = o.UpperIncluded();
                result.upper_includedSpecified = true;
            }

            return result;
        }

        protected virtual IntervalOfInteger CloneIntervalOfInteger(AdlParser.IntervalInteger_32 o)
        {
            IntervalOfInteger result = null;
            result = new IntervalOfInteger();

            System.Diagnostics.Debug.Assert(!result.lower_includedSpecified, "lower included specified must be false!");

            result.lower_unbounded = o.LowerUnbounded();

            if (!o.LowerUnbounded())
            {
                result.lower = o.Lower();
                result.lowerSpecified = true;
                result.lower_included = o.LowerIncluded();
                result.lower_includedSpecified = true;
            }

            result.upper_unbounded = o.UpperUnbounded();

            if (!o.UpperUnbounded())
            {
                result.upper = o.Upper();
                result.upperSpecified = true;
                result.upper_included = o.UpperIncluded();
                result.upper_includedSpecified = true;
            }

            return result;
        }

        protected virtual C_PRIMITIVE CloneC_Primitive(AdlParser.CPrimitive o)
        {
            C_PRIMITIVE result = null;

            if (o is AdlParser.CReal)
                result = CloneReal(o as AdlParser.CReal);
            else if (o is AdlParser.CInteger)
                result = CloneInteger(o as AdlParser.CInteger);
            else if (o is AdlParser.CString)
                result = CloneString(o as AdlParser.CString);
            else if (o is AdlParser.CBoolean)
                result = CloneBoolean(o as AdlParser.CBoolean);
            else if (o is AdlParser.CDuration)
                result = CloneDuration(o as AdlParser.CDuration);
            else if (o is AdlParser.CDateTime)
                result = CloneDateTime(o as AdlParser.CDateTime);
            else if (o is AdlParser.CDate)
                result = CloneDate(o as AdlParser.CDate);
            else if (o is AdlParser.CTime)
                result = CloneTime(o as AdlParser.CTime);
            else
                throw new NotImplementedException("The Visitor method 'Visit' with parameter type '" + o.GetType() + "' is not implemented.");

            return result;
        }

        protected virtual C_QUANTITY_ITEM CloneQuantityItem(AdlParser.CQuantityItem o)
        {
            C_QUANTITY_ITEM result = new C_QUANTITY_ITEM();

            result.units = o.Units().ToCil();

            if (o.Magnitude() != null)
                result.magnitude = CloneIntervalOfReal(o.Magnitude());

            if (!o.AnyPrecisionAllowed())
                result.precision = CloneIntervalOfInteger(o.Precision());

            return result;
        }

        protected virtual ASSERTION[] CloneAssertion(EiffelStructures.List.ArrayedListReference adlList)
        {
            ASSERTION[] result = null;

            if (adlList != null)
            {
                result = new ASSERTION[adlList.Count()];

                for (int i = 1; i <= result.Length; i++)
                {
                    AdlParser.Assertion assert = adlList.ITh(i) as AdlParser.Assertion;
                    ASSERTION localAssertion = new ASSERTION();

                    if (assert.Tag() != null)
                        localAssertion.tag = assert.Tag().ToString();

                    // 0..1 string_expression string
                    //localAssertion.string_expression  (not implemented in adl object)

                    if (assert.Expression() != null)
                        localAssertion.expression = CloneExprItem(assert.Expression());

                    // 0..* variables ASSERTION_VARIABLE (not implememented in adl object)                            

                    result[i - 1] = localAssertion;
                }
            }

            return result;
        }

        protected virtual EXPR_ITEM CloneExprItem(AdlParser.ExprItem o)
        {
            EXPR_ITEM result = null;

            if (o is AdlParser.ExprLeaf)
                result = CloneExprItem(o as AdlParser.ExprLeaf);
            else if (o is AdlParser.ExprBinaryOperator)
                result = CloneExprItem(o as AdlParser.ExprBinaryOperator);
            else if (o is AdlParser.ExprUnaryOperator)
                result = CloneExprItem(o as AdlParser.ExprUnaryOperator);

            return result;
        }

        protected virtual EXPR_ITEM CloneExprItem(AdlParser.ExprLeaf o)
        {
            EXPR_LEAF result = new EXPR_LEAF();

            result.type = o.Type().ToString();

            if (o.ReferenceType() != null)
                result.reference_type = o.ReferenceType().ToString();

            switch (result.type)
            {
                case "C_STRING":
                    result.item = CloneString(o.Item() as AdlParser.CString);
                    break;

                case "String":
                    result.item = o.Item().ToString();
                    break;

                default:
                    throw new NotSupportedException(result.type);
            }

            return result;
        }

        protected virtual EXPR_ITEM CloneExprItem(AdlParser.ExprBinaryOperator o)
        {
            EXPR_BINARY_OPERATOR result = new EXPR_BINARY_OPERATOR();

            if (o.Type() != null)
                result.type = o.Type().ToString();

            result.precedence_overridden = o.PrecedenceOverridden();

            if (o.Operator().Value() >= 2001 && o.Operator().Value() <= 2020)
                result.@operator = ((OPERATOR_KIND)o.Operator().Value()) - 2001;

            if (o.LeftOperand() != null)
                result.left_operand = CloneExprItem(o.LeftOperand());

            if (o.RightOperand() != null)
                result.right_operand = CloneExprItem(o.RightOperand());

            return result;
        }

        protected virtual EXPR_ITEM CloneExprItem(AdlParser.ExprUnaryOperator o)
        {
            EXPR_UNARY_OPERATOR result = new EXPR_UNARY_OPERATOR();

            if (o.Type() != null)
                result.type = o.Type().ToString();

            if (o.Operator().Value() >= 2001 && o.Operator().Value() <= 2020)
                result.@operator = ((OPERATOR_KIND)o.Operator().Value()) - 2001;

            result.precedence_overridden = o.PrecedenceOverridden();

            if (o.Operand() != null)
                result.operand = CloneExprItem(o.Operand());

            return result;
        }

        protected virtual CODE_PHRASE CloneCodePhrase(AdlParser.CodePhrase o)
        {
            CODE_PHRASE result = null;

            if (o != null)
            {
                result = new CODE_PHRASE();

                if (o.CodeString() != null)
                    result.code_string = o.CodeString().ToString();
                else
                    result.code_string = "";

                if (o.TerminologyId() != null)
                {
                    TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                    terminologyId.value = o.TerminologyId().Value().ToString();
                    result.terminology_id = terminologyId;
                }
            }

            return result;
        }

        protected virtual ARCHETYPE CloneArchetypeDetails(AdlParser.Archetype archetype)
        {
            ARCHETYPE result = new ARCHETYPE();

            // 0..1 uid HIER_OBJECT_ID (not implemented in adl object)            

            if (archetype.ArchetypeId() != null)
            {
                ARCHETYPE_ID archetypeId = new ARCHETYPE_ID();
                archetypeId.value = archetype.ArchetypeId().Value().ToString();
                result.archetype_id = archetypeId;
            }

            if (archetype.Version() != null)
                result.adl_version = archetype.AdlVersion().ToString();

            if (archetype.Concept() != null)
                result.concept = archetype.Concept().ToString();

            result.original_language = CloneCodePhrase(archetype.OriginalLanguage());
            result.is_controlled = archetype.IsControlled();

            if (archetype.ParentArchetypeId() != null)
            {
                ARCHETYPE_ID parentId = new ARCHETYPE_ID();
                parentId.value = archetype.ParentArchetypeId().Value().ToString();
                result.parent_archetype_id = parentId;
            }

            result.description = CloneDescription(archetype.Description());
            result.ontology = CloneOntology(archetype.Ontology());

            // 0..1 revision_history REVISION_HISTORY (does not occur in NHS archetypes, do later)
            //if (archetype.revision_history() != null)
            // result.revision_history = CloneAuthoredResource(archetype.revision_history());

            if (archetype.Translations() != null && archetype.Translations().Count() >= 0)
            {
                TRANSLATION_DETAILS[] translations = new TRANSLATION_DETAILS[archetype.Translations().Count()];
                archetype.Translations().Start();

                for (int i = 1; i <= translations.Length; i++)
                {
                    TRANSLATION_DETAILS translation = new TRANSLATION_DETAILS();
                    AdlParser.TranslationDetails td = archetype.Translations().ItemForIteration() as AdlParser.TranslationDetails;

                    if (td.Accreditation() != null)
                        translation.accreditation = td.Accreditation().ToString();

                    translation.author = CloneHashTableAny(td.Author());
                    translation.language = CloneCodePhrase(td.Language());
                    translation.other_details = CloneHashTableAny(td.OtherDetails());
                    translations[i - 1] = translation;

                    archetype.Translations().Forth();
                }

                result.translations = translations;
            }

            result.invariants = CloneAssertion(archetype.Invariants());

            return result;
        }

        protected virtual ARCHETYPE_ONTOLOGY CloneOntology(AdlParser.ArchetypeOntology o)
        {
            ARCHETYPE_ONTOLOGY result = new ARCHETYPE_ONTOLOGY();

            result.term_definitions = CloneCodeDefinitions(o.TermDefinitions());
            result.term_bindings = CloneTermBindingSet(o.TermBindings());
            result.constraint_definitions = CloneCodeDefinitions(o.ConstraintDefinitions());
            result.constraint_bindings = CloneConstraintBindingSet(o.ConstraintBindings());

            return result;
        }

        protected virtual TermBindingSet[] CloneTermBindingSet(EiffelStructures.Table.HashTableReferenceReference o)
        {
            TermBindingSet[] result = null;

            if (o != null && o.Count() > 0)
            {
                result = new TermBindingSet[o.Count()];
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
                    result[i - 1] = termBindingSet;
                    o.Forth();
                }
            }

            return result;
        }

        protected virtual CodeDefinitionSet[] CloneCodeDefinitions(EiffelStructures.Table.HashTableReferenceReference o)
        {
            CodeDefinitionSet[] result = null;

            if (o != null)
            {
                result = new CodeDefinitionSet[o.Count()];
                o.Start();

                for (int i = 1; i <= result.Length; i++)
                {
                    CodeDefinitionSet codeDefinitionSet = new CodeDefinitionSet();
                    codeDefinitionSet.language = o.KeyForIteration().ToString();
                    EiffelStructures.Table.HashTableReferenceReference adlTerms = o.ItemForIteration() as EiffelStructures.Table.HashTableReferenceReference;
                    SortedList<string, ARCHETYPE_TERM> localTerms = new SortedList<string, ARCHETYPE_TERM>();
                    adlTerms.Start();

                    for (int j = 1; j <= adlTerms.Count(); j++)
                    {
                        AdlParser.ArchetypeTerm term = adlTerms.ItemForIteration() as AdlParser.ArchetypeTerm;
                        ARCHETYPE_TERM localTerm = new ARCHETYPE_TERM();
                        localTerm.code = term.Code().ToString();
                        localTerm.items = CloneDsHashTableAny(term.Items());
                        localTerms.Add(localTerm.code, localTerm);
                        adlTerms.Forth();
                    }

                    codeDefinitionSet.items = new ARCHETYPE_TERM[localTerms.Count];
                    localTerms.Values.CopyTo(codeDefinitionSet.items, 0);
                    CleanUpCodeDefinitionSet(codeDefinitionSet);
                    result[i - 1] = codeDefinitionSet;
                    o.Forth();
                }
            }

            return result;
        }

        protected virtual void CleanUpCodeDefinitionSet(CodeDefinitionSet codeDefinitionSet)
        {
        }

        protected virtual ConstraintBindingSet[] CloneConstraintBindingSet(EiffelStructures.Table.HashTableReferenceReference o)
        {
            ConstraintBindingSet[] result = null;

            if (o != null && o.Count() > 0)
            {
                result = new ConstraintBindingSet[o.Count()];
                o.Start();

                for (int i = 1; i <= result.Length; i++)
                {
                    ConstraintBindingSet constraintBindingSet = new ConstraintBindingSet();
                    constraintBindingSet.terminology = o.KeyForIteration().ToString();
                    EiffelStructures.Table.HashTableReferenceReference adlTerms = o.ItemForIteration() as EiffelStructures.Table.HashTableReferenceReference;
                    CONSTRAINT_BINDING_ITEM[] localTerms = new CONSTRAINT_BINDING_ITEM[adlTerms.Count()];
                    adlTerms.Start();

                    for (int j = 1; j <= localTerms.Length; j++)
                    {
                        AdlParser.Uri term = adlTerms.ItemForIteration() as AdlParser.Uri;
                        CONSTRAINT_BINDING_ITEM localTerm = new CONSTRAINT_BINDING_ITEM();
                        localTerm.code = adlTerms.KeyForIteration().ToString();
                        localTerm.value = adlTerms.ItemForIteration().ToString();
                        localTerms[j - 1] = localTerm;
                        adlTerms.Forth();
                    }

                    constraintBindingSet.items = localTerms;
                    result[i - 1] = constraintBindingSet;
                    o.Forth();
                }
            }

            return result;
        }

        protected virtual RESOURCE_DESCRIPTION CloneDescription(AdlParser.ResourceDescription o)
        {
            RESOURCE_DESCRIPTION result = new RESOURCE_DESCRIPTION();

            result.original_author = CloneHashTableAny(o.OriginalAuthor());

            if (o.OtherContributors() != null && o.OtherContributors().Count() > 0)
            {
                result.other_contributors = new string[o.OtherContributors().Count()];

                for (int i = 1; i < result.other_contributors.Length; i++)
                    result.other_contributors[i - 1] = ((EiffelKernel.String_8)o.OtherContributors().ITh(i)).ToCil();
            }

            result.lifecycle_state = o.LifecycleState().ToCil();

            if (o.ResourcePackageUri() != null)
                result.resource_package_uri = o.ResourcePackageUri().ToString();

            result.other_details = CloneHashTableAny(o.OtherDetails());

            // 0..1 parent_resource AUTHORED_RESOURCE (does not occur in NHS archetypes, do later)
            //if (o.parent_resource() != null)
            //result.parent_resource = CloneAuthoredResource(o.parent_resource());

            if (o.Details().Count() > 0)
            {
                RESOURCE_DESCRIPTION_ITEM[] details = new RESOURCE_DESCRIPTION_ITEM[o.Details().Count()];
                o.Details().Start();

                for (int i = 1; i <= details.Length; i++)
                {
                    AdlParser.ResourceDescriptionItem item = o.Details().ItemForIteration() as AdlParser.ResourceDescriptionItem;
                    details[i - 1] = new RESOURCE_DESCRIPTION_ITEM();
                    details[i - 1].language = CloneCodePhrase(item.Language());

                    if (item.Purpose() != null)
                        details[i - 1].purpose = item.Purpose().ToString();

                    if (item.Use() != null)
                        details[i - 1].use = item.Use().ToString();

                    if (item.Misuse() != null)
                        details[i - 1].misuse = item.Misuse().ToString();

                    if (item.Copyright() != null)
                        details[i - 1].copyright = item.Copyright().ToString();

                    if (item.OriginalResourceUri() != null && item.OriginalResourceUri().Count() > 0)
                        result.resource_package_uri = item.OriginalResourceUri().ToString();

                    // 0..* other_details StringDictionaryItem
                    //result.other_details = CloneHashTableAny(item.other_details());

                    if (item.Keywords() != null && item.Keywords().Count() > 0)
                    {
                        string[] keywords = new string[item.Keywords().Count()];
                        details[i - 1].keywords = keywords;

                        for (int j = 1; j <= keywords.Length; j++)
                        {
                            if (item.Keywords().ITh(j) != null)
                                keywords[j - 1] = item.Keywords().ITh(j).ToString();
                        }
                    }

                    o.Details().Forth();
                }

                result.details = details;
            }

            return result;
        }

        protected virtual StringDictionaryItem[] CloneHashTableAny(EiffelStructures.Table.HashTableReferenceReference o)
        {
            StringDictionaryItem[] result = null;

            if (o != null && o.Count() > 0)
            {
                SortedList<string, StringDictionaryItem> dictionaryItem = new SortedList<string, StringDictionaryItem>();
                o.Start();

                while (!o.Off())
                {
                    StringDictionaryItem item = new StringDictionaryItem();
                    item.id = o.KeyForIteration().ToString();
                    item.Value = o.ItemForIteration().ToString();
                    dictionaryItem.Add(item.id, item);
                    o.Forth();
                }

                result = new StringDictionaryItem[dictionaryItem.Count];
                dictionaryItem.Values.CopyTo(result, 0);
            }

            return result;
        }

        protected virtual StringDictionaryItem[] CloneDsHashTableAny(Gobo.Library.Structure.Table.DsHashTableReferenceReference o)
        {
            StringDictionaryItem[] result = null;

            if (o != null && o.Count() > 0)
            {
                SortedList<string, StringDictionaryItem> dictionaryItem = new SortedList<string, StringDictionaryItem>();
                o.Start();

                while (!o.Off())
                {
                    StringDictionaryItem item = new StringDictionaryItem();
                    item.id = o.KeyForIteration().ToString();
                    item.Value = o.ItemForIteration().ToString();
                    dictionaryItem.Add(item.id, item);
                    o.Forth();
                }

                result = new StringDictionaryItem[dictionaryItem.Count];
                dictionaryItem.Values.CopyTo(result, 0);
            }

            return result;
        }

        protected virtual CARDINALITY CloneCardinality(AdlParser.Cardinality o)
        {
            CARDINALITY result = new CARDINALITY();

            result.is_ordered = o.IsOrdered();
            result.is_unique = o.IsUnique();
            result.interval = CloneIntervalOfInteger(o.Interval());

            return result;
        }

        protected virtual C_ATTRIBUTE CloneAttribute(AdlParser.CAttribute o)
        {
            C_ATTRIBUTE result;

            if (o.Cardinality() == null)
                result = new C_SINGLE_ATTRIBUTE();
            else
            {
                C_MULTIPLE_ATTRIBUTE cloneMultiple = new C_MULTIPLE_ATTRIBUTE();
                cloneMultiple.cardinality = CloneCardinality(o.Cardinality());
                result = cloneMultiple;
            }

            result.rm_attribute_name = o.RmAttributeName().ToCil();

            if (o.Existence() != null)
                result.existence = CloneIntervalOfInteger(o.Existence());
            else
            {
                result.existence = new IntervalOfInteger();
                result.existence.lower = 1;
                result.existence.upper = 1;
                result.existence.lower_included = true;
                result.existence.upper_included = true;
                result.existence.lower_includedSpecified = true;
                result.existence.upper_includedSpecified = true;
            }

            // 0..* children C_OBJECT (set in CloneTree)

            return result;
        }
    }
}
