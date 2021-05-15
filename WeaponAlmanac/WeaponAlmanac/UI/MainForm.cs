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
                    break;
                case ContentMode.Collectors:
                    break;
                case ContentMode.OwnWeapon:
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
            m_weaponButton.Enabled = IsWeaponEnabled;
            m_collectorsButton.Enabled = IsCollectorsEnabled;
            m_searchButton.Enabled = IsSearchEnabled;
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

        UIMode m_mode;
        ContentMode m_content;
    }
}
