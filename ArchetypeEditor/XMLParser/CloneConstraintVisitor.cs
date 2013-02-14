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
    public class CloneConstraintVisitor
    {
        protected MethodInfo lastMethod;

        protected openehr.openehr.am.archetype.constraint_model.C_OBJECT lastObject;

        protected virtual CloneConstraintVisitor NewInstance()
        {
            return new CloneConstraintVisitor();
        }

        /// <summary>
        /// Clone an Eiffel ADL archetype as an OpenEhr.V1.Its.Xml.AM.ARCHETYPE.
        /// </summary>
        public ARCHETYPE CloneArchetype(openehr.openehr.am.archetype.ARCHETYPE adlArchetype)
        {
            ARCHETYPE result = CloneArchetypeDetails(adlArchetype);

            object rootNode = Visit(adlArchetype.definition(), 0);
            C_COMPLEX_OBJECT rootComplexObject = rootNode as C_COMPLEX_OBJECT;
            result.definition = rootComplexObject;
            CloneTree(adlArchetype.definition(), rootComplexObject, 0);

            return result;
        }

        protected virtual void CloneTree(openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT adlComplexObject, C_COMPLEX_OBJECT parentComplexObject, int depth)
        {
            CloneConstraintVisitor nodeVisitor = NewInstance();

            openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE adlAttribute;
            parentComplexObject.attributes = new C_ATTRIBUTE[adlComplexObject.attributes().count()];

            for (int i = 1; i <= adlComplexObject.attributes().count(); i++)
            {
                adlAttribute = adlComplexObject.attributes().i_th(i) as openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE;
                C_ATTRIBUTE attributeNode = nodeVisitor.CloneAttribute(adlAttribute);
                attributeNode.children = new C_OBJECT[adlAttribute.children().count()];
                parentComplexObject.attributes[i - 1] = attributeNode;

                for (int j = 1; j <= adlAttribute.children().count(); j++)
                {
                    openehr.openehr.am.archetype.constraint_model.C_OBJECT child = adlAttribute.children().i_th(j) as openehr.openehr.am.archetype.constraint_model.C_OBJECT;
                    object childNode = nodeVisitor.Visit(child, depth);
                    attributeNode.children[j - 1] = childNode as C_OBJECT;

                    if (child is openehr.openehr.am.archetype.constraint_model.Impl.C_COMPLEX_OBJECT)
                        CloneTree(child as openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, childNode as C_COMPLEX_OBJECT, ++depth);
                }
            }
        }

        protected virtual object Visit(openehr.openehr.am.archetype.constraint_model.C_OBJECT o, int depth)
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

        protected virtual object Visit(openehr.openehr.am.archetype.constraint_model.Impl.C_COMPLEX_OBJECT o, int depth)
        {
            C_COMPLEX_OBJECT result = new C_COMPLEX_OBJECT();
            CloneC_Object(result, o);
            return result;
        }

        protected virtual object Visit(openehr.openehr.am.archetype.constraint_model.Impl.C_PRIMITIVE_OBJECT o, int depth)
        {
            C_PRIMITIVE_OBJECT result = new C_PRIMITIVE_OBJECT();
            CloneC_Object(result, o);

            if (o.item() != null)
                result.item = CloneC_Primitive(o.item());

            return result;
        }

        protected virtual C_DATE CloneDate(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DATE o)
        {
            C_DATE result = new C_DATE();

            if (o.has_assumed_value())
                result.assumed_value = o.assumed_value().ToString();

            if (o.pattern() != null)
                result.pattern = o.pattern().ToString();

            result.range = CloneIntervalOfDate(o.interval());

            return result;
        }

        protected virtual C_DATE_TIME CloneDateTime(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DATE_TIME o)
        {
            C_DATE_TIME result = new C_DATE_TIME();

            if (o.has_assumed_value())
                result.assumed_value = o.assumed_value().ToString();

            if (o.pattern() != null)
                result.pattern = o.pattern().ToString();

            result.range = CloneIntervalOfDateTime(o.interval());

            return result;
        }

        protected virtual C_TIME CloneTime(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_TIME o)
        {
            C_TIME result = new C_TIME();

            if (o.has_assumed_value())
                result.assumed_value = o.assumed_value().ToString();

            if (o.pattern() != null)
                result.pattern = o.pattern().ToString();

            result.range = CloneIntervalOfTime(o.interval());

            return result;
        }

        protected virtual C_DURATION CloneDuration(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DURATION o)
        {
            C_DURATION result = new C_DURATION();

            if (o.has_assumed_value())
                result.assumed_value = o.assumed_value().ToString();

            if (o.pattern() != null)
                result.pattern = o.pattern().ToString();

            result.range = cloneDurationRange(o.interval());

            return result;
        }

        protected virtual IntervalOfDuration cloneDurationRange(openehr.common_libs.basic.INTERVAL_REFERENCE o)
        {
            IntervalOfDuration result = null;

            if (o != null)
            {
                result = new IntervalOfDuration();
                result.lower_unbounded = o.lower_unbounded();

                if (!o.lower_unbounded())
                {
                    result.lower = o.lower().ToString();
                    result.lower_included = o.lower_included();
                    result.lower_includedSpecified = true;
                }

                result.upper_unbounded = o.upper_unbounded();

                if (!o.upper_unbounded())
                {
                    result.upper = o.upper().ToString();
                    result.upper_included = o.upper_included();
                    result.upper_includedSpecified = true;
                }
            }

            return result;
        }

        protected virtual IntervalOfDate CloneIntervalOfDate(openehr.common_libs.basic.INTERVAL_REFERENCE o)
        {
            IntervalOfDate result = null;

            if (o != null)
            {
                result = new IntervalOfDate();
                result.lower_unbounded = o.lower_unbounded();

                if (!o.lower_unbounded())
                {
                    result.lower = o.lower().ToString();
                    result.lower_included = o.lower_included();
                    result.lower_includedSpecified = true;
                }

                result.upper_unbounded = o.upper_unbounded();

                if (!o.upper_unbounded())
                {
                    result.upper = o.upper().ToString();
                    result.upper_included = o.upper_included();
                    result.upper_includedSpecified = true;
                }
            }

            return result;
        }

        protected virtual IntervalOfDateTime CloneIntervalOfDateTime(openehr.common_libs.basic.INTERVAL_REFERENCE o)
        {
            IntervalOfDateTime result = null;

            if (o != null)
            {
                result = new IntervalOfDateTime();
                result.lower_unbounded = o.lower_unbounded();

                if (!o.lower_unbounded())
                {
                    result.lower = o.lower().ToString();
                    result.lower_included = o.lower_included();
                    result.lower_includedSpecified = true;
                }

                result.upper_unbounded = o.upper_unbounded();

                if (!o.upper_unbounded())
                {
                    result.upper = o.upper().ToString();
                    result.upper_included = o.upper_included();
                    result.upper_includedSpecified = true;
                }
            }

            return result;
        }

        protected virtual IntervalOfTime CloneIntervalOfTime(openehr.common_libs.basic.INTERVAL_REFERENCE o)
        {
            IntervalOfTime result = null;

            if (o != null)
            {
                result = new IntervalOfTime();
                result.lower_unbounded = o.lower_unbounded();

                if (!o.lower_unbounded())
                {
                    result.lower = o.lower().ToString();
                    result.lower_included = o.lower_included();
                    result.lower_includedSpecified = true;
                }

                result.upper_unbounded = o.upper_unbounded();

                if (!o.upper_unbounded())
                {
                    result.upper = o.upper().ToString();
                    result.upper_included = o.upper_included();
                    result.upper_includedSpecified = true;
                }
            }

            return result;
        }

        protected virtual C_BOOLEAN CloneBoolean(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_BOOLEAN o)
        {
            C_BOOLEAN result = new C_BOOLEAN();
            result.true_valid = o.true_valid();
            result.false_valid = o.false_valid();

            if (o.has_assumed_value())
            {
                result.assumed_valueSpecified = true;
                result.assumed_value = ((EiffelKernel.BOOLEAN_REF)o.assumed_value()).item();
            }

            return result;
        }

        protected virtual C_STRING CloneString(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_STRING o)
        {
            C_STRING result = new C_STRING();

            if (o.regexp() != null)
                result.pattern = o.regexp().to_cil();

            if (o.has_assumed_value())
                result.assumed_value = o.assumed_value().ToString();

            if (o.strings() != null && o.strings().count() > 0)
            {
                result.list = new string[o.strings().count()];

                for (int i = 1; i <= result.list.Length; i++)
                    result.list[i - 1] = o.strings().i_th(i).ToString();
            }

            if (o.is_open())
            {
                result.list_open = true;
                result.list_openSpecified = true;
            }

            return result;
        }

        protected virtual C_INTEGER CloneInteger(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_INTEGER o)
        {
            C_INTEGER result = new C_INTEGER();

            if (o.interval() != null)
                result.range = CloneIntervalOfInteger(o.interval());

            if (o.has_assumed_value())
            {
                result.assumed_valueSpecified = true;
                result.assumed_value = ((EiffelKernel.INTEGER_32_REF)o.assumed_value()).item();
            }

            if (o.list() != null && o.list().count() > 0)
            {
                result.list = new int[o.list().count()];

                for (int i = 1; i <= o.list().count(); i++)
                    result.list[i - 1] = o.list().i_th(i);
            }

            return result;
        }

        protected virtual C_REAL CloneReal(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_REAL o)
        {
            C_REAL result = new C_REAL();

            if (o.interval() != null)
                result.range = CloneIntervalOfReal(o.interval());

            if (o.has_assumed_value())
            {
                result.assumed_valueSpecified = true;
                result.assumed_value = ((EiffelKernel.dotnet.REAL_32_REF)o.assumed_value()).item();
            }

            if (o.list() != null && o.list().count() > 0)
            {
                result.list = new float[o.list().count()];

                for (int i = 1; i <= result.list.Length; i++)
                    result.list[i - 1] = (float)o.list().i_th(i);
            }

            return result;
        }

        protected virtual object Visit(openehr.openehr.am.archetype.constraint_model.Impl.CONSTRAINT_REF o, int depth)
        {
            CONSTRAINT_REF result = new CONSTRAINT_REF();
            CloneC_Object(result, o);
            result.reference = o.target().ToString();

            return result;
        }

        protected virtual object Visit(openehr.openehr.am.archetype.constraint_model.Impl.ARCHETYPE_INTERNAL_REF o, int depth)
        {
            ARCHETYPE_INTERNAL_REF result = new ARCHETYPE_INTERNAL_REF();
            CloneC_Object(result, o);
            result.target_path = o.target_path().to_cil();

            return result;
        }

        protected virtual object Visit(openehr.openehr.am.archetype.constraint_model.Impl.ARCHETYPE_SLOT o, int depth)
        {
            ARCHETYPE_SLOT result = new ARCHETYPE_SLOT();
            CloneC_Object(result, o);

            if (o.has_includes())
                result.includes = CloneAssertion(o.includes());

            if (o.has_excludes())
                result.excludes = CloneAssertion(o.excludes());

            return result;
        }

        protected virtual object Visit(openehr.openehr.am.openehr_profile.data_types.quantity.Impl.C_DV_QUANTITY o, int depth)
        {
            C_DV_QUANTITY result = new C_DV_QUANTITY();
            CloneC_Object(result, o);

            if (o.assumed_value() != null)
                result.assumed_value = CloneDvQuantity((openehr.openehr.am.openehr_profile.data_types.quantity.Impl.QUANTITY)o.assumed_value());

            if (o.property() != null)
            {
                result.property = CloneCodePhrase(o.property());

                if (o.list() != null && o.list().count() > 0)
                {
                    result.list = new C_QUANTITY_ITEM[o.list().count()];

                    for (int i = 1; i <= result.list.Length; i++)
                        result.list[i - 1] = CloneQuantityItem(o.list().i_th(i) as openehr.openehr.am.openehr_profile.data_types.quantity.Impl.C_QUANTITY_ITEM);
                }
            }

            return result;
        }

        protected virtual DV_ORDINAL CloneDvOrdinal(openehr.openehr.am.openehr_profile.data_types.quantity.Impl.ORDINAL o)
        {
            DV_ORDINAL result = new DV_ORDINAL();

            // Inherits DV_ORDERED (only in Reference Model)
            // 0..1 normal_range DV_INTERVAL
            // 0..* other_reference_ranges REFERENCE_RANGE                        
            // 0..1 normal_status CODE_PHRASE            

            result.value = o.value();

            result.symbol = new DV_CODED_TEXT();
            result.symbol.defining_code = CloneCodePhrase(o.symbol());
            result.symbol.value = "";  // what should this be?

            return result;
        }

        protected virtual DV_QUANTITY CloneDvQuantity(openehr.openehr.am.openehr_profile.data_types.quantity.Impl.QUANTITY o)
        {
            DV_QUANTITY result = new DV_QUANTITY();

            // Inherits DV_AMOUNT (only in Reference Model)
            // 0..1 normal_range DV_INTERVAL
            // 0..* other_reference_ranges REFERENCE_RANGE
            // 0..1 normal_status CODE_PHRASE
            // 0..1 magnitude_status string
            // 0..1 accuracy float
            // 0..1 accuracy_is_percent boolean

            result.magnitude = o.magnitude();
            result.precision = o.precision();

            if (o.units() != null)
                result.units = o.units().ToString();

            return result;
        }

        protected virtual object Visit(openehr.openehr.am.openehr_profile.data_types.quantity.Impl.C_DV_ORDINAL o, int depth)
        {
            C_DV_ORDINAL result = new C_DV_ORDINAL();
            CloneC_Object(result, o);

            if (o.assumed_value() != null)
                result.assumed_value = CloneDvOrdinal((openehr.openehr.am.openehr_profile.data_types.quantity.Impl.ORDINAL)o.assumed_value());

            if (!o.any_allowed())
            {
                EiffelStructures.list.LINKED_LIST_REFERENCE adlOrdinals = o.items();
                result.list = new DV_ORDINAL[adlOrdinals.count()];
                adlOrdinals.start();

                for (int i = 0; i < result.list.Length; i++)
                {
                    result.list[i] = CloneDvOrdinal((openehr.openehr.am.openehr_profile.data_types.quantity.Impl.ORDINAL)adlOrdinals.active().item());
                    adlOrdinals.forth();
                }
            }

            return result;
        }

        protected virtual object Visit(openehr.openehr.am.openehr_profile.data_types.text.Impl.C_CODE_PHRASE o, int depth)
        {
            C_CODE_PHRASE result = new C_CODE_PHRASE();
            CloneC_Object(result, o);

            if (o.terminology_id() != null)
            {
                TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                terminologyId.value = o.terminology_id().value().ToString();
                result.terminology_id = terminologyId;
            }

            if (o.code_list() != null)
            {
                result.code_list = new string[o.code_list().count()];

                for (int i = 1; i <= result.code_list.Length; i++)
                    result.code_list[i - 1] = o.code_list().i_th(i).ToString();
            }

            if (o.has_assumed_value())
                result.assumed_value = CloneCodePhrase((openehr.openehr.rm.data_types.text.Impl.CODE_PHRASE)o.assumed_value());

            return result;
        }

        protected virtual void CloneC_Object(C_OBJECT cloneObject, openehr.openehr.am.archetype.constraint_model.C_OBJECT o)
        {
            if (!o.node_id().to_cil().StartsWith("unknown"))  //hack for xml conversion 
                cloneObject.node_id = o.node_id().to_cil();
            else
                cloneObject.node_id = "";

            cloneObject.rm_type_name = o.rm_type_name().to_cil();
            cloneObject.occurrences = CloneIntervalOfInteger(o.occurrences());
        }

        protected virtual IntervalOfReal CloneIntervalOfReal(openehr.common_libs.basic.INTERVAL_REAL_32 o)
        {
            IntervalOfReal result = new IntervalOfReal();

            System.Diagnostics.Debug.Assert(!result.lower_includedSpecified, "lower included specified must be false!");

            result.lower_unbounded = o.lower_unbounded();

            if (!o.lower_unbounded())
            {
                result.lower = o.lower();
                result.lowerSpecified = true;
                result.lower_included = o.lower_included();
                result.lower_includedSpecified = true;
            }

            result.upper_unbounded = o.upper_unbounded();

            if (!o.upper_unbounded())
            {
                result.upper = o.upper();
                result.upperSpecified = true;
                result.upper_included = o.upper_included();
                result.upper_includedSpecified = true;
            }

            return result;
        }

        protected virtual IntervalOfInteger CloneIntervalOfInteger(openehr.common_libs.basic.INTERVAL_INTEGER_32 o)
        {
            IntervalOfInteger result = null;
            result = new IntervalOfInteger();

            System.Diagnostics.Debug.Assert(!result.lower_includedSpecified, "lower included specified must be false!");

            result.lower_unbounded = o.lower_unbounded();

            if (!o.lower_unbounded())
            {
                result.lower = o.lower();
                result.lowerSpecified = true;
                result.lower_included = o.lower_included();
                result.lower_includedSpecified = true;
            }

            result.upper_unbounded = o.upper_unbounded();

            if (!o.upper_unbounded())
            {
                result.upper = o.upper();
                result.upperSpecified = true;
                result.upper_included = o.upper_included();
                result.upper_includedSpecified = true;
            }

            return result;
        }

        protected virtual C_PRIMITIVE CloneC_Primitive(openehr.openehr.am.archetype.constraint_model.primitive.C_PRIMITIVE o)
        {
            string typeName = o.GetType().Name;
            typeName = typeName.ToUpper();

            switch (typeName)
            {
                case "C_REAL":
                    return CloneReal(o as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_REAL);

                case "C_INTEGER":
                    return CloneInteger(o as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_INTEGER);

                case "C_STRING":
                    return CloneString(o as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_STRING);

                case "C_BOOLEAN":
                    return CloneBoolean(o as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_BOOLEAN);

                case "C_DURATION":
                    return CloneDuration(o as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DURATION);

                case "C_DATE_TIME":
                    return CloneDateTime(o as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DATE_TIME);

                case "C_DATE":
                    return CloneDate(o as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DATE);

                case "C_TIME":
                    return CloneTime(o as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_TIME);

                default:
                    throw new NotImplementedException("The Visitor method 'Visit' with parameter type '" + o.GetType().ToString() + "' is not implemented.");
            }
        }

        protected virtual C_QUANTITY_ITEM CloneQuantityItem(openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY_ITEM o)
        {
            C_QUANTITY_ITEM result = new C_QUANTITY_ITEM();

            result.units = o.units().to_cil();

            if (o.magnitude() != null)
                result.magnitude = CloneIntervalOfReal(o.magnitude());

            if (!o.any_precision_allowed())
                result.precision = CloneIntervalOfInteger(o.precision());

            return result;
        }

        protected virtual ASSERTION[] CloneAssertion(EiffelStructures.list.ARRAYED_LIST_REFERENCE adlList)
        {
            ASSERTION[] result = null;

            if (adlList != null)
            {
                result = new ASSERTION[adlList.count()];

                for (int i = 1; i <= adlList.count(); i++)
                {
                    openehr.openehr.am.archetype.assertion.ASSERTION assert = adlList.i_th(i) as openehr.openehr.am.archetype.assertion.ASSERTION;
                    ASSERTION localAssertion = new ASSERTION();

                    if (assert.tag() != null)
                        localAssertion.tag = assert.tag().ToString();

                    // 0..1 string_expression string
                    //localAssertion.string_expression  (not implemented in adl object)

                    if (assert.expression() != null)
                        localAssertion.expression = CloneExprItem(assert.expression());

                    // 0..* variables ASSERTION_VARIABLE (not implememented in adl object)                            

                    result[i - 1] = localAssertion;
                }
            }

            return result;
        }

        protected virtual EXPR_ITEM CloneExprItem(openehr.openehr.am.archetype.assertion.EXPR_ITEM o)
        {
            if (o is openehr.openehr.am.archetype.assertion.EXPR_LEAF)
                return CloneExprItem(o as openehr.openehr.am.archetype.assertion.EXPR_LEAF);

            else if (o is openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR)
                return CloneExprItem(o as openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR);

            else if (o is openehr.openehr.am.archetype.assertion.EXPR_UNARY_OPERATOR)
                return CloneExprItem(o as openehr.openehr.am.archetype.assertion.EXPR_UNARY_OPERATOR);

            else
                return null;
        }

        protected virtual EXPR_ITEM CloneExprItem(openehr.openehr.am.archetype.assertion.EXPR_LEAF o)
        {
            EXPR_LEAF result = new EXPR_LEAF();

            result.type = o.type().ToString();

            if (result.type.StartsWith("OE_"))
                result.type = result.type.Substring(3);

            if (o.reference_type() != null)
                result.reference_type = o.reference_type().ToString();

            switch (result.type)
            {
                case "C_STRING":
                    result.item = CloneString(o.item() as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_STRING);
                    break;

                case "String":
                    result.item = o.item().ToString();
                    break;

                default:
                    throw new NotSupportedException(result.type);
            }

            return result;
        }

        protected virtual EXPR_ITEM CloneExprItem(openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR o)
        {
            EXPR_BINARY_OPERATOR result = new EXPR_BINARY_OPERATOR();

            if (o.type() != null)
                result.type = o.type().ToString();

            result.precedence_overridden = o.precedence_overridden();

            if (o.@operator().value() >= 2001 && o.@operator().value() <= 2020)
                result.@operator = ((OPERATOR_KIND)o.@operator().value()) - 2001;

            if (o.left_operand() != null)
                result.left_operand = CloneExprItem(o.left_operand());

            if (o.right_operand() != null)
                result.right_operand = CloneExprItem(o.right_operand());

            return result;
        }

        protected virtual EXPR_ITEM CloneExprItem(openehr.openehr.am.archetype.assertion.EXPR_UNARY_OPERATOR o)
        {
            EXPR_UNARY_OPERATOR result = new EXPR_UNARY_OPERATOR();

            if (o.type() != null)
                result.type = o.type().ToString();

            if (o.@operator().value() >= 2001 && o.@operator().value() <= 2020)
                result.@operator = ((OPERATOR_KIND)o.@operator().value()) - 2001;

            result.precedence_overridden = o.precedence_overridden();

            if (o.operand() != null)
                result.operand = CloneExprItem(o.operand());

            return result;
        }

        protected virtual CODE_PHRASE CloneCodePhrase(openehr.openehr.rm.data_types.text.CODE_PHRASE o)
        {
            CODE_PHRASE result = null;

            if (o != null)
            {
                result = new CODE_PHRASE();

                if (o.code_string() != null)
                    result.code_string = o.code_string().ToString();
                else
                    result.code_string = "";

                if (o.terminology_id() != null)
                {
                    TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                    terminologyId.value = o.terminology_id().value().ToString();
                    result.terminology_id = terminologyId;
                }
            }

            return result;
        }

        protected virtual ARCHETYPE CloneArchetypeDetails(openehr.openehr.am.archetype.ARCHETYPE archetype)
        {
            ARCHETYPE result = new ARCHETYPE();

            // 0..1 uid HIER_OBJECT_ID (not implemented in adl object)            

            if (archetype.archetype_id() != null)
            {
                ARCHETYPE_ID archetypeId = new ARCHETYPE_ID();
                archetypeId.value = archetype.archetype_id().value().ToString();
                result.archetype_id = archetypeId;
            }

            if (archetype.version() != null)
                result.adl_version = archetype.adl_version().ToString();

            if (archetype.concept() != null)
                result.concept = archetype.concept().ToString();

            result.original_language = CloneCodePhrase(archetype.original_language());
            result.is_controlled = archetype.is_controlled();

            if (archetype.parent_archetype_id() != null)
            {
                ARCHETYPE_ID parentId = new ARCHETYPE_ID();
                parentId.value = archetype.parent_archetype_id().value().ToString();
                result.parent_archetype_id = parentId;
            }

            result.description = CloneDescription(archetype.description());
            result.ontology = CloneOntology(archetype.ontology());

            // 0..1 revision_history REVISION_HISTORY (does not occur in NHS archetypes, do later)
            //if (archetype.revision_history() != null)
            // result.revision_history = CloneAuthoredResource(archetype.revision_history());

            if (archetype.translations() != null && archetype.translations().count() >= 0)
            {
                TRANSLATION_DETAILS[] translations = new TRANSLATION_DETAILS[archetype.translations().count()];
                archetype.translations().start();

                for (int i = 1; i <= archetype.translations().count(); i++)
                {
                    TRANSLATION_DETAILS translation = new TRANSLATION_DETAILS();
                    openehr.openehr.rm.common.resource.TRANSLATION_DETAILS td = archetype.translations().item_for_iteration() as openehr.openehr.rm.common.resource.TRANSLATION_DETAILS;

                    if (td.accreditation() != null)
                        translation.accreditation = td.accreditation().ToString();

                    translation.author = CloneHashTableAny(td.author());
                    translation.language = CloneCodePhrase(td.language());
                    translation.other_details = CloneHashTableAny(td.other_details());
                    translations[i - 1] = translation;

                    archetype.translations().forth();
                }

                result.translations = translations;
            }

            result.invariants = CloneAssertion(archetype.invariants());

            return result;
        }

        protected virtual ARCHETYPE_ONTOLOGY CloneOntology(openehr.openehr.am.archetype.ontology.ARCHETYPE_ONTOLOGY o)
        {
            ARCHETYPE_ONTOLOGY result = new ARCHETYPE_ONTOLOGY();

            result.term_definitions = CloneCodeDefinitions(o.term_definitions());
            result.term_bindings = CloneTermBindingSet(o.term_bindings());
            result.constraint_definitions = CloneCodeDefinitions(o.constraint_definitions());
            result.constraint_bindings = CloneConstraintBindingSet(o.constraint_bindings());

            return result;
        }

        protected virtual TermBindingSet[] CloneTermBindingSet(EiffelStructures.table.HASH_TABLE_REFERENCE_REFERENCE o)
        {
            TermBindingSet[] result = null;

            if (o != null && o.count() > 0)
            {
                result = new TermBindingSet[o.count()];
                o.start();

                for (int i = 1; i <= o.count(); i++)
                {
                    TermBindingSet termBindingSet = new TermBindingSet();
                    termBindingSet.terminology = o.key_for_iteration().ToString();

                    EiffelStructures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE adlTerms = o.item_for_iteration() as EiffelStructures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE;
                    SortedList<string, TERM_BINDING_ITEM> localTerms = new SortedList<string, TERM_BINDING_ITEM>();
                    adlTerms.start();

                    for (int j = 1; j <= adlTerms.count(); j++)
                    {
                        openehr.openehr.rm.data_types.text.CODE_PHRASE term = adlTerms.item_for_iteration() as openehr.openehr.rm.data_types.text.CODE_PHRASE;

                        if (term != null)
                        {
                            TERM_BINDING_ITEM localTerm = new TERM_BINDING_ITEM();
                            localTerm.code = adlTerms.key_for_iteration().ToString();
                            CODE_PHRASE codePhrase = new CODE_PHRASE();
                            codePhrase.code_string = term.code_string().ToString();

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
                    result[i - 1] = termBindingSet;
                    o.forth();
                }
            }

            return result;
        }

        protected virtual CodeDefinitionSet[] CloneCodeDefinitions(EiffelStructures.table.HASH_TABLE_REFERENCE_REFERENCE o)
        {
            CodeDefinitionSet[] result = null;

            if (o != null)
            {
                result = new CodeDefinitionSet[o.count()];
                o.start();

                for (int i = 1; i <= result.Length; i++)
                {
                    CodeDefinitionSet codeDefinitionSet = new CodeDefinitionSet();
                    codeDefinitionSet.language = o.key_for_iteration().ToString();
                    EiffelStructures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE adlTerms = o.item_for_iteration() as EiffelStructures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE;
                    SortedList<string, ARCHETYPE_TERM> localTerms = new SortedList<string, ARCHETYPE_TERM>();
                    adlTerms.start();

                    for (int j = 1; j <= adlTerms.count(); j++)
                    {
                        openehr.openehr.am.archetype.ontology.Impl.ARCHETYPE_TERM term = adlTerms.item_for_iteration() as openehr.openehr.am.archetype.ontology.Impl.ARCHETYPE_TERM;
                        ARCHETYPE_TERM localTerm = new ARCHETYPE_TERM();
                        localTerm.code = term.code().ToString();
                        localTerm.items = CloneHashTableAny(term.items());
                        localTerms.Add(localTerm.code, localTerm);
                        adlTerms.forth();
                    }

                    codeDefinitionSet.items = new ARCHETYPE_TERM[localTerms.Count];
                    localTerms.Values.CopyTo(codeDefinitionSet.items, 0);
                    CleanUpCodeDefinitionSet(codeDefinitionSet);
                    result[i - 1] = codeDefinitionSet;
                    o.forth();
                }
            }

            return result;
        }

        protected virtual void CleanUpCodeDefinitionSet(CodeDefinitionSet codeDefinitionSet)
        {
        }

        protected virtual ConstraintBindingSet[] CloneConstraintBindingSet(EiffelStructures.table.HASH_TABLE_REFERENCE_REFERENCE o)
        {
            ConstraintBindingSet[] result = null;

            if (o != null && o.count() > 0)
            {
                result = new ConstraintBindingSet[o.count()];
                o.start();

                for (int i = 1; i <= result.Length; i++)
                {
                    ConstraintBindingSet constraintBindingSet = new ConstraintBindingSet();
                    constraintBindingSet.terminology = o.key_for_iteration().ToString();
                    EiffelStructures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE adlTerms = o.item_for_iteration() as EiffelStructures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE;
                    CONSTRAINT_BINDING_ITEM[] localTerms = new CONSTRAINT_BINDING_ITEM[adlTerms.count()];
                    adlTerms.start();

                    for (int j = 1; j <= adlTerms.count(); j++)
                    {
                        openehr.common_libs.basic.Impl.URI term = adlTerms.item_for_iteration() as openehr.common_libs.basic.Impl.URI;
                        CONSTRAINT_BINDING_ITEM localTerm = new CONSTRAINT_BINDING_ITEM();
                        localTerm.code = adlTerms.key_for_iteration().ToString();
                        localTerm.value = adlTerms.item_for_iteration().ToString();
                        localTerms[j - 1] = localTerm;
                        adlTerms.forth();
                    }

                    constraintBindingSet.items = localTerms;
                    result[i - 1] = constraintBindingSet;
                    o.forth();
                }
            }

            return result;
        }

        protected virtual RESOURCE_DESCRIPTION CloneDescription(openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION o)
        {
            RESOURCE_DESCRIPTION result = new RESOURCE_DESCRIPTION();

            result.original_author = CloneHashTableAny(o.original_author());

            if (o.other_contributors() != null && o.other_contributors().count() > 0)
            {
                result.other_contributors = new string[o.other_contributors().count()];

                for (int i = 1; i < result.other_contributors.Length; i++)
                    result.other_contributors[i - 1] = ((EiffelKernel.STRING_8)o.other_contributors().i_th(i)).to_cil();
            }

            result.lifecycle_state = o.lifecycle_state().to_cil();

            if (o.resource_package_uri() != null)
                result.resource_package_uri = o.resource_package_uri().ToString();

            result.other_details = CloneHashTableAny(o.other_details());

            // 0..1 parent_resource AUTHORED_RESOURCE (does not occur in NHS archetypes, do later)
            //if (o.parent_resource() != null)
            //result.parent_resource = CloneAuthoredResource(o.parent_resource());            

            if (o.details().count() > 0)
            {
                RESOURCE_DESCRIPTION_ITEM[] details = new RESOURCE_DESCRIPTION_ITEM[o.details().count()];
                o.details().start();

                for (int i = 1; i <= o.details().count(); i++)
                {
                    openehr.openehr.rm.common.resource.Impl.RESOURCE_DESCRIPTION_ITEM item = o.details().item_for_iteration() as openehr.openehr.rm.common.resource.Impl.RESOURCE_DESCRIPTION_ITEM;
                    details[i - 1] = new RESOURCE_DESCRIPTION_ITEM();
                    details[i - 1].language = CloneCodePhrase(item.language());

                    if (item.purpose() != null)
                        details[i - 1].purpose = item.purpose().ToString();

                    if (item.use() != null)
                        details[i - 1].use = item.use().ToString();

                    if (item.misuse() != null)
                        details[i - 1].misuse = item.misuse().ToString();

                    if (item.copyright() != null)
                        details[i - 1].copyright = item.copyright().ToString();

                    if (item.original_resource_uri() != null && item.original_resource_uri().count() > 0)
                        result.resource_package_uri = item.original_resource_uri().ToString();

                    // 0..* other_details StringDictionaryItem
                    //cloneObject.other_details = CloneHashTableAny(item.other_details());

                    if (item.keywords() != null && item.keywords().count() > 0)
                    {
                        string[] keywords = new string[item.keywords().count()];
                        details[i - 1].keywords = keywords;

                        for (int j = 1; j <= keywords.Length; j++)
                        {
                            if (item.keywords().i_th(j) != null)
                                keywords[j - 1] = item.keywords().i_th(j).ToString();
                        }
                    }

                    o.details().forth();
                }

                result.details = details;
            }

            return result;
        }

        protected virtual StringDictionaryItem[] CloneHashTableAny(EiffelStructures.table.HASH_TABLE_REFERENCE_REFERENCE o)
        {
            StringDictionaryItem[] result = null;

            if (o != null && o.count() > 0)
            {
                SortedList<string, StringDictionaryItem> dictionaryItem = new SortedList<string, StringDictionaryItem>();
                o.start();

                while (!o.off())
                {
                    StringDictionaryItem item = new StringDictionaryItem();
                    item.id = o.key_for_iteration().ToString();
                    item.Value = o.item_for_iteration().ToString();
                    dictionaryItem.Add(item.id, item);
                    o.forth();
                }

                result = new StringDictionaryItem[dictionaryItem.Count];
                dictionaryItem.Values.CopyTo(result, 0);
            }

            return result;
        }

        protected virtual CARDINALITY CloneCardinality(openehr.openehr.am.archetype.constraint_model.CARDINALITY o)
        {
            CARDINALITY result = new CARDINALITY();

            result.is_ordered = o.is_ordered();
            result.is_unique = o.is_unique();
            result.interval = CloneIntervalOfInteger(o.interval());

            return result;
        }

        protected virtual C_ATTRIBUTE CloneAttribute(openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE o)
        {
            C_ATTRIBUTE result;

            if (o.cardinality() == null)
                result = new C_SINGLE_ATTRIBUTE();
            else
            {
                C_MULTIPLE_ATTRIBUTE cloneMultiple = new C_MULTIPLE_ATTRIBUTE();
                cloneMultiple.cardinality = CloneCardinality(o.cardinality());
                result = cloneMultiple;
            }

            result.rm_attribute_name = o.rm_attribute_name().to_cil();

            if (o.existence() != null)
                result.existence = CloneIntervalOfInteger(o.existence());

            // 0..* children C_OBJECT (set in CloneTree)

            return result;
        }
    }
}
