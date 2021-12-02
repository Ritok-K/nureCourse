
namespace MovieStore.UI
{
    partial class LoginForm
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
            this.m_passwordLabel = new System.Windows.Forms.Label();
            this.m_loginTextBox = new System.Windows.Forms.TextBox();
            this.m_passwordTextBox = new System.Windows.Forms.TextBox();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.m_registerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_loginLabel
            // 
            this.m_loginLabel.AutoSize = true;
            this.m_loginLabel.Location = new System.Drawing.Point(12, 18);
            this.m_loginLabel.Name = "m_loginLabel";
            this.m_loginLabel.Size = new System.Drawing.Size(136, 20);
            this.m_loginLabel.TabIndex = 0;
            this.m_loginLabel.Text = "Your login (e-mail):";
            // 
            // m_passwordLabel
            // 
            this.m_passwordLabel.AutoSize = true;
            this.m_passwordLabel.Location = new System.Drawing.Point(12, 60);
            this.m_passwordLabel.Name = "m_passwordLabel";
            this.m_passwordLabel.Size = new System.Drawing.Size(108, 20);
            this.m_passwordLabel.TabIndex = 1;
            this.m_passwordLabel.Text = "Your password:";
            // 
            // m_loginTextBox
            // 
            this.m_loginTextBox.Location = new System.Drawing.Point(154, 15);
            this.m_loginTextBox.Name = "m_loginTextBox";
            this.m_loginTextBox.Size = new System.Drawing.Size(287, 27);
            this.m_loginTextBox.TabIndex = 1;
            // 
            // m_passwordTextBox
            // 
            this.m_passwordTextBox.Location = new System.Drawing.Point(154, 57);
            this.m_passwordTextBox.Name = "m_passwordTextBox";
            this.m_passwordTextBox.PasswordChar = '*';
            this.m_passwordTextBox.Size = new System.Drawing.Size(287, 27);
            this.m_passwordTextBox.TabIndex = 2;
            // 
            // m_okButton
            // 
            this.m_okButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.m_okButton.Location = new System.Drawing.Point(147, 142);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(94, 29);
            this.m_okButton.TabIndex = 3;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = false;
            this.m_okButton.Click += new System.EventHandler(this.OnOk);
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(347, 142);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(94, 29);
            this.m_cancelButton.TabIndex = 5;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = false;
            // 
            // m_registerButton
            // 
            this.m_registerButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.m_registerButton.Location = new System.Drawing.Point(247, 142);
            this.m_registerButton.Name = "m_registerButton";
            this.m_registerButton.Size = new System.Drawing.Size(94, 29);
            this.m_registerButton.TabIndex = 4;
            this.m_registerButton.Text = "New user";
            this.m_registerButton.UseVisualStyleBackColor = false;
            this.m_registerButton.Click += new System.EventHandler(this.OnRegisterNewUser);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkKhaki;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(465, 194);
            this.Controls.Add(this.m_registerButton);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_passwordTextBox);
            this.Controls.Add(this.m_loginTextBox);
            this.Controls.Add(this.m_passwordLabel);
            this.Controls.Add(this.m_loginLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_loginLabel;
        private System.Windows.Forms.Label m_passwordLabel;
        private System.Windows.Forms.TextBox m_loginTextBox;
        private System.Windows.Forms.TextBox m_passwordTextBox;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.Button m_registerButton;
    }
}