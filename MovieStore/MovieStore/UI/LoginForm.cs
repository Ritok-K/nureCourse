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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void OnOk(object sender, EventArgs e)
        {
            Close();
        }

        private void OnRegisterNewUser(object sender, EventArgs e)
        {
            using(var newUserForm = new NewUserForm())
            {
                newUserForm.ShowDialog(this);
            }
        }
    }
}
