using System;
using System.Collections.Generic;
using System.Text;

namespace XMLParser
{

    // A delegate for dealing with adding a language to the ontology
    public delegate void LanguageAddedEventHandler(object sender, string language, string languageCodeSet);

    public class Ontology
    {
        ARCHETYPE _archetype;

        //event that arises if a language is added
        internal event LanguageAddedEventHandler LanguageAdded;

        public Ontology(ARCHETYPE an_archetype)
        {
            _archetype = an_archetype;
            if ((_language == null) || (!this.AvailableLanguages().Contains(_language)))
                _language = an_archetype.original_language.code_string;
        }

        public string PrimaryLanguageCode
        {
            get{
            return _archetype.original_language.code_string;
            }
        }

        public void SetPrimaryLanguage(string a_language_code)
        {
            _archetype.original_language.code_string = a_language_code;
            if (!_languages_available.Contains(a_language_code))
                _languages_available.Add(a_language_code);
        }


        protected virtual void OnLanguageAdded(string language, string languageCodeSet)
        {
            if (LanguageAdded != null)
                LanguageAdded(this, language, languageCodeSet);
        }

        string _language;

        public string LanguageCode
        {
            get{
                if(_language == null || _language == "")
                    return _archetype.original_language.code_string;
                else
                    return _language;
            }
        }

        public void SetLanguage(string a_language_code)
        {
            if(this.LanguageAvailable(a_language_code))
                _language = a_language_code;
        }

        public int NumberOfSpecialisations
        {
            get{                
                //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1                
                //return int.Parse(_archetype.ontology.specialisation_depth);
                
                //Number of specialisations is the number of hyphens that appear in the second section of the archetype id
                int specialisationDepth = 0;

                if (_archetype.archetype_id != null)
                {
                    string archetypeId = _archetype.archetype_id.value;                    
                    char[] splitter = {'.'};
                    string[] idComponents = archetypeId.Split(splitter);

                    if (idComponents.GetUpperBound(0) >= 1)
                    {
                        splitter[0] = '-';
                        string[] specialisations = idComponents[1].Split(splitter);
                        specialisationDepth = specialisations.GetUpperBound(0);
                    }
                }
                return specialisationDepth;
            }
        }

        public void Reset()
        {
            _languages_available = null;
            _terminologies_available = null;
        }

        public bool HasTermBinding(string a_terminology_id, string a_path)
        {
            //foreach (terminology_set t in _archetype.ontology.term_bindings) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            foreach (TermBindingSet t in _archetype.ontology.term_bindings)
            {
                if (t.terminology == a_terminology_id)
                {
                    //foreach (binding b in t.bindings) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1                    
                    foreach (TERM_BINDING_ITEM b in t.items) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                    {
                        if (b.code == a_path)
                            return true;
                    }
                }
            }
            return false;
        }

        public string TermBinding(string a_terminology_id, string a_path)
        {
            //foreach (terminology_set t in _archetype.ontology.term_bindings) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            foreach (TermBindingSet t in _archetype.ontology.term_bindings)
            {
                //SRH: 3 Jun 2008 - changed to starts with to allow return if release not specified
                if (t.terminology.StartsWith(a_terminology_id))
                //if (t.terminology == a_terminology_id)
                {
                    //foreach (binding b in t.bindings) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                    foreach (TERM_BINDING_ITEM b in t.items)
                    {
                        if (b.code == a_path)
                            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1 
                            //return b.value; //b.value is CODE_PHRASE
                            if (b.value != null)
                                return b.value.code_string;                       
                    }
                }
            }
            return "";
        }

        public void ReplaceTermDefinition(string language_code, ARCHETYPE_TERM a_term, bool replace_translations)
        {

            string code = a_term.code.ToLower(System.Globalization.CultureInfo.InvariantCulture);

            System.Diagnostics.Debug.Assert(this.HasTermCode(a_term.code), "Does not have code " + code);
            //language_set[] languageSets;
            CodeDefinitionSet[] languageSets;

            if(code.StartsWith("at"))
                languageSets = _archetype.ontology.term_definitions;
            else
                languageSets = _archetype.ontology.constraint_definitions;

            foreach (CodeDefinitionSet ls in languageSets) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            {
                if (ls.language == language_code)
                {
                    ReplaceTerm(ls, a_term);
                }
                else
                {
                    if (replace_translations && language_code == this.PrimaryLanguageCode)
                    {           
                        foreach (StringDictionaryItem di in a_term.items) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                        {                            
                            di.Value = string.Format("*{0}({1})", di.Value, language_code);
                        }
                        ReplaceTerm(ls, a_term);
                    }
                }
            }
        }
               
