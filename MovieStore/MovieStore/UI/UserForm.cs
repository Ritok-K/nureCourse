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
    enum UserFormMode
    {
        NewLogging,
        NewUser,
        EditUser,
        ViewUser,
    }

    public partial class UserForm : Form
    {
        UserFormMode Mode { get; set; } = UserFormMode.NewLogging;
        Data.User User { get; set; } = new Data.User();

        public UserForm()
        {
            InitializeComponent();
            UpdateControls();
            UpdateData(false);
        }

        internal void SetMode(UserFormMode mode, Data.User user)
        {
            Mode = mode;

            if (mode == UserFormMode.EditUser)
            {
                User = user;
            }

            UpdateControls();
            UpdateData(false);
        }

        void UpdateControls()
        {
            switch(Mode)
            {
                case UserFormMode.NewLogging:
                    Text = "Add new user and logging";
                    m_okButton.Text = "Create && Loggin";
                    break;
                case UserFormMode.NewUser:
                    Text = "Add new user";
                    m_okButton.Text = "Add user";
                    break;
                case UserFormMode.EditUser:
                    Text = "Update user's data";
                    m_okButton.Text = "Update user";
                    break;
                case UserFormMode.ViewUser:
                    Text = "View user's data";
                    m_okButton.Text = "Ok";
                    break;
            }

            var isViewMode = Mode == UserFormMode.ViewUser;
            m_userRadioButton.Enabled = !isViewMode;
            m_managerRadioButton.Enabled = !isViewMode;
            m_password1TextBox.ReadOnly = isViewMode;
            m_password2TextBox.ReadOnly = isViewMode;
            m_firstNameTextBox.ReadOnly = isViewMode;
            m_secondNameTextBox.ReadOnly = isViewMode;
            m_loginTextBox.ReadOnly = isViewMode;
        }

        void UpdateData(bool save)
        {
            if (save)
            {
                if (string.IsNullOrEmpty(m_loginTextBox.Text))
                {
                    throw new Exception("E-mail should not be empty");
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

                User.FirstName = m_firstNameTextBox.Text;
                User.SecondName = m_secondNameTextBox.Text;
                User.EMail = m_loginTextBox.Text;
                User.Role = m_userRadioButton.Checked ? Data.UserRole.User : Data.UserRole.Manager;

                User.SetPassword(m_password1TextBox.Text);
            }
            else
            {
                m_loginTextBox.Text = User.EMail;
                m_firstNameTextBox.Text = User.FirstName;
                m_secondNameTextBox.Text = User.SecondName;
                m_userRadioButton.Checked = User.Role == Data.UserRole.User;
                m_managerRadioButton.Checked = User.Role == Data.UserRole.Manager;
            }
        }

        private void OnOk(object sender, EventArgs e)
        {
            try
            {
                switch (Mode)
                {
                    case UserFormMode.NewLogging:
                        UpdateData(true);
                        Program.DB.LoginAsNewUser(User);
                        DialogResult = DialogResult.OK;
                        break;

                    case UserFormMode.NewUser:
                        UpdateData(true);
                        Program.DB.AddUsers(new Data.User[] { User });
                        DialogResult = DialogResult.OK;
                        break;

                    case UserFormMode.EditUser:
                        UpdateData(true);
                        Program.DB.UpdateUsers(new Data.User[] { User });
                        DialogResult = DialogResult.OK;
                        break;

                    case UserFormMode.ViewUser:
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
