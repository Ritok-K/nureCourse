
namespace MovieStore.UI
{
    partial class OrdersReportForm
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
            this.m_infoLabel = new System.Windows.Forms.Label();
            this.m_fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.m_fromDateLabel = new System.Windows.Forms.Label();
            this.m_toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.m_toDateLabel = new System.Windows.Forms.Label();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_fileNameTextBox = new System.Windows.Forms.TextBox();
            this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_showFileDialogButton = new System.Windows.Forms.Button();
            this.m_fileNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_infoLabel
            // 
            this.m_infoLabel.AutoSize = true;
            this.m_infoLabel.Location = new System.Drawing.Point(21, 26);
            this.m_infoLabel.Name = "m_infoLabel";
            this.m_infoLabel.Size = new System.Drawing.Size(348, 20);
            this.m_infoLabel.TabIndex = 0;
            this.m_infoLabel.Text = "Please select date period for building orders report";
            // 
            // m_fromDateTimePicker
            // 
            this.m_fromDateTimePicker.Location = new System.Drawing.Point(106, 71);
            this.m_fromDateTimePicker.Name = "m_fromDateTimePicker";
            this.m_fromDateTimePicker.Size = new System.Drawing.Size(250, 27);
            this.m_fromDateTimePicker.TabIndex = 1;
            // 
            // m_fromDateLabel
            // 
            this.m_fromDateLabel.AutoSize = true;
            this.m_fromDateLabel.Location = new System.Drawing.Point(38, 76);
            this.m_fromDateLabel.Name = "m_fromDateLabel";
            this.m_fromDateLabel.Size = new System.Drawing.Size(43, 20);
            this.m_fromDateLabel.TabIndex = 2;
            this.m_fromDateLabel.Text = "From";
            // 
            // m_toDateTimePicker
            // 
            this.m_toDateTimePicker.Location = new System.Drawing.Point(106, 124);
            this.m_toDateTimePicker.Name = "m_toDateTimePicker";
            this.m_toDateTimePicker.Size = new System.Drawing.Size(250, 27);
            this.m_toDateTimePicker.TabIndex = 1;
            // 
            // m_toDateLabel
            // 
            this.m_toDateLabel.AutoSize = true;
            this.m_toDateLabel.Location = new System.Drawing.Point(38, 129);
            this.m_toDateLabel.Name = "m_toDateLabel";
            this.m_toDateLabel.Size = new System.Drawing.Size(25, 20);
            this.m_toDateLabel.TabIndex = 2;
            this.m_toDateLabel.Text = "To";
            // 
            // m_okButton
            // 
            this.m_okButton.Location = new System.Drawing.Point(262, 260);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(94, 29);
            this.m_okButton.TabIndex = 3;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOk);
            // 
            // m_fileNameTextBox
            // 
            this.m_fileNameTextBox.Location = new System.Drawing.Point(106, 200);
            this.m_fileNameTextBox.Name = "m_fileNameTextBox";
            this.m_fileNameTextBox.ReadOnly = true;
            this.m_fileNameTextBox.Size = new System.Drawing.Size(211, 27);
            this.m_fileNameTextBox.TabIndex = 4;
            // 
            // m_saveFileDialog
            // 
            this.m_saveFileDialog.DefaultExt = "txt";
            this.m_saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.m_saveFileDialog.Title = "Save orders report";
            // 
            // m_showFileDialogButton
            // 
            this.m_showFileDialogButton.Location = new System.Drawing.Point(323, 200);
            this.m_showFileDialogButton.Name = "m_showFileDialogButton";
            this.m_showFileDialogButton.Size = new System.Drawing.Size(33, 27);
            this.m_showFileDialogButton.TabIndex = 5;
            this.m_showFileDialogButton.Text = "...";
            this.m_showFileDialogButton.UseVisualStyleBackColor = true;
            this.m_showFileDialogButton.Click += new System.EventHandler(this.OnShowFilePathDialog);
            // 
            // m_fileNameLabel
            // 
            this.m_fileNameLabel.AutoSize = true;
            this.m_fileNameLabel.Location = new System.Drawing.Point(34, 204);
            this.m_fileNameLabel.Name = "m_fileNameLabel";
            this.m_fileNameLabel.Size = new System.Drawing.Size(66, 20);
            this.m_fileNameLabel.TabIndex = 6;
            this.m_fileNameLabel.Text = "File path";
            // 
            // OrdersReportForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 314);
            this.Controls.Add(this.m_fileNameLabel);
            this.Controls.Add(this.m_showFileDialogButton);
            this.Controls.Add(this.m_fileNameTextBox);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_toDateLabel);
            this.Controls.Add(this.m_fromDateLabel);
            this.Controls.Add(this.m_toDateTimePicker);
            this.Controls.Add(this.m_fromDateTimePicker);
            this.Controls.Add(this.m_infoLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrdersReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orders Report";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_infoLabel;
        private System.Windows.Forms.DateTimePicker m_fromDateTimePicker;
        private System.Windows.Forms.Label m_fromDateLabel;
        private System.Windows.Forms.DateTimePicker m_toDateTimePicker;
        private System.Windows.Forms.Label m_toDateLabel;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.TextBox m_fileNameTextBox;
        private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
        private System.Windows.Forms.Button m_showFileDialogButton;
        private System.Windows.Forms.Label m_fileNameLabel;
    }
}