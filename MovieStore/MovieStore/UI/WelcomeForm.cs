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
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void OnLogin(object sender, EventArgs e)
        {
            using(var loginForm = new LoginForm())
            {
                var resp = loginForm.ShowDialog(this);

                DialogResult = resp;
            }
        }
    }
}
