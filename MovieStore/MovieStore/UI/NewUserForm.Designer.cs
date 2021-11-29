
namespace MovieStore.UI
{
    partial class NewUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_loginLabel = new System.Windows.Forms.Label();
            this.m_firstNameLabel = new System.Windows.Forms.Label();
            this.m_secondNameLabel = new System.Windows.Forms.Label();
            this.m_roleLabel = new System.Windows.Forms.Label();
            this.m_loginTextBox = new System.Windows.Forms.TextBox();
            this.m_firstNameTextBox = new System.Windows.Forms.TextBox();
            this.m_secondNameTextBox = new System.Windows.Forms.TextBox();
            this.m_userRadioButton = new System.Windows.Forms.RadioButton();
            this.m_managerRadioButton = new System.Windows.Forms.RadioButton();
            this.m_passwordLabel = new System.Windows.Forms.Label();
            this.m_password1TextBox = new System.Windows.Forms.TextBox();
            this.m_password2TextBox = new System.Windows.Forms.TextBox();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_loginLabel
            // 
            this.m_loginLabel.AutoSize = true;
            this.m_loginLabel.Location = new System.Drawing.Point(28, 26);
            this.m_loginLabel.Name = "m_loginLabel";
            this.m_loginLabel.Size = new System.Drawing.Size(106, 20);
            this.m_loginLabel.TabIndex = 0;
            this.m_loginLabel.Text = "Login (e-mail):";
            // 
            // m_firstNameLabel
            // 
            this.m_firstNameLabel.AutoSize = true;
            this.m_firstNameLabel.Location = new System.Drawing.Point(28, 73);
            this.m_firstNameLabel.Name = "m_firstNameLabel";
            this.m_firstNameLabel.Size = new System.Drawing.Size(80, 20);
            this.m_firstNameLabel.TabIndex = 1;
            this.m_firstNameLabel.Text = "First name:";
            // 
            // m_secondNameLabel
            // 
            this.m_secondNameLabel.AutoSize = true;
            this.m_secondNameLabel.Location = new System.Drawing.Point(28, 120);
            this.m_secondNameLabel.Name = "m_secondNameLabel";
            this.m_secondNameLabel.Size = new System.Drawing.Size(102, 20);
            this.m_secondNameLabel.TabIndex = 2;
            this.m_secondNameLabel.Text = "Second name:";
            // 
            // m_roleLabel
            // 
            this.m_roleLabel.AutoSize = true;
            this.m_roleLabel.Location = new System.Drawing.Point(28, 169);
            this.m_roleLabel.Name = "m_roleLabel";
            this.m_roleLabel.Size = new System.Drawing.Size(96, 20);
            this.m_roleLabel.TabIndex = 3;
            this.m_roleLabel.Text = "Account role:";
            // 
            // m_loginTextBox
            // 
            this.m_loginTextBox.Location = new System.Drawing.Point(149, 23);
            this.m_loginTextBox.Name = "m_loginTextBox";
            this.m_loginTextBox.PlaceholderText = "your e-mail";
            this.m_loginTextBox.Size = new System.Drawing.Size(270, 27);
            this.m_loginTextBox.TabIndex = 4;
            // 
            // m_firstNameTextBox
            // 
            this.m_firstNameTextBox.Location = new System.Drawing.Point(149, 70);
            this.m_firstNameTextBox.Name = "m_firstNameTextBox";
            this.m_firstNameTextBox.Size = new System.Drawing.Size(270, 27);
            this.m_firstNameTextBox.TabIndex = 5;
            // 
            // m_secondNameTextBox
            // 
            this.m_secondNameTextBox.Location = new System.Drawing.Point(149, 117);
            this.m_secondNameTextBox.Name = "m_secondNameTextBox";
            this.m_secondNameTextBox.Size = new System.Drawing.Size(270, 27);
            this.m_secondNameTextBox.TabIndex = 6;
            // 
            // m_userRadioButton
            // 
            this.m_userRadioButton.AutoSize = true;
            this.m_userRadioButton.Checked = true;
            this.m_userRadioButton.Location = new System.Drawing.Point(149, 169);
            this.m_userRadioButton.Name = "m_userRadioButton";
            this.m_userRadioButton.Size = new System.Drawing.Size(93, 24);
            this.m_userRadioButton.TabIndex = 7;
            this.m_userRadioButton.TabStop = true;
            this.m_userRadioButton.Text = "Customer";
            this.m_userRadioButton.UseVisualStyleBackColor = true;
            // 
            // m_managerRadioButton
            // 
            this.m_managerRadioButton.AutoSize = true;
            this.m_managerRadioButton.Location = new System.Drawing.Point(149, 199);
            this.m_managerRadioButton.Name = "m_managerRadioButton";
            this.m_managerRadioButton.Size = new System.Drawing.Size(89, 24);
            this.m_managerRadioButton.TabIndex = 8;
            this.m_managerRadioButton.TabStop = true;
            this.m_managerRadioButton.Text = "Manager";
            this.m_managerRadioButton.UseVisualStyleBackColor = true;
            // 
            // m_passwordLabel
            // 
            this.m_passwordLabel.AutoSize = true;
            this.m_passwordLabel.Location = new System.Drawing.Point(35, 241);
            this.m_passwordLabel.Name = "m_passwordLabel";
            this.m_passwordLabel.Size = new System.Drawing.Size(73, 20);
            this.m_passwordLabel.TabIndex = 9;
            this.m_passwordLabel.Text = "Password:";
            // 
            // m_password1TextBox
            // 
            this.m_password1TextBox.Location = new System.Drawing.Point(149, 238);
            this.m_password1TextBox.Name = "m_password1TextBox";
            this.m_password1TextBox.PlaceholderText = "your password";
            this.m_password1TextBox.Size = new System.Drawing.Size(270, 27);
            this.m_password1TextBox.TabIndex = 10;
            // 
            // m_password2TextBox
            // 
            this.m_password2TextBox.Location = new System.Drawing.Point(149, 271);
            this.m_password2TextBox.Name = "m_password2TextBox";
            this.m_password2TextBox.PlaceholderText = "enter passwrod again";
            this.m_password2TextBox.Size = new System.Drawing.Size(270, 27);
            this.m_password2TextBox.TabIndex = 11;
            // 
            // m_okButton
            // 
            this.m_okButton.Location = new System.Drawing.Point(149, 340);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(155, 29);
            this.m_okButton.TabIndex = 12;
            this.m_okButton.Text = "Create && Login";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOk);
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(325, 340);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(94, 29);
            this.m_cancelButton.TabIndex = 13;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // NewUserForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(441, 387);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_password2TextBox);
            this.Controls.Add(this.m_password1TextBox);
            this.Controls.Add(this.m_passwordLabel);
            this.Controls.Add(this.m_managerRadioButton);
            this.Controls.Add(this.m_userRadioButton);
            this.Controls.Add(this.m_secondNameTextBox);
            this.Controls.Add(this.m_firstNameTextBox);
            this.Controls.Add(this.m_loginTextBox);
            this.Controls.Add(this.m_roleLabel);
            this.Controls.Add(this.m_secondNameLabel);
            this.Controls.Add(this.m_firstNameLabel);
            this.Controls.Add(this.m_loginLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New account";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_loginLabel;
        private System.Windows.Forms.Label m_firstNameLabel;
        private System.Windows.Forms.Label m_secondNameLabel;
        private System.Windows.Forms.Label m_roleLabel;
        private System.Windows.Forms.TextBox m_loginTextBox;
        private System.Windows.Forms.TextBox m_firstNameTextBox;
        private System.Windows.Forms.TextBox m_secondNameTextBox;
        private System.Windows.Forms.RadioButton m_userRadioButton;
        private System.Windows.Forms.RadioButton m_managerRadioButton;
        private System.Windows.Forms.Label m_passwordLabel;
        private System.Windows.Forms.TextBox m_password1TextBox;
        private System.Windows.Forms.TextBox m_password2TextBox;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
    }
}