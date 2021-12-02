
namespace MovieStore.UI
{
    partial class MyBasketForm
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
            this.m_cancleButton = new System.Windows.Forms.Button();
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_infoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_okButton
            // 
            this.m_okButton.Location = new System.Drawing.Point(499, 399);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(140, 29);
            this.m_okButton.TabIndex = 0;
            this.m_okButton.Text = "Make order";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.OnOk);
            // 
            // m_cancleButton
            // 
            this.m_cancleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancleButton.Location = new System.Drawing.Point(671, 399);
            this.m_cancleButton.Name = "m_cancleButton";
            this.m_cancleButton.Size = new System.Drawing.Size(94, 29);
            this.m_cancleButton.TabIndex = 1;
            this.m_cancleButton.Text = "Cancel";
            this.m_cancleButton.UseVisualStyleBackColor = true;
            // 
            // m_listView
            // 
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(21, 61);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(744, 307);
            this.m_listView.TabIndex = 2;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            // 
            // m_infoLabel
            // 
            this.m_infoLabel.AutoSize = true;
            this.m_infoLabel.Location = new System.Drawing.Point(21, 27);
            this.m_infoLabel.Name = "m_infoLabel";
            this.m_infoLabel.Size = new System.Drawing.Size(153, 20);
            this.m_infoLabel.TabIndex = 3;
            this.m_infoLabel.Text = "List of items in backet";
            // 
            // MyBasketForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancleButton;
            this.ClientSize = new System.Drawing.Size(795, 450);
            this.Controls.Add(this.m_infoLabel);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_cancleButton);
            this.Controls.Add(this.m_okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyBasketForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "My Basket";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancleButton;
        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.Label m_infoLabel;
    }
}