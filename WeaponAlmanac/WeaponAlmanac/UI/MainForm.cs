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
    enum UIMode
    { 
        User,
        Administator
    }

    public partial class MainForm : Form
    {
        internal UIMode Mode { get; set; } = UIMode.User;

        public MainForm()
        {
            InitializeComponent();
        }

    }
}
