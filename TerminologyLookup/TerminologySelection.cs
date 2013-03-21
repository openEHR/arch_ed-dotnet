using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;

namespace TerminologyLookup
{
    public abstract class TerminologySelection
    {
        public abstract string Name { get; }

        public abstract string Url { get; set; }

        protected virtual void ClearTerminologies()
        {
            terminologies = null;
        }

        protected abstract void LoadTerminologies();

        private List<Terminology> terminologies;

        public virtual IEnumerable<Terminology> Terminologies
        {
            get
            {
                if (terminologies == null)
                {
                    terminologies = new List<Terminology>();
                    LoadTerminologies();
                }

                return terminologies;
            }
        }

        public virtual Terminology TerminologyById(string terminologyId)
        {
            Terminology result = null;

            foreach (Terminology t in Terminologies)
                if (t.TerminologyId == terminologyId)
                    result = t;

            return result;
        }

        public virtual void AddTerminology(string terminologyId)
        {
            Terminology terminology = TerminologyById(terminologyId);

            if (terminology == null)
            {
                terminology = new Terminology(terminologyId);
                terminologies.Add(terminology);
            }
        }

        public delegate void OnLoaded(DataSet concepts);

        public delegate void OnError(Exception ex);

        public abstract void LoadTerminologyLanguages(Terminology terminology, OnError onError);
        
        public abstract void LoadConcepts(OnLoaded onLoaded, OnError onError, string terminologyId, string language);

        public abstract void LoadChildConcepts(OnLoaded onLoaded, OnError onError, string terminologyId, string language, string parentConcept);

        public abstract void LoadConceptsFromSubset(OnLoaded onLoaded, OnError onError, string terminologyId, string queryId, string language);

        public abstract void LoadChildConceptsFromSubset(OnLoaded onLoaded, OnError onError, string terminologyId, string queryId, string language, string parentConcept);

        public abstract void LoadPreferredTerms(OnLoaded onLoaded, OnError onError, string terminologyId, string language, string[] conceptIds);

        public abstract TermLookupController NewTermLookupController { get; }
    }
}
