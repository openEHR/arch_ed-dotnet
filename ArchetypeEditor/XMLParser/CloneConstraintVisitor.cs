using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

#if XMLParser
using XMLParser;
#else
using OpenEhr.V1.Its.Xml.AM;
#endif

namespace XMLParser.OpenEhr.V1.Its.Xml.AM
{
    class CloneConstraintVisitor
    {
        //private static CloneConstraintVisitor nodeVisitor = new CloneConstraintVisitor();

        private System.Reflection.MethodInfo lastMethod = null;
        private openehr.openehr.am.archetype.constraint_model.C_OBJECT lastObject = null;
                  
        /// <summary>
        /// Uses the Eiffel engine to parse an archetype to create an empty instance using local classes
        /// Class to clone the archetype
        /// </summary>
        public ARCHETYPE CloneArchetype(openehr.openehr.am.archetype.ARCHETYPE adlArchetype)
        {
            // clone archetype
            ARCHETYPE cloneObject = CloneArchetypeDetails(adlArchetype);
            
            // clone definition (root C_COMPLEX_OBJECT)
            //object rootNode = nodeVisitor.Visit(adlArchetype.definition(), 0);
            object rootNode = Visit(adlArchetype.definition(), 0);

            C_COMPLEX_OBJECT rootComplexObject = rootNode as C_COMPLEX_OBJECT;
                        
            // link defintion to archetype
            cloneObject.definition = rootComplexObject;
            
            // clone recursive definition tree
            CloneTree(adlArchetype.definition(), rootComplexObject, 0);

            // return cloned archetype
            return cloneObject;
        }

        //static private void CloneTree(openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT adlComplexObject, C_COMPLEX_OBJECT parentComplexObject, int depth)
        private void CloneTree(openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT adlComplexObject, C_COMPLEX_OBJECT parentComplexObject, int depth)
        {
            CloneConstraintVisitor nodeVisitor = new CloneConstraintVisitor();

            openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE adlAttribute;
            parentComplexObject.attributes = new C_ATTRIBUTE[adlComplexObject.attributes().count()];

            for (int i = 1; i <= adlComplexObject.attributes().count(); i++)
            {
                // clone attribute
                adlAttribute = adlComplexObject.attributes().i_th(i) as openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE;
                C_ATTRIBUTE attributeNode = nodeVisitor.CloneAttribute(adlAttribute);
                attributeNode.children = new C_OBJECT[adlAttribute.children().count()];                                      

                //Console.WriteLine("C_ATTRIBUTE\t\t" + string.Join("\t", new string[depth + 1]) + adlAttribute.rm_attribute_name().to_cil());

                // link attribute to complex object
                parentComplexObject.attributes[i - 1] = attributeNode;

                for (int j = 1; j <= adlAttribute.children().count(); j++)
                {
                    openehr.openehr.am.archetype.constraint_model.C_OBJECT child;
                    child = adlAttribute.children().i_th(j) as openehr.openehr.am.archetype.constraint_model.C_OBJECT;

                    // create instance of child
                    object childNode = nodeVisitor.Visit(child, depth);

                    // link child to attribute
                    attributeNode.children[j - 1] = childNode as C_OBJECT;

                    if (child is openehr.openehr.am.archetype.constraint_model.Impl.C_COMPLEX_OBJECT)
                        CloneTree(child as openehr.openehr.am.archetype.constraint_model.C_COMPLEX_OBJECT, childNode as C_COMPLEX_OBJECT, ++depth);
                }
            }
        }
                               
        private object Visit(openehr.openehr.am.archetype.constraint_model.C_OBJECT currentObject, int depth)
        {            
            if (currentObject == null)
                throw new ArgumentNullException("currentObject parameter must not be null.  depth = " + depth);
            