        //private void ReplaceTerm(language_set a_language_set, ARCHETYPE_TERM a_term)    
        private void ReplaceTerm(CodeDefinitionSet a_language_set, ARCHETYPE_TERM a_term)
        {
            a_language_set.items [GetIndexOfTerm(a_language_set, a_term)] = a_term;
        }
        
        //private int GetIndexOfTerm(language_set a_language_set, ARCHETYPE_TERM a_term)
        private int GetIndexOfTerm(CodeDefinitionSet a_language_set, ARCHETYPE_TERM a_term)
        {
            int i;
            for (i = 0; i < a_language_set.items.Length; i++)
            {
                if (a_language_set.items[i].code == a_term.code)
                    return i;
            }
            return -1;
        }

        private ARCHETYPE_TERM FindTermForCode(string a_code, ARCHETYPE_TERM[] terms)
        {
            a_code = a_code.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            foreach (ARCHETYPE_TERM at in terms)
            {
                if (at.code.ToLower(System.Globalization.CultureInfo.InvariantCulture) == a_code)
                    return at;
            }
            return null;
        }

        public void AddLanguage(string a_language_code)
        {
            if (!_languages_available.Contains(a_language_code))
            {
                // add the new language
                _languages_available.Add(a_language_code);

                if (_languages_available.Count > 1)
                {

                    //populate a translation set from the original language
                    CodeDefinitionSet ls = TermDefinitions(_archetype.original_language.code_string);
                    CodeDefinitionSet new_ls = GetTermLanguageSet(a_language_code);
                    ARCHETYPE_TERM[] new_terms = Array.CreateInstance(typeof(ARCHETYPE_TERM), ls.items.Length) as ARCHETYPE_TERM[];

                    for(int i=0; i < ls.items.Length; i++)
                    {
                        ARCHETYPE_TERM at = ls.items[i];
                        ARCHETYPE_TERM new_at = new ARCHETYPE_TERM();
                        new_at.code = at.code;
                        StringDictionaryItem[] new_items = Array.CreateInstance(typeof(StringDictionaryItem), at.items.Length) as StringDictionaryItem[];
                        for (int j = 0; j < at.items.Length; j++)
                        {
                            StringDictionaryItem di = at.items[j];
                            StringDictionaryItem new_di = new StringDictionaryItem();
                            new_di.id = di.id ;
                            new_di.Value = string.Format("{0}{1}({2})", "*", di.Value, _archetype.original_language.code_string);
                            new_items[j] = new_di;
                        }
                        new_at.items = new_items;
                        new_terms[i] = new_at;
                    }
                    new_ls.items = new_terms;


                    //now get a translation set for constraint definitions
                    ls = ConstraintDefinitions(_archetype.original_language.code_string);
                    if (ls != null)  //May not be any Constraint definitions
                    {
                        new_ls = GetConstraintLanguageSet(a_language_code);
                        new_terms = Array.CreateInstance(typeof(ARCHETYPE_TERM), ls.items.Length) as ARCHETYPE_TERM[];
                        for (int i = 0; i < ls.items.Length; i++)
                        {
                            ARCHETYPE_TERM ac = ls.items[i];
                            ARCHETYPE_TERM new_ac = new ARCHETYPE_TERM();
                            new_ac.code = ac.code;
                            StringDictionaryItem[] new_items = Array.CreateInstance(typeof(StringDictionaryItem), ac.items.Length) as StringDictionaryItem[];
                            for (int j = 0; j < ac.items.Length; j++)
                            {
                                StringDictionaryItem di = ac.items[j];
                                StringDictionaryItem new_di = new StringDictionaryItem();
                                new_di.id = di.id;
                                new_di.Value = string.Format("{0}{1}({2})", "*", di.Value, _archetype.original_language.code_string);
                                new_items[j] = new_di;
                            }
                            new_ac.items = new_items;
                            new_terms[i] = new_ac;
                        }
                        new_ls.items = new_terms;
                    }
                }

                //populate a new description by raising an event
                this.LanguageAdded(this, a_language_code, _archetype.original_language.terminology_id.value);                
            }
        }


        public void AddTerminology(string a_terminology_id)
        {
            if (!_terminologies_available.Contains(a_terminology_id))
                _terminologies_available.Add(a_terminology_id);
        }

        public bool HasTermCode(string a_term_code)
        {
            a_term_code = a_term_code.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            if(a_term_code.StartsWith("at"))
            {
                if(_archetype.ontology.term_definitions.Length > 0)
                {
                  foreach(ARCHETYPE_TERM at in _archetype.ontology.term_definitions[0].items)
                  {
                      if(at.code.ToLower(System.Globalization.CultureInfo.InvariantCulture) == a_term_code)
                          return true;
                  }
                }
            }
            else if (a_term_code.StartsWith("ac"))
            {
                if(_archetype.ontology.constraint_definitions.Length > 0)
                {
                  foreach(ARCHETYPE_TERM ac in _archetype.ontology.constraint_definitions[0].items)
                  {
                      if(ac.code.ToLower(System.Globalization.CultureInfo.InvariantCulture) == a_term_code)
                          return true;
                  }
                }
            }
            else
                System.Diagnostics.Debug.Assert(false, a_term_code + " does not appear to be a valid code");
                
            return false;
        }

