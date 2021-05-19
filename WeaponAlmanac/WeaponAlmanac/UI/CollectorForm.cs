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
    }
}
