using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace TerminologyLookup
{
    public partial class TerminologySelectionForm : Form
    {
        public TerminologySelectionForm(TerminologySelection service)
        {
            Service = service;
            InitializeComponent();
        }

        protected readonly TerminologySelection Service;

        private string terminologyId;

        public virtual string TerminologyId
        {
            get { return terminologyId; }
            set { terminologyId = value; }
        }

        private string subsetId;

        public virtual string SubsetId
        {
            get { return subsetId; }
            set { subsetId = value; }
        }

        private string subsetLanguage;

        public virtual string SubsetLanguage
        {
            get { return subsetLanguage; }
            set { subsetLanguage = value; }
        }

        protected readonly Stack<string> referenceIds = new Stack<string>();

        public virtual string ReferenceId
        {
            get
            {
                return referenceIds.Count > 0 ? referenceIds.Peek() : "";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    referenceIds.Push(value);
            }
        }

        private string termText;

        public virtual string TermText
        {
            get { return termText; }
        }

        protected virtual string SelectedValue(string columnName, string defaultValue)
        {
            string result = defaultValue;

            if (Grid.CurrentRow != null && Grid.Columns.Contains(columnName))
            {
                result = Grid.CurrentRow.Cells[columnName].Value as string;
            }

            return result;
        }

        protected virtual void ApplySelection()
        {
            TerminologyId = SelectedValue("TerminologyId", TerminologyId);
            SubsetId = SelectedValue("QueryId", SubsetId);
            SubsetLanguage = SelectedValue("Language", SubsetLanguage);
            termText = SelectedValue("TermText", termText);
            ReferenceId = SelectedValue("ReferenceId", "");
        }

        protected virtual void PopulateGrid()
        {
            ProgressBar.Visible = false;
            OkButton.Enabled = true;

            if (string.IsNullOrEmpty(TerminologyId))
            {
                Text = "Select a Terminology";
                Grid.DataSource = null;
                Grid.DataSource = Service.Terminologies;

                if (Grid.Columns.Count > 0)
                {
                    Grid.Columns[0].HeaderText = "Terminology";
                }
            }
            else if (string.IsNullOrEmpty(SubsetId) || string.IsNullOrEmpty(SubsetLanguage))
            {
                Text = "Select a Subset for Terminology " + TerminologyId;
                Grid.DataSource = null;

                Terminology terminology = Service.TerminologyById(TerminologyId);

                if (terminology != null)
                {
                    List<TerminologyQuery> subsets = terminology.Queries;

                    if (subsets.Count > 0)
                    {
                        Grid.DataSource = terminology.Queries;

                        if (Grid.Columns.Count > 0)
                            Grid.Columns[0].HeaderText = "Subset";
                    }
                    else
                    {
                        if (terminology.Languages == null)
                            Service.LoadTerminologyLanguages(terminology, ShowException);

                        if (terminology.Languages.Count > 0)
                        {
                            Text = "Concepts for Terminology " + TerminologyId + " (no subsets have been defined for this terminology)";
                            ProgressBar.Visible = true;

                            if (string.IsNullOrEmpty(ReferenceId))
                                Service.LoadConcepts(OnConceptsLoaded, ShowException, TerminologyId, terminology.Languages[0]);
                            else
                                Service.LoadChildConcepts(OnConceptsLoaded, ShowException, TerminologyId, terminology.Languages[0], ReferenceId);
                        }
                    }
                }
            }
            else
            {
                Text = "Concepts for Terminology " + TerminologyId + " (" + SubsetId + ")";
                ProgressBar.Visible = true;

                if (string.IsNullOrEmpty(ReferenceId))
                    Service.LoadConceptsFromSubset(OnConceptsLoaded, ShowException, TerminologyId, SubsetId, SubsetLanguage);
                else
                    Service.LoadChildConceptsFromSubset(OnConceptsLoaded, ShowException, TerminologyId, SubsetId, SubsetLanguage, ReferenceId);
            }
        }

        protected virtual void OnConceptsLoaded(DataSet concepts)
        {
            ProgressBar.Visible = false;
            Grid.DataSource = null;
            Grid.DataSource = concepts != null && concepts.Tables != null && concepts.Tables.Count > 0 ? concepts.Tables[0] : null;
        }

        protected virtual void ShowException(Exception ex)
        {
            ProgressBar.Visible = false;
            MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message, Service.Name + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void Form_Shown(object sender, EventArgs e)
        {
            try
            {
                referenceIds.Clear();
                ProgressBar.Visible = true;
                OkButton.Enabled = false;
                Grid.Focus();
                PopulateGrid();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            ApplySelection();
        }

        protected void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BrowseButton.PerformClick();
        }

        protected void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    e.SuppressKeyPress = true;

                    if (e.Modifiers == Keys.None)
                        BackButton.Focus();
                    else if (e.Modifiers == Keys.Shift)
                        CancelCloseButton.Focus();

                    break;
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    BrowseButton.PerformClick();
                    break;
                case Keys.Back:
                    e.SuppressKeyPress = true;
                    BackButton.PerformClick();
                    break;
            }
        }

        protected void BrowseButton_Click(object sender, EventArgs e)
        {
            ApplySelection();
            PopulateGrid();
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            if (referenceIds.Count > 0)
                referenceIds.Pop();
            else if (!string.IsNullOrEmpty(SubsetId))
                SubsetId = "";
            else
                TerminologyId = "";

            PopulateGrid();
        }
    }
}
