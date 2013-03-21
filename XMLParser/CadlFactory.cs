using System;
using System.Collections.Generic;
using System.Text;

namespace XMLParser
{
    /// <summary>
    /// Class for generating AOM classes from XML
    /// </summary>
   public class AomFactory
    {
        public AomFactory()
        {
            //Constructor
        }
       
       /// <summary>
       /// Returns a C_COMPLEX_OBJECT class
       /// </summary>
       /// <param name="reference_model_class_name">The name of the reference model class as a string</param>
       /// <param name="node_id">The node id of this class</param>
       /// <param name="an_occurrences">The occurrences as an object</param>
       /// <returns>an AOM C_Complex_object that has these features set</returns>
       //public C_COMPLEX_OBJECT MakeComplexObject(string reference_model_class_name, string node_id, interval_of_integer an_occurrences) 
       public C_COMPLEX_OBJECT MakeComplexObject(string reference_model_class_name, string node_id, IntervalOfInteger an_occurrences)//JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        {
            C_COMPLEX_OBJECT result = new C_COMPLEX_OBJECT();
            result.rm_type_name = reference_model_class_name;
            result.node_id = node_id;
            result.occurrences = an_occurrences;
            return result;
        }

       /// <summary>
       /// Returns a C_COMPLEX_OBJECT class
       /// </summary>
       /// <param name="reference_model_class_name">The name of the reference model class as a string</param>
       /// <param name="node_id">The node id of this class</param>
       /// <returns>an AOM C_Complex_object that has these features set</returns>
       public C_COMPLEX_OBJECT MakeComplexObject(string reference_model_class_name, string node_id)
       {
           C_COMPLEX_OBJECT result = new C_COMPLEX_OBJECT();
           result.rm_type_name = reference_model_class_name;
           result.node_id = node_id;
           result.occurrences = default_occurrences();

           return result;
       }

       /// <summary>
       /// Returns a C_COMPLEX_OBJECT class 
       /// </summary>
       /// <param name="reference_model_class_name">The name of the reference model class as a string</param>
       /// <returns>an AOM C_Complex_object that has these features set</returns>
       public C_COMPLEX_OBJECT MakeComplexObject(string reference_model_class_name)
       {
           C_COMPLEX_OBJECT result = new C_COMPLEX_OBJECT();
           result.rm_type_name = reference_model_class_name;
           result.node_id = "";
           result.occurrences = default_occurrences();
           return result;
       }

       /// <summary>
       /// Returns a C_COMPLEX_OBJECT class
       /// </summary>
       /// <param name="reference_model_class_name">The name of the reference model class as a string</param>
       /// <param name="an_occurrence">The number occurrences of this object</param>
       /// <returns>an AOM C_Complex_object that has these features set</returns>
       public C_COMPLEX_OBJECT MakeComplexObject(string reference_model_class_name, IntervalOfInteger an_occurrence)
       {
           C_COMPLEX_OBJECT result = new C_COMPLEX_OBJECT();           
           result.rm_type_name = reference_model_class_name;
           result.node_id = "";
           result.occurrences = an_occurrence;
           return result;
       }

       /// /// <summary>
       /// Returns a C_COMPLEX_OBJECT class
       /// </summary>
       /// <param name="an_attribute">The attribute that has the C_COMPLEX_OBJECT as a child</param>
       /// <param name="reference_model_class_name">The name of the reference model class as a string</param>
       /// <param name="node_id">The node id of this class</param>
       /// <param name="an_occurrences">The occurrences as an object</param>
       /// <returns>an AOM C_Complex_object that has these features set</returns>
       public C_COMPLEX_OBJECT MakeComplexObject(C_ATTRIBUTE an_attribute, string reference_model_class_name, string node_id, IntervalOfInteger an_occurrences)
       {
           C_COMPLEX_OBJECT result = MakeComplexObject(reference_model_class_name, node_id, an_occurrences);
           add_object(an_attribute, result);
           return result;
       }

       /// /// <summary>
       /// Returns a C_COMPLEX_OBJECT class
       /// </summary>
       /// <param name="an_attribute">The attribute that has the C_COMPLEX_OBJECT as a child</param>
       /// <param name="reference_model_class_name">The name of the reference model class as a string</param>
       /// <param name="node_id">The node id of this class</param>
       /// <returns>an AOM C_Complex_object that has these features set</returns>
       public C_COMPLEX_OBJECT MakeComplexObject(C_ATTRIBUTE an_attribute, string reference_model_class_name, string node_id)
       {
           C_COMPLEX_OBJECT result = MakeComplexObject(reference_model_class_name, node_id);
           add_object(an_attribute, result);
           return result;
       }

