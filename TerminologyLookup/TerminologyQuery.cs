using System;
using System.Collections.Generic;
using System.Text;

namespace TerminologyLookup
{
    public class TerminologyQuery
    {
        public TerminologyQuery(string id)
        {
            queryId = id;
        }

        private readonly string queryId;

        public virtual string QueryId
        {
            get { return queryId; }
        }

        private string description;

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string language;

        public virtual string Language
        {
            get { return language; }
            set { language = value; }
        }
    }
}
