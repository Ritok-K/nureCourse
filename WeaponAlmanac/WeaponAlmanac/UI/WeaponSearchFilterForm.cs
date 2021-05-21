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
    public partial class WeaponSearchFilterForm : Form
    {
        public WeaponSearchFilterForm()
        {
            InitializeComponent();
        }

        public WeaponFilter Filter { get; set; }

        #region Helper Methods

        void UpdateData(bool save)
        {
            if (save)
            {
                Filter.Name = m_nameTextBox.Text.Trim();
                Filter.Country = m_countryTextBox.Text.Trim();
                Filter.Description = m_descriptionTextBox.Text.Trim();

                Filter.ManufacturedStartDate = DataModelUtils.ParseYear(m_manifacturedFromTextBox.Text);
                Filter.ManufacturedEndDate = DataModelUtils.ParseYear(m_manifacturedToTextBox.Text);
                if (!Filter.HasValidManufacturedDate)
                {
                    Filter.ManufacturedStartDate = DataModelUtils.InvalidDate;
                    Filter.ManufacturedEndDate = DataModelUtils.InvalidDate;
                }
            }
            else
            {
                m_nameTextBox.Text = Filter.Name;
                m_countryTextBox.Text = Filter.Country;
                m_descriptionTextBox.Text = Filter.Description;
                m_manifacturedFromTextBox.Text = DataModelUtils.FormatYear(Filter.ManufacturedStartDate);
                m_manifacturedToTextBox.Text = DataModelUtils.FormatYear(Filter.ManufacturedEndDate);
            }
        }

        #endregion

        #region Evet Handlers

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateData(false);
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            UpdateData(true);

            DialogResult = DialogResult.OK;
        }

        private void OnResetClick(object sender, EventArgs e)
        {
            m_nameTextBox.Text = string.Empty;
            m_countryTextBox.Text = string.Empty;
            m_descriptionTextBox.Text = string.Empty;
            m_manifacturedFromTextBox.Text = string.Empty;
            m_manifacturedToTextBox.Text = string.Empty;

            UpdateData(true);

            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
