
namespace MovieStore.UI
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
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_menuStrip = new System.Windows.Forms.MenuStrip();
            this.m_viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_moviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_actorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_movieToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.m_studiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_myBasketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_listView
            // 
            this.m_listView.FullRowSelect = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(12, 37);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(758, 403);
            this.m_listView.TabIndex = 0;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            // 
            // m_menuStrip
            // 
            this.m_menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_viewToolStripMenuItem,
            this.m_logoutToolStripMenuItem,
            this.m_exitToolStripMenuItem});
            this.m_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_menuStrip.Name = "m_menuStrip";
            this.m_menuStrip.Size = new System.Drawing.Size(782, 28);
            this.m_menuStrip.TabIndex = 1;
            this.m_menuStrip.Text = "menuStrip1";
            // 
            // m_viewToolStripMenuItem
            // 
            this.m_viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_moviesToolStripMenuItem,
            this.m_actorsToolStripMenuItem,
            this.m_movieToolStripSeparator,
            this.m_studiosToolStripMenuItem,
            this.m_ordersToolStripMenuItem,
            this.m_usersToolStripMenuItem,
            this.m_myBasketToolStripMenuItem});
            this.m_viewToolStripMenuItem.Name = "m_viewToolStripMenuItem";
            this.m_viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.m_viewToolStripMenuItem.Text = "View";
            // 
            // m_moviesToolStripMenuItem
            // 
            this.m_moviesToolStripMenuItem.Name = "m_moviesToolStripMenuItem";
            this.m_moviesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_moviesToolStripMenuItem.Text = "Movies";
            this.m_moviesToolStripMenuItem.Click += new System.EventHandler(this.OnMoviesMode);
            // 
            // m_actorsToolStripMenuItem
            // 
            this.m_actorsToolStripMenuItem.Name = "m_actorsToolStripMenuItem";
            this.m_actorsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_actorsToolStripMenuItem.Text = "Actors";
            this.m_actorsToolStripMenuItem.Click += new System.EventHandler(this.OnActorsMode);
            // 
            // m_movieToolStripSeparator
            // 
            this.m_movieToolStripSeparator.Name = "m_movieToolStripSeparator";
            this.m_movieToolStripSeparator.Size = new System.Drawing.Size(221, 6);
            // 
            // m_studiosToolStripMenuItem
            // 
            this.m_studiosToolStripMenuItem.Name = "m_studiosToolStripMenuItem";
            this.m_studiosToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_studiosToolStripMenuItem.Text = "Studio";
            this.m_studiosToolStripMenuItem.Click += new System.EventHandler(this.OnStudioMode);
            // 
            // m_ordersToolStripMenuItem
            // 
            this.m_ordersToolStripMenuItem.Name = "m_ordersToolStripMenuItem";
            this.m_ordersToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_ordersToolStripMenuItem.Text = "Orders";
            this.m_ordersToolStripMenuItem.Click += new System.EventHandler(this.OnOrdersMode);
            // 
            // m_usersToolStripMenuItem
            // 
            this.m_usersToolStripMenuItem.Name = "m_usersToolStripMenuItem";
            this.m_usersToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_usersToolStripMenuItem.Text = "Users";
            this.m_usersToolStripMenuItem.Click += new System.EventHandler(this.OnUsersMode);
            // 
            // m_myBasketToolStripMenuItem
            // 
            this.m_myBasketToolStripMenuItem.Name = "m_myBasketToolStripMenuItem";
            this.m_myBasketToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_myBasketToolStripMenuItem.Text = "My basket";
            this.m_myBasketToolStripMenuItem.Click += new System.EventHandler(this.OnMyBasketMode);
            // 
            // m_logoutToolStripMenuItem
            // 
            this.m_logoutToolStripMenuItem.Name = "m_logoutToolStripMenuItem";
            this.m_logoutToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.m_logoutToolStripMenuItem.Text = "Logout";
            this.m_logoutToolStripMenuItem.Click += new System.EventHandler(this.OnLogout);
            // 
            // m_exitToolStripMenuItem
            // 
            this.m_exitToolStripMenuItem.Name = "m_exitToolStripMenuItem";
            this.m_exitToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.m_exitToolStripMenuItem.Text = "Exit";
            this.m_exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_menuStrip);
            this.MainMenuStrip = this.m_menuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movies";
            this.Load += new System.EventHandler(this.OnLoad);
            this.m_menuStrip.ResumeLayout(false);
            this.m_menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.MenuStrip m_menuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_moviesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_actorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_studiosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator m_movieToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem m_myBasketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_logoutToolStripMenuItem;
    }
}

