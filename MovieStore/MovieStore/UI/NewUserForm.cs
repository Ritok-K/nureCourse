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
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        Data.User BuildUser()
        {
            if (string.IsNullOrEmpty(m_loginTextBox.Text))
            {
                throw new Exception("Login should not be empty");
            }

            if (string.IsNullOrEmpty(m_firstNameTextBox.Text))
            {
                throw new Exception("First name should not be empty");
            }

            if (string.IsNullOrEmpty(m_password1TextBox.Text) ||
                string.IsNullOrEmpty(m_password2TextBox.Text))
            {
                throw new Exception("Password should not be empty");
            }

            if (!m_password1TextBox.Text.Equals(m_password2TextBox.Text))
            {
                throw new Exception("Both password fields should be the same");
            }

            var user = new Data.User()
            {
                FirstName = m_firstNameTextBox.Text,
                SecondName = m_secondNameTextBox.Text,
                EMail = m_loginTextBox.Text,
                Role = m_userRadioButton.Checked ? Data.UserRole.User : Data.UserRole.Manager,
            };
            user.SetPassword(m_password1TextBox.Text);

            return user;
        }

        private void OnOk(object sender, EventArgs e)
        {
            try
            {
                var user = BuildUser();

                Program.DB.LoginAsNewUser(user);

                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
