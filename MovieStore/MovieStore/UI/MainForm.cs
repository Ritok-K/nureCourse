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

        private void OnLoad(object sender, EventArgs e)
        {
            using (var loginForm = new LoginForm())
            {
                var result = loginForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {

                }
                else
                {
                    Close();
                }
            }
        }
    }
}
