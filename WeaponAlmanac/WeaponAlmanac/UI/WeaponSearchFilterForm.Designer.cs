
namespace WeaponAlmanac.UI
{
    partial class WeaponSearchFilterForm
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
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_resetButton = new System.Windows.Forms.Button();
            this.m_countryTextBox = new System.Windows.Forms.TextBox();
            this.m_countryLabel = new System.Windows.Forms.Label();
            this.m_nameTextBox = new System.Windows.Forms.TextBox();
            this.m_nameLabel = new System.Windows.Forms.Label();
            this.m_titleLabel = new System.Windows.Forms.Label();
            this.m_descriptionLabel = new System.Windows.Forms.Label();
            this.m_descriptionTextBox = new System.Windows.Forms.TextBox();
            this.m_manufactoredFromYearLabel = new System.Windows.Forms.Label();
            this.m_manifacturedFromTextBox = new System.Windows.Forms.TextBox();
            this.m_manifacturedToTextBox = new System.Windows.Forms.TextBox();
            this.m_manufacturedToLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.Location = new System.Drawing.Point(393, 292);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(94, 29);
            this.m_okButton.TabIndex = 6;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOkClick);
            // 
            // m_resetButton
            // 
            this.m_resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_resetButton.Location = new System.Drawing.Point(223, 292);
            this.m_resetButton.Name = "m_resetButton";
            this.m_resetButton.Size = new System.Drawing.Size(142, 29);
            this.m_resetButton.TabIndex = 5;
            this.m_resetButton.Text = "Reset Search";
            this.m_resetButton.UseVisualStyleBackColor = true;
            this.m_resetButton.Click += new System.EventHandler(this.OnResetClick);
            // 
            // m_countryTextBox
            // 
            this.m_countryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_countryTextBox.Location = new System.Drawing.Point(174, 115);
            this.m_countryTextBox.Name = "m_countryTextBox";
            this.m_countryTextBox.Size = new System.Drawing.Size(312, 27);
            this.m_countryTextBox.TabIndex = 1;
            // 
            // m_countryLabel
            // 
            this.m_countryLabel.AutoSize = true;
            this.m_countryLabel.Location = new System.Drawing.Point(45, 118);
            this.m_countryLabel.Name = "m_countryLabel";
            this.m_countryLabel.Size = new System.Drawing.Size(122, 20);
            this.m_countryLabel.TabIndex = 11;
            this.m_countryLabel.Text = "Country contains:";
            // 
            // m_nameTextBox
            // 
            this.m_nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_nameTextBox.Location = new System.Drawing.Point(174, 60);
            this.m_nameTextBox.Name = "m_nameTextBox";
            this.m_nameTextBox.Size = new System.Drawing.Size(312, 27);
            this.m_nameTextBox.TabIndex = 0;
            // 
            // m_nameLabel
            // 
            this.m_nameLabel.AutoSize = true;
            this.m_nameLabel.Location = new System.Drawing.Point(45, 63);
            this.m_nameLabel.Name = "m_nameLabel";
            this.m_nameLabel.Size = new System.Drawing.Size(111, 20);
            this.m_nameLabel.TabIndex = 9;
            this.m_nameLabel.Text = "Name contains:";
            // 
            // m_titleLabel
            // 
            this.m_titleLabel.AutoSize = true;
            this.m_titleLabel.Location = new System.Drawing.Point(21, 21);
            this.m_titleLabel.Name = "m_titleLabel";
            this.m_titleLabel.Size = new System.Drawing.Size(348, 20);
            this.m_titleLabel.TabIndex = 7;
            this.m_titleLabel.Text = "Find all weapon when all following criteria are true:";
            // 
            // m_descriptionLabel
            // 
            this.m_descriptionLabel.AutoSize = true;
            this.m_descriptionLabel.Location = new System.Drawing.Point(45, 173);
            this.m_descriptionLabel.Name = "m_descriptionLabel";
            this.m_descriptionLabel.Size = new System.Drawing.Size(147, 20);
            this.m_descriptionLabel.TabIndex = 11;
            this.m_descriptionLabel.Text = "Description contains:";
            // 
            // m_descriptionTextBox
            // 
            this.m_descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_descriptionTextBox.Location = new System.Drawing.Point(198, 170);
            this.m_descriptionTextBox.Name = "m_descriptionTextBox";
            this.m_descriptionTextBox.Size = new System.Drawing.Size(288, 27);
            this.m_descriptionTextBox.TabIndex = 2;
            // 
            // m_manufactoredFromYearLabel
            // 
            this.m_manufactoredFromYearLabel.AutoSize = true;
            this.m_manufactoredFromYearLabel.Location = new System.Drawing.Point(45, 228);
            this.m_manufactoredFromYearLabel.Name = "m_manufactoredFromYearLabel";
            this.m_manufactoredFromYearLabel.Size = new System.Drawing.Size(140, 20);
            this.m_manufactoredFromYearLabel.TabIndex = 12;
            this.m_manufactoredFromYearLabel.Text = "Manufactured from:";
            // 
            // m_manifacturedFromTextBox
            // 
            this.m_manifacturedFromTextBox.Location = new System.Drawing.Point(198, 225);
            this.m_manifacturedFromTextBox.Name = "m_manifacturedFromTextBox";
            this.m_manifacturedFromTextBox.Size = new System.Drawing.Size(125, 27);
            this.m_manifacturedFromTextBox.TabIndex = 3;
            // 
            // m_manifacturedToTextBox
            // 
            this.m_manifacturedToTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_manifacturedToTextBox.Location = new System.Drawing.Point(361, 225);
            this.m_manifacturedToTextBox.Name = "m_manifacturedToTextBox";
            this.m_manifacturedToTextBox.Size = new System.Drawing.Size(125, 27);
            this.m_manifacturedToTextBox.TabIndex = 4;
            // 
            // m_manufacturedToLabel
            // 
            this.m_manufacturedToLabel.AutoSize = true;
            this.m_manufacturedToLabel.Location = new System.Drawing.Point(332, 228);
            this.m_manufacturedToLabel.Name = "m_manufacturedToLabel";
            this.m_manufacturedToLabel.Size = new System.Drawing.Size(23, 20);
            this.m_manufacturedToLabel.TabIndex = 14;
            this.m_manufacturedToLabel.Text = "to";
            // 
            // WeaponSearchFilterForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 333);
            this.Controls.Add(this.m_manufacturedToLabel);
            this.Controls.Add(this.m_manifacturedToTextBox);
            this.Controls.Add(this.m_manifacturedFromTextBox);
            this.Controls.Add(this.m_manufactoredFromYearLabel);
            this.Controls.Add(this.m_resetButton);
            this.Controls.Add(this.m_descriptionTextBox);
            this.Controls.Add(this.m_descriptionLabel);
            this.Controls.Add(this.m_countryTextBox);
            this.Controls.Add(this.m_countryLabel);
            this.Controls.Add(this.m_nameTextBox);
            this.Controls.Add(this.m_nameLabel);
            this.Controls.Add(this.m_titleLabel);
            this.Controls.Add(this.m_okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(530, 380);
            this.Name = "WeaponSearchFilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Weapon searching";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_resetButton;
        private System.Windows.Forms.TextBox m_countryTextBox;
        private System.Windows.Forms.Label m_countryLabel;
        private System.Windows.Forms.TextBox m_nameTextBox;
        private System.Windows.Forms.Label m_nameLabel;
        private System.Windows.Forms.Label m_titleLabel;
        private System.Windows.Forms.Label m_descriptionLabel;
        private System.Windows.Forms.TextBox m_descriptionTextBox;
        private System.Windows.Forms.Label m_manufactoredFromYearLabel;
        private System.Windows.Forms.TextBox m_manifacturedFromTextBox;
        private System.Windows.Forms.TextBox m_manifacturedToTextBox;
        private System.Windows.Forms.Label m_manufacturedToLabel;
    }
}