       /// /// <summary>
       /// Returns a C_COMPLEX_OBJECT class
       /// </summary>
       /// <param name="an_attribute">The attribute that has the C_COMPLEX_OBJECT as a child</param>
       /// <param name="reference_model_class_name">The name of the reference model class as a string</param>
       /// <returns>an AOM C_Complex_object that has these features set</returns>
       public C_COMPLEX_OBJECT MakeComplexObject(C_ATTRIBUTE an_attribute, string reference_model_class_name)
       {
           C_COMPLEX_OBJECT result = MakeComplexObject(reference_model_class_name);
           add_object(an_attribute, result);
           return result;
       }

       /// <summary>
       /// Returns a default occurrence instance
       /// </summary>
       /// <returns>Interval of integer representing the default occurrences</returns>
        private IntervalOfInteger default_occurrences()
        {
            IntervalOfInteger result = new IntervalOfInteger();
            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            result.upperSpecified = true;
            result.upper_included = true;
            result.upper_includedSpecified = true;
            result.upper = 1;
            result.lowerSpecified = true;
            result.lower_included = true;
            result.lower_includedSpecified = true;
            result.lower = 1; //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1.  Wasn't called anywhere so set lower = 1
            //result.lower = 0;          
            //result.includes_maximum = true;
            //result.includes_minimum = true;
            //result.maximum = "1";
            //result.minimum = "0";
            return result;
        }


       //public C_SINGLE_ATTRIBUTE MakeSingleAttribute(C_COMPLEX_OBJECT an_object, string name) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
       public C_SINGLE_ATTRIBUTE MakeSingleAttribute(C_COMPLEX_OBJECT an_object, string name, IntervalOfInteger existence)
       {
           C_SINGLE_ATTRIBUTE result = new C_SINGLE_ATTRIBUTE();
           result.rm_attribute_name = name;           
           result.existence = existence; //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
           add_attribute(an_object, result); 
           return result;
       }

       //public C_MULTIPLE_ATTRIBUTE MakeMultipleAttribute(C_COMPLEX_OBJECT an_object, string name, CARDINALITY a_cardinality)//, int capacity) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
       public C_MULTIPLE_ATTRIBUTE MakeMultipleAttribute(C_COMPLEX_OBJECT an_object, string name, CARDINALITY a_cardinality, IntervalOfInteger existence)//, int capacity)
       {
           C_MULTIPLE_ATTRIBUTE result = new C_MULTIPLE_ATTRIBUTE();
           result.rm_attribute_name = name;
           result.cardinality = a_cardinality;
           result.existence = existence; //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
           //result.children = Array.CreateInstance(typeof(XMLParser.C_OBJECT), capacity) as C_OBJECT[];
           add_attribute(an_object, result);
           return result;
       }

       private void add_attribute(C_COMPLEX_OBJECT an_object, C_ATTRIBUTE an_attribute)
       {
           int i;
           if (an_object.attributes == null)
           {
               an_object.attributes = Array.CreateInstance(typeof(C_ATTRIBUTE), 1) as C_ATTRIBUTE[];
               i = 0;
           }
           else
           {
               C_ATTRIBUTE[] new_attributes = an_object.attributes;
               i = new_attributes.Length;
               Array.Resize(ref new_attributes, i + 1);
               an_object.attributes = new_attributes;
           }
           an_object.attributes[i] = an_attribute;
       }

       public void add_object(C_ATTRIBUTE an_attribute, C_OBJECT an_object)
       {
           int i;
           if (an_attribute.children == null)
           {
               an_attribute.children = Array.CreateInstance(typeof(C_OBJECT), 1) as C_OBJECT[];
               i = 0;
           }
           else
           {
               C_OBJECT[] new_objects = an_attribute.children;
               i = new_objects.Length;
               Array.Resize(ref new_objects, i + 1);
               an_attribute.children = new_objects;
           }
           an_attribute.children[i] = an_object;
       }

       public ARCHETYPE_INTERNAL_REF MakeArchetypeRef(C_ATTRIBUTE an_attribute, string reference_model_class, string path)
       {
           ARCHETYPE_INTERNAL_REF result = new ARCHETYPE_INTERNAL_REF();
           result.rm_type_name = reference_model_class;

           //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
           result.node_id = "";
           result.occurrences = default_occurrences();               

           result.target_path = path;
           add_object(an_attribute, result);
           return result;
       }

       public C_PRIMITIVE_OBJECT MakePrimitiveObject(C_ATTRIBUTE an_attribute, C_PRIMITIVE a_primative)
       {
           C_PRIMITIVE_OBJECT result = new C_PRIMITIVE_OBJECT();

           if (a_primative != null)
           {
               result.item = a_primative;
           }

           //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
           result.node_id = "";
           result.occurrences = default_occurrences();               
           
           //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
           //else
           //    result.any_allowed = true;           

           add_object(an_attribute, result);

           switch (a_primative.GetType().ToString().ToLower(System.Globalization.CultureInfo.InvariantCulture))
           {
               case "xmlparser.c_boolean":
                   result.rm_type_name = "BOOLEAN";
                   break;
               case "xmlparser.c_integer":
                   result.rm_type_name = "INTEGER";
                   break;
               case "xmlparser.c_code_phrase":
                   result.rm_type_name = "CODE_PHRASE";
                   break;
               case "xmlparser.c_date":
                   result.rm_type_name = "DATE";
                   break;
               case "xmlparser.c_date_time":
                   result.rm_type_name = "DATE_TIME";
                   break;
               case "xmlparser.c_duration":
                   result.rm_type_name = "DURATION";
                   break;
               case "xmlparser.c_ordinal":
                   result.rm_type_name = "ORDINAL";
                   break;
               case "xmlparser.c_quantity":
                   result.rm_type_name = "QUANTITY";
                   break;
               case "xmlparser.c_string":
                   result.rm_type_name = "STRING";
                   break;
               case "xmlparser.c_time":
                   result.rm_type_name = "TIME";
                   break;
               case "xmlparser.c_real":
                   result.rm_type_name = "REAL";
                   break;
               default:
                   System.Diagnostics.Debug.Assert(false, a_primative.GetType().ToString() + " is not handled");
                   break;
           }
           return result;
       }

       public void AddIncludeToSlot(ARCHETYPE_SLOT slot, ASSERTION assert)
       {
           int i;

           if (slot.includes == null)
           {
               slot.includes = Array.CreateInstance(typeof(ASSERTION), 1) as ASSERTION[];
               i = 0;
           }
           else
           {
               ASSERTION[] assertions = slot.includes;
               i = assertions.Length;
               Array.Resize(ref assertions, i + 1);
               slot.includes = assertions;
           }
           slot.includes[i] = assert;
       }

       public void AddExcludeToSlot(ARCHETYPE_SLOT slot, ASSERTION assert)
       {
           int i;

           if (slot.excludes == null)
           {
               slot.excludes = Array.CreateInstance(typeof(ASSERTION), 1) as ASSERTION[];
               i = 0;
           }
           else
           {
               ASSERTION[] assertions = slot.excludes;
               i = assertions.Length;
               Array.Resize(ref assertions, i + 1);
               slot.excludes = assertions;
           }
           slot.excludes[i] = assert;
       }

       public C_DURATION create_c_duration_bounded(string a_minimum,
        string a_maximum, bool includesMin, bool includesMax)
       {
           C_DURATION result = new C_DURATION();
           //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1           
           //interval_of_duration interval = new interval_of_duration();
           IntervalOfDuration interval = new IntervalOfDuration();
           interval.lower = a_minimum;
           interval.lower_included = includesMin;
           interval.lower_includedSpecified = includesMin;
           interval.upper = a_maximum;
           interval.upper_included = includesMax;
           interval.upper_includedSpecified = includesMax;
           //interval.minimum = a_minimum;
           //interval.maximum = a_maximum;
           //interval.includes_maximum = includesMax;
           //interval.includes_minimum = includesMin;                  
           result.range = interval;
           return result;
       }

       public C_DURATION create_c_duration_from_pattern(string a_pattern)
       {
           C_DURATION result = new C_DURATION();
           result.pattern = a_pattern;
           return result;
       }

       public C_DURATION create_c_duration_upper_unbounded(string a_minimum,
        bool includesMin)
       {
           C_DURATION result = new C_DURATION();
           //interval_of_duration interval = new interval_of_duration(); //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1           
           IntervalOfDuration interval = new IntervalOfDuration();
           interval.lower = a_minimum;
           interval.lower_included = includesMin;
           interval.lower_includedSpecified = includesMin;           
           interval.upper_included = false;
           interval.upper_includedSpecified = false;
           //interval.minimum = a_minimum;
           //interval.includes_minimum = includesMin;
           //interval.includes_maximum = false;
           result.range = interval;
           return result;
       }
    }
}
