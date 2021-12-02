
namespace MovieStore.UI
{
    partial class OrdersReceiptForm
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
            this.m_fileNameLabel = new System.Windows.Forms.Label();
            this.m_showFileDialogButton = new System.Windows.Forms.Button();
            this.m_fileNameTextBox = new System.Windows.Forms.TextBox();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_infoLabel = new System.Windows.Forms.Label();
            this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // m_fileNameLabel
            // 
            this.m_fileNameLabel.AutoSize = true;
            this.m_fileNameLabel.Location = new System.Drawing.Point(22, 88);
            this.m_fileNameLabel.Name = "m_fileNameLabel";
            this.m_fileNameLabel.Size = new System.Drawing.Size(66, 20);
            this.m_fileNameLabel.TabIndex = 11;
            this.m_fileNameLabel.Text = "File path";
            // 
            // m_showFileDialogButton
            // 
            this.m_showFileDialogButton.Location = new System.Drawing.Point(460, 84);
            this.m_showFileDialogButton.Name = "m_showFileDialogButton";
            this.m_showFileDialogButton.Size = new System.Drawing.Size(33, 27);
            this.m_showFileDialogButton.TabIndex = 10;
            this.m_showFileDialogButton.Text = "...";
            this.m_showFileDialogButton.UseVisualStyleBackColor = true;
            this.m_showFileDialogButton.Click += new System.EventHandler(this.OnShowFilePathDialog);
            // 
            // m_fileNameTextBox
            // 
            this.m_fileNameTextBox.Location = new System.Drawing.Point(94, 84);
            this.m_fileNameTextBox.Name = "m_fileNameTextBox";
            this.m_fileNameTextBox.ReadOnly = true;
            this.m_fileNameTextBox.Size = new System.Drawing.Size(360, 27);
            this.m_fileNameTextBox.TabIndex = 9;
            // 
            // m_okButton
            // 
            this.m_okButton.Location = new System.Drawing.Point(399, 144);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(94, 29);
            this.m_okButton.TabIndex = 8;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOk);
            // 
            // m_infoLabel
            // 
            this.m_infoLabel.AutoSize = true;
            this.m_infoLabel.Location = new System.Drawing.Point(22, 29);
            this.m_infoLabel.Name = "m_infoLabel";
            this.m_infoLabel.Size = new System.Drawing.Size(307, 20);
            this.m_infoLabel.TabIndex = 7;
            this.m_infoLabel.Text = "Please select file path to export order receipt";
            // 
            // m_saveFileDialog
            // 
            this.m_saveFileDialog.DefaultExt = "txt";
            this.m_saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.m_saveFileDialog.Title = "Save order receipt";
            // 
            // OrdersReceiptForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 204);
            this.Controls.Add(this.m_fileNameLabel);
            this.Controls.Add(this.m_showFileDialogButton);
            this.Controls.Add(this.m_fileNameTextBox);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_infoLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrdersReceiptForm";
            this.Text = "Order Receipt";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_fileNameLabel;
        private System.Windows.Forms.Button m_showFileDialogButton;
        private System.Windows.Forms.TextBox m_fileNameTextBox;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Label m_infoLabel;
        private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
    }
}