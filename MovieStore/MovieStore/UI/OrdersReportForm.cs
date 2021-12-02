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
    public partial class OrdersReportForm : Form
    {
        DateTime FromDate { get; set; } = DateTime.Now;
        DateTime ToDate { get; set; } = DateTime.Now;
        string FileName { get; set; }

        public OrdersReportForm()
        {
            InitializeComponent();
        }

        void UpdateDate(bool save)
        {
            if (save)
            {
                if (m_fromDateTimePicker.Value > m_toDateTimePicker.Value)
                {
                    throw new Exception("Invalid date period. From date should be less or equal than To date.");
                }

                if (string.IsNullOrEmpty(m_fileNameTextBox.Text))
                {
                    throw new Exception("Select file path to export.");
                }

                FromDate = m_fromDateTimePicker.Value;
                ToDate = m_toDateTimePicker.Value;
                FileName = m_fileNameTextBox.Text;
            }
            else
            {
                m_fromDateTimePicker.Value = FromDate;
                m_toDateTimePicker.Value = ToDate;
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

                var reportBuilder = new Reports.OrdersReport(FileName, FromDate, ToDate);
                reportBuilder.Build();

                var resp = MessageBox.Show(this, "Report has been built. Do you want to open it?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(FileName) { UseShellExecute = true });
                }

                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void OnShowFilePathDialog(object sender, EventArgs e)
        {
            try
            {
                if (m_saveFileDialog.ShowDialog()==DialogResult.OK)
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
