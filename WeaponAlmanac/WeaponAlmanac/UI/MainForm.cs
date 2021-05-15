using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Collector,
            OwnWeapon
        }

        UIMode Mode { get; set; } = UIMode.User;

        ContentMode Content { get; set; } = ContentMode.Weapon;

        public MainForm()
        {
            InitializeComponent();
            UpdateControlsState();
        }

        #region Helper Methods

        void UpdateControlsState()
        {

        }

        #endregion

        #region Event Handlers

        private void OnWeaponClick(object sender, EventArgs e)
        {

        }

        private void OnCollectorsClick(object sender, EventArgs e)
        {

        }

        private void OnOwnWeaponClick(object sender, EventArgs e)
        {

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

        #endregion
    }
}
