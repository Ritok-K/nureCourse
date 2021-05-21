
namespace WeaponAlmanac.UI
{
    partial class WeaponForm
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
            this.m_nameLabel = new System.Windows.Forms.Label();
            this.m_nameTextBox = new System.Windows.Forms.TextBox();
            this.m_descriptionLabel = new System.Windows.Forms.Label();
            this.m_descriptionTextBox = new System.Windows.Forms.TextBox();
            this.m_countryLabel = new System.Windows.Forms.Label();
            this.m_countryTextBox = new System.Windows.Forms.TextBox();
            this.m_pictureBox = new System.Windows.Forms.PictureBox();
            this.m_materialLabel = new System.Windows.Forms.Label();
            this.m_materialTextBox = new System.Windows.Forms.TextBox();
            this.m_issuedNumberLabel = new System.Windows.Forms.Label();
            this.m_manufacturedYearLabel = new System.Windows.Forms.Label();
            this.m_manufacturedYearTextBox = new System.Windows.Forms.TextBox();
            this.m_rareCheckBox = new System.Windows.Forms.CheckBox();
            this.m_pictureButton = new System.Windows.Forms.Button();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_issuedNumberTextBox = new System.Windows.Forms.TextBox();
            this.m_pictureBrowseFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // m_nameLabel
            // 
            this.m_nameLabel.AutoSize = true;
            this.m_nameLabel.Location = new System.Drawing.Point(248, 17);
            this.m_nameLabel.Name = "m_nameLabel";
            this.m_nameLabel.Size = new System.Drawing.Size(49, 20);
            this.m_nameLabel.TabIndex = 0;
            this.m_nameLabel.Text = "Name";
            // 
            // m_nameTextBox
            // 
            this.m_nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_nameTextBox.Location = new System.Drawing.Point(316, 14);
            this.m_nameTextBox.Name = "m_nameTextBox";
            this.m_nameTextBox.Size = new System.Drawing.Size(304, 27);
            this.m_nameTextBox.TabIndex = 1;
            this.m_nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnNameValidating);
            // 
            // m_descriptionLabel
            // 
            this.m_descriptionLabel.AutoSize = true;
            this.m_descriptionLabel.Location = new System.Drawing.Point(248, 158);
            this.m_descriptionLabel.Name = "m_descriptionLabel";
            this.m_descriptionLabel.Size = new System.Drawing.Size(85, 20);
            this.m_descriptionLabel.TabIndex = 2;
            this.m_descriptionLabel.Text = "Description";
            // 
            // m_descriptionTextBox
            // 
            this.m_descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_descriptionTextBox.Location = new System.Drawing.Point(264, 187);
            this.m_descriptionTextBox.Multiline = true;
            this.m_descriptionTextBox.Name = "m_descriptionTextBox";
            this.m_descriptionTextBox.Size = new System.Drawing.Size(356, 168);
            this.m_descriptionTextBox.TabIndex = 3;
            // 
            // m_countryLabel
            // 
            this.m_countryLabel.AutoSize = true;
            this.m_countryLabel.Location = new System.Drawing.Point(248, 64);
            this.m_countryLabel.Name = "m_countryLabel";
            this.m_countryLabel.Size = new System.Drawing.Size(60, 20);
            this.m_countryLabel.TabIndex = 4;
            this.m_countryLabel.Text = "Country";
            // 
            // m_countryTextBox
            // 
            this.m_countryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_countryTextBox.Location = new System.Drawing.Point(316, 61);
            this.m_countryTextBox.Name = "m_countryTextBox";
            this.m_countryTextBox.Size = new System.Drawing.Size(304, 27);
            this.m_countryTextBox.TabIndex = 5;
            // 
            // m_pictureBox
            // 
            this.m_pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pictureBox.Location = new System.Drawing.Point(12, 12);
            this.m_pictureBox.Name = "m_pictureBox";
            this.m_pictureBox.Size = new System.Drawing.Size(230, 307);
            this.m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_pictureBox.TabIndex = 6;
            this.m_pictureBox.TabStop = false;
            // 
            // m_materialLabel
            // 
            this.m_materialLabel.AutoSize = true;
            this.m_materialLabel.Location = new System.Drawing.Point(248, 111);
            this.m_materialLabel.Name = "m_materialLabel";
            this.m_materialLabel.Size = new System.Drawing.Size(64, 20);
            this.m_materialLabel.TabIndex = 7;
            this.m_materialLabel.Text = "Material";
            // 
            // m_materialTextBox
            // 
            this.m_materialTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_materialTextBox.Location = new System.Drawing.Point(316, 108);
            this.m_materialTextBox.Name = "m_materialTextBox";
            this.m_materialTextBox.Size = new System.Drawing.Size(304, 27);
            this.m_materialTextBox.TabIndex = 8;
            // 
            // m_issuedNumberLabel
            // 
            this.m_issuedNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_issuedNumberLabel.AutoSize = true;
            this.m_issuedNumberLabel.Location = new System.Drawing.Point(12, 393);
            this.m_issuedNumberLabel.Name = "m_issuedNumberLabel";
            this.m_issuedNumberLabel.Size = new System.Drawing.Size(159, 20);
            this.m_issuedNumberLabel.TabIndex = 9;
            this.m_issuedNumberLabel.Text = "Manufactured Number";
            // 
            // m_manufacturedYearLabel
            // 
            this.m_manufacturedYearLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_manufacturedYearLabel.AutoSize = true;
            this.m_manufacturedYearLabel.Location = new System.Drawing.Point(12, 429);
            this.m_manufacturedYearLabel.Name = "m_manufacturedYearLabel";
            this.m_manufacturedYearLabel.Size = new System.Drawing.Size(133, 20);
            this.m_manufacturedYearLabel.TabIndex = 11;
            this.m_manufacturedYearLabel.Text = "Manufactured Year";
            // 
            // m_manufacturedYearTextBox
            // 
            this.m_manufacturedYearTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_manufacturedYearTextBox.Location = new System.Drawing.Point(177, 426);
            this.m_manufacturedYearTextBox.Name = "m_manufacturedYearTextBox";
            this.m_manufacturedYearTextBox.Size = new System.Drawing.Size(124, 27);
            this.m_manufacturedYearTextBox.TabIndex = 12;
            this.m_manufacturedYearTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnManufacturedYearValidating);
            // 
            // m_rareCheckBox
            // 
            this.m_rareCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_rareCheckBox.AutoSize = true;
            this.m_rareCheckBox.Location = new System.Drawing.Point(317, 392);
            this.m_rareCheckBox.Name = "m_rareCheckBox";
            this.m_rareCheckBox.Size = new System.Drawing.Size(113, 24);
            this.m_rareCheckBox.TabIndex = 13;
            this.m_rareCheckBox.Text = "Is rare today";
            this.m_rareCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_pictureButton
            // 
            this.m_pictureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_pictureButton.Location = new System.Drawing.Point(12, 326);
            this.m_pictureButton.Name = "m_pictureButton";
            this.m_pictureButton.Size = new System.Drawing.Size(230, 29);
            this.m_pictureButton.TabIndex = 14;
            this.m_pictureButton.Text = "Browse for Picture";
            this.m_pictureButton.UseVisualStyleBackColor = true;
            this.m_pictureButton.Click += new System.EventHandler(this.OnPictureBrowseClick);
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.Location = new System.Drawing.Point(515, 424);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(94, 29);
            this.m_okButton.TabIndex = 15;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOkClick);
            // 
            // m_issuedNumberTextBox
            // 
            this.m_issuedNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_issuedNumberTextBox.Location = new System.Drawing.Point(177, 390);
            this.m_issuedNumberTextBox.Name = "m_issuedNumberTextBox";
            this.m_issuedNumberTextBox.Size = new System.Drawing.Size(124, 27);
            this.m_issuedNumberTextBox.TabIndex = 10;
            this.m_issuedNumberTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnIssuedNumberValidating);
            // 
            // m_pictureBrowseFileDialog
            // 
            this.m_pictureBrowseFileDialog.DefaultExt = "jpg";
            this.m_pictureBrowseFileDialog.Filter = "All Images (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif";
            // 
            // WeaponForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 473);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_pictureButton);
            this.Controls.Add(this.m_rareCheckBox);
            this.Controls.Add(this.m_manufacturedYearTextBox);
            this.Controls.Add(this.m_materialTextBox);
            this.Controls.Add(this.m_issuedNumberLabel);
            this.Controls.Add(this.m_materialLabel);
            this.Controls.Add(this.m_manufacturedYearLabel);
            this.Controls.Add(this.m_issuedNumberTextBox);
            this.Controls.Add(this.m_pictureBox);
            this.Controls.Add(this.m_countryTextBox);
            this.Controls.Add(this.m_countryLabel);
            this.Controls.Add(this.m_descriptionTextBox);
            this.Controls.Add(this.m_descriptionLabel);
            this.Controls.Add(this.m_nameTextBox);
            this.Controls.Add(this.m_nameLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 520);
            this.Name = "WeaponForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Weapon";
            this.Load += new System.EventHandler(this.OnLoda);
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_nameLabel;
        private System.Windows.Forms.TextBox m_nameTextBox;
        private System.Windows.Forms.Label m_descriptionLabel;
        private System.Windows.Forms.TextBox m_descriptionTextBox;
        private System.Windows.Forms.Label m_countryLabel;
        private System.Windows.Forms.TextBox m_countryTextBox;
        private System.Windows.Forms.PictureBox m_pictureBox;
        private System.Windows.Forms.Label m_materialLabel;
        private System.Windows.Forms.TextBox m_materialTextBox;
        private System.Windows.Forms.Label m_issuedNumberLabel;
        private System.Windows.Forms.Label m_manufacturedYearLabel;
        private System.Windows.Forms.TextBox m_manufacturedYearTextBox;
        private System.Windows.Forms.CheckBox m_rareCheckBox;
        private System.Windows.Forms.Button m_pictureButton;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.TextBox m_issuedNumberTextBox;
        private System.Windows.Forms.OpenFileDialog m_pictureBrowseFileDialog;
    }
}