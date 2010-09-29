using System;
using System.Collections.Generic;
using System.Text;

namespace Ots
{
    public class Terminology
    {
        public Terminology(string id)
        {
            terminologyId = id;
        }

        protected readonly string terminologyId;

        public virtual string TerminologyId
        {
            get { return terminologyId; }
        }

        public readonly List<TerminologyQuery> Queries = new List<TerminologyQuery>();
    }
}
