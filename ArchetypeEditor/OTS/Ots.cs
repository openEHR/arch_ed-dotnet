using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace Ots
{
    public class Ots
    {
        public static readonly Ots Once = new Ots();

        public Ots()
        {
            WebService = new OtsWebService.OTSWebService();
        }

        public readonly OtsWebService.OTSWebService WebService;

        public delegate void OnError(object sender, AsyncCompletedEventArgs e);

        public virtual string Url
        {
            get { return WebService.Url; }
            set
            {
                if (WebService.Url != value)
                {
                    WebService.Url = value;
                    terminologies = null;
                }
            }
        }

        protected List<Terminology> terminologies;

        public virtual IEnumerable<Terminology> Terminologies
        {
            get
            {
                if (terminologies == null)
                {
                    terminologies = new List<Terminology>();

                    foreach (OtsWebService.QueryRegistrationView q in WebService.GetRegisteredQueries())
                    {
                        TerminologyQuery query = new TerminologyQuery(q.QueryName);
                        query.Description = q.Description;
                        query.Language = q.Language;
                        Queries(q.TerminologyName).Add(query);
                    }
                }

                return terminologies;
            }
        }

        public virtual IList<TerminologyQuery> Queries(string terminologyId)
        {
            Terminology terminology = null;

            foreach (Terminology t in terminologies)
                if (t.TerminologyId == terminologyId)
                    terminology = t;

            if (terminology == null)
            {
                terminology = new Terminology(terminologyId);
                terminologies.Add(terminology);
            }

            return terminology.Queries;
        }

        public delegate void OnConceptsLoaded(DataSet concepts);

        public virtual void LoadConcepts(OnConceptsLoaded onLoaded, OnError onError, string terminologyId, string queryId, string language)
        {
            OtsWebService.QueryOpenCompletedEventHandler handler = null;

            handler = delegate(object sender, OtsWebService.QueryOpenCompletedEventArgs e)
            {
                WebService.QueryOpenCompleted -= handler;

                if (e.Error != null)
                {
                    if (onError != null)
                        onError(this, e);
                }
                else if (onLoaded != null)
                    onLoaded(e.Result);
            };

            WebService.QueryOpenCompleted += handler;
            WebService.QueryOpenAsync(terminologyId, queryId, language);
        }

        public virtual void LoadChildConcepts(OnConceptsLoaded onLoaded, OnError onError, string terminologyId, string queryId, string language, string parentConcept)
        {
            OtsWebService.QueryGoDownCompletedEventHandler handler = null;

            handler = delegate(object sender, OtsWebService.QueryGoDownCompletedEventArgs e)
            {
                WebService.QueryGoDownCompleted -= handler;

                if (e.Error != null)
                {
                    if (onError != null)
                        onError(this, e);
                }
                else if (onLoaded != null)
                    onLoaded(e.Result);
            };

            OtsWebService.OTSNodeRef nodeRef = new OtsWebService.OTSNodeRef();
            nodeRef.Id = parentConcept;
            WebService.QueryGoDownCompleted += handler;
            WebService.QueryGoDownAsync(terminologyId, queryId, language, nodeRef);
        }
    }
}