        public string ConstraintBinding(string a_terminology_id, string a_path)
        {
            foreach (ConstraintBindingSet t in _archetype.ontology.constraint_bindings)
            {
                //SRH: 3 Jun 2008 - change to starts with to work if release is not specified
                //if (t.terminology == a_terminology_id)
                if (t.terminology.StartsWith( a_terminology_id))
                {
                    foreach (CONSTRAINT_BINDING_ITEM b in t.items)
                    {
                        if (b.code == a_path)
                            return b.value;
                    }
                }
            }
            return null;
        }

        public bool HasConstraintBinding(string a_terminology_id, string a_path)
        {
            foreach (ConstraintBindingSet t in _archetype.ontology.constraint_bindings)
            {
                //SRH: 3 Jun 2008 - change to starts with to work if release is not specified
                //if (t.terminology == a_terminology_id)
                if (t.terminology.StartsWith(a_terminology_id))
                {
                    foreach (CONSTRAINT_BINDING_ITEM b in t.items)
                    {
                        if (b.code == a_path)
                            return true;
                    }
                }
            }
            return false;
        }

        public void AddOrReplaceConstraintBinding(string terminology_query, string archetype_path, string terminology)
        {
            ConstraintBindingSet ts = GetConstraintBindingSet(terminology);
            AddOrReplaceBinding(ts, terminology_query, archetype_path);            
        }

        // EDT-134
        //public void AddOrReplaceTermBinding(string terminology_code, string archetype_path, string terminology)
        //{
        //    TermBindingSet ts = GetTermBindingSet(terminology);
        //    AddOrReplaceBinding(ts, terminology_code, archetype_path);            
        public void AddOrReplaceTermBinding(string code_string, string archetype_path, 
            string terminology_key, string code_terminology_id)
        {
            TermBindingSet ts = GetTermBindingSet(terminology_key);
            AddOrReplaceBinding(ts, code_string, archetype_path, code_terminology_id);            
        }

        // EDT-134
        //private void AddOrReplaceBinding(TermBindingSet a_terminology_set, string terminology_code, string archetype_path)
        private void AddOrReplaceBinding(TermBindingSet a_terminology_set,
            string code_string, string archetype_path, string code_terminology_id)
        {
            int i = 0;
            TERM_BINDING_ITEM[] resize_bindings;
            if (a_terminology_set.items != null)
            {
                foreach (TERM_BINDING_ITEM b in a_terminology_set.items)
                {
                    if (b.code == archetype_path)
                    {
                        //already there so just change the path                                               
                        
                        //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1 
                        //b.value = terminology_code;

                        if (b.value == null)
                        {
                            CODE_PHRASE codePhrase = new CODE_PHRASE();
                            b.value = codePhrase;
                        }

                        if (b.value.terminology_id == null)
                        {
                            TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                            b.value.terminology_id = terminologyId;
                        }

                        // EDT-134
                        //b.value.terminology_id.value = terminology_code;
                        b.value.terminology_id.value = code_terminology_id;
                        b.value.code_string = code_string;
                        
                        return;
                    }
                }
                resize_bindings = a_terminology_set.items;
                i = resize_bindings.Length;
                Array.Resize(ref resize_bindings, i + 1);
            }
            else
            {
                resize_bindings = Array.CreateInstance(typeof(TERM_BINDING_ITEM), 1) as TERM_BINDING_ITEM[];
            }

            //didn't find it so create a new binding
            TERM_BINDING_ITEM new_binding = new TERM_BINDING_ITEM();
            new_binding.code = archetype_path.ToLower(System.Globalization.CultureInfo.InvariantCulture);

            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1 CHECK
            //new_binding.value = terminology_code;

            if (new_binding.value == null)
            {
                CODE_PHRASE codePhrase = new CODE_PHRASE();
                new_binding.value = codePhrase;
            }

            if (new_binding.value.terminology_id == null)
            {
                TERMINOLOGY_ID terminologyId = new TERMINOLOGY_ID();
                new_binding.value.terminology_id = terminologyId;
            }

            // EDT-134
            //new_binding.value.terminology_id.value = terminology_code;
            new_binding.value.terminology_id.value = code_terminology_id;
            new_binding.value.code_string = code_string;


            //resize the bindings array

            //add in the new binding
            resize_bindings[i] = new_binding;
            a_terminology_set.items = resize_bindings;
        }
        
