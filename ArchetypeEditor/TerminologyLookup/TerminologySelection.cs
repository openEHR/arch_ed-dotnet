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

        public virtual IList<TerminologyQuery> Queries(string terminologyId)
        {
            Terminology terminology = null;

            foreach (Terminology t in Terminologies)
                if (t.TerminologyId == terminologyId)
                    terminology = t;

            if (terminology == null)
            {
                terminology = new Terminology(terminologyId);
                terminologies.Add(terminology);
            }

            return terminology.Queries;
        }

        public delegate void OnLoaded(DataSet concepts);

        public delegate void OnError(Exception ex);

        public abstract void LoadConcepts(OnLoaded onLoaded, OnError onError, string terminologyId, string queryId, string language);

        public abstract void LoadChildConcepts(OnLoaded onLoaded, OnError onError, string terminologyId, string queryId, string language, string parentConcept);

        public abstract void LoadPreferredTerms(OnLoaded onLoaded, OnError onError, string terminologyId, string language, string[] conceptIds);

        public abstract TermLookupController NewTermLookupController { get; }
    }
}
