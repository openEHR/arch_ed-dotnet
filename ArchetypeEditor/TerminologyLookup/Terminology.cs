using System;
using System.Collections.Generic;
using System.Text;

namespace TerminologyLookup
{
    public class Terminology
    {
        public Terminology(string id)
        {
            terminologyId = id;
        }

        private readonly string terminologyId;

        public virtual string TerminologyId
        {
            get { return terminologyId; }
        }

        public readonly List<TerminologyQuery> Queries = new List<TerminologyQuery>();

        public virtual List<string> Languages { get; set; }
    }
}