        //private void AddOrReplaceBinding(terminology_set a_terminology_set, string terminology_code, string archetype_path) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        private void AddOrReplaceBinding(ConstraintBindingSet a_terminology_set, string terminology_code, string archetype_path)
        {
            int i = 0;
            CONSTRAINT_BINDING_ITEM[] resize_bindings;
            if (a_terminology_set.items != null)
            {
                foreach (CONSTRAINT_BINDING_ITEM b in a_terminology_set.items)
                {
                    if (b.code == archetype_path)
                    {
                        //already there so just change the path
                        b.value = terminology_code;
                        return;
                    }
                }
                resize_bindings = a_terminology_set.items;
                i = resize_bindings.Length;
                Array.Resize(ref resize_bindings, i + 1);
            }
            else
            {
                resize_bindings = Array.CreateInstance(typeof(CONSTRAINT_BINDING_ITEM), 1) as CONSTRAINT_BINDING_ITEM[];
            }

            //didn't find it so create a new binding
            CONSTRAINT_BINDING_ITEM new_binding = new CONSTRAINT_BINDING_ITEM();
            new_binding.code = archetype_path.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            new_binding.value = terminology_code;

            //resize the bindings array
           
            //add in the new binding
            resize_bindings[i] = new_binding;
            a_terminology_set.items = resize_bindings;
        }


        public bool LanguageAvailable(string a_language_code)
        {
            return this.AvailableLanguages().Contains(a_language_code);
        }

        System.Collections.ArrayList _languages_available;

        public System.Collections.ArrayList AvailableLanguages()
        {
            if(_languages_available == null)
            {
                _languages_available = new System.Collections.ArrayList();
                populate_language_list();
            }
            return _languages_available;
        }

        private void populate_language_list()
        {
            //current language is added by default so need to ensure we do not get it twice

            if(!_languages_available.Contains(_archetype.original_language.code_string))
                _languages_available.Add(_archetype.original_language.code_string);
 
            if (_archetype.translations != null)
            {
                foreach (TRANSLATION_DETAILS t in _archetype.translations)
                {
                    if (!_languages_available.Contains(t.language.code_string))
                        _languages_available.Add(t.language.code_string);
                }
            }
        }

        public bool IsMultiLanguage()
        {
            return this.AvailableLanguages().Count > 1;
        }

        public bool TerminologyAvailable(string a_terminology)
        {
            return this.AvailableTerminologies().Contains(a_terminology);
        }

        System.Collections.ArrayList _terminologies_available;

        public System.Collections.ArrayList AvailableTerminologies()
        {
            if (_terminologies_available == null)
            {
               populate_terminology_list();
            }
            return _terminologies_available;
        }

        private void populate_terminology_list()
        {
            _terminologies_available = new System.Collections.ArrayList();

            if (_archetype.ontology.term_bindings != null)
            {
                foreach (TermBindingSet t in _archetype.ontology.term_bindings)
                {
                    _terminologies_available.Add(t.terminology);
                }
            }
            if (_archetype.ontology.constraint_bindings != null)
            {
                foreach (ConstraintBindingSet t in _archetype.ontology.constraint_bindings)
                {
                    if (!_terminologies_available.Contains(t.terminology))
                        _terminologies_available.Add(t.terminology);
                }
            }
        }

         public string NextSpecialisedTermId(string term_id)
        {
            System.Collections.ArrayList counters = new System.Collections.ArrayList();
            foreach (ARCHETYPE_TERM t in _archetype.ontology.term_definitions[0].items)
            {
                if(t.code.StartsWith(term_id))
                {
                    counters.Add(t.code.Substring(term_id.Length + 1));
                }
            }
            int i = 1;
            while( counters.Contains(i.ToString()))
            {
                i++;
            }
            return term_id + "." + i.ToString();
         }
                
        public string NextTermId()
        {
            System.Collections.ArrayList counters = new System.Collections.ArrayList();
            foreach (ARCHETYPE_TERM t in _archetype.ontology.term_definitions[0].items)
            {
                //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                //if (_archetype.ontology.specialisation_depth == null |
                //    _archetype.ontology.specialisation_depth == "" |
                //    _archetype.ontology.specialisation_depth == "0")
                //{
                //    counters.Add(int.Parse(t.code.Substring(2))); //leave the at off the front
                //}
                //else if (number_of(Char.Parse("."), t.code) == int.Parse(_archetype.ontology.specialisation_depth))
                //{
                //    counters.Add(int.Parse(t.code.Substring(t.code.LastIndexOf(".") + 1)));
                //}
                if (NumberOfSpecialisations == 0)
                {
                    counters.Add(int.Parse(t.code.Substring(2))); //leave the at off the front
                }
                else if (number_of(Char.Parse("."), t.code) == NumberOfSpecialisations)
                {
                    counters.Add(int.Parse(t.code.Substring(t.code.LastIndexOf(".") + 1)));
                }

            }
            int i = 1;
            while (counters.Contains(i))
            {
                i++;
            }

            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            //if (int.Parse(_archetype.ontology.specialisation_depth) == 0)
            //    return "at" + i.ToString().PadLeft(4, Char.Parse("0"));
            //else
            //{
            //    string result = "at";
            //    for (i = 0; i < int.Parse(_archetype.ontology.specialisation_depth); i++)
            //    {
            //        result += "0.";
            //    }
            //    result += i.ToString();
            //    return result;
            //}
            if (NumberOfSpecialisations == 0)
                return "at" + i.ToString().PadLeft(4, Char.Parse("0"));
            else
            {
                string result = "at";
                for (i = 0; i < NumberOfSpecialisations; i++)
                {
                    result += "0.";
                }
                result += i.ToString();
                return result;
            }
        }

