
namespace MovieStore.UI
{
    partial class StudioForm
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
            this.m_titleLabel = new System.Windows.Forms.Label();
            this.m_titleTextBox = new System.Windows.Forms.TextBox();
            this.m_countryLable = new System.Windows.Forms.Label();
            this.m_countryTextBox = new System.Windows.Forms.TextBox();
            this.m_foundationDateLabel = new System.Windows.Forms.Label();
            this.m_foundationDateTextBox = new System.Windows.Forms.TextBox();
            this.m_productionLabel = new System.Windows.Forms.Label();
            this.m_productionTextBox = new System.Windows.Forms.TextBox();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            this.m_titleLabel.AutoSize = true;
            this.m_titleLabel.Location = new System.Drawing.Point(29, 29);
            this.m_titleLabel.Name = "m_titleLabel";
            this.m_titleLabel.Size = new System.Drawing.Size(41, 20);
            this.m_titleLabel.TabIndex = 1;
            this.m_titleLabel.Text = "Title:";
            // 
            // m_titleTextBox
            // 
            this.m_titleTextBox.Location = new System.Drawing.Point(123, 26);
            this.m_titleTextBox.Name = "m_titleTextBox";
            this.m_titleTextBox.Size = new System.Drawing.Size(472, 27);
            this.m_titleTextBox.TabIndex = 2;
            // 
            // m_countryLable
            // 
            this.m_countryLable.AutoSize = true;
            this.m_countryLable.Location = new System.Drawing.Point(29, 103);
            this.m_countryLable.Name = "m_countryLable";
            this.m_countryLable.Size = new System.Drawing.Size(63, 20);
            this.m_countryLable.TabIndex = 3;
            this.m_countryLable.Text = "Country:";
            // 
            // m_countryTextBox
            // 
            this.m_countryTextBox.Location = new System.Drawing.Point(123, 100);
            this.m_countryTextBox.Name = "m_countryTextBox";
            this.m_countryTextBox.Size = new System.Drawing.Size(472, 27);
            this.m_countryTextBox.TabIndex = 4;
            // 
            // m_foundationDateLabel
            // 
            this.m_foundationDateLabel.AutoSize = true;
            this.m_foundationDateLabel.Location = new System.Drawing.Point(29, 184);
            this.m_foundationDateLabel.Name = "m_foundationDateLabel";
            this.m_foundationDateLabel.Size = new System.Drawing.Size(121, 20);
            this.m_foundationDateLabel.TabIndex = 5;
            this.m_foundationDateLabel.Text = "Foundation date:";
            // 
            // m_foundationDateTextBox
            // 
            this.m_foundationDateTextBox.Location = new System.Drawing.Point(178, 181);
            this.m_foundationDateTextBox.Name = "m_foundationDateTextBox";
            this.m_foundationDateTextBox.Size = new System.Drawing.Size(417, 27);
            this.m_foundationDateTextBox.TabIndex = 6;
            // 
            // m_productionLabel
            // 
            this.m_productionLabel.AutoSize = true;
            this.m_productionLabel.Location = new System.Drawing.Point(29, 270);
            this.m_productionLabel.Name = "m_productionLabel";
            this.m_productionLabel.Size = new System.Drawing.Size(84, 20);
            this.m_productionLabel.TabIndex = 7;
            this.m_productionLabel.Text = "Production:";
            // 
            // m_productionTextBox
            // 
            this.m_productionTextBox.Location = new System.Drawing.Point(123, 267);
            this.m_productionTextBox.Multiline = true;
            this.m_productionTextBox.Name = "m_productionTextBox";
            this.m_productionTextBox.Size = new System.Drawing.Size(472, 106);
            this.m_productionTextBox.TabIndex = 8;
            // 
            // m_okButton
            // 
            this.m_okButton.Location = new System.Drawing.Point(322, 402);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(144, 29);
            this.m_okButton.TabIndex = 9;
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
            this.m_cancelButton.TabIndex = 10;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // StudioForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(637, 456);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_productionTextBox);
            this.Controls.Add(this.m_productionLabel);
            this.Controls.Add(this.m_foundationDateTextBox);
            this.Controls.Add(this.m_foundationDateLabel);
            this.Controls.Add(this.m_countryTextBox);
            this.Controls.Add(this.m_countryLable);
            this.Controls.Add(this.m_titleTextBox);
            this.Controls.Add(this.m_titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StudioForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Studio";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_titleLabel;
        private System.Windows.Forms.TextBox m_titleTextBox;
        private System.Windows.Forms.Label m_countryLable;
        private System.Windows.Forms.TextBox m_countryTextBox;
        private System.Windows.Forms.Label m_foundationDateLabel;
        private System.Windows.Forms.TextBox m_foundationDateTextBox;
        private System.Windows.Forms.Label m_productionLabel;
        private System.Windows.Forms.TextBox m_productionTextBox;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
    }
}