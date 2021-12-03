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
    public partial class OrderForm : Form
    {
        Data.Order Order { get; set; }

        ColumnHeader[] Columns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Movie.Title) },
            new ColumnHeader() { Text = "Price", Name = nameof(Data.Movie.Price) },
        };


        public OrderForm()
        {
            InitializeComponent();
        }

        internal void SetMode(Data.Order order)
        {
            Order = order;
        }

        void InitControls()
        {
            Utility.UIListView.InitColumns(m_listView, Columns);

            UpdateData();
            UpdateControls();
        }

        void PopulateListView()
        {
            if (Order?.Movies?.Any() ?? false)
            {
                var view = Order.Movies.Select(m => new Dictionary<string, string>()
                                          {
                                              { nameof(Data.Movie.Title),     m.Title },
                                              { nameof(Data.Movie.Price),     Utility.UIPrimitiveFormatting.FormatPrice(m.Price) },
                                          }).ToList();

                Utility.UIListView.PopulateItems(m_listView, Order.Movies, view);
                Utility.UIListView.ExpandColumns(m_listView);
            }
        }

        void UpdateData()
        {
            PopulateListView();

            if (Order != null)
            {
                m_infoLabel.Text = $"Order #{Order.Id}";

                var total = Order.Movies?.Select(m => m.Price).Aggregate((p, c) => p + c) ?? 0;
                m_totalLabel.Text = $"Total: {Utility.UIPrimitiveFormatting.FormatPrice(total)}";
            }
        }

        void UpdateControls()
        {
        }

        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                InitControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