        public string NextConstraintId()
        {
            System.Collections.ArrayList counters = new System.Collections.ArrayList();
            foreach (ARCHETYPE_TERM t in _archetype.ontology.constraint_definitions[0].items)
            {
                //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                //if (_archetype.ontology.specialisation_depth == null |
                //    _archetype.ontology.specialisation_depth == "" |
                //    _archetype.ontology.specialisation_depth == "0")
                //{
                //    counters.Add(int.Parse(t.code.Substring(2))); //leave the ac off the front
                //}
                //else if (number_of(Char.Parse("."), t.code) == int.Parse(_archetype.ontology.specialisation_depth))
                //{
                //    counters.Add(int.Parse(t.code.Substring(t.code.LastIndexOf(".") + 1)));
                //}
                if (NumberOfSpecialisations == 0)
                {
                    counters.Add(int.Parse(t.code.Substring(2))); //leave the ac off the front
                }
                else if (number_of(Char.Parse("."), t.code) == NumberOfSpecialisations)
                {
                    counters.Add(int.Parse(t.code.Substring(t.code.LastIndexOf(".") + 1)));
                }
            }
            int i = 1;
            while (counters.Contains(i))
            {
                i++;
            }

            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            //if (int.Parse(_archetype.ontology.specialisation_depth) == 0)
            //    return "ac" + i.ToString().PadRight(4, Char.Parse("0"));
            //else
            //{
            //    string result = "ac";
            //    for (i = 0; i < int.Parse(_archetype.ontology.specialisation_depth); i++)
            //    {
            //        result += "0.";
            //    }
            //    result += i.ToString();
            //    return result;
            //}
            if (NumberOfSpecialisations == 0)
                return "ac" + i.ToString().PadRight(4, Char.Parse("0"));
            else
            {
                string result = "ac";
                for (i = 0; i < NumberOfSpecialisations; i++)
                {
                    result += "0.";
                }
                result += i.ToString();
                return result;
            }
        }

        private int number_of(char c, string a_string)
        {
            int i = 0;
            foreach (char cc in a_string.ToCharArray())
            {
                if (cc == c)
                    i++;
            }
            return i;
        }

        public void AddTermOrConstraintDefinition(ARCHETYPE_TERM a_term, bool isLoading)
        {
            AddTermOrConstraintDefinition(_language, a_term, isLoading);
        }

        //public language_set GetTermLanguageSet(string a_language)
        public CodeDefinitionSet GetTermLanguageSet(string a_language)
        {
            int i = 1;
            //language_set[] languageSets;
            CodeDefinitionSet[] languageSets;
            languageSets = _archetype.ontology.term_definitions;
            if (languageSets != null)
            {
                foreach (CodeDefinitionSet ls in languageSets)
                {
                    if (ls.language == a_language)
                        return ls;
                }
                i = languageSets.Length + 1;
            }
            Array.Resize(ref languageSets, i);
            CodeDefinitionSet new_ls = new CodeDefinitionSet();
            new_ls.language = a_language;
            languageSets[i - 1] = new_ls;
            _archetype.ontology.term_definitions = languageSets;
            return new_ls;
        }

        //public language_set GetConstraintLanguageSet(string a_language)
        public CodeDefinitionSet GetConstraintLanguageSet(string a_language)
        {
            int i= 1;
            //language_set[] languageSets;
            CodeDefinitionSet[] languageSets;
            languageSets = _archetype.ontology.constraint_definitions;
            if (languageSets != null)
            {
                foreach (CodeDefinitionSet ls in languageSets)
                {
                    if (ls.language == a_language)
                        return ls;
                }
                i = languageSets.Length + 1;
            }
            Array.Resize(ref languageSets, i);
            CodeDefinitionSet new_ls = new CodeDefinitionSet();
            new_ls.language = a_language;
            languageSets[i - 1] = new_ls;
            _archetype.ontology.constraint_definitions = languageSets;
            return new_ls;
        }

        //private terminology_set GetTermBindingSet(string a_terminology) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        private TermBindingSet GetTermBindingSet(string a_terminology)
        {
            int i = 1;
            //terminology_set[] terminologySets; //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
            TermBindingSet[] terminologySets;

            terminologySets = _archetype.ontology.term_bindings;
            if (terminologySets != null)
            {
                foreach (TermBindingSet ts in terminologySets)
                {
                    if (ts.terminology == a_terminology)
                        return ts;
                }
                i = terminologySets.Length + 1;
            }
            Array.Resize(ref terminologySets, i);
            TermBindingSet new_ts = new TermBindingSet();
            new_ts.terminology = a_terminology;
            terminologySets[i - 1] = new_ts;
            _archetype.ontology.term_bindings = terminologySets;
            return new_ts;
        }

        //private terminology_set GetConstraintBindingSet(string a_terminology)
        private ConstraintBindingSet GetConstraintBindingSet(string a_terminology)
        {
            int i = 1;
            //terminology_set[] terminologySets;
            ConstraintBindingSet[] terminologySets;
            terminologySets = _archetype.ontology.constraint_bindings;
            if (terminologySets != null)
            {
                foreach (ConstraintBindingSet ts in terminologySets)
                {
                    if (ts.terminology == a_terminology)
                        return ts;
                }
                i = terminologySets.Length + 1;
            }
            Array.Resize(ref terminologySets, i);
            ConstraintBindingSet new_ts = new ConstraintBindingSet();
            new_ts.terminology = a_terminology;
            terminologySets[i - 1] = new_ts;
            _archetype.ontology.constraint_bindings = terminologySets;
            return new_ts;
        }

        public void AddTermOrConstraintDefinition(string a_language, ARCHETYPE_TERM a_term, bool isLoading)
        {
            CodeDefinitionSet languageSet;
            CodeDefinitionSet[] languageSets;
            string code = a_term.code.ToLowerInvariant();


            if (code.StartsWith("at"))
            {
                languageSet = this.GetTermLanguageSet(a_language);
                languageSets = _archetype.ontology.term_definitions;
            }
            else if (code.StartsWith("ac"))
            {
                languageSet = GetConstraintLanguageSet(a_language);
                languageSets = _archetype.ontology.constraint_definitions;
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "Error in code " + code);
                return;
            }

            //Add the term to that language - ensures there is a language set for the language
            AddTermOrConstraintDefinitionForLanguage(languageSet, a_term);

            if (!isLoading & languageSets.Length > 1) // if there is more than one language then add the term to those
            {
                foreach (CodeDefinitionSet ls in languageSets)
                {
                    if (ls.language != a_language)
                    {
                        foreach (StringDictionaryItem di in a_term.items)
                        {
                            di.Value = string.Format("{0}{1}({2})", "*", di.Value, a_language);
                        }
                        AddTermOrConstraintDefinitionForLanguage(ls, a_term);
                    }
                }
            }
        }

        //private void AddTermOrConstraintDefinitionForLanguage(language_set ls, ARCHETYPE_TERM a_term)
        private void AddTermOrConstraintDefinitionForLanguage(CodeDefinitionSet ls, ARCHETYPE_TERM a_term)
        {
            int i;

            if (ls.items == null)
            {
                ls.items = Array.CreateInstance(typeof(ARCHETYPE_TERM), 1) as ARCHETYPE_TERM[];
                i = 0;
            }
            else
            {
                ARCHETYPE_TERM[] t = ls.items;
                Array.Resize(ref t, t.Length + 1);
                i = t.Length - 1;
                ls.items = t;
            }
            ls.items[i] = a_term;

        }

        //private language_set TermDefinitions(string a_language_code)
        private CodeDefinitionSet TermDefinitions(string a_language_code)
        {
            foreach (CodeDefinitionSet ls in _archetype.ontology.term_definitions)
            {
                if (ls.language == a_language_code)
                    return ls;
            }
            return null;
        }

        public ARCHETYPE_TERM TermDefinition(string a_language_code, string a_code)
        {
            CodeDefinitionSet ls = TermDefinitions(a_language_code);
            if (ls != null)
            {
                foreach (ARCHETYPE_TERM at in ls.items)
                {
                    if (at.code == a_code)
                        return at;
                }
            }
            return null;
        }


        public ARCHETYPE_TERM ConstraintDefinition(string a_language_code, string a_code)
        {
            CodeDefinitionSet ls = ConstraintDefinitions(a_language_code);
            if (ls != null)
            {
                foreach (ARCHETYPE_TERM at in ls.items)
                {
                    if (at.code == a_code)
                        return at;
                }
            }
            return null;
        }

        public void RemoveTermBinding(string a_path, string a_terminology_id)
        {
            TermBindingSet ts = GetBindings(a_terminology_id, _archetype.ontology.term_bindings);
            System.Collections.ArrayList bindingsArray = new System.Collections.ArrayList(ts.items);
            foreach (TERM_BINDING_ITEM b in ts.items)
            {
                if (b.code == a_path)
                {
                    bindingsArray.Remove(b);
                    break;
                }
            }
            ts.items = bindingsArray.ToArray() as TERM_BINDING_ITEM[];
        }

        public void RemoveConstraintBinding(string a_query, string a_terminology_id)
        {
            ConstraintBindingSet ts = GetBindings(a_terminology_id, _archetype.ontology.constraint_bindings);
            System.Collections.ArrayList bindingsArray = new System.Collections.ArrayList(ts.items);
            foreach (CONSTRAINT_BINDING_ITEM b in ts.items)
            {
                if (b.code == a_query)
                {
                    bindingsArray.Remove(b);
                    break;
                }
            }
            ts.items = bindingsArray.ToArray() as CONSTRAINT_BINDING_ITEM[];
        }

        //public terminology_set GetBindings(string a_terminology_id, terminology_set[] terminology_sets) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        public TermBindingSet GetBindings(string a_terminology_id, TermBindingSet[] terminology_sets)
        {
            foreach (TermBindingSet ts in terminology_sets)
            {
                if (ts.terminology == a_terminology_id)
                {
                    return ts;
                }
            }
            return null;
        }

        //public terminology_set GetBindings(string a_terminology_id, terminology_set[] terminology_sets) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        public ConstraintBindingSet GetBindings(string a_terminology_id, ConstraintBindingSet[] terminology_sets)
        {
            foreach (ConstraintBindingSet ts in terminology_sets)
            {
                if (ts.terminology == a_terminology_id)
                {
                    return ts;
                }
            }
            return null;
        }

        private System.Collections.ArrayList CodesUsed(ARCHETYPE an_archetype)
        {
            System.Collections.ArrayList result = new System.Collections.ArrayList();
            result.Add(an_archetype.concept);
            //Get all ac and at codes used in the archetype
            AddTerms(result, an_archetype.definition);
            return result;
        }

        private void AddTerms(System.Collections.ArrayList result, C_OBJECT obj)
        {
            if (obj.GetType() == typeof(C_COMPLEX_OBJECT))
            {
                C_COMPLEX_OBJECT co = (C_COMPLEX_OBJECT)obj;
                //Add the node id
                //if(obj.node_id != null && !result.Contains(obj.node_id))
                if (!string.IsNullOrEmpty(obj.node_id) && !result.Contains(obj.node_id)) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                    result.Add(obj.node_id);

                if (co.attributes != null)
                    foreach (C_ATTRIBUTE attribute in co.attributes)
                        if (attribute.children != null)
                            foreach (C_OBJECT obj_1 in attribute.children)
                                AddTerms(result, obj_1);
            }
            else if (obj.GetType() == typeof(ARCHETYPE_SLOT))
            {
                if (!string.IsNullOrEmpty(obj.node_id) && !result.Contains(obj.node_id))
                    result.Add(obj.node_id);
            }
            //check for internal codes
            else if(obj.GetType() == typeof(C_CODE_PHRASE))
            {
                C_CODE_PHRASE ct = (C_CODE_PHRASE)obj;
               //Check reference 
                
                //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                //if (ct.terminology.ToLowerInvariant() == "local" && ct.code_list != null)
                if (ct.terminology_id != null)                 
                {
                    if (ct.terminology_id.value.ToString().ToLowerInvariant() == "local" && ct.code_list != null)
                    {
                        foreach (string code in ct.code_list)
                        {
                            if (!result.Contains(code))
                                result.Add(code);
                        }
                    }
                }
            }
                
                //Ordinals use internal codes

            else if (obj.GetType() == typeof(C_DV_ORDINAL))
            {
                C_DV_ORDINAL co = (C_DV_ORDINAL)obj;
                if (co.list != null && co.list.Length > 0)
                {
                    foreach (DV_ORDINAL o in co.list)
                    {
                        //if (o.symbol != null && o.symbol.code_string != null)
                        if(o.symbol != null && o.symbol.defining_code != null)
                        {
                            //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
                            //if (!result.Contains(o.symbol.code_string )) 
                                //result.Add(o.symbol.code_string); 
                            if (!result.Contains(o.symbol.defining_code.code_string))
                                result.Add(o.symbol.defining_code.code_string); 
                        }
                    }
                }
            }

                //Constraint references us acXXXX codes

            else if (obj.GetType() == typeof(CONSTRAINT_REF))
            {
                CONSTRAINT_REF cr = (CONSTRAINT_REF)obj;
                if (cr.reference != null)
                {
                    if (!result.Contains(cr.reference))
                        result.Add(cr.reference);
                }
            }
        }

