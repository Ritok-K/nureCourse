using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponAlmanac.Data_Model;

namespace WeaponAlmanac.UI
{
    public partial class MainForm : Form
    {
        enum UIMode
        {
            User,
            Administator
        }

        enum ContentMode
        {
            Weapon,
            Collectors,
            OwnWeapon
        }


        UIMode Mode 
        {
            get => m_mode; 
            set { if (m_mode != value) { m_mode = value; UpdateState(); } }
        }

        ContentMode Content
        {
            get => m_content;
            set { if (m_content != value) { m_content = value; UpdateState(); } }
        }

        bool IsListEditable => (Mode == UIMode.User) ? Content == ContentMode.OwnWeapon :
                                                       Content != ContentMode.OwnWeapon;
        bool IsOwnWeaponEnabled => (Mode == UIMode.User) && (Content != ContentMode.OwnWeapon);
        bool IsOwnWeaponVisible => (Mode == UIMode.User);
        bool IsWeaponEnabled => (Content != ContentMode.Weapon);
        bool IsCollectorsEnabled => (Content != ContentMode.Collectors);
        bool IsSearchEnabled => true;

        public MainForm()
        {
            m_mode = UIMode.User;
            m_content = ContentMode.Weapon;

            InitializeComponent();

            UpdateListContent();
            UpdateState();
        }

        #region Helper Methods

        void UpdateListContent()
        {
            switch(Content)
            {
                case ContentMode.Weapon:
                    PopulateWeaponList(Program.Repository.GetWeapon());
                    break;
                case ContentMode.Collectors:
                    PopulateCollectorsList(Program.Repository.GetCollectors());
                    break;
                case ContentMode.OwnWeapon:
                    PopulateWeaponList(Program.Repository.GetOwnWeapon());
                    break;
            }

            UpdateListItemSize();
        }

        void PopulateWeaponList(IList<Weapon> weapon)
        {
            m_listView.Items.Clear();
            m_listView.FullRowSelect = true;
            m_listView.GridLines = true;
            m_listView.Sorting = SortOrder.Ascending;
            m_listView.View = View.Tile;
            m_listView.Columns.Clear();
            m_listView.Columns.Add(Properties.Resources.WeaponNameColumn, -2, HorizontalAlignment.Left);
            m_listView.Columns.Add(Properties.Resources.WeaponDescriptionColumn, -2, HorizontalAlignment.Left);
            m_listView.Columns.Add(Properties.Resources.WeaponIsRareColumn, -2, HorizontalAlignment.Left);

            var items = new List<ListViewItem>();
            foreach (var w in weapon)
            {
                var item = new ListViewItem(new string[] { w.Name,
                                                           w.Description, 
                                                           w.IsRare ? Properties.Resources.IsRareItem :
                                                                      Properties.Resources.IsNotRareItem })
                {
                    Tag = w,
                    Name = w.Name,
                };
                items.Add(item);
            }

            m_listView.Items.AddRange(items.ToArray());
        }

        void PopulateCollectorsList(IList<Collector> collectors)
        {
            m_listView.Items.Clear();
            m_listView.FullRowSelect = true;
            m_listView.GridLines = true;
            m_listView.Sorting = SortOrder.Ascending;
            m_listView.View = View.Details;
            m_listView.Columns.Clear();
            m_listView.Columns.Add(Properties.Resources.CollectorNameColumn, -2, HorizontalAlignment.Left);
            m_listView.Columns.Add(Properties.Resources.CollectorPhoneColumn, -2, HorizontalAlignment.Left);
            m_listView.Columns.Add(Properties.Resources.CollectorEMailColumn, -2, HorizontalAlignment.Left);
            m_listView.Columns.Add(Properties.Resources.CollecrorHasRareColumn, -2, HorizontalAlignment.Left);

            var items = new List<ListViewItem>();
            foreach (var c in collectors)
            {
                var item = new ListViewItem(new string[] { c.Name,
                                                           c.Phone,
                                                           c.EMail, 
                                                           c.RareIds.Count==0 ? Properties.Resources.NoItem :
                                                                                Properties.Resources.YesItem })
                {
                    Tag = c,
                    Name = c.Name,
                };
                items.Add(item);
            }

            m_listView.Items.AddRange(items.ToArray());
        }

        void UpdateListItemSize()
        {
            switch (Content)
            {
                case ContentMode.Weapon:
                case ContentMode.OwnWeapon:
                    m_listView.TileSize = new Size(m_listView.Width - SystemInformation.VerticalScrollBarWidth, 80);
                    break;
                case ContentMode.Collectors:
                    m_listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    break;
            }
        }

        void UpdateState()
        {
            m_footerPanel.Visible = IsListEditable;
            m_addButton.Enabled = IsListEditable;
            m_deleteButton.Enabled = IsListEditable;
            m_editButton.Enabled = IsListEditable;
            m_ownWeaponButton.Enabled = IsOwnWeaponEnabled;
            m_ownWeaponButton.Visible = IsOwnWeaponVisible;
            m_weaponButton.Enabled = IsWeaponEnabled;
            m_collectorsButton.Enabled = IsCollectorsEnabled;
            m_searchButton.Enabled = IsSearchEnabled;
        }

        void SwitchUIMode()
        {
            switch(Mode)
            {
                case UIMode.User:
                    if (Content==ContentMode.OwnWeapon)
                    {
                        Content = ContentMode.Weapon;
                        UpdateListContent();
                    }
                    Mode = UIMode.Administator;
                    break;

                case UIMode.Administator:
                    Mode = UIMode.User;
                    break;
            }
        }

        #endregion

        #region Overrides

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift| Keys.A))
            {
                SwitchUIMode();

                switch (Mode)
                {
                    case UIMode.User:
                        MessageBox.Show(Properties.Resources.UserModeActicated, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case UIMode.Administator:
                        MessageBox.Show(Properties.Resources.AdministrationModeActivated, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Event Handlers

        private void OnWeaponClick(object sender, EventArgs e)
        {
            Content = ContentMode.Weapon;
            UpdateListContent();
        }

        private void OnCollectorsClick(object sender, EventArgs e)
        {
            Content = ContentMode.Collectors;
            UpdateListContent();
        }

        private void OnOwnWeaponClick(object sender, EventArgs e)
        {
            Content = ContentMode.OwnWeapon;
            UpdateListContent();
        }

        private void OnAddClick(object sender, EventArgs e)
        {

        }

        private void OnDeleteClick(object sender, EventArgs e)
        {

        }

        private void OnEditClick(object sender, EventArgs e)
        {

        }

        private void OnSearchClick(object sender, EventArgs e)
        {

        }

        private void OnItemActivated(object sender, EventArgs e)
        {

        }

        private void OnListSizeChanged(object sender, EventArgs e)
        {
            UpdateListItemSize();
        }

        #endregion

        UIMode m_mode;
        ContentMode m_content;
    }
}
