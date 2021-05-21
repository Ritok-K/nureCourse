using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        bool IsDeleteEnabled => IsListEditable && m_listView.SelectedItems.Count > 0;
        bool IsEditEnabled => IsListEditable && m_listView.SelectedItems.Count > 0;
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
            m_listView.Columns.Add(Properties.Resources.WeaponNameColumn);
            m_listView.Columns.Add(Properties.Resources.WeaponDescriptionColumn);
            m_listView.Columns.Add(Properties.Resources.WeaponYearColumn);
            m_listView.Columns.Add(Properties.Resources.WeaponIsRareColumn);

            var items = new List<ListViewItem>();
            foreach (var w in weapon)
            {
                var item = new ListViewItem(new string[] { w.Name,
                                                           w.Description,
                                                           DataModelUtils.FormatYear(w.ManufactureDate),
                                                           w.IsRare ? Properties.Resources.IsRareItem :
                                                                      Properties.Resources.IsNotRareItem })
                {
                    Tag = w,
                    Name = w.Name,
                    ImageKey = w.Id,
                };
                items.Add(item);
            }

            m_listView.Items.AddRange(items.ToArray());
            PopulateImageList(weapon);
        }

        void PopulateImageList(IList<Weapon> weapon)
        {
            m_listViewImageList.Images.Clear();
            foreach(var w in weapon)
            {
                if (w.Image != null)
                {
                    m_listViewImageList.Images.Add(w.Id, w.Image);
                }
            }
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

        void AddDataModelObject()
        {
            switch (Content)
            {
                case ContentMode.OwnWeapon:
                case ContentMode.Weapon:
                    {
                        var weapon = new Weapon();
                        using (var weaponForm = new WeaponForm() { Weapon = weapon })
                        {
                            var dialogResult = weaponForm.ShowDialog(this);
                            if (dialogResult == DialogResult.OK)
                            {
                                if (Content == ContentMode.Weapon)
                                {
                                    Program.Repository.SetWeapon(weapon);
                                }
                                else
                                {
                                    Program.Repository.SetOwnWeapon(weapon);
                                }

                                UpdateListContent();
                            }
                        }
                    }
                    break;
                case ContentMode.Collectors:
                    {
                        var collector = new Collector();
                        using (var collectorForm = new CollectorForm() { Collector = collector })
                        {
                            var dialogResult = collectorForm.ShowDialog(this);
                            if (dialogResult == DialogResult.OK)
                            {
                                Program.Repository.SetCollector(collector);
                                UpdateListContent();
                            }
                        }
                    }
                    break;
            }
        }

        void EditSelectedDataModelObject()
        {
            if (m_listView.SelectedItems.Count > 0)
            {
                var dataModelObject = m_listView.SelectedItems[0].Tag as DataModelObject;
                ShowDataModelObject(dataModelObject, false);
            }
        }

        void ViewSelectedDataModelObject()
        {
            if (m_listView.SelectedItems.Count > 0)
            {
                var dataModelObject = m_listView.SelectedItems[0].Tag as DataModelObject;
                ShowDataModelObject(dataModelObject, true);
            }
        }

        void ShowDataModelObject(DataModelObject dataModelObject, bool viewOnlyMode)
        {
            switch (Content)
            {
                case ContentMode.OwnWeapon:
                case ContentMode.Weapon:
                    {
                        var weapon = dataModelObject as Weapon;
                        using (var weaponForm = new WeaponForm() { Weapon = weapon,
                                                                   ViewOnly = viewOnlyMode })
                        {
                            var dialogResult = weaponForm.ShowDialog(this);
                            if ((dialogResult == DialogResult.OK) && !viewOnlyMode)
                            {
                                if (Content == ContentMode.Weapon)
                                {
                                    Program.Repository.SetWeapon(weapon);
                                }
                                else
                                {
                                    Program.Repository.SetOwnWeapon(weapon);
                                }

                                UpdateListContent();
                            }
                        }
                    }
                    break;
                case ContentMode.Collectors:
                    {
                        var collector = dataModelObject as Collector;
                        using (var collectorForm = new CollectorForm() { Collector = collector,
                                                                         ViewOnly = viewOnlyMode })
                        {
                            var dialogResult = collectorForm.ShowDialog(this);
                            if ((dialogResult == DialogResult.OK) && !viewOnlyMode)
                            {
                                Program.Repository.SetCollector(collector);
                                UpdateListContent();
                            }
                        }
                    }
                    break;
            }
        }

        void DeleteSelectedDataModelObjects()
        {
            var itemIdsToDelete = new List<DataModelObject>();
            foreach(ListViewItem item in m_listView.SelectedItems)
            {
                itemIdsToDelete.Add(item.Tag as DataModelObject);
            }

            DeleteDataModelObjects(itemIdsToDelete);
        }

        void DeleteDataModelObjects(IList<DataModelObject> dataModelObjects)
        {
            switch (Content)
            {
                case ContentMode.Weapon:
                    foreach (var dataModelObject in dataModelObjects)
                    {
                        Program.Repository.RemoveWeapon(dataModelObject.Id);
                        RemoveListItem(dataModelObject);
                    }
                    break;
                case ContentMode.Collectors:
                    foreach (var dataModelObject in dataModelObjects)
                    {
                        Program.Repository.RemoveCollector(dataModelObject.Id);
                        RemoveListItem(dataModelObject);
                    }
                    break;
                case ContentMode.OwnWeapon:
                    foreach (var dataModelObject in dataModelObjects)
                    {
                        Program.Repository.RemoveOwnWeapon(dataModelObject.Id);
                        RemoveListItem(dataModelObject);
                    }
                    break;
            }
        }

        void RemoveListItem(DataModelObject dataModelObject)
        {
            foreach (ListViewItem item in m_listView.Items)
            {
                if ((item.Tag as DataModelObject).Id == dataModelObject.Id)
                {
                    m_listView.Items.Remove(item);
                    break;
                }
            }
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
                    m_listView.AutoResizeColumns(m_listView.Items.Count>0 ? ColumnHeaderAutoResizeStyle.ColumnContent : 
                                                                            ColumnHeaderAutoResizeStyle.HeaderSize);
                    break;
            }
        }

        void UpdateState()
        {
            m_footerPanel.Visible = IsListEditable;
            m_addButton.Enabled = IsListEditable;
            m_deleteButton.Enabled = IsDeleteEnabled;
            m_editButton.Enabled = IsEditEnabled;
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

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateState();
            UpdateListContent();
        }

        private void OnWeaponClick(object sender, EventArgs e)
        {
            try
            {
                Content = ContentMode.Weapon;
                UpdateListContent();
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message), 
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnCollectorsClick(object sender, EventArgs e)
        {
            try
            {
                Content = ContentMode.Collectors;
                UpdateListContent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message), 
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnOwnWeaponClick(object sender, EventArgs e)
        {
            try
            {
                Content = ContentMode.OwnWeapon;
                UpdateListContent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            try
            {
                AddDataModelObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                var confirmRes = MessageBox.Show(Properties.Resources.ConfirmDeletion, 
                                                 this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmRes == DialogResult.Yes)
                {
                    DeleteSelectedDataModelObjects();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message), 
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnEditClick(object sender, EventArgs e)
        {
            try
            {
                EditSelectedDataModelObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSearchClick(object sender, EventArgs e)
        {

        }

        private void OnItemActivated(object sender, EventArgs e)
        {
            try
            {
                if (IsListEditable)
                {
                    EditSelectedDataModelObject();
                }
                else
                {
                    ViewSelectedDataModelObject();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnListSizeChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateListItemSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        UIMode m_mode;
        ContentMode m_content;
    }
}