        public void RemoveUnusedCodes()
        {
            System.Collections.ArrayList termsUsed = CodesUsed(_archetype);
           
            foreach (string language in this.AvailableLanguages())
            {
                RemoveUnusedTerms(TermDefinitions(language), termsUsed);
                RemoveUnusedTerms(ConstraintDefinitions(language), termsUsed);
            }
            RemoveUnusedTerminologies();
        }

        private void RemoveUnusedTerminologies()
        {
            if (_archetype.ontology.term_bindings != null)
            {
                _archetype.ontology.term_bindings = PackTerminologySets(_archetype.ontology.term_bindings);
            }

            if (_archetype.ontology.constraint_bindings != null)
            {
                _archetype.ontology.constraint_bindings = PackTerminologySets(_archetype.ontology.constraint_bindings);
            }
            populate_terminology_list();
        }

        //private terminology_set[] PackTerminologySets(terminology_set[] terminologySets) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        private TermBindingSet[] PackTerminologySets(TermBindingSet[] terminologySets)
        {
            TermBindingSet t;
            bool removeBinding = true;
            System.Collections.ArrayList tempTerminologySets =
                new System.Collections.ArrayList(terminologySets);
            bool resetBindings = false;

            for (int i = 0; i < terminologySets.Length; i++)
            {
                t = terminologySets[i];
                if (t.items == null || t.items.Length == 0)
                {
                    tempTerminologySets.Remove(t);
                    resetBindings = true;
                }
                else
                {
                    removeBinding = false;
                }
            }
            if (removeBinding)
            {
                //No bindings with terms etc so remove term bindings completely
                return null;
            }
            else
            {
                //If there has been a term binding removed as it is empty
                if (resetBindings)
                {
                    return tempTerminologySets.ToArray(typeof(TermBindingSet)) as TermBindingSet[];
                }
                else
                    return terminologySets;
            }
        }

        //private terminology_set[] PackTerminologySets(terminology_set[] terminologySets) //JAR: 30APR2007, AE-42 Support XML Schema 1.0.1
        private ConstraintBindingSet[] PackTerminologySets(ConstraintBindingSet[] terminologySets)
        {
            ConstraintBindingSet t;
            bool removeBinding = true;
            System.Collections.ArrayList tempTerminologySets =
                new System.Collections.ArrayList(terminologySets);
            bool resetBindings = false;

            for (int i = 0; i < terminologySets.Length; i++)
            {
                t = terminologySets[i];
                if (t.items == null || t.items.Length == 0)
                {
                    tempTerminologySets.Remove(t);
                    resetBindings = true;
                }
                else
                {
                    removeBinding = false;
                }
            }
            if (removeBinding)
            {
                //No bindings with terms etc so remove term bindings completely
                return null;
            }
            else
            {
                //If there has been a term binding removed as it is empty
                if (resetBindings)
                {
                    return tempTerminologySets.ToArray(typeof(ConstraintBindingSet)) as ConstraintBindingSet[];
                }
                else
                    return terminologySets;
            }
        }

        //private void RemoveTermsByIndex(language_set languageSet, int[] indexes)
        private void RemoveTermsByIndex(CodeDefinitionSet languageSet, int[] indexes)
        {
            System.Collections.ArrayList temp = new System.Collections.ArrayList(languageSet.items);

            foreach (int i in indexes)
            {
                temp.RemoveAt(i);
            }
            languageSet.items = (ARCHETYPE_TERM[])temp.ToArray(typeof(ARCHETYPE_TERM));
        }

        //private void RemoveUnusedTerms(language_set a_language_set, System.Collections.ArrayList termsUsed)
        private void RemoveUnusedTerms(CodeDefinitionSet a_language_set, System.Collections.ArrayList termsUsed)
        {
            System.Collections.ArrayList indexOfTermsNotUsed = new System.Collections.ArrayList();

            if (a_language_set != null)
            {
                for (int i = 0; i < a_language_set.items.Length; i++)
                {
                    ARCHETYPE_TERM at = a_language_set.items[i];
                    if (!termsUsed.Contains(at.code))
                    {
                        indexOfTermsNotUsed.Add(i);
                    }
                }
                if (indexOfTermsNotUsed.Count > 0)
                {
                    indexOfTermsNotUsed.Reverse(); //Ensures the higher indexes are removed first
                    RemoveTermsByIndex(a_language_set, (int[])indexOfTermsNotUsed.ToArray(typeof(int)));
                }
            }
        }

        //private language_set ConstraintDefinitions(string a_language_code)
        private CodeDefinitionSet ConstraintDefinitions(string a_language_code)
        {
            if (_archetype.ontology.constraint_definitions != null)
            {
                foreach (CodeDefinitionSet ls in _archetype.ontology.constraint_definitions)
                {
                    if (ls.language == a_language_code)
                        return ls;
                }
            }
            return null;
        }
    }
}
