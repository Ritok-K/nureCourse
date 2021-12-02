
namespace MovieStore.UI
{
    partial class ActorForm
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
            this.m_firstNameLabel = new System.Windows.Forms.Label();
            this.m_firstNameTextBox = new System.Windows.Forms.TextBox();
            this.m_secondNameLabel = new System.Windows.Forms.Label();
            this.m_secondNameTextBox = new System.Windows.Forms.TextBox();
            this.m_birthDateLabel = new System.Windows.Forms.Label();
            this.m_birthDateTextBox = new System.Windows.Forms.TextBox();
            this.m_familyStatusLabel = new System.Windows.Forms.Label();
            this.m_countryLabel = new System.Windows.Forms.Label();
            this.m_countryTextBox = new System.Windows.Forms.TextBox();
            this.m_awardsDescriptionLabel = new System.Windows.Forms.Label();
            this.m_awardsDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.m_singleRadioButton = new System.Windows.Forms.RadioButton();
            this.m_marriedRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // m_firstNameLabel
            // 
            this.m_firstNameLabel.AutoSize = true;
            this.m_firstNameLabel.Location = new System.Drawing.Point(29, 29);
            this.m_firstNameLabel.Name = "m_firstNameLabel";
            this.m_firstNameLabel.Size = new System.Drawing.Size(80, 20);
            this.m_firstNameLabel.TabIndex = 1;
            this.m_firstNameLabel.Text = "First name:";
            // 
            // m_firstNameTextBox
            // 
            this.m_firstNameTextBox.Location = new System.Drawing.Point(153, 26);
            this.m_firstNameTextBox.Name = "m_firstNameTextBox";
            this.m_firstNameTextBox.Size = new System.Drawing.Size(447, 27);
            this.m_firstNameTextBox.TabIndex = 2;
            // 
            // m_secondNameLabel
            // 
            this.m_secondNameLabel.AutoSize = true;
            this.m_secondNameLabel.Location = new System.Drawing.Point(29, 90);
            this.m_secondNameLabel.Name = "m_secondNameLabel";
            this.m_secondNameLabel.Size = new System.Drawing.Size(102, 20);
            this.m_secondNameLabel.TabIndex = 3;
            this.m_secondNameLabel.Text = "Second name:";
            // 
            // m_secondNameTextBox
            // 
            this.m_secondNameTextBox.Location = new System.Drawing.Point(153, 87);
            this.m_secondNameTextBox.Name = "m_secondNameTextBox";
            this.m_secondNameTextBox.Size = new System.Drawing.Size(447, 27);
            this.m_secondNameTextBox.TabIndex = 4;
            // 
            // m_birthDateLabel
            // 
            this.m_birthDateLabel.AutoSize = true;
            this.m_birthDateLabel.Location = new System.Drawing.Point(29, 157);
            this.m_birthDateLabel.Name = "m_birthDateLabel";
            this.m_birthDateLabel.Size = new System.Drawing.Size(77, 20);
            this.m_birthDateLabel.TabIndex = 5;
            this.m_birthDateLabel.Text = "Birth date:";
            // 
            // m_birthDateTextBox
            // 
            this.m_birthDateTextBox.Location = new System.Drawing.Point(153, 154);
            this.m_birthDateTextBox.Name = "m_birthDateTextBox";
            this.m_birthDateTextBox.Size = new System.Drawing.Size(167, 27);
            this.m_birthDateTextBox.TabIndex = 6;
            // 
            // m_familyStatusLabel
            // 
            this.m_familyStatusLabel.AutoSize = true;
            this.m_familyStatusLabel.Location = new System.Drawing.Point(349, 157);
            this.m_familyStatusLabel.Name = "m_familyStatusLabel";
            this.m_familyStatusLabel.Size = new System.Drawing.Size(96, 20);
            this.m_familyStatusLabel.TabIndex = 7;
            this.m_familyStatusLabel.Text = "Family status:";
            // 
            // m_countryLabel
            // 
            this.m_countryLabel.AutoSize = true;
            this.m_countryLabel.Location = new System.Drawing.Point(29, 219);
            this.m_countryLabel.Name = "m_countryLabel";
            this.m_countryLabel.Size = new System.Drawing.Size(63, 20);
            this.m_countryLabel.TabIndex = 9;
            this.m_countryLabel.Text = "Country:";
            // 
            // m_countryTextBox
            // 
            this.m_countryTextBox.Location = new System.Drawing.Point(153, 219);
            this.m_countryTextBox.Name = "m_countryTextBox";
            this.m_countryTextBox.Size = new System.Drawing.Size(447, 27);
            this.m_countryTextBox.TabIndex = 10;
            // 
            // m_awardsDescriptionLabel
            // 
            this.m_awardsDescriptionLabel.AutoSize = true;
            this.m_awardsDescriptionLabel.Location = new System.Drawing.Point(29, 279);
            this.m_awardsDescriptionLabel.Name = "m_awardsDescriptionLabel";
            this.m_awardsDescriptionLabel.Size = new System.Drawing.Size(139, 20);
            this.m_awardsDescriptionLabel.TabIndex = 11;
            this.m_awardsDescriptionLabel.Text = "Awards description:";
            // 
            // m_awardsDescriptionTextBox
            // 
            this.m_awardsDescriptionTextBox.Location = new System.Drawing.Point(211, 276);
            this.m_awardsDescriptionTextBox.Multiline = true;
            this.m_awardsDescriptionTextBox.Name = "m_awardsDescriptionTextBox";
            this.m_awardsDescriptionTextBox.Size = new System.Drawing.Size(389, 96);
            this.m_awardsDescriptionTextBox.TabIndex = 12;
            // 
            // m_okButton
            // 
            this.m_okButton.Location = new System.Drawing.Point(322, 402);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(144, 29);
            this.m_okButton.TabIndex = 13;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOk);
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(487, 403);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(108, 29);
            this.m_cancelButton.TabIndex = 14;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // m_singleRadioButton
            // 
            this.m_singleRadioButton.AutoSize = true;
            this.m_singleRadioButton.Checked = true;
            this.m_singleRadioButton.Location = new System.Drawing.Point(476, 141);
            this.m_singleRadioButton.Name = "m_singleRadioButton";
            this.m_singleRadioButton.Size = new System.Drawing.Size(71, 24);
            this.m_singleRadioButton.TabIndex = 15;
            this.m_singleRadioButton.TabStop = true;
            this.m_singleRadioButton.Text = "Single";
            this.m_singleRadioButton.UseVisualStyleBackColor = true;
            // 
            // m_marriedRadioButton
            // 
            this.m_marriedRadioButton.AutoSize = true;
            this.m_marriedRadioButton.Location = new System.Drawing.Point(476, 171);
            this.m_marriedRadioButton.Name = "m_marriedRadioButton";
            this.m_marriedRadioButton.Size = new System.Drawing.Size(82, 24);
            this.m_marriedRadioButton.TabIndex = 16;
            this.m_marriedRadioButton.TabStop = true;
            this.m_marriedRadioButton.Text = "Married";
            this.m_marriedRadioButton.UseVisualStyleBackColor = true;
            // 
            // ActorForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(637, 456);
            this.Controls.Add(this.m_marriedRadioButton);
            this.Controls.Add(this.m_singleRadioButton);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_awardsDescriptionTextBox);
            this.Controls.Add(this.m_awardsDescriptionLabel);
            this.Controls.Add(this.m_countryTextBox);
            this.Controls.Add(this.m_countryLabel);
            this.Controls.Add(this.m_familyStatusLabel);
            this.Controls.Add(this.m_birthDateTextBox);
            this.Controls.Add(this.m_birthDateLabel);
            this.Controls.Add(this.m_secondNameTextBox);
            this.Controls.Add(this.m_secondNameLabel);
            this.Controls.Add(this.m_firstNameTextBox);
            this.Controls.Add(this.m_firstNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Actor";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_firstNameLabel;
        private System.Windows.Forms.TextBox m_firstNameTextBox;
        private System.Windows.Forms.Label m_secondNameLabel;
        private System.Windows.Forms.TextBox m_secondNameTextBox;
        private System.Windows.Forms.Label m_birthDateLabel;
        private System.Windows.Forms.TextBox m_birthDateTextBox;
        private System.Windows.Forms.Label m_familyStatusLabel;
        private System.Windows.Forms.Label m_countryLabel;
        private System.Windows.Forms.TextBox m_countryTextBox;
        private System.Windows.Forms.Label m_awardsDescriptionLabel;
        private System.Windows.Forms.TextBox m_awardsDescriptionTextBox;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.RadioButton m_singleRadioButton;
        private System.Windows.Forms.RadioButton m_marriedRadioButton;
    }
}