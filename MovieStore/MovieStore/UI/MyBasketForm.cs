using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieStore.UI
{
    public partial class MyBasketForm : Form
    {
        internal IEnumerable<int> BasketList { get; init; } = Enumerable.Empty<int>();

        public MyBasketForm()
        {
            InitializeComponent();
        }
    }
}
