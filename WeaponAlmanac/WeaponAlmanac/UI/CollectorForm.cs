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
using WeaponAlmanac.Data_Model.Filters;

namespace WeaponAlmanac.UI
{
    public partial class CollectorForm : Form
    {
        public CollectorForm()
        {
            InitializeComponent();
        }

        public Collector Collector { get; set; }
        public bool ViewOnly { get; set; } = false;

        #region Helper Methods

        void UpdateState()
        {
            m_nameTextBox.ReadOnly = ViewOnly;
            m_countryTextBox.ReadOnly = ViewOnly;
            m_emailTextBox.ReadOnly = ViewOnly;
            m_phoneTextBox.ReadOnly = ViewOnly;
        }

        void UpdateControls(bool saveData)
        {
            if (saveData)
            {
                Collector.Name = m_nameTextBox.Text;
                Collector.Country = m_countryTextBox.Text;
                Collector.EMail = m_emailTextBox.Text;
                Collector.Phone = m_phoneTextBox.Text;

                Collector.OwnIds.Clear();
                foreach(ListViewItem item in m_rareWeaponListView.Items)
                {
                    if (item.Checked)
                    {
                        Collector.OwnIds.Add((item.Tag as DataModelObject).Id);
                    }
                }
            }
            else
            {
                m_nameTextBox.Text = Collector.Name;
                m_countryTextBox.Text = Collector.Country;
                m_emailTextBox.Text = Collector.EMail;
                m_phoneTextBox.Text = Collector.Phone;

                WeaponFilter weaponFilter = null;
                if (ViewOnly)
                {
                    // in view only mode we use filter so as to load collector's weapon only
                    weaponFilter = new WeaponFilter() { Ids = Collector.OwnIds };
                }
                var weapon = Program.Repository.GetWeapon(weaponFilter);

                m_rareWeaponListView.Items.Clear();
                m_rareWeaponListView.View = View.Details;
                m_rareWeaponListView.FullRowSelect = true;
                m_rareWeaponListView.GridLines = true;
                m_rareWeaponListView.Sorting = SortOrder.Ascending;
                m_rareWeaponListView.Columns.Add(Properties.Resources.WeaponNameColumn);
                m_rareWeaponListView.Columns.Add(Properties.Resources.WeaponIsRareColumn);
                m_rareWeaponListView.Columns.Add(Properties.Resources.WeaponDescriptionColumn);

                if (!ViewOnly)
                {
                    m_rareWeaponListView.CheckBoxes = true;
                }

                var items = new List<ListViewItem>();
                foreach (var w in weapon)
                {
                    var item = new ListViewItem(new string[] { w.Name, 
                                                               w.IsRare ? Properties.Resources.YesItem : 
                                                                          Properties.Resources.NoItem,
                                                               w.Description })
                    {
                        Name = w.Name, 
                        Checked = IsCollectorWeapon(w),
                        Tag = w
                    };

                    items.Add(item);
                }

                m_rareWeaponListView.Items.AddRange(items.ToArray());
                m_rareWeaponListView.AutoResizeColumns(items.Count > 0 ? ColumnHeaderAutoResizeStyle.ColumnContent : 
                                                                         ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        bool IsCollectorWeapon(Weapon weapon)
        {
            return Collector.OwnIds.FindIndex(id => weapon.Id == id) >= 0;
        }

        #endregion

        #region Overrides

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ViewOnly && (keyData == Keys.Escape))
            {
                Close();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Event Handlers

        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                UpdateControls(false);
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            try
            {
                UpdateControls(true);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnNameValidating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(m_nameTextBox.Text.Trim()))
                {
                    e.Cancel = true;
                    MessageBox.Show(Properties.Resources.EmptyNameValidating,
                                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;

                MessageBox.Show(string.Format(Properties.Resources.ExceptionError, ex.Message),
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
