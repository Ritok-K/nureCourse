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
    enum ActorFormMode
    {
        NewActor,
        EditActor,
        ViewActor,
    }

    public partial class ActorForm : Form
    {
        ActorFormMode Mode { get; set; } = ActorFormMode.NewActor;
        Data.Actor Actor { get; set; } = new Data.Actor();

        public ActorForm() 
        {
            InitializeComponent();
        }

        internal void SetMode(ActorFormMode mode, Data.Actor actor)
        {
            Mode = mode;

            if (mode == ActorFormMode.EditActor ||
                mode == ActorFormMode.ViewActor)
            {
                Actor = actor;
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
                case ActorFormMode.NewActor:
                    Text = "Add new actor";
                    m_okButton.Text = "Add actor";
                    break;
                case ActorFormMode.EditActor:
                    Text = "Update actor";
                    m_okButton.Text = "Update actor";
                    break;
                case ActorFormMode.ViewActor:
                    Text = "Actor";
                    m_okButton.Text = "Ok";
                    break;
            }

            var isViewMode = Mode == ActorFormMode.ViewActor;
            m_singleRadioButton.Enabled = !isViewMode;
            m_marriedRadioButton.Enabled = !isViewMode;
            m_firstNameTextBox.ReadOnly = isViewMode;
            m_secondNameTextBox.ReadOnly = isViewMode;
            m_birthDateTextBox.ReadOnly = isViewMode;
            m_countryTextBox.ReadOnly = isViewMode;
            m_awardsDescriptionTextBox.ReadOnly = isViewMode;
        }

        void UpdateData(bool save)
        {
            if (save)
            {
                if (string.IsNullOrEmpty(m_firstNameTextBox.Text))
                {
                    throw new Exception("First name should not be empty");
                }

                if (string.IsNullOrEmpty(m_secondNameTextBox.Text))
                {
                    throw new Exception("Second name should not be empty");
                }

                Actor.FirstName = m_firstNameTextBox.Text;
                Actor.SecondName = m_secondNameTextBox.Text;
                Actor.BirthDate = DateTime.ParseExact(m_birthDateTextBox.Text, "yyyy-mm-dd", CultureInfo.CurrentUICulture);
                Actor.FamilyStatus = m_singleRadioButton.Checked ? Data.ActorFamilyStatus.Married : Data.ActorFamilyStatus.Married;
                Actor.Country = m_countryTextBox.Text;
                Actor.AwardsDescription = m_awardsDescriptionTextBox.Text;
            }
            else
            {
                m_firstNameTextBox.Text = Actor.FirstName;
                m_secondNameTextBox.Text = Actor.SecondName;
                m_birthDateTextBox.Text = Utility.UIPrimitiveFormatting.Format(Actor.BirthDate, "yyyy-mm-dd");
                m_singleRadioButton.Checked = Actor.FamilyStatus == Data.ActorFamilyStatus.Single;
                m_marriedRadioButton.Checked = Actor.FamilyStatus == Data.ActorFamilyStatus.Married;
                m_countryTextBox.Text = Actor.Country;
                m_awardsDescriptionTextBox.Text = Actor.AwardsDescription;
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
                    case ActorFormMode.NewActor:
                        UpdateData(true);
                        Program.DB.AddActors(new Data.Actor[] { Actor });
                        DialogResult = DialogResult.OK;
                        break;

                    case ActorFormMode.EditActor:
                        UpdateData(true);
                        Program.DB.UpdateActors(new Data.Actor[] { Actor });
                        DialogResult = DialogResult.OK;
                        break;

                    case ActorFormMode.ViewActor:
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
