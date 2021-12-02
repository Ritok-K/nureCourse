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

        IList<Data.Movie> Movies { get; set; }

        ColumnHeader[] Columns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Movie.Title) },
            new ColumnHeader() { Text = "Price", Name = nameof(Data.Movie.Price) },
        };

        public MyBasketForm()
        {
            InitializeComponent();
        }

        void InitControls()
        {
            Utility.UIListView.InitColumns(m_listView, Columns);
        }

        void PopulateListView()
        {
            var filter = new DB.Filters.MovieFilter();
            filter.WithIds(BasketList);

            Movies = Program.DB.GetMovies(filter: filter, loadActors: false);

            var view = Movies.Select(m => new Dictionary<string, string>()
                                          {
                                              { nameof(Data.Movie.Title),     m.Title },
                                              { nameof(Data.Movie.Price),     Utility.UIPrimitiveFormatting.FormatPrice(m.Price) },
                                          }).ToList();

            Utility.UIListView.PopulateItems(m_listView, Movies, view);
            Utility.UIListView.ExpandColumns(m_listView);
        }

        void UpdateControls()
        {
            m_okButton.Enabled = Movies?.Any() ?? false;
        }

        void MakeOrder()
        {
            var order = new Data.Order() 
            { 
                Date = DateTime.Now,
                User = Program.DB.CurrentUser,
                Movies = Movies,
            };

            Program.DB.AddOrders(new Data.Order[] { order });
        }

        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                InitControls();
                PopulateListView();
                UpdateControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnOk(object sender, EventArgs e)
        {
            try
            {
                MakeOrder();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
