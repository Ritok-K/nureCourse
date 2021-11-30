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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void RefreshMoviesTable()
        {
            var movies = Program.DB.GetMovies();
        }

        void UpdateLayout()
        {

        }

        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                using (var loginForm = new LoginForm())
                {
                    var result = loginForm.ShowDialog(this);
                    if (result == DialogResult.OK)
                    {
                        UpdateLayout();
                        RefreshMoviesTable();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
