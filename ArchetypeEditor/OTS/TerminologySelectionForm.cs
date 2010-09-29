using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Ots
{
    public partial class TerminologySelectionForm : Form
    {
        public TerminologySelectionForm()
        {
            InitializeComponent();
        }

        public virtual string Url
        {
            get { return Ots.Once.Url; }
            set { Ots.Once.Url = value; }
        }

        protected string terminologyId;

        public virtual string TerminologyId
        {
            get { return terminologyId; }
            set { terminologyId = value; }
        }

        protected string subsetId;

        public virtual string SubsetId
        {
            get { return subsetId; }
            set { subsetId = value; }
        }

        protected string subsetLanguage;

        protected readonly Stack<string> referenceIds = new Stack<string>();

        protected virtual string SelectedValue(string columnName, string defaultValue)
        {
            string result = defaultValue;

            if (Grid.Columns.Contains(columnName))
            {
                result = Grid.CurrentRow.Cells[columnName].Value as string;
            }

            return result;
        }

        protected virtual void ApplySelection()
        {
            TerminologyId = SelectedValue("TerminologyId", TerminologyId);
            SubsetId = SelectedValue("QueryId", SubsetId);
            subsetLanguage = SelectedValue("Language", subsetLanguage);
            string referenceId = SelectedValue("ReferenceId", "");

            if (!string.IsNullOrEmpty(referenceId))
                referenceIds.Push(referenceId);
        }

        protected virtual void PopulateGrid()
        {
            ProgressBar.Visible = false;
            OkButton.Enabled = true;

            if (string.IsNullOrEmpty(TerminologyId))
            {
                Text = "Select a Terminology";
                Grid.DataSource = null;
                Grid.DataSource = Ots.Once.Terminologies;

                if (Grid.Columns.Count > 0)
                {
                    Grid.Columns[0].HeaderText = "Terminology";
                }
            }
            else if (string.IsNullOrEmpty(SubsetId))
            {
                Text = "Select a Subset for Terminology " + TerminologyId;
                Grid.DataSource = null;
                Grid.DataSource = Ots.Once.Queries(TerminologyId);

                if (Grid.Columns.Count > 0)
                {
                    Grid.Columns[0].HeaderText = "Subset";
                }
            }
            else
            {
                Ots.OnConceptsLoaded handler = delegate(DataSet concepts)
                {
                    ProgressBar.Visible = false;
                    Grid.DataSource = null;
                    Grid.DataSource = concepts != null && concepts.Tables != null && concepts.Tables.Count > 0 ? concepts.Tables[0] : null;
                };

                Text = "Concepts for Terminology " + TerminologyId + " (" + SubsetId + ")";
                ProgressBar.Visible = true;

                if (referenceIds.Count == 0)
                    Ots.Once.LoadConcepts(handler, OnOtsError, TerminologyId, SubsetId, subsetLanguage);
                else
                    Ots.Once.LoadChildConcepts(handler, OnOtsError, TerminologyId, SubsetId, subsetLanguage, referenceIds.Peek());
            }
        }

        protected virtual void OnOtsError(object sender, AsyncCompletedEventArgs e)
        {
            ProgressBar.Visible = false;
            ShowException(e.Error);
        }

        protected virtual void ShowException(Exception ex)
        {
            MessageBox.Show(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
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
