
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
            this.m_searchButton = new System.Windows.Forms.Button();
            this.m_ownCollectionButton = new System.Windows.Forms.Button();
            this.m_collectorsButton = new System.Windows.Forms.Button();
            this.m_weaponButton = new System.Windows.Forms.Button();
            this.m_footerPanel = new System.Windows.Forms.Panel();
            this.m_editButton = new System.Windows.Forms.Button();
            this.m_deleteButton = new System.Windows.Forms.Button();
            this.m_addButton = new System.Windows.Forms.Button();
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_headerPanel.SuspendLayout();
            this.m_footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_headerPanel
            // 
            this.m_headerPanel.AutoSize = true;
            this.m_headerPanel.BackColor = System.Drawing.Color.Aqua;
            this.m_headerPanel.Controls.Add(this.m_searchButton);
            this.m_headerPanel.Controls.Add(this.m_ownCollectionButton);
            this.m_headerPanel.Controls.Add(this.m_collectorsButton);
            this.m_headerPanel.Controls.Add(this.m_weaponButton);
            this.m_headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_headerPanel.Location = new System.Drawing.Point(0, 0);
            this.m_headerPanel.Name = "m_headerPanel";
            this.m_headerPanel.Padding = new System.Windows.Forms.Padding(3);
            this.m_headerPanel.Size = new System.Drawing.Size(882, 45);
            this.m_headerPanel.TabIndex = 0;
            // 
            // m_searchButton
            // 
            this.m_searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_searchButton.AutoSize = true;
            this.m_searchButton.Location = new System.Drawing.Point(773, 9);
            this.m_searchButton.Name = "m_searchButton";
            this.m_searchButton.Size = new System.Drawing.Size(94, 30);
            this.m_searchButton.TabIndex = 3;
            this.m_searchButton.Text = "Search";
            this.m_searchButton.UseVisualStyleBackColor = true;
            // 
            // m_ownCollectionButton
            // 
            this.m_ownCollectionButton.AutoSize = true;
            this.m_ownCollectionButton.Location = new System.Drawing.Point(221, 9);
            this.m_ownCollectionButton.Name = "m_ownCollectionButton";
            this.m_ownCollectionButton.Size = new System.Drawing.Size(120, 30);
            this.m_ownCollectionButton.TabIndex = 2;
            this.m_ownCollectionButton.Text = "Own Collection";
            this.m_ownCollectionButton.UseVisualStyleBackColor = true;
            // 
            // m_collectorsButton
            // 
            this.m_collectorsButton.AutoSize = true;
            this.m_collectorsButton.Location = new System.Drawing.Point(115, 9);
            this.m_collectorsButton.Name = "m_collectorsButton";
            this.m_collectorsButton.Size = new System.Drawing.Size(100, 30);
            this.m_collectorsButton.TabIndex = 1;
            this.m_collectorsButton.Text = "Collectors";
            this.m_collectorsButton.UseVisualStyleBackColor = true;
            // 
            // m_weaponButton
            // 
            this.m_weaponButton.AutoSize = true;
            this.m_weaponButton.Location = new System.Drawing.Point(12, 9);
            this.m_weaponButton.Name = "m_weaponButton";
            this.m_weaponButton.Size = new System.Drawing.Size(94, 30);
            this.m_weaponButton.TabIndex = 0;
            this.m_weaponButton.Text = "Weapon";
            this.m_weaponButton.UseVisualStyleBackColor = true;
            // 
            // m_footerPanel
            // 
            this.m_footerPanel.AutoSize = true;
            this.m_footerPanel.BackColor = System.Drawing.Color.MistyRose;
            this.m_footerPanel.Controls.Add(this.m_editButton);
            this.m_footerPanel.Controls.Add(this.m_deleteButton);
            this.m_footerPanel.Controls.Add(this.m_addButton);
            this.m_footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_footerPanel.Location = new System.Drawing.Point(0, 511);
            this.m_footerPanel.Name = "m_footerPanel";
            this.m_footerPanel.Padding = new System.Windows.Forms.Padding(3);
            this.m_footerPanel.Size = new System.Drawing.Size(882, 42);
            this.m_footerPanel.TabIndex = 1;
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
            // m_deleteButton
            // 
            this.m_deleteButton.Location = new System.Drawing.Point(118, 6);
            this.m_deleteButton.Name = "m_deleteButton";
            this.m_deleteButton.Size = new System.Drawing.Size(94, 30);
            this.m_deleteButton.TabIndex = 1;
            this.m_deleteButton.Text = "Delete";
            this.m_deleteButton.UseVisualStyleBackColor = true;
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
            // m_listView
            // 
            this.m_listView.BackColor = System.Drawing.Color.LightPink;
            this.m_listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(0, 45);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(882, 466);
            this.m_listView.TabIndex = 2;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_footerPanel);
            this.Controls.Add(this.m_headerPanel);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "Weapon Almanac";
            this.m_headerPanel.ResumeLayout(false);
            this.m_headerPanel.PerformLayout();
            this.m_footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel m_headerPanel;
        private System.Windows.Forms.Panel m_footerPanel;
        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.Button m_editButton;
        private System.Windows.Forms.Button m_deleteButton;
        private System.Windows.Forms.Button m_addButton;
        private System.Windows.Forms.Button m_searchButton;
        private System.Windows.Forms.Button m_ownCollectionButton;
        private System.Windows.Forms.Button m_collectorsButton;
        private System.Windows.Forms.Button m_weaponButton;
    }
}

