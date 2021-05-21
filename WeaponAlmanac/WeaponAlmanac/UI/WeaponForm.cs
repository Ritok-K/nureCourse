﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeaponAlmanac.Data_Model;

namespace WeaponAlmanac.UI
{
    public partial class WeaponForm : Form
    {
        public WeaponForm()
        {
            InitializeComponent();
        }

        public Weapon Weapon { get; set; }
        public bool ViewOnly { get; set; } = false;

        #region Helper Methods
        void UpdateState()
        {
            m_nameTextBox.ReadOnly = ViewOnly;
            m_countryTextBox.ReadOnly = ViewOnly;
            m_descriptionTextBox.ReadOnly = ViewOnly;
            m_materialTextBox.ReadOnly = ViewOnly;
            m_issuedNumberTextBox.ReadOnly = ViewOnly;
            m_manufacturedYearTextBox.ReadOnly = ViewOnly;
            m_rareCheckBox.Enabled = !ViewOnly;
            m_pictureButton.Enabled = !ViewOnly;
            m_pictureButton.Visible = !ViewOnly;

            if (ViewOnly)
            {
                m_pictureBox.Height += m_pictureButton.Bottom - m_pictureBox.Bottom;
            }
        }

        void UpdateControls(bool saveData)
        {
            if (saveData)
            {
                Weapon.Name = m_nameTextBox.Text;
                Weapon.Country = m_countryTextBox.Text;
                Weapon.Material = m_materialTextBox.Text;
                Weapon.Description = m_descriptionTextBox.Text;
                Weapon.IssuedNumber = uint.Parse(m_issuedNumberTextBox.Text);
                Weapon.ManufactureDate = DateTime.ParseExact(m_manufacturedYearTextBox.Text, "yyyy", CultureInfo.CurrentUICulture);
                Weapon.IsRare = m_rareCheckBox.Checked;

                Weapon.Image?.Dispose();
                Weapon.Image = (m_bitmap?.Clone() as Bitmap);
            }
            else
            {
                m_nameTextBox.Text = Weapon.Name;
                m_countryTextBox.Text = Weapon.Country;
                m_materialTextBox.Text = Weapon.Material;
                m_descriptionTextBox.Text = Weapon.Description;
                m_issuedNumberTextBox.Text = Weapon.IssuedNumber.ToString();
                m_manufacturedYearTextBox.Text = Weapon.ManufactureDate.ToString("yyyy", CultureInfo.CurrentUICulture);
                m_rareCheckBox.Checked = Weapon.IsRare;

                m_bitmap = (Weapon.Image?.Clone() as Bitmap);
                if (m_bitmap != null)
                {
                    m_pictureBox.Image = m_bitmap;
                }
            }
        }

        #endregion

        #region Event Handlers

        private void OnLoda(object sender, EventArgs e)
        {
            UpdateControls(false);
            UpdateState();
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            UpdateControls(true);

            DialogResult = DialogResult.OK;
        }

        private void OnPictureBrowseClick(object sender, EventArgs e)
        {
            if (m_pictureBrowseFileDialog.ShowDialog()==DialogResult.OK)
            {
                var filePath = m_pictureBrowseFileDialog.FileName;
                Debug.Assert(File.Exists(filePath));

                m_bitmap = new Bitmap(filePath);
                m_pictureBox.Image = m_bitmap;
            }
        }

        #endregion

        #region Member Variables

        Bitmap m_bitmap;

        #endregion
    }
}