            try
            {                                
                System.Reflection.MethodInfo method = this.GetType().GetMethod("Visit",
                               System.Reflection.BindingFlags.ExactBinding | System.Reflection.BindingFlags.NonPublic
                               | System.Reflection.BindingFlags.Instance, Type.DefaultBinder,
                               new Type[] { currentObject.GetType(), depth.GetType() }, new System.Reflection.ParameterModifier[0]);

                if (method != null)
                    // Avoid StackOverflow exceptions by executing only if the method and visitable  
                    // are different from the last parameters used.
                    if (method != lastMethod || currentObject != lastObject)
                    {
                        lastMethod = method;
                        lastObject = currentObject;
                        object itemObject = method.Invoke(this, new object[] { currentObject, depth });

                        return itemObject;
                    }
                    else
                    {
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
            throw new NotImplementedException("The Visitor method 'Visit' with parameter type '" + currentObject.GetType().ToString() + "' is not implemented.");
        }     

        private object Visit(openehr.openehr.am.archetype.constraint_model.Impl.C_COMPLEX_OBJECT currentObject, int depth)
        {
            ////Console.WriteLine("C_COMPLEX_OBJECT\t" + string.Join("\t", new string[depth + 1]) + currentObject.rm_type_name().to_cil());
            C_COMPLEX_OBJECT cloneObject = new C_COMPLEX_OBJECT();
        
            // Inherts C_OBJECT
            cloneObject = CloneC_Object(cloneObject, currentObject) as C_COMPLEX_OBJECT;                                    
                                   
            // 0..* Attributes C_ATTRIBUTE (added in CloneTree)
                                    
            return cloneObject;            
        }
        
        private object Visit(openehr.openehr.am.archetype.constraint_model.Impl.C_PRIMITIVE_OBJECT currentObject, int depth)
        {
            //Console.WriteLine("C_PRIMITIVE_OBJECT\t" + string.Join("\t", new string[depth + 1]) + currentObject.rm_type_name().to_cil());

            C_PRIMITIVE_OBJECT cloneObject = new C_PRIMITIVE_OBJECT();

            // Inherts C_OBJECT
            cloneObject = CloneC_Object(cloneObject, currentObject) as C_PRIMITIVE_OBJECT;
            
            // C_Primitive Item 0..1
            if (currentObject.item() != null )
                cloneObject.item = CloneC_Primitive(currentObject.item());                
                        
            return cloneObject;
        }

        
        private C_DATE CloneDate(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DATE currentObject)
        {
            C_DATE cloneObject = new C_DATE();

            // 0..1 assumed_value Iso8601Date
            if (currentObject.has_assumed_value().Equals(true))                                
                cloneObject.assumed_value = currentObject.assumed_value().ToString();

            // 0..1 pattern DateConstraintPattern
            if (currentObject.pattern() != null)
                cloneObject.pattern = currentObject.pattern().ToString();

            // 0..1 timezone_validity VALIDITY_KIND    
            //cloneObject.timezone_validity = VALIDITY_KIND.Item1001 

            // 0..1 range IntervalOfDate            
            cloneObject.range = CloneIntervalOfDate(currentObject.interval());

            return cloneObject;
        }
        
        private C_DATE_TIME CloneDateTime(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DATE_TIME currentObject)
        {
            C_DATE_TIME cloneObject = new C_DATE_TIME();

            // 0..1 assumed_value Iso8601DateTime
            if (currentObject.has_assumed_value().Equals(true))
                cloneObject.assumed_value = currentObject.assumed_value().ToString();

            //0..1 pattern DateTimeConstraintPattern
            if (currentObject.pattern() != null)
                cloneObject.pattern = currentObject.pattern().ToString();

            // 0..1 timezone_validity VALIDITY_KIND

            // 0..1 range IntervalOfDateTime
            cloneObject.range = CloneIntervalOfDateTime(currentObject.interval());

            return cloneObject;
        }


        private C_TIME CloneTime(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_TIME currentObject)
        {
            C_TIME cloneObject = new C_TIME();

            // 0..1 assumed_value Iso8601DateTime
            if (currentObject.has_assumed_value().Equals(true))
                cloneObject.assumed_value = currentObject.assumed_value().ToString();

            //0..1 pattern TimeConstraintPattern
            if (currentObject.pattern() != null)
                cloneObject.pattern = currentObject.pattern().ToString();

            // 0..1 timezone_validity VALIDITY_KIND

            // 0..1 range IntervalOfTime
            cloneObject.range = CloneIntervalOfTime(currentObject.interval());

            return cloneObject;
        }


        private C_DURATION CloneDuration(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DURATION currentObject)
        {
            C_DURATION cloneObject = new C_DURATION();

            // 0..1 assumed_value Iso8601Duration
            if (currentObject.has_assumed_value().Equals(true))
                cloneObject.assumed_value = currentObject.assumed_value().ToString(); 
                        
            // 0..1 pattern DurationConstraintPattern
            if (currentObject.pattern() != null)
                cloneObject.pattern = currentObject.pattern().ToString();

            // 0..1 range IntervalOfDuration
            cloneObject.range = cloneDurationRange(currentObject.interval()); 
            
            return cloneObject;
        }

        
        private IntervalOfDuration cloneDurationRange(openehr.common_libs.basic.INTERVAL_REFERENCE currentObject) //Type
        {            
            if (currentObject != null)
            {                                	
                IntervalOfDuration cloneObject = new IntervalOfDuration();
                
                cloneObject.lower_unbounded = currentObject.lower_unbounded();
                if (!currentObject.lower_unbounded())
                {                    
                    cloneObject.lower = currentObject.lower().ToString();  //cloneObject.lower = currentObject.lower();  //local lower is an object!!!     
                    cloneObject.lower_included = currentObject.lower_included();
                    cloneObject.lower_includedSpecified = true;
                }

                cloneObject.upper_unbounded = currentObject.upper_unbounded();
                if (!currentObject.upper_unbounded())
                {
                    cloneObject.upper = currentObject.upper().ToString();  //local upper is an object!!!
                    cloneObject.upper_included = currentObject.upper_included();
                    cloneObject.upper_includedSpecified = true;
                }
                
                return cloneObject;
            }
            else
                return null;            
        }

        private IntervalOfDate CloneIntervalOfDate(openehr.common_libs.basic.INTERVAL_REFERENCE currentObject) 
        {
            if (currentObject != null)
            {
                IntervalOfDate cloneObject = new IntervalOfDate();
                
                cloneObject.lower_unbounded = currentObject.lower_unbounded();
                if (!currentObject.lower_unbounded())
                {
                    cloneObject.lower = currentObject.lower().ToString();  
                    cloneObject.lower_included = currentObject.lower_included();
                    cloneObject.lower_includedSpecified = true;                     
                }

                cloneObject.upper_unbounded = currentObject.upper_unbounded();
                if (!currentObject.upper_unbounded())
                {
                    cloneObject.upper = currentObject.upper().ToString();  
                    cloneObject.upper_included = currentObject.upper_included();
                    cloneObject.upper_includedSpecified = true;                    
                }

                return cloneObject;
            }
            else
                return null;
        }

        private IntervalOfDateTime CloneIntervalOfDateTime(openehr.common_libs.basic.INTERVAL_REFERENCE currentObject)
        {
            if (currentObject != null)
            {
                IntervalOfDateTime cloneObject = new IntervalOfDateTime();

                cloneObject.lower_unbounded = currentObject.lower_unbounded();
                if (!currentObject.lower_unbounded())
                {
                    cloneObject.lower = currentObject.lower().ToString();
                    cloneObject.lower_included = currentObject.lower_included();
                    cloneObject.lower_includedSpecified = true;
                }

                cloneObject.upper_unbounded = currentObject.upper_unbounded();
                if (!currentObject.upper_unbounded())
                {
                    cloneObject.upper = currentObject.upper().ToString();
                    cloneObject.upper_included = currentObject.upper_included();
                    cloneObject.upper_includedSpecified = true;                            
                }

                return cloneObject;
            }
            else
                return null;
        }

        private IntervalOfTime CloneIntervalOfTime(openehr.common_libs.basic.INTERVAL_REFERENCE currentObject)
        {
            if (currentObject != null)
            {
                IntervalOfTime cloneObject = new IntervalOfTime();

                cloneObject.lower_unbounded = currentObject.lower_unbounded();
                if (!currentObject.lower_unbounded())
                {
                    cloneObject.lower = currentObject.lower().ToString();
                    cloneObject.lower_included = currentObject.lower_included();
                    cloneObject.lower_includedSpecified = true;
                }

                cloneObject.upper_unbounded = currentObject.upper_unbounded();
                if (!currentObject.upper_unbounded())
                {
                    cloneObject.upper = currentObject.upper().ToString();
                    cloneObject.upper_included = currentObject.upper_included();
                    cloneObject.upper_includedSpecified = true;
                }

                return cloneObject;
            }
            else
                return null;
        }

        private C_BOOLEAN CloneBoolean(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_BOOLEAN currentObject)
        {
            C_BOOLEAN cloneObject = new C_BOOLEAN();

            // 1 true_valid
            cloneObject.true_valid = currentObject.true_valid();

            // 1 false_valid
            cloneObject.false_valid = currentObject.false_valid();
            
            // 0..1 assumed_value boolean
            if (currentObject.has_assumed_value().Equals(true))
            {
                cloneObject.assumed_valueSpecified = true;
                cloneObject.assumed_value = (currentObject.assumed_value() as EiffelSoftware.Library.Base.kernel.BOOLEAN_REF).item();
            }

            return cloneObject;
        }
        

        private C_STRING CloneString(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_STRING currentObject)
        {
            C_STRING cloneObject = new C_STRING();
            
            //0..1 pattern string
            //HKF: EDT-415
            //cloneObject.pattern = currentObject.ToString();//pattern().to_cil(); //jar check
            if (currentObject.regexp() != null)
                cloneObject.pattern = currentObject.regexp().to_cil();

            // 0..1 assumed_value string
            if (currentObject.has_assumed_value().Equals(true))
                cloneObject.assumed_value = currentObject.assumed_value().ToString();

            // 0..* string list
            if (currentObject.strings() != null && currentObject.strings().count() > 0)
            {
                int numStrings = currentObject.strings().count();
                cloneObject.list = new string[numStrings];
                for (int i = 1; i <= numStrings; i++)
                    cloneObject.list[i - 1] = currentObject.strings().i_th(i).ToString();
            }

            // 0..1 list_open
            if (currentObject.is_open())
            {
                cloneObject.list_open = true;
                cloneObject.list_openSpecified = true;
            }

            return cloneObject;
        }


        //See AE XML_Archetype BuildReal
        private C_INTEGER CloneInteger(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_INTEGER currentObject)
        {
            C_INTEGER cloneObject = new C_INTEGER ();

            //0..1 range Interval
            if (currentObject.interval() != null)
                cloneObject.range = CloneIntervalOfInteger(currentObject.interval());            

            // 0..1 assumed_value int
            if (currentObject.has_assumed_value().Equals(true))             
            {
                cloneObject.assumed_valueSpecified = true;
                cloneObject.assumed_value = (currentObject.assumed_value() as EiffelSoftware.Library.Base.kernel.INTEGER_32_REF).item();                
                //cloneObject.assumed_value = Int32.Parse(currentObject.assumed_value().ToString());
            }
                        
            // 0..1 list int 
            if (currentObject.list() != null)
                if (currentObject.list().count() > 0)
                {
                    int[] localList = new int[currentObject.list().count()];
                    cloneObject.list = localList; 
                    for (int i = 1; i <= currentObject.list().count(); i++)
                        localList[i - 1] = currentObject.list().i_th(i);
                }

            return cloneObject;
        }

        //See AE XML_Archetype BuildReal
        private C_REAL CloneReal(openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_REAL currentObject)
        {            
            C_REAL cloneObject = new C_REAL();
                        
            //0..1 range IntervalOfReal
            if (currentObject.interval() != null)
                cloneObject.range = CloneIntervalOfReal (currentObject.interval());
            
            // 0..1 assumed_value float
            if (currentObject.has_assumed_value().Equals(true))
            {                
                cloneObject.assumed_valueSpecified = true;
                cloneObject.assumed_value = (currentObject.assumed_value() as EiffelSoftware.Library.Base.kernel.dotnet.Impl.REAL_32_REF).item(); //as REAL_REF; //float to object;
            }

            // 0..1 list float 
            if (currentObject.list() != null)
                if (currentObject.list().count() > 0)
                {
                    float[] localList = new float[currentObject.list().count()];
                    cloneObject.list = localList;
                    for (int i = 1; i <= currentObject.list().count(); i++)
                        localList[i - 1] = (float) currentObject.list().i_th(i);
                }

            return cloneObject;
        }
        

        private object Visit(openehr.openehr.am.archetype.constraint_model.Impl.CONSTRAINT_REF currentObject, int depth)
        {
            //Console.WriteLine("CONSTRAINT_REF\t" + string.Join("\t", new string[depth + 1])+ currentObject.rm_type_name().to_cil());

            CONSTRAINT_REF cloneObject = new CONSTRAINT_REF();

            // Inherts C_OBJECT
            cloneObject = CloneC_Object(cloneObject, currentObject) as CONSTRAINT_REF;

            // 1 reference string
            cloneObject.reference = currentObject.target().ToString();
            
            return cloneObject;
        }

        private object Visit(openehr.openehr.am.archetype.constraint_model.Impl.ARCHETYPE_INTERNAL_REF currentObject, int depth)
        {
            //Console.WriteLine("ARCHETYPE_INTERNAL_REF\t" + string.Join("\t", new string[depth + 1]) + currentObject.rm_type_name().to_cil());

            ARCHETYPE_INTERNAL_REF cloneObject = new ARCHETYPE_INTERNAL_REF();

            // Inherts C_OBJECT
            cloneObject = CloneC_Object(cloneObject, currentObject) as ARCHETYPE_INTERNAL_REF;

            // 1 target_path string
            cloneObject.target_path = currentObject.target_path().to_cil();

            return cloneObject;
        }

        private object Visit(openehr.openehr.am.archetype.constraint_model.Impl.ARCHETYPE_SLOT currentObject, int depth)
        {
            //Console.WriteLine("ARCHETYPE_SLOT\t" + string.Join("\t", new string[depth + 1]) + currentObject.rm_type_name().to_cil());

            ARCHETYPE_SLOT cloneObject = new ARCHETYPE_SLOT();

            // Inherts C_OBJECT
            cloneObject = CloneC_Object(cloneObject, currentObject) as ARCHETYPE_SLOT;

            // 0..* includes ASSERTION
            if (currentObject.has_includes())
                cloneObject.includes = CloneAssertion(currentObject.includes());

            // 0..* excludes ASSERTION
            if (currentObject.has_excludes())
                cloneObject.excludes = CloneAssertion(currentObject.excludes());
            
           return cloneObject;
        }

        private object Visit(openehr.openehr.am.openehr_profile.data_types.quantity.Impl.C_DV_QUANTITY currentObject, int depth)
        {
            //Console.WriteLine("C_DV_QUANTITY\t" + string.Join("\t", new string[depth + 1]) + currentObject.rm_type_name().to_cil());

            C_DV_QUANTITY cloneObject = new C_DV_QUANTITY();

            // Inherts C_OBJECT
            cloneObject = CloneC_Object(cloneObject, currentObject) as C_DV_QUANTITY;

            // 0..1 assumed_value DV_QUANTITY
            if (currentObject.assumed_value() != null)                
                cloneObject.assumed_value 
                    = CloneDvQuantity((openehr.openehr.am.openehr_profile.data_types.quantity.Impl.QUANTITY)currentObject.assumed_value());

            if (currentObject.property() != null)
            {
                // 0..1 property CODE_PHASE    
                cloneObject.property = CloneCodePhrase(currentObject.property());

                // 0..* List C_QUANTITY_ITEM
                if (currentObject.list() != null && currentObject.list().count() > 0)
                {
                    C_QUANTITY_ITEM[] localQuantityList = new C_QUANTITY_ITEM[currentObject.list().count()];
                    cloneObject.list = localQuantityList;

                    for (int i = 1; i <= currentObject.list().count(); i++)
                        localQuantityList[i - 1] = CloneQuantityItem(currentObject.list().i_th(i) as openehr.openehr.am.openehr_profile.data_types.quantity.Impl.C_QUANTITY_ITEM);
                }
            }       
            return cloneObject;
        }

        private  DV_ORDINAL CloneDvOrdinal(openehr.openehr.am.openehr_profile.data_types.quantity.Impl.ORDINAL currentObject)
        {
            // Ordinal constraint
            DV_ORDINAL cloneObject = new DV_ORDINAL();

            // Inherits DV_ORDERED (only in Reference Model)
            // 0..1 normal_range DV_INTERVAL
            // 0..* other_reference_ranges REFERENCE_RANGE                        
            // 0..1 normal_status CODE_PHRASE            

            // 1 value int
            cloneObject.value = currentObject.value();

            // 1 symbol DV_CODED_TEXT            
            cloneObject.symbol = new DV_CODED_TEXT();
            cloneObject.symbol.defining_code = CloneCodePhrase(currentObject.symbol());
            cloneObject.symbol.value = "";  // what should this be?
            
            return cloneObject;
        }

        private DV_QUANTITY CloneDvQuantity(openehr.openehr.am.openehr_profile.data_types.quantity.Impl.QUANTITY currentObject)
        {
            DV_QUANTITY cloneObject = new DV_QUANTITY();

            // Inherits DV_AMOUNT (only in Reference Model)
            // 0..1 normal_range DV_INTERVAL
            // 0..* other_reference_ranges REFERENCE_RANGE
            // 0..1 normal_status CODE_PHRASE
            // 0..1 magnitude_status string
            // 0..1 accuracy float
            // 0..1 accuracy_is_percent boolean
            
            // 1 magnitude double
            cloneObject.magnitude = currentObject.magnitude();

            // 0..1 precision int
            cloneObject.precision = currentObject.precision();            
            
            // 1 units string
            if (currentObject.units() != null)
                cloneObject.units = currentObject.units().ToString();

            return cloneObject;
        }

        private object Visit(openehr.openehr.am.openehr_profile.data_types.quantity.Impl.C_DV_ORDINAL currentObject, int depth)
        {
            //Console.WriteLine("C_DV_ORDINAL\t" + string.Join("\t", new string[depth + 1]) + currentObject.rm_type_name().to_cil());
            
            C_DV_ORDINAL cloneObject = new C_DV_ORDINAL();            

            // Inherts C_OBJECT
            cloneObject = CloneC_Object(cloneObject, currentObject) as C_DV_ORDINAL;

            // 0..1 assumed_value DV_ORDINAL
            if (currentObject.assumed_value() != null)
                cloneObject.assumed_value 
                    = CloneDvOrdinal((openehr.openehr.am.openehr_profile.data_types.quantity.Impl.ORDINAL)currentObject.assumed_value());                        

            // 0..* List DV_ORDINAL
            if (currentObject.any_allowed().Equals (false))
            {                
                EiffelSoftware.Library.Base.structures.list.LINKED_LIST_REFERENCE adlOrdinals = currentObject.items();
                DV_ORDINAL[] localOrdinals = new DV_ORDINAL[adlOrdinals.count()];                        
                
                adlOrdinals.start();
                for (int i = 0; i < adlOrdinals.count(); i++)
                {
                    openehr.openehr.am.openehr_profile.data_types.quantity.Impl.ORDINAL adlOrdinal = (openehr.openehr.am.openehr_profile.data_types.quantity.Impl.ORDINAL)adlOrdinals.active().item();
                    DV_ORDINAL localOrdinal = CloneDvOrdinal(adlOrdinal);                
                    localOrdinals[i] = localOrdinal;                    

                    adlOrdinals.forth();
                }
                           
                cloneObject.list = localOrdinals;
            }            
            return cloneObject;
        }

        private object Visit(openehr.openehr.am.openehr_profile.data_types.text.Impl.C_CODE_PHRASE currentObject, int depth)
        {
            //Console.WriteLine("C_CODE_PHRASE\t" + string.Join("\t", new string[depth + 1]) + currentObject.rm_type_name().to_cil());
            
            C_CODE_PHRASE cloneObject = new C_CODE_PHRASE();

            // Inherts C_OBJECT
            cloneObject = CloneC_Object(cloneObject, currentObject) as C_CODE_PHRASE;
            
            // 0..1 terminology_id TERMINOLOGY_ID
            if (currentObject.terminology_id() != null)
            {
                TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                terminologyId.value = currentObject.terminology_id().value().ToString();
                cloneObject.terminology_id = terminologyId;
            }
        
            // 0..* code_list string
            if (currentObject.code_list() != null)
            {
                EiffelSoftware.Library.Base.structures.list.Impl.ARRAYED_LIST_REFERENCE castList = currentObject.code_list() as EiffelSoftware.Library.Base.structures.list.Impl.ARRAYED_LIST_REFERENCE;   // returns null (not exception)                
                EiffelSoftware.Library.Base.kernel.dotnet.Impl.SPECIAL_REFERENCE sList = (EiffelSoftware.Library.Base.kernel.dotnet.Impl.SPECIAL_REFERENCE)(castList.area());

                string[] copyList = new string[sList.count()];

                for (int i = 0; i < sList.count(); i++)
                    if (sList.item(i) != null)
                        copyList[i] = sList.item(i).ToString();

                cloneObject.code_list = copyList;
            }

            // 0..1 assumed_value CODE_PHRASE
            if (currentObject.has_assumed_value().Equals(true))
                cloneObject.assumed_value 
                    = CloneCodePhrase((openehr.openehr.rm.data_types.text.Impl.CODE_PHRASE)currentObject.assumed_value());
            
            return cloneObject;
        }

        private C_OBJECT CloneC_Object(C_OBJECT cloneObject, openehr.openehr.am.archetype.constraint_model.C_OBJECT currentObject)
        {
            // 1 node_id string            
            if (!currentObject.node_id().to_cil().StartsWith ("unknown"))  //hack for xml conversion 
                cloneObject.node_id = currentObject.node_id().to_cil();                     
            else
                cloneObject.node_id = "";

            // 1 rm_type_name string
            cloneObject.rm_type_name = currentObject.rm_type_name().to_cil();               
            
            // 1 occurences IntervalOfInteger
            cloneObject.occurrences = CloneIntervalOfInteger(currentObject.occurrences());  

            return cloneObject;
        }


        private IntervalOfReal CloneIntervalOfReal(openehr.common_libs.basic.INTERVAL_REAL_32  currentObject)
        {
            IntervalOfReal cloneObject = new IntervalOfReal();
                        
            System.Diagnostics.Debug.Assert(!cloneObject.lower_includedSpecified, "lower included specified must be false!");
            
            cloneObject.lower_unbounded = currentObject.lower_unbounded();
            if (!currentObject.lower_unbounded())
            {
                cloneObject.lower = currentObject.lower();
                cloneObject.lowerSpecified = true;
                cloneObject.lower_included = currentObject.lower_included();
                cloneObject.lower_includedSpecified = true;    
            }

            cloneObject.upper_unbounded = currentObject.upper_unbounded(); 
            if (!currentObject.upper_unbounded())
            {
                cloneObject.upper = currentObject.upper();
                cloneObject.upperSpecified = true;
                cloneObject.upper_included = currentObject.upper_included();
                cloneObject.upper_includedSpecified = true;                
            }
            
            return cloneObject;
        }

        
        private IntervalOfInteger CloneIntervalOfInteger(openehr.common_libs.basic.INTERVAL_INTEGER_32 currentObject)
        {
            IntervalOfInteger cloneObject = null;
            cloneObject = new IntervalOfInteger();

            System.Diagnostics.Debug.Assert(!cloneObject.lower_includedSpecified, "lower included specified must be false!");

            cloneObject.lower_unbounded = currentObject.lower_unbounded();
            if (!currentObject.lower_unbounded())
            {
                cloneObject.lower = currentObject.lower();
                cloneObject.lowerSpecified = true;
                cloneObject.lower_included = currentObject.lower_included();
                cloneObject.lower_includedSpecified = true;
            }

            cloneObject.upper_unbounded = currentObject.upper_unbounded();
            if (!currentObject.upper_unbounded())
            {
                cloneObject.upper = currentObject.upper();
                cloneObject.upperSpecified = true;
                cloneObject.upper_included = currentObject.upper_included();
                cloneObject.upper_includedSpecified = true;
            }
            
            return cloneObject;
        }

 
        private C_PRIMITIVE CloneC_Primitive(openehr.openehr.am.archetype.constraint_model.primitive.C_PRIMITIVE currentObject)
        {
            string typeName = currentObject.GetType().Name;
            typeName = typeName.ToUpper();

            switch (typeName)
            {
                case "C_REAL":
                    return CloneReal(currentObject as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_REAL);

                case "C_INTEGER":
                    return CloneInteger(currentObject as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_INTEGER);

                case "C_STRING":
                    return CloneString(currentObject as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_STRING);
                   
                case "C_BOOLEAN":
                    return CloneBoolean(currentObject as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_BOOLEAN);                  
            
                case "C_DURATION":
                    return CloneDuration(currentObject as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DURATION);                
                
                case "C_DATE_TIME":
                    return CloneDateTime(currentObject as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DATE_TIME);
                   
                case "C_DATE":
                    return CloneDate(currentObject as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_DATE);
                
                case "C_TIME":
                    return CloneTime(currentObject as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_TIME);
                   
                default:
                    throw new NotImplementedException("The Visitor method 'Visit' with parameter type '" + currentObject.GetType().ToString() + "' is not implemented.");
                   
            }
        }


        private C_QUANTITY_ITEM CloneQuantityItem(openehr.openehr.am.openehr_profile.data_types.quantity.C_QUANTITY_ITEM currentObject)
        {
            C_QUANTITY_ITEM cloneObject = new C_QUANTITY_ITEM();

            // 0..1 units string
            cloneObject.units = currentObject.units().to_cil();

            // 0..1 magnitude IntervalOfReal
            if (currentObject.magnitude() != null)
                cloneObject.magnitude = CloneIntervalOfReal(currentObject.magnitude());
            
            // 0..1 precision IntervalOfInteger
            if (!currentObject.any_precision_allowed())
                cloneObject.precision = CloneIntervalOfInteger(currentObject.precision());

            return cloneObject;
        }


        private ASSERTION[] CloneAssertion(EiffelSoftware.Library.Base.structures.list.ARRAYED_LIST_REFERENCE adlList)
        {
            if (adlList != null)
            {
                ASSERTION[] localList = new ASSERTION[adlList.count()];

                for (int i = 1; i <= adlList.count(); i++)
                {
                    openehr.openehr.am.archetype.assertion.ASSERTION assert = adlList.i_th(i) as openehr.openehr.am.archetype.assertion.ASSERTION;
                    ASSERTION localAssertion = new ASSERTION();

                    // 0..1 tag string
                    if (assert.tag() != null)
                        localAssertion.tag = assert.tag().ToString();

                    // 0..1 string_expression string
                    //localAssertion.string_expression  (not implemented in adl object)
                   
                    // 1 expression EXPR_ITEM 
                    if (assert.expression() != null)
                        localAssertion.expression = CloneExprItem(assert.expression());

                    // 0..* variables ASSERTION_VARIABLE (not implememented in adl object)                            
                    
                    localList[i - 1] = localAssertion;
                }

                return localList;
            }
            return null;
        }

        private EXPR_ITEM CloneExprItem(openehr.openehr.am.archetype.assertion.EXPR_ITEM currentObject)
        {
            if (currentObject is openehr.openehr.am.archetype.assertion.EXPR_LEAF)
                return CloneExprItem(currentObject as openehr.openehr.am.archetype.assertion.EXPR_LEAF);
        
            else if (currentObject is openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR)
                return CloneExprItem(currentObject as openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR);
            
            else if (currentObject is openehr.openehr.am.archetype.assertion.EXPR_UNARY_OPERATOR)
                return CloneExprItem(currentObject as openehr.openehr.am.archetype.assertion.EXPR_UNARY_OPERATOR);
            
            else
                return null;
        }
        
        private EXPR_ITEM CloneExprItem(openehr.openehr.am.archetype.assertion.EXPR_LEAF currentObject)
        {
            EXPR_LEAF cloneObject = new EXPR_LEAF();

            // 1 type string
            if (currentObject.type() == null)
                throw new ApplicationException();

            cloneObject.type = currentObject.type().ToString();
            if (cloneObject.type.StartsWith("OE_"))
                cloneObject.type = cloneObject.type.Substring(3);

            // 1 reference_type
            if (currentObject.reference_type() != null)
                cloneObject.reference_type = currentObject.reference_type().ToString();
                                    
            // 1 item anyType
            switch (cloneObject.type)
            {
                case "C_STRING":
                    // HKF: EDT-415
                    //cloneObject.item = CloneC_Primitive((openehr.openehr.am.archetype.constraint_model.primitive.C_PRIMITIVE) currentObject.item());
                    cloneObject.item = CloneString(currentObject.item() as openehr.openehr.am.archetype.constraint_model.primitive.Impl.C_STRING);
                    break;

                case "String":
                    cloneObject.item = currentObject.item().ToString();
                    break;

                default:
                    throw new NotSupportedException(cloneObject.type);
            }

            return cloneObject as EXPR_ITEM;
        }

        private EXPR_ITEM CloneExprItem(openehr.openehr.am.archetype.assertion.EXPR_BINARY_OPERATOR currentObject)
        {            
            EXPR_BINARY_OPERATOR cloneObject = new EXPR_BINARY_OPERATOR();

            // 1 type string
            if (currentObject.type() != null)
                cloneObject.type = currentObject.type().ToString();

            // 1 precedence_overridden boolean
            cloneObject.precedence_overridden = currentObject.precedence_overridden();

            // 1 operator OPERATOR_KIND
            if (currentObject.@operator().value() >= 2001 && currentObject.@operator().value() <= 2020)
                cloneObject.@operator = ((OPERATOR_KIND)currentObject.@operator().value()) - 2001;  //enum cast            

            // 1 left_operand EXPR_ITEM
            if (currentObject.left_operand() != null)
                cloneObject.left_operand = CloneExprItem(currentObject.left_operand()); //recursion

            // 1 right_operand EXPR_ITEM
            if (currentObject.right_operand() != null)
                cloneObject.right_operand = CloneExprItem(currentObject.right_operand()); //recursion

            return cloneObject as EXPR_ITEM;
        }

        private EXPR_ITEM CloneExprItem(openehr.openehr.am.archetype.assertion.EXPR_UNARY_OPERATOR currentObject)
        {
            EXPR_UNARY_OPERATOR cloneObject = new EXPR_UNARY_OPERATOR();

            // 1 type string
            if (currentObject.type() != null)
                cloneObject.type = currentObject.type().ToString();
            
            // 1 operator OPERATOR_KIND
            if (currentObject.@operator().value() >= 2001 && currentObject.@operator().value() <= 2020)
                cloneObject.@operator = ((OPERATOR_KIND)currentObject.@operator().value()) - 2001;  //enum cast            

            // 1 precedence_overridden boolean
            cloneObject.precedence_overridden = currentObject.precedence_overridden();

            // 1 operand EXPR_ITEM
            if (currentObject.operand() != null)
                cloneObject.operand = CloneExprItem(currentObject.operand()); //recursive
            
            return cloneObject as EXPR_ITEM;
        }

        
        private CODE_PHRASE CloneCodePhrase(openehr.openehr.rm.data_types.text.CODE_PHRASE currentObject)
        {
            if (currentObject != null)
            {
                CODE_PHRASE cloneObject = new CODE_PHRASE();

                // 1 code_string string
                if (currentObject.code_string() != null)
                    cloneObject.code_string = currentObject.code_string().ToString();
                else
                    cloneObject.code_string = "";

                // 1 terminology_id TERMINOLOGY_ID
                if (currentObject.terminology_id() != null)
                {
                    TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                    terminologyId.value = currentObject.terminology_id().value().ToString();
                    cloneObject.terminology_id = terminologyId;
                }
                return cloneObject;
            }            
            return null;
        }
        
        private ARCHETYPE CloneArchetypeDetails(openehr.openehr.am.archetype.ARCHETYPE archetype)
        {
            ARCHETYPE cloneObject = new ARCHETYPE();
            
            // 0..1 uid HIER_OBJECT_ID (not implemented in adl object)            

            // 1 archtype_id ARCHETYPE_ID
            if (archetype.archetype_id() != null)
            {
                ARCHETYPE_ID archetypeId = new ARCHETYPE_ID();
                archetypeId.value = archetype.archetype_id().value().ToString();                
                cloneObject.archetype_id = archetypeId;
            }

            // 0..1 adl_version string
            if (archetype.version() != null)
                //JAR: 21MAY2007, EDT-61 generated XML contains wrong field for ADL version
                //cloneObject.adl_version = archetype.version().ToString();
                cloneObject.adl_version = archetype.adl_version().ToString();
            
            // 1 concept string
            if (archetype.concept() != null)
                cloneObject.concept = archetype.concept().ToString();
            
            // 1 original_language CODE_PHRASE
            cloneObject.original_language = CloneCodePhrase(archetype.original_language());

            // 0..1 is_controlled boolean
            cloneObject.is_controlled = archetype.is_controlled();

            // 0..1 parent_archetype_id ARCHETYPE_ID
            if (archetype.parent_archetype_id() != null)
            {
                ARCHETYPE_ID parentId = new ARCHETYPE_ID();
                parentId.value = archetype.parent_archetype_id().value().ToString();
                cloneObject.parent_archetype_id = parentId;
            }
            
            // 0..1 description RESOURCE_DESCRIPTION
            cloneObject.description = CloneDescription(archetype.description());

            // 1 ontology ARCHETYPE_ONTOLOGY
            cloneObject.ontology = CloneOntology(archetype.ontology());
            
            // 0..1 revision_history REVISION_HISTORY (does not occur in NHS archetypes, do later)
            //if (archetype.revision_history() != null)
               // cloneObject.revision_history = CloneAuthoredResource(archetype.revision_history());
 
            // 0..* translations TRANSLATION_DETAILS
            if (archetype.translations() != null)
            {
                if (archetype.translations().count() >= 0)
                {
                    TRANSLATION_DETAILS[] translations = new TRANSLATION_DETAILS[archetype.translations().count()];
                    archetype.translations().start();

                    for (int i = 1; i <= archetype.translations().count(); i++)
                    {
                        TRANSLATION_DETAILS translation = new TRANSLATION_DETAILS();                        
                        openehr.openehr.rm.common.resource.TRANSLATION_DETAILS td = archetype.translations().item_for_iteration() as openehr.openehr.rm.common.resource.TRANSLATION_DETAILS;

                        if (td.accreditation() != null)
                            translation.accreditation = td.accreditation().ToString();
                        //else
                        //    translation.accreditation = "";
                                       
                        translation.author = CloneHashTableAny(td.author());
                        translation.language = CloneCodePhrase(td.language());
                        translation.other_details = CloneHashTableAny(td.other_details());
                        translations [i-1] = translation;

                        archetype.translations().forth();
                    }
                    cloneObject.translations = translations;
                }
            }
            
            // 0..1 definition C_COMPLEX_OBJECT (set in cloneArchetype)

            // 0..* invariants ASSERTION            
            cloneObject.invariants = CloneAssertion(archetype.invariants());

            return cloneObject;                        
        }


        private ARCHETYPE_ONTOLOGY CloneOntology(openehr.openehr.am.archetype.ontology.ARCHETYPE_ONTOLOGY currentObject)
        {
            ARCHETYPE_ONTOLOGY cloneObject = new ARCHETYPE_ONTOLOGY();
                                                
            // 1..* term_definitions CodeDefinitionSet
            cloneObject.term_definitions = CloneCodeDefinitions(currentObject.term_definitions());
            
            // 0..* term_bindings TermBindingSet
            cloneObject.term_bindings = CloneTermBindingSet(currentObject.term_bindings());            
            
            // 0..* constraint_definitions CodeDefinitionSet
            cloneObject.constraint_definitions = CloneCodeDefinitions(currentObject.constraint_definitions());
            
            // 0..* constraint_bindings ConstraintBindingSet
            cloneObject.constraint_bindings = CloneConstraintBindingSet(currentObject.constraint_bindings());            

            return cloneObject;
        }

        private TermBindingSet[] CloneTermBindingSet(EiffelSoftware.Library.Base.structures.table.HASH_TABLE_REFERENCE_REFERENCE currentObject)
        {            
            if (currentObject != null)
            {
                if (currentObject.count() > 0)
                {
                    TermBindingSet[] termBindingSets = new TermBindingSet[currentObject.count()];
                    
                    currentObject.start();

                    // 0..* items TERM_BINDING_ITEM                    
                    for (int i = 1; i <= currentObject.count(); i++)
                    {
                        TermBindingSet termBindingSet = new TermBindingSet();
                        
                        //1 terminology string
                        termBindingSet.terminology = currentObject.key_for_iteration().ToString();

                        //terms HASH table
                        EiffelSoftware.Library.Base.structures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE adlTerms;
                        adlTerms = currentObject.item_for_iteration() as EiffelSoftware.Library.Base.structures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE;

                        //BJP: 18/9/2008 The term_bindings should also be sorted.
                        //TERM_BINDING_ITEM[] localTerms = new TERM_BINDING_ITEM[adlTerms.count()];
                        SortedList<string, TERM_BINDING_ITEM> localTerms =
                            new SortedList<string, TERM_BINDING_ITEM>();

                        adlTerms.start();
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
                                // HKF: TLS-6
                                //codePhrase.code_string = "";
                                codePhrase.code_string = term.code_string().ToString();

                                // 1 terminology_id TERMINOLOGY_ID                                
                                if (term.code_string() != null) 
                                {                                
                                    TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                                    // HKF: TLS-6
                                    //terminologyId.value = term.code_string().ToString();
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

                        //termBindingSet.items = localTerms;
                        
                        termBindingSets[i - 1] = termBindingSet;               
                        currentObject.forth();
                    }
                    return termBindingSets;
                }
            }
            return null;
        }

        private CodeDefinitionSet[] CloneCodeDefinitions(EiffelSoftware.Library.Base.structures.table.HASH_TABLE_REFERENCE_REFERENCE currentObject)
        {            
            if (currentObject != null)
            {
                //if (currentObject.count() > 0)
                {
                    CodeDefinitionSet[] codeDefinitionSets = new CodeDefinitionSet[currentObject.count()];
                                        
                    currentObject.start();
                    
                    // 0..* items ARCHETYPE_TERM
                    for (int i = 1; i <= currentObject.count(); i++)
                    {
                        CodeDefinitionSet codeDefinitionSet = new CodeDefinitionSet();

                        // 1 language string
                        codeDefinitionSet.language = currentObject.key_for_iteration().ToString();

                        //terms HASH table
                        EiffelSoftware.Library.Base.structures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE adlTerms;
                        adlTerms = currentObject.item_for_iteration() as EiffelSoftware.Library.Base.structures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE;

                        System.Collections.Generic.SortedList<string, ARCHETYPE_TERM> localTerms =
                            new System.Collections.Generic.SortedList<string, ARCHETYPE_TERM>();                        

                        adlTerms.start();
                        for (int j = 1; j <= adlTerms.count(); j++)
                        {
                            openehr.openehr.am.archetype.ontology.Impl.ARCHETYPE_TERM term = adlTerms.item_for_iteration() as openehr.openehr.am.archetype.ontology.Impl.ARCHETYPE_TERM;                            
                                                        
                            ARCHETYPE_TERM localTerm = new ARCHETYPE_TERM();
                            
                            // 1 code string
                            localTerm.code = term.code().ToString();                            

                            // 1..* items StringDictionaryItem                            
                            localTerm.items = CloneHashTableAny(term.items());     //Order: description/text
                            
                            localTerms.Add(localTerm.code, localTerm);
                            
                            adlTerms.forth();
                        }
                                                                        
                        codeDefinitionSet.items = new ARCHETYPE_TERM[localTerms.Count];                        
                        localTerms.Values.CopyTo(codeDefinitionSet.items, 0);

                        codeDefinitionSets[i - 1] = codeDefinitionSet; 
                        currentObject.forth();
                    }
                    
                    return codeDefinitionSets;
                }
            }
            return null;
        }

        private ConstraintBindingSet[] CloneConstraintBindingSet(EiffelSoftware.Library.Base.structures.table.HASH_TABLE_REFERENCE_REFERENCE currentObject)
        {
            if (currentObject != null)
            {
                if (currentObject.count() > 0)
                {
                    ConstraintBindingSet[] constraintBindingSets = new ConstraintBindingSet[currentObject.count()];

                    currentObject.start();                   
                    for (int i = 1; i <= currentObject.count(); i++)
                    {
                        ConstraintBindingSet constraintBindingSet = new ConstraintBindingSet();

                        // 1 terminology string
                        constraintBindingSet.terminology = currentObject.key_for_iteration().ToString();
                        
                        // 0..* items CONSTRAINT_BINDING_ITEM
                        EiffelSoftware.Library.Base.structures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE adlTerms;
                        adlTerms = currentObject.item_for_iteration() as EiffelSoftware.Library.Base.structures.table.Impl.HASH_TABLE_REFERENCE_REFERENCE;

                        CONSTRAINT_BINDING_ITEM[] localTerms = new CONSTRAINT_BINDING_ITEM[adlTerms.count()];

                        adlTerms.start();
                        for (int j = 1; j <= adlTerms.count(); j++)
                        {                            
                            openehr.common_libs.basic.Impl.URI term = adlTerms.item_for_iteration() as openehr.common_libs.basic.Impl.URI;

                            CONSTRAINT_BINDING_ITEM localTerm = new CONSTRAINT_BINDING_ITEM();
                            
                            // 1 code string
                            localTerm.code = adlTerms.key_for_iteration().ToString();
                            
                            // 1 value anyURI (note localTerm.value is string!)
                            localTerm.value = adlTerms.item_for_iteration().ToString();                            
                            localTerms[j - 1] = localTerm;
                            
                            adlTerms.forth();
                        }

                        constraintBindingSet.items = localTerms;
                        constraintBindingSets[i - 1] = constraintBindingSet;
                        currentObject.forth();
                    }
                    return constraintBindingSets;
                }
            }
            return null;
        }


        private RESOURCE_DESCRIPTION CloneDescription(openehr.openehr.rm.common.resource.RESOURCE_DESCRIPTION currentObject)
        {
            RESOURCE_DESCRIPTION cloneObject = new RESOURCE_DESCRIPTION();
                        
            // 1..* original_author StringDictionaryItem
            cloneObject.original_author = CloneHashTableAny(currentObject.original_author());

            // 0..* other_contributors string
            if (currentObject.other_contributors() != null)            
                if (currentObject.other_contributors().count() > 0)
                {
                    string[] contributorList = new string[currentObject.other_contributors().count()];
                    cloneObject.other_contributors = contributorList;

                    currentObject.other_contributors().start();
                    for (int i = 0; i < currentObject.other_contributors().count(); i++)
                    {
                        EiffelSoftware.Library.Base.structures.list.Impl.ARRAYED_LIST_REFERENCE castList = currentObject.other_contributors() as EiffelSoftware.Library.Base.structures.list.Impl.ARRAYED_LIST_REFERENCE;
                        EiffelSoftware.Library.Base.kernel.dotnet.Impl.SPECIAL_REFERENCE sList = (EiffelSoftware.Library.Base.kernel.dotnet.Impl.SPECIAL_REFERENCE)(castList.area());
                        contributorList[i] = sList.item(i).ToString();
                        currentObject.other_contributors().forth();
                    }
                }

            // 1 lifecycle_state string
            cloneObject.lifecycle_state = currentObject.lifecycle_state().to_cil();

            // 0..1 resource_package_uri string
            if (currentObject.resource_package_uri() != null)
                cloneObject.resource_package_uri = currentObject.resource_package_uri().ToString();

            // 0..* other_details StringDictionaryItem
            cloneObject.other_details = CloneHashTableAny(currentObject.other_details());

            // 0..1 parent_resource AUTHORED_RESOURCE (does not occur in NHS archetypes, do later)
            //if (currentObject.parent_resource() != null)
                //cloneObject.parent_resource = CloneAuthoredResource(currentObject.parent_resource());            

            //0..* details RESOURCE_DESCRIPTION_ITEM
            if (currentObject.details().count() > 0)
            {
                RESOURCE_DESCRIPTION_ITEM[] details = new RESOURCE_DESCRIPTION_ITEM[currentObject.details().count()];

                currentObject.details().start();              
                for (int i = 1; i <= currentObject.details().count(); i++)
                {
                    openehr.openehr.rm.common.resource.Impl.RESOURCE_DESCRIPTION_ITEM item = currentObject.details().item_for_iteration() as openehr.openehr.rm.common.resource.Impl.RESOURCE_DESCRIPTION_ITEM;
                    
                    details[i - 1] = new RESOURCE_DESCRIPTION_ITEM ();

                    // 1 language CODE_PHRASE
                    details[i - 1].language = CloneCodePhrase(item.language());

                    // 1 purpose string
                    if (item.purpose() != null)
                        details[i - 1].purpose = item.purpose().ToString();

                    // 0..1 use string
                    if (item.use() != null)
                        details[i - 1].use = item.use().ToString();

                    // 0..1 misuse string
                    if (item.misuse() != null)
                        details[i - 1].misuse = item.misuse().ToString();                                        
                    
                    // 0..1 copyright string
                    if (item.copyright() != null) 
                        details[i - 1].copyright = item.copyright().ToString();

                    // 0..* original_resource_uri StringDictionaryItem
                    if (item.original_resource_uri() != null)
                        if (item.original_resource_uri().count() > 0)
                            cloneObject.resource_package_uri = item.original_resource_uri().ToString();

                    // 0..* other_details StringDictionaryItem
                    //cloneObject.other_details = CloneHashTableAny(item.other_details());

                    // 0..* keywords string
                    if (item.keywords() != null)                    
                        if (item.keywords().count() > 0)
                        {
                            string[] keyWords = new string[item.keywords().count()];
                            details[i - 1].keywords = keyWords;

                            item.keywords().start();
                            for (int j = 0; j < item.keywords().count(); j++)
                            {
                                if (item.keywords().i_th(j+1) != null)
                                    keyWords[j] = item.keywords().i_th(j+1).ToString();                                
                                
                                item.keywords().forth();
                            }
                            details[i - 1].keywords = keyWords;
                        }                                                                                                 
                    
                    currentObject.details().forth();               
                }
                cloneObject.details = details;
            }           

            return cloneObject;            
        }

        private StringDictionaryItem[] CloneHashTableAny(EiffelSoftware.Library.Base.structures.table.HASH_TABLE_REFERENCE_REFERENCE currentObject)
        {            
            if (currentObject != null)
            {
                if (currentObject.count() > 0)
                {
                    //StringDictionaryItem[] dictionaryItem = new StringDictionaryItem[currentObject.count()];
                    SortedList<string, StringDictionaryItem> dictionaryItem = new SortedList<string, StringDictionaryItem>();
                    currentObject.start();
                    while (currentObject.key_for_iteration() != null)
                    {

                        StringDictionaryItem item = new StringDictionaryItem();
                        item.id = currentObject.key_for_iteration().ToString();
                        item.Value = currentObject.item_for_iteration().ToString();
                        dictionaryItem.Add(item.id, item);
                        currentObject.forth();
                    }

                    StringDictionaryItem[] items = new StringDictionaryItem[dictionaryItem.Count];

                    dictionaryItem.Values.CopyTo(items,0);

                    return items;
                }
            }
            return null;
        }

        private CARDINALITY CloneCardinality(openehr.openehr.am.archetype.constraint_model.CARDINALITY  currentObject)
        {            
            CARDINALITY cloneObject = new CARDINALITY();                    
            
            // 1 is_ordered string
            cloneObject.is_ordered = currentObject.is_ordered();                         

            // is_unique boolean
            cloneObject.is_unique = currentObject.is_unique();                           

            // 1 interval IntervalOfInteger
            cloneObject.interval = CloneIntervalOfInteger(currentObject.interval());     

            return cloneObject;            
        }
       
        private C_ATTRIBUTE CloneAttribute(openehr.openehr.am.archetype.constraint_model.C_ATTRIBUTE currentObject)
        {            
            C_ATTRIBUTE cloneAttribute = null;

            // Single
            if (currentObject.cardinality() == null)             
            {                                
                C_SINGLE_ATTRIBUTE cloneSingle = new C_SINGLE_ATTRIBUTE();                                                
                cloneAttribute = cloneSingle;
            }
            
            // Multiple
            else
            {
                C_MULTIPLE_ATTRIBUTE cloneMultiple = new C_MULTIPLE_ATTRIBUTE();

                // 1 cardinality CARDINALITY
                if (currentObject.cardinality() != null)
                    cloneMultiple.cardinality = CloneCardinality(currentObject.cardinality());
                cloneAttribute = cloneMultiple;
            }

            // 1 rm_attribute_name string
            cloneAttribute.rm_attribute_name = currentObject.rm_attribute_name().to_cil();   

            // 1 existence IntervalOfInteger
            if (currentObject.existence() != null)
                cloneAttribute.existence = CloneIntervalOfInteger(currentObject.existence());

            // 0..* children C_OBJECT (set in CloneTree)

            return cloneAttribute;
        }
    }
}


