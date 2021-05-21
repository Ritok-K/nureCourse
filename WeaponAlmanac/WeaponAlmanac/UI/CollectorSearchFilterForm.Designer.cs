
namespace WeaponAlmanac.UI
{
    partial class CollectorSearchFilterForm
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
            this.m_descriptionLabel = new System.Windows.Forms.Label();
            this.m_nameLabel = new System.Windows.Forms.Label();
            this.m_nameTextBox = new System.Windows.Forms.TextBox();
            this.m_countryLabel = new System.Windows.Forms.Label();
            this.m_countryTextBox = new System.Windows.Forms.TextBox();
            this.m_rareCheckBox = new System.Windows.Forms.CheckBox();
            this.m_resetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.Location = new System.Drawing.Point(380, 222);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(94, 29);
            this.m_okButton.TabIndex = 4;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOkClick);
            // 
            // m_descriptionLabel
            // 
            this.m_descriptionLabel.AutoSize = true;
            this.m_descriptionLabel.Location = new System.Drawing.Point(22, 18);
            this.m_descriptionLabel.Name = "m_descriptionLabel";
            this.m_descriptionLabel.Size = new System.Drawing.Size(353, 20);
            this.m_descriptionLabel.TabIndex = 1;
            this.m_descriptionLabel.Text = "Find all collector when all following criteria are true:";
            // 
            // m_nameLabel
            // 
            this.m_nameLabel.AutoSize = true;
            this.m_nameLabel.Location = new System.Drawing.Point(46, 60);
            this.m_nameLabel.Name = "m_nameLabel";
            this.m_nameLabel.Size = new System.Drawing.Size(111, 20);
            this.m_nameLabel.TabIndex = 2;
            this.m_nameLabel.Text = "Name contains:";
            // 
            // m_nameTextBox
            // 
            this.m_nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_nameTextBox.Location = new System.Drawing.Point(175, 57);
            this.m_nameTextBox.Name = "m_nameTextBox";
            this.m_nameTextBox.Size = new System.Drawing.Size(299, 27);
            this.m_nameTextBox.TabIndex = 0;
            // 
            // m_countryLabel
            // 
            this.m_countryLabel.AutoSize = true;
            this.m_countryLabel.Location = new System.Drawing.Point(46, 113);
            this.m_countryLabel.Name = "m_countryLabel";
            this.m_countryLabel.Size = new System.Drawing.Size(122, 20);
            this.m_countryLabel.TabIndex = 4;
            this.m_countryLabel.Text = "Country contains:";
            // 
            // m_countryTextBox
            // 
            this.m_countryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_countryTextBox.Location = new System.Drawing.Point(175, 110);
            this.m_countryTextBox.Name = "m_countryTextBox";
            this.m_countryTextBox.Size = new System.Drawing.Size(299, 27);
            this.m_countryTextBox.TabIndex = 1;
            // 
            // m_rareCheckBox
            // 
            this.m_rareCheckBox.AutoSize = true;
            this.m_rareCheckBox.Checked = true;
            this.m_rareCheckBox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.m_rareCheckBox.Location = new System.Drawing.Point(175, 162);
            this.m_rareCheckBox.Name = "m_rareCheckBox";
            this.m_rareCheckBox.Size = new System.Drawing.Size(272, 24);
            this.m_rareCheckBox.TabIndex = 2;
            this.m_rareCheckBox.Text = "Owns or Does not own rare weapoin";
            this.m_rareCheckBox.ThreeState = true;
            this.m_rareCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_resetButton
            // 
            this.m_resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_resetButton.Location = new System.Drawing.Point(214, 222);
            this.m_resetButton.Name = "m_resetButton";
            this.m_resetButton.Size = new System.Drawing.Size(142, 29);
            this.m_resetButton.TabIndex = 3;
            this.m_resetButton.Text = "Reset Search";
            this.m_resetButton.UseVisualStyleBackColor = true;
            this.m_resetButton.Click += new System.EventHandler(this.OnResetClick);
            // 
            // CollectorSearchFilterForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 263);
            this.Controls.Add(this.m_resetButton);
            this.Controls.Add(this.m_rareCheckBox);
            this.Controls.Add(this.m_countryTextBox);
            this.Controls.Add(this.m_countryLabel);
            this.Controls.Add(this.m_nameTextBox);
            this.Controls.Add(this.m_nameLabel);
            this.Controls.Add(this.m_descriptionLabel);
            this.Controls.Add(this.m_okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(505, 310);
            this.Name = "CollectorSearchFilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Collector searching";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Label m_descriptionLabel;
        private System.Windows.Forms.Label m_nameLabel;
        private System.Windows.Forms.TextBox m_nameTextBox;
        private System.Windows.Forms.Label m_countryLabel;
        private System.Windows.Forms.TextBox m_countryTextBox;
        private System.Windows.Forms.CheckBox m_rareCheckBox;
        private System.Windows.Forms.Button m_resetButton;
    }
}