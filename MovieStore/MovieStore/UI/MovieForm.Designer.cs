
namespace MovieStore.UI
{
    partial class MovieForm
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
            this.m_genreLabel = new System.Windows.Forms.Label();
            this.m_genreTextBox = new System.Windows.Forms.TextBox();
            this.m_descriptionLabel = new System.Windows.Forms.Label();
            this.m_descriptionTextBox = new System.Windows.Forms.TextBox();
            this.m_countryLabel = new System.Windows.Forms.Label();
            this.m_countryTextBox = new System.Windows.Forms.TextBox();
            this.m_priceLabel = new System.Windows.Forms.Label();
            this.m_priceTextBox = new System.Windows.Forms.TextBox();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.m_imdbLabel = new System.Windows.Forms.Label();
            this.m_imdbTextBox = new System.Windows.Forms.TextBox();
            this.m_studioLabel = new System.Windows.Forms.Label();
            this.m_studioComboBox = new System.Windows.Forms.ComboBox();
            this.m_yearLabel = new System.Windows.Forms.Label();
            this.m_yearTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            this.m_titleLabel.AutoSize = true;
            this.m_titleLabel.Location = new System.Drawing.Point(29, 29);
            this.m_titleLabel.Name = "m_titleLabel";
            this.m_titleLabel.Size = new System.Drawing.Size(41, 20);
            this.m_titleLabel.TabIndex = 0;
            this.m_titleLabel.Text = "Title:";
            // 
            // m_titleTextBox
            // 
            this.m_titleTextBox.Location = new System.Drawing.Point(123, 26);
            this.m_titleTextBox.Name = "m_titleTextBox";
            this.m_titleTextBox.Size = new System.Drawing.Size(472, 27);
            this.m_titleTextBox.TabIndex = 1;
            // 
            // m_genreLabel
            // 
            this.m_genreLabel.AutoSize = true;
            this.m_genreLabel.Location = new System.Drawing.Point(29, 74);
            this.m_genreLabel.Name = "m_genreLabel";
            this.m_genreLabel.Size = new System.Drawing.Size(51, 20);
            this.m_genreLabel.TabIndex = 0;
            this.m_genreLabel.Text = "Genre:";
            // 
            // m_genreTextBox
            // 
            this.m_genreTextBox.Location = new System.Drawing.Point(123, 71);
            this.m_genreTextBox.Name = "m_genreTextBox";
            this.m_genreTextBox.Size = new System.Drawing.Size(290, 27);
            this.m_genreTextBox.TabIndex = 1;
            // 
            // m_descriptionLabel
            // 
            this.m_descriptionLabel.AutoSize = true;
            this.m_descriptionLabel.Location = new System.Drawing.Point(29, 122);
            this.m_descriptionLabel.Name = "m_descriptionLabel";
            this.m_descriptionLabel.Size = new System.Drawing.Size(88, 20);
            this.m_descriptionLabel.TabIndex = 0;
            this.m_descriptionLabel.Text = "Description:";
            // 
            // m_descriptionTextBox
            // 
            this.m_descriptionTextBox.Location = new System.Drawing.Point(123, 119);
            this.m_descriptionTextBox.Multiline = true;
            this.m_descriptionTextBox.Name = "m_descriptionTextBox";
            this.m_descriptionTextBox.Size = new System.Drawing.Size(472, 161);
            this.m_descriptionTextBox.TabIndex = 1;
            // 
            // m_countryLabel
            // 
            this.m_countryLabel.AutoSize = true;
            this.m_countryLabel.Location = new System.Drawing.Point(29, 355);
            this.m_countryLabel.Name = "m_countryLabel";
            this.m_countryLabel.Size = new System.Drawing.Size(63, 20);
            this.m_countryLabel.TabIndex = 0;
            this.m_countryLabel.Text = "Country:";
            // 
            // m_countryTextBox
            // 
            this.m_countryTextBox.Location = new System.Drawing.Point(123, 352);
            this.m_countryTextBox.Name = "m_countryTextBox";
            this.m_countryTextBox.Size = new System.Drawing.Size(472, 27);
            this.m_countryTextBox.TabIndex = 1;
            // 
            // m_priceLabel
            // 
            this.m_priceLabel.AutoSize = true;
            this.m_priceLabel.Location = new System.Drawing.Point(29, 406);
            this.m_priceLabel.Name = "m_priceLabel";
            this.m_priceLabel.Size = new System.Drawing.Size(56, 20);
            this.m_priceLabel.TabIndex = 0;
            this.m_priceLabel.Text = "Price $:";
            // 
            // m_priceTextBox
            // 
            this.m_priceTextBox.Location = new System.Drawing.Point(123, 403);
            this.m_priceTextBox.Name = "m_priceTextBox";
            this.m_priceTextBox.Size = new System.Drawing.Size(102, 27);
            this.m_priceTextBox.TabIndex = 1;
            // 
            // m_okButton
            // 
            this.m_okButton.Location = new System.Drawing.Point(322, 402);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(144, 29);
            this.m_okButton.TabIndex = 2;
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
            this.m_cancelButton.TabIndex = 3;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // m_imdbLabel
            // 
            this.m_imdbLabel.AutoSize = true;
            this.m_imdbLabel.Location = new System.Drawing.Point(29, 301);
            this.m_imdbLabel.Name = "m_imdbLabel";
            this.m_imdbLabel.Size = new System.Drawing.Size(49, 20);
            this.m_imdbLabel.TabIndex = 0;
            this.m_imdbLabel.Text = "IMDB:";
            // 
            // m_imdbTextBox
            // 
            this.m_imdbTextBox.Location = new System.Drawing.Point(123, 298);
            this.m_imdbTextBox.Name = "m_imdbTextBox";
            this.m_imdbTextBox.Size = new System.Drawing.Size(102, 27);
            this.m_imdbTextBox.TabIndex = 1;
            // 
            // m_studioLabel
            // 
            this.m_studioLabel.AutoSize = true;
            this.m_studioLabel.Location = new System.Drawing.Point(261, 301);
            this.m_studioLabel.Name = "m_studioLabel";
            this.m_studioLabel.Size = new System.Drawing.Size(55, 20);
            this.m_studioLabel.TabIndex = 0;
            this.m_studioLabel.Text = "Studio:";
            // 
            // m_studioComboBox
            // 
            this.m_studioComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_studioComboBox.FormattingEnabled = true;
            this.m_studioComboBox.Location = new System.Drawing.Point(322, 298);
            this.m_studioComboBox.Name = "m_studioComboBox";
            this.m_studioComboBox.Size = new System.Drawing.Size(273, 28);
            this.m_studioComboBox.TabIndex = 4;
            // 
            // m_yearLabel
            // 
            this.m_yearLabel.AutoSize = true;
            this.m_yearLabel.Location = new System.Drawing.Point(437, 74);
            this.m_yearLabel.Name = "m_yearLabel";
            this.m_yearLabel.Size = new System.Drawing.Size(40, 20);
            this.m_yearLabel.TabIndex = 0;
            this.m_yearLabel.Text = "Year:";
            // 
            // m_yearTextBox
            // 
            this.m_yearTextBox.Location = new System.Drawing.Point(493, 71);
            this.m_yearTextBox.Name = "m_yearTextBox";
            this.m_yearTextBox.Size = new System.Drawing.Size(102, 27);
            this.m_yearTextBox.TabIndex = 1;
            // 
            // MovieForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(637, 456);
            this.Controls.Add(this.m_studioComboBox);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_descriptionTextBox);
            this.Controls.Add(this.m_descriptionLabel);
            this.Controls.Add(this.m_yearTextBox);
            this.Controls.Add(this.m_imdbTextBox);
            this.Controls.Add(this.m_studioLabel);
            this.Controls.Add(this.m_imdbLabel);
            this.Controls.Add(this.m_priceTextBox);
            this.Controls.Add(this.m_priceLabel);
            this.Controls.Add(this.m_countryTextBox);
            this.Controls.Add(this.m_countryLabel);
            this.Controls.Add(this.m_genreTextBox);
            this.Controls.Add(this.m_yearLabel);
            this.Controls.Add(this.m_genreLabel);
            this.Controls.Add(this.m_titleTextBox);
            this.Controls.Add(this.m_titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MovieForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Movie";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_titleLabel;
        private System.Windows.Forms.TextBox m_titleTextBox;
        private System.Windows.Forms.Label m_genreLabel;
        private System.Windows.Forms.TextBox m_genreTextBox;
        private System.Windows.Forms.Label m_descriptionLabel;
        private System.Windows.Forms.TextBox m_descriptionTextBox;
        private System.Windows.Forms.Label m_countryLabel;
        private System.Windows.Forms.TextBox m_countryTextBox;
        private System.Windows.Forms.Label m_priceLabel;
        private System.Windows.Forms.TextBox m_priceTextBox;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.Label m_imdbLabel;
        private System.Windows.Forms.TextBox m_imdbTextBox;
        private System.Windows.Forms.Label m_studioLabel;
        private System.Windows.Forms.ComboBox m_studioComboBox;
        private System.Windows.Forms.Label m_yearLabel;
        private System.Windows.Forms.TextBox m_yearTextBox;
    }
}