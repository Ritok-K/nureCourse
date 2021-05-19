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

        void UpdateState()
        {
            m_nameTextBox.ReadOnly = ViewOnly;
            m_countryTextBox.ReadOnly = ViewOnly;
            m_emailTextBox.ReadOnly = ViewOnly;
            m_phoneTextBox.ReadOnly = ViewOnly;
            m_rareWeaponListView.Enabled = !ViewOnly;
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
            }
            else
            {
                m_nameTextBox.Text = Collector.Name;
                m_countryTextBox.Text = Collector.Country;
                m_emailTextBox.Text = Collector.EMail;
                m_phoneTextBox.Text = Collector.Phone;
            }
        }

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
