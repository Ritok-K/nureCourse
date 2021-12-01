using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        //void InitControls()
        //{
        //    var studio = Program.DB.GetStudio();
        //    m_studioComboBox.DataSource = studio;
        //    m_studioComboBox.DisplayMember = nameof(Data.Studio.Title);
        //    m_studioComboBox.ValueMember = nameof(Data.Studio.Id);

        //    UpdateControls();
        //    UpdateData(false);
        //}

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

            //var isViewMode = Mode == MovieFormMode.ViewMovie;
            //m_studioComboBox.Enabled = !isViewMode;
            //m_titleTextBox.ReadOnly = isViewMode;
            //m_countryTextBox.ReadOnly = isViewMode;
            //m_yearTextBox.ReadOnly = isViewMode;
            //m_descriptionTextBox.ReadOnly = isViewMode;
            //m_genreTextBox.ReadOnly = isViewMode;
            //m_priceTextBox.ReadOnly = isViewMode;
            //m_imdbTextBox.ReadOnly = isViewMode;
        }

    }
}
