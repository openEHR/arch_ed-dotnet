using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TerminologyLookup
{
    public abstract class TermLookupController
    {
        public abstract Control Control { get; }

        public abstract string TerminologyName { get; set; }

        public abstract string QueryName { get; set; }

        public abstract string Language { get; set; }

        public abstract string TermName { get; set; }

        public abstract string ConceptId { get; }

        public event EventHandler TermChanged;

        protected virtual void DoTermChanged(object sender, EventArgs e)
        {
            if (TermChanged != null)
                TermChanged(sender, e);
        }
    }
}
