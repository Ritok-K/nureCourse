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
    enum MovieFormMode
    {
        NewMovie,
        EditMovie,
        ViewMovie,
    }

    public partial class MovieForm : Form
    {
        MovieFormMode Mode { get; set; } = MovieFormMode.NewMovie;
        Data.Movie Movie { get; set; } = new Data.Movie();

        public MovieForm()
        {
            InitializeComponent();
        }

        internal void SetMode(MovieFormMode mode, Data.Movie movie)
        {
            Mode = mode;

            if (mode == MovieFormMode.EditMovie || 
                mode == MovieFormMode.ViewMovie)
            {
                Movie = movie;
            }
        }

        void InitControls()
        {
            var studio = Program.DB.GetStudio();
            m_studioComboBox.DataSource = studio;
            m_studioComboBox.DisplayMember = nameof(Data.Studio.Title);
            m_studioComboBox.ValueMember = nameof(Data.Studio.Id);

            UpdateControls();
            UpdateData(false);
        }

        void UpdateControls()
        {
            switch (Mode)
            {
                case MovieFormMode.NewMovie:
                    Text = "Add new movie";
                    m_okButton.Text = "Add movie";
                    break;
                case MovieFormMode.EditMovie:
                    Text = "Update movie";
                    m_okButton.Text = "Update movie";
                    break;
                case MovieFormMode.ViewMovie:
                    Text = "Movie";
                    m_okButton.Text = "Ok";
                    break;
            }

            var isViewMode = Mode == MovieFormMode.ViewMovie;
            m_okButton.Enabled = !isViewMode;
            m_studioComboBox.Enabled = !isViewMode;
            m_titleTextBox.ReadOnly = isViewMode;
            m_countryTextBox.ReadOnly = isViewMode;
            m_yearTextBox.ReadOnly = isViewMode;
            m_descriptionTextBox.ReadOnly = isViewMode;
            m_genreTextBox.ReadOnly = isViewMode;
            m_priceTextBox.ReadOnly = isViewMode;
        }

        void UpdateData(bool save)
        {
            if (save)
            {
                if (string.IsNullOrEmpty(m_titleTextBox.Text))
                {
                    throw new Exception("Title should not be empty");
                }

                Movie.Title = m_titleTextBox.Text;
                Movie.Genre = m_genreTextBox.Text;
                Movie.Year = DateTime.ParseExact(m_yearTextBox.Text, "yyyy", CultureInfo.CurrentUICulture);
                Movie.Description = m_descriptionTextBox.Text;
                Movie.Imdb = float.Parse(m_imdbTextBox.Text);
                Movie.Studio = (m_studioComboBox.SelectedIndex>=0) ?  new Data.Studio() { Id = (m_studioComboBox.SelectedItem as Data.Studio).Id } : null;
                Movie.Country = m_countryTextBox.Text;
                Movie.Price = (int)Math.Round(float.Parse(m_priceTextBox.Text) * 100);
            }
            else
            {
                m_titleTextBox.Text = Movie.Title;
                m_genreTextBox.Text = Movie.Genre;
                m_yearTextBox.Text = Utility.UIPrimitiveFormatting.Format(Movie.Year, "yyyy");
                m_descriptionTextBox.Text = Movie.Description;
                m_countryTextBox.Text = Movie.Country;
                m_priceTextBox.Text = Utility.UIPrimitiveFormatting.FormatPrice(Movie.Price);
                m_imdbTextBox.Text = Utility.UIPrimitiveFormatting.FormatImdb(Movie.Imdb);
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
                    case MovieFormMode.NewMovie:
                        UpdateData(true);
                        //Program.DB.AddUsers(new Data.User[] { User });
                        DialogResult = DialogResult.OK;
                        break;

                    case MovieFormMode.EditMovie:
                        UpdateData(true);
                        //Program.DB.UpdateUsers(new Data.User[] { User });
                        DialogResult = DialogResult.OK;
                        break;

                    case MovieFormMode.ViewMovie:
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
