﻿using System;
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
            try
            {
                Program.DB.Login(m_loginTextBox.Text, m_passwordTextBox.Text);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnRegisterNewUser(object sender, EventArgs e)
        {
            try
            {
                using (var newUserForm = new NewUserForm())
                {
                    //newUserForm.ShowDialog(this);

                    var user = new Data.User() { FirstName = "e", SecondName = "k", EMail = "kyselgov@gmail.com", Role = Data.UserRole.Manager };
                    user.SetPassword("test");

                    Program.DB.LoginAsNewUser(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
