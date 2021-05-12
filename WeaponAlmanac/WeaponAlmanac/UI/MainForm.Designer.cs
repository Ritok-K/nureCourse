
namespace WeaponAlmanac.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_headerPanel = new System.Windows.Forms.Panel();
            this.m_footerPanel = new System.Windows.Forms.Panel();
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_addButton = new System.Windows.Forms.Button();
            this.m_deleteButton = new System.Windows.Forms.Button();
            this.m_editButton = new System.Windows.Forms.Button();
            this.m_footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_headerPanel
            // 
            this.m_headerPanel.BackColor = System.Drawing.Color.Aqua;
            this.m_headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_headerPanel.Location = new System.Drawing.Point(0, 0);
            this.m_headerPanel.Name = "m_headerPanel";
            this.m_headerPanel.Size = new System.Drawing.Size(800, 43);
            this.m_headerPanel.TabIndex = 0;
            // 
            // m_footerPanel
            // 
            this.m_footerPanel.BackColor = System.Drawing.Color.MistyRose;
            this.m_footerPanel.Controls.Add(this.m_editButton);
            this.m_footerPanel.Controls.Add(this.m_deleteButton);
            this.m_footerPanel.Controls.Add(this.m_addButton);
            this.m_footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_footerPanel.Location = new System.Drawing.Point(0, 408);
            this.m_footerPanel.Name = "m_footerPanel";
            this.m_footerPanel.Size = new System.Drawing.Size(800, 42);
            this.m_footerPanel.TabIndex = 1;
            // 
            // m_listView
            // 
            this.m_listView.BackColor = System.Drawing.Color.LightPink;
            this.m_listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(0, 43);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(800, 365);
            this.m_listView.TabIndex = 2;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            // 
            // m_addButton
            // 
            this.m_addButton.Location = new System.Drawing.Point(12, 6);
            this.m_addButton.Name = "m_addButton";
            this.m_addButton.Size = new System.Drawing.Size(94, 30);
            this.m_addButton.TabIndex = 0;
            this.m_addButton.Text = "Add";
            this.m_addButton.UseVisualStyleBackColor = true;
            // 
            // m_deleteButton
            // 
            this.m_deleteButton.Location = new System.Drawing.Point(118, 6);
            this.m_deleteButton.Name = "m_deleteButton";
            this.m_deleteButton.Size = new System.Drawing.Size(94, 30);
            this.m_deleteButton.TabIndex = 1;
            this.m_deleteButton.Text = "Delete";
            this.m_deleteButton.UseVisualStyleBackColor = true;
            // 
            // m_editButton
            // 
            this.m_editButton.Location = new System.Drawing.Point(224, 6);
            this.m_editButton.Name = "m_editButton";
            this.m_editButton.Size = new System.Drawing.Size(94, 30);
            this.m_editButton.TabIndex = 2;
            this.m_editButton.Text = "Edit";
            this.m_editButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_footerPanel);
            this.Controls.Add(this.m_headerPanel);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MainForm";
            this.Text = "Weapon Almanac";
            this.m_footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_headerPanel;
        private System.Windows.Forms.Panel m_footerPanel;
        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.Button m_editButton;
        private System.Windows.Forms.Button m_deleteButton;
        private System.Windows.Forms.Button m_addButton;
    }
}

