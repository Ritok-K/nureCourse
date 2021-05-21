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
    public partial class CollectorSearchFilterForm : Form
    {
        public CollectorSearchFilterForm()
        {
            InitializeComponent();
        }

        public CollectorFilter Filter { get; set; }

        #region Helper Methods

        void UpdateControls(bool saveData)
        {
            if (saveData)
            {
                Filter.Name = m_nameTextBox.Text.Trim();
                Filter.Country = m_countryTextBox.Text.Trim();

                Filter.HasRareWeapon = null;
                if (m_rareCheckBox.CheckState != CheckState.Indeterminate)
                {
                    Filter.HasRareWeapon = m_rareCheckBox.Checked;
                }
            }
            else
            {
                m_nameTextBox.Text = Filter.Name;
                m_countryTextBox.Text = Filter.Country;
                m_rareCheckBox.CheckState = Filter.HasRareWeapon.HasValue ? (Filter.HasRareWeapon.Value ? CheckState.Checked : CheckState.Unchecked) :
                                                                             CheckState.Indeterminate;

            }
        }

        #endregion

        #region Evet Handlers

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateControls(false);
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            UpdateControls(true);

            DialogResult = DialogResult.OK;
        }

        private void OnResetClick(object sender, EventArgs e)
        {
            m_nameTextBox.Text = string.Empty;
            m_countryTextBox.Text = string.Empty;
            m_rareCheckBox.CheckState = CheckState.Indeterminate;

            UpdateControls(true);

            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
