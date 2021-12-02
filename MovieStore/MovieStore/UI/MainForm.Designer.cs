
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_menuStrip = new System.Windows.Forms.MenuStrip();
            this.m_viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_moviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_actorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_studiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_myBasketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_topsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_topMoviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_topUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStrip = new System.Windows.Forms.ToolStrip();
            this.m_deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_addNewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_nextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_prevToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_receiptToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_reportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuStrip.SuspendLayout();
            this.m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_listView
            // 
            this.m_listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(0, 28);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(782, 498);
            this.m_listView.TabIndex = 1;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            this.m_listView.ItemActivate += new System.EventHandler(this.OnItemActivated);
            this.m_listView.SelectedIndexChanged += new System.EventHandler(this.OnListViewSelectionChanged);
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
            this.m_studiosToolStripMenuItem,
            this.m_ordersToolStripMenuItem,
            this.m_usersToolStripMenuItem,
            this.toolStripSeparator1,
            this.m_myBasketToolStripMenuItem,
            this.m_topsToolStripMenuItem});
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
            // m_topsToolStripMenuItem
            // 
            this.m_topsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_topMoviesToolStripMenuItem,
            this.m_topUsersToolStripMenuItem});
            this.m_topsToolStripMenuItem.Name = "m_topsToolStripMenuItem";
            this.m_topsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_topsToolStripMenuItem.Text = "Tops";
            // 
            // m_topMoviesToolStripMenuItem
            // 
            this.m_topMoviesToolStripMenuItem.Name = "m_topMoviesToolStripMenuItem";
            this.m_topMoviesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_topMoviesToolStripMenuItem.Text = "Top Movies";
            this.m_topMoviesToolStripMenuItem.Click += new System.EventHandler(this.OnTopMoviesMode);
            // 
            // m_topUsersToolStripMenuItem
            // 
            this.m_topUsersToolStripMenuItem.Name = "m_topUsersToolStripMenuItem";
            this.m_topUsersToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.m_topUsersToolStripMenuItem.Text = "Top Users";
            this.m_topUsersToolStripMenuItem.Click += new System.EventHandler(this.OnTopUsersMode);
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
            // m_toolStrip
            // 
            this.m_toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_deleteToolStripButton,
            this.m_addNewToolStripButton,
            this.m_nextToolStripButton,
            this.m_prevToolStripButton,
            this.m_receiptToolStripButton,
            this.m_reportToolStripButton});
            this.m_toolStrip.Location = new System.Drawing.Point(0, 526);
            this.m_toolStrip.Name = "m_toolStrip";
            this.m_toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_toolStrip.Size = new System.Drawing.Size(782, 27);
            this.m_toolStrip.TabIndex = 0;
            // 
            // m_deleteToolStripButton
            // 
            this.m_deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_deleteToolStripButton.Image")));
            this.m_deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_deleteToolStripButton.Name = "m_deleteToolStripButton";
            this.m_deleteToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.m_deleteToolStripButton.Text = "Delete selected";
            this.m_deleteToolStripButton.Click += new System.EventHandler(this.OnDeleteSelected);
            // 
            // m_addNewToolStripButton
            // 
            this.m_addNewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_addNewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_addNewToolStripButton.Image")));
            this.m_addNewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_addNewToolStripButton.Name = "m_addNewToolStripButton";
            this.m_addNewToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.m_addNewToolStripButton.Text = "Add new";
            this.m_addNewToolStripButton.Click += new System.EventHandler(this.OnAddNew);
            // 
            // m_nextToolStripButton
            // 
            this.m_nextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_nextToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_nextToolStripButton.Image")));
            this.m_nextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_nextToolStripButton.Name = "m_nextToolStripButton";
            this.m_nextToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.m_nextToolStripButton.Text = "Next";
            // 
            // m_prevToolStripButton
            // 
            this.m_prevToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_prevToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_prevToolStripButton.Image")));
            this.m_prevToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_prevToolStripButton.Name = "m_prevToolStripButton";
            this.m_prevToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.m_prevToolStripButton.Text = "Previous";
            // 
            // m_receiptToolStripButton
            // 
            this.m_receiptToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_receiptToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_receiptToolStripButton.Image")));
            this.m_receiptToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_receiptToolStripButton.Name = "m_receiptToolStripButton";
            this.m_receiptToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.m_receiptToolStripButton.Text = "Export receipt";
            this.m_receiptToolStripButton.Click += new System.EventHandler(this.OnBuildReceipt);
            // 
            // m_reportToolStripButton
            // 
            this.m_reportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_reportToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_reportToolStripButton.Image")));
            this.m_reportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_reportToolStripButton.Name = "m_reportToolStripButton";
            this.m_reportToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.m_reportToolStripButton.Text = "Orders Report";
            this.m_reportToolStripButton.Click += new System.EventHandler(this.OnBuildReport);
            // 
            // tToolStripMenuItem
            // 
            this.tToolStripMenuItem.Name = "tToolStripMenuItem";
            this.tToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.tToolStripMenuItem.Text = "T";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_toolStrip);
            this.Controls.Add(this.m_menuStrip);
            this.MainMenuStrip = this.m_menuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movies";
            this.Load += new System.EventHandler(this.OnLoad);
            this.m_menuStrip.ResumeLayout(false);
            this.m_menuStrip.PerformLayout();
            this.m_toolStrip.ResumeLayout(false);
            this.m_toolStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem m_myBasketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.ToolStripButton m_deleteToolStripButton;
        private System.Windows.Forms.ToolStripButton m_addNewToolStripButton;
        private System.Windows.Forms.ToolStripButton m_nextToolStripButton;
        private System.Windows.Forms.ToolStripButton m_prevToolStripButton;
        private System.Windows.Forms.ToolStripButton m_receiptToolStripButton;
        private System.Windows.Forms.ToolStripButton m_reportToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem m_topMoviesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_topUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_topsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

