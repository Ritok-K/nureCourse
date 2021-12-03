
namespace MovieStore.UI
{
    partial class OrderForm
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
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_totalLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_infoLabel
            // 
            this.m_infoLabel.AutoSize = true;
            this.m_infoLabel.Location = new System.Drawing.Point(28, 25);
            this.m_infoLabel.Name = "m_infoLabel";
            this.m_infoLabel.Size = new System.Drawing.Size(50, 20);
            this.m_infoLabel.TabIndex = 6;
            this.m_infoLabel.Text = "Order:";
            // 
            // m_listView
            // 
            this.m_listView.BackColor = System.Drawing.Color.AntiqueWhite;
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(28, 59);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(744, 307);
            this.m_listView.TabIndex = 5;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            // 
            // m_okButton
            // 
            this.m_okButton.BackColor = System.Drawing.Color.DarkKhaki;
            this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_okButton.Location = new System.Drawing.Point(632, 400);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(140, 29);
            this.m_okButton.TabIndex = 4;
            this.m_okButton.Text = "Ok";
            this.m_okButton.UseVisualStyleBackColor = false;
            // 
            // m_totalLabel
            // 
            this.m_totalLabel.AutoSize = true;
            this.m_totalLabel.Location = new System.Drawing.Point(28, 384);
            this.m_totalLabel.Name = "m_totalLabel";
            this.m_totalLabel.Size = new System.Drawing.Size(45, 20);
            this.m_totalLabel.TabIndex = 7;
            this.m_totalLabel.Text = "Total:";
            // 
            // OrderForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_okButton;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_totalLabel);
            this.Controls.Add(this.m_infoLabel);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Order";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_infoLabel;
        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Label m_totalLabel;
    }
}