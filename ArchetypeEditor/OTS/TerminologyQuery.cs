using System;
using System.Collections.Generic;
using System.Text;

namespace Ots
{
    public class TerminologyQuery
    {
        public TerminologyQuery(string id)
        {
            queryId = id;
        }

        protected readonly string queryId;

        public virtual string QueryId
        {
            get { return queryId; }
        }

        protected string description;

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        protected string language;

        public virtual string Language
        {
            get { return language; }
            set { language = value; }
        }
    }
}
