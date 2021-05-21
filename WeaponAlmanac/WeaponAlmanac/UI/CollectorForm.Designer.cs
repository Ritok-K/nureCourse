
namespace WeaponAlmanac.UI
{
    partial class CollectorForm
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
            this.m_countryLabel = new System.Windows.Forms.Label();
            this.m_countryTextBox = new System.Windows.Forms.TextBox();
            this.m_emailLabel = new System.Windows.Forms.Label();
            this.m_emailTextBox = new System.Windows.Forms.TextBox();
            this.m_phoneLabel = new System.Windows.Forms.Label();
            this.m_phoneTextBox = new System.Windows.Forms.TextBox();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_rareWeaponListView = new System.Windows.Forms.ListView();
            this.m_rareWeaponLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_nameLabel
            // 
            this.m_nameLabel.AutoSize = true;
            this.m_nameLabel.Location = new System.Drawing.Point(29, 35);
            this.m_nameLabel.Name = "m_nameLabel";
            this.m_nameLabel.Size = new System.Drawing.Size(49, 20);
            this.m_nameLabel.TabIndex = 0;
            this.m_nameLabel.Text = "Name";
            // 
            // m_nameTextBox
            // 
            this.m_nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_nameTextBox.Location = new System.Drawing.Point(95, 32);
            this.m_nameTextBox.Name = "m_nameTextBox";
            this.m_nameTextBox.Size = new System.Drawing.Size(421, 27);
            this.m_nameTextBox.TabIndex = 1;
            this.m_nameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnNameValidating);
            // 
            // m_countryLabel
            // 
            this.m_countryLabel.AutoSize = true;
            this.m_countryLabel.Location = new System.Drawing.Point(29, 83);
            this.m_countryLabel.Name = "m_countryLabel";
            this.m_countryLabel.Size = new System.Drawing.Size(60, 20);
            this.m_countryLabel.TabIndex = 2;
            this.m_countryLabel.Text = "Country";
            // 
            // m_countryTextBox
            // 
            this.m_countryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_countryTextBox.Location = new System.Drawing.Point(95, 80);
            this.m_countryTextBox.Name = "m_countryTextBox";
            this.m_countryTextBox.Size = new System.Drawing.Size(421, 27);
            this.m_countryTextBox.TabIndex = 2;
            // 
            // m_emailLabel
            // 
            this.m_emailLabel.AutoSize = true;
            this.m_emailLabel.Location = new System.Drawing.Point(287, 128);
            this.m_emailLabel.Name = "m_emailLabel";
            this.m_emailLabel.Size = new System.Drawing.Size(46, 20);
            this.m_emailLabel.TabIndex = 4;
            this.m_emailLabel.Text = "Email";
            // 
            // m_emailTextBox
            // 
            this.m_emailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_emailTextBox.Location = new System.Drawing.Point(348, 125);
            this.m_emailTextBox.Name = "m_emailTextBox";
            this.m_emailTextBox.Size = new System.Drawing.Size(168, 27);
            this.m_emailTextBox.TabIndex = 4;
            // 
            // m_phoneLabel
            // 
            this.m_phoneLabel.AutoSize = true;
            this.m_phoneLabel.Location = new System.Drawing.Point(29, 128);
            this.m_phoneLabel.Name = "m_phoneLabel";
            this.m_phoneLabel.Size = new System.Drawing.Size(50, 20);
            this.m_phoneLabel.TabIndex = 6;
            this.m_phoneLabel.Text = "Phone";
            // 
            // m_phoneTextBox
            // 
            this.m_phoneTextBox.Location = new System.Drawing.Point(95, 125);
            this.m_phoneTextBox.Name = "m_phoneTextBox";
            this.m_phoneTextBox.Size = new System.Drawing.Size(168, 27);
            this.m_phoneTextBox.TabIndex = 3;
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.Location = new System.Drawing.Point(422, 366);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(94, 29);
            this.m_okButton.TabIndex = 0;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOkClick);
            // 
            // m_rareWeaponListView
            // 
            this.m_rareWeaponListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_rareWeaponListView.HideSelection = false;
            this.m_rareWeaponListView.Location = new System.Drawing.Point(43, 195);
            this.m_rareWeaponListView.Name = "m_rareWeaponListView";
            this.m_rareWeaponListView.Size = new System.Drawing.Size(473, 152);
            this.m_rareWeaponListView.TabIndex = 5;
            this.m_rareWeaponListView.UseCompatibleStateImageBehavior = false;
            this.m_rareWeaponListView.View = System.Windows.Forms.View.Details;
            // 
            // m_rareWeaponLabel
            // 
            this.m_rareWeaponLabel.AutoSize = true;
            this.m_rareWeaponLabel.Location = new System.Drawing.Point(29, 169);
            this.m_rareWeaponLabel.Name = "m_rareWeaponLabel";
            this.m_rareWeaponLabel.Size = new System.Drawing.Size(202, 20);
            this.m_rareWeaponLabel.TabIndex = 10;
            this.m_rareWeaponLabel.Text = "Owns following rare weapon:";
            // 
            // CollectorForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 408);
            this.Controls.Add(this.m_rareWeaponLabel);
            this.Controls.Add(this.m_rareWeaponListView);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_phoneTextBox);
            this.Controls.Add(this.m_phoneLabel);
            this.Controls.Add(this.m_emailTextBox);
            this.Controls.Add(this.m_emailLabel);
            this.Controls.Add(this.m_countryTextBox);
            this.Controls.Add(this.m_countryLabel);
            this.Controls.Add(this.m_nameTextBox);
            this.Controls.Add(this.m_nameLabel);
            this.MinimumSize = new System.Drawing.Size(565, 455);
            this.Name = "CollectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Collector";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_nameLabel;
        private System.Windows.Forms.TextBox m_nameTextBox;
        private System.Windows.Forms.Label m_countryLabel;
        private System.Windows.Forms.TextBox m_countryTextBox;
        private System.Windows.Forms.Label m_emailLabel;
        private System.Windows.Forms.TextBox m_emailTextBox;
        private System.Windows.Forms.Label m_phoneLabel;
        private System.Windows.Forms.TextBox m_phoneTextBox;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.ListView m_rareWeaponListView;
        private System.Windows.Forms.Label m_rareWeaponLabel;
    }
}