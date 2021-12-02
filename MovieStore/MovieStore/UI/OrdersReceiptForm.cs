using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieStore.UI
{
    public partial class OrdersReceiptForm : Form
    {
        internal IEnumerable<Data.Order> Orders { get; set; }

        string FileName { get; set; }

        public OrdersReceiptForm()
        {
            InitializeComponent();
        }

        void UpdateDate(bool save)
        {
            if (save)
            {
                if (string.IsNullOrEmpty(m_fileNameTextBox.Text))
                {
                    throw new Exception("Select file path to export.");
                }

                FileName = m_fileNameTextBox.Text;
            }
            else
            {
                if (!(Orders?.Any() ?? false))
                {
                    throw new Exception("No orders selected");
                }
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateDate(false);
        }

        private void OnOk(object sender, EventArgs e)
        {
            try
            {
                UpdateDate(true);

                var reportBuilder = new Reports.OrdersReceipt(Orders.Select(o => o.Id).ToList(), FileName);
                reportBuilder.Build();

                var resp = MessageBox.Show(this, "Receipt has been built. Do you want to open it?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(FileName) { UseShellExecute = true });
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void OnShowFilePathDialog(object sender, EventArgs e)
        {
            try
            {
                if (m_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    m_fileNameTextBox.Text = m_saveFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
