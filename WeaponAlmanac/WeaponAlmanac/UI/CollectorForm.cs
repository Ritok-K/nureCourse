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

                Collector.RareIds.Clear();
                foreach(ListViewItem item in m_rareWeaponListView.Items)
                {
                    if (item.Checked)
                    {
                        Collector.RareIds.Add((item.Tag as DataModelObject).Id);
                    }
                }
            }
            else
            {
                m_nameTextBox.Text = Collector.Name;
                m_countryTextBox.Text = Collector.Country;
                m_emailTextBox.Text = Collector.EMail;
                m_phoneTextBox.Text = Collector.Phone;

                var weapon = Program.Repository.GetWeapon();
                if (ViewOnly)
                {
                    weapon = BuildCollectorWeapon(weapon);
                }

                m_rareWeaponListView.Items.Clear();
                m_rareWeaponListView.View = View.Details;
                m_rareWeaponListView.FullRowSelect = true;
                m_rareWeaponListView.GridLines = true;
                m_rareWeaponListView.Sorting = SortOrder.Ascending;
                m_rareWeaponListView.Columns.Add(Properties.Resources.WeaponNameColumn);
                m_rareWeaponListView.Columns.Add(Properties.Resources.WeaponDescriptionColumn);

                if (!ViewOnly)
                {
                    m_rareWeaponListView.CheckBoxes = true;
                }

                var items = new List<ListViewItem>();
                foreach (var w in weapon)
                {
                    var item = new ListViewItem(new string[] { w.Name, w.Description })
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

        List<Weapon> BuildCollectorWeapon(List<Weapon> allWeapon)
        {
            var collectorsWeapon = new List<Weapon>();
            foreach (var w in allWeapon)
            {
                if (IsCollectorWeapon(w))
                {
                    collectorsWeapon.Add(w);
                }
            }

            return collectorsWeapon;
        }

        bool IsCollectorWeapon(Weapon weapon)
        {
            return Collector.RareIds.FindIndex(id => weapon.Id == id) >= 0;
        }

        #endregion

        #region Event Handlers

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateControls(false);
            UpdateState();
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            UpdateControls(true);

            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
