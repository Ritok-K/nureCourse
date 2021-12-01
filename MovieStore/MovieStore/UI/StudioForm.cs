using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieStore.UI
{
    enum StudioFormMode
    {
        NewStudio,
        EditStudio,
        ViewStudio,
    }
    public partial class StudioForm : Form
    {
        StudioFormMode Mode { get; set; } = StudioFormMode.NewStudio;
        Data.Studio Studio { get; set; } = new Data.Studio();

        public StudioForm()
        {
            InitializeComponent();
        }

        internal void SetMode(StudioFormMode mode, Data.Studio studio)
        {
            Mode = mode;

            if (mode == StudioFormMode.EditStudio ||
                mode == StudioFormMode.ViewStudio)
            {
                Studio = studio;
            }
        }

        void InitControls()
        {
            UpdateControls();
            UpdateData(false);
        }

        void UpdateControls()
        {
            switch (Mode)
            {
                case StudioFormMode.NewStudio:
                    Text = "Add new studio";
                    m_okButton.Text = "Add studio";
                    break;
                case StudioFormMode.EditStudio:
                    Text = "Update studio";
                    m_okButton.Text = "Update studio";
                    break;
                case StudioFormMode.ViewStudio:
                    Text = "Studio";
                    m_okButton.Text = "Ok";
                    break;
            }

            var isViewMode = Mode == StudioFormMode.ViewStudio;
            m_titleTextBox.ReadOnly = isViewMode;
            m_countryTextBox.ReadOnly = isViewMode;
            m_foundationDateTextBox.ReadOnly = isViewMode;
            m_productionTextBox.ReadOnly = isViewMode;
        }

        void UpdateData(bool save)
        {
            if (save)
            {
                if (string.IsNullOrEmpty(m_titleTextBox.Text))
                {
                    throw new Exception("Title should not be empty");
                }

                Studio.Title = m_titleTextBox.Text;
                Studio.Country = m_countryTextBox.Text;
                Studio.FoundationDate = DateTime.ParseExact(m_foundationDateTextBox.Text, "yyyy", CultureInfo.CurrentUICulture);
                Studio.Production = m_productionTextBox.Text;
            }
            else
            {
                m_titleTextBox.Text = Studio.Title;
                m_countryTextBox.Text = Studio.Country;
                m_foundationDateTextBox.Text = Utility.UIPrimitiveFormatting.Format(Studio.FoundationDate, "yyyy");
                m_productionTextBox.Text = Studio.Production;
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            InitControls();
        }

        private void OnOk(object sender, EventArgs e)
        {
            try
            {
                switch (Mode)
                {
                    case StudioFormMode.NewStudio:
                        UpdateData(true);
                        Program.DB.AddStudio(new Data.Studio[] { Studio });
                        DialogResult = DialogResult.OK;
                        break;

                    case StudioFormMode.EditStudio:
                        UpdateData(true);
                        Program.DB.UpdateStudio(new Data.Studio[] { Studio });
                        DialogResult = DialogResult.OK;
                        break;

                    case StudioFormMode.ViewStudio:
                        DialogResult = DialogResult.Cancel; // Just view and cancel
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
