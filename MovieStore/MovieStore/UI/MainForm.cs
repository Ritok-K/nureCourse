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
    enum ViewMode
    {
        Movies,
        Actors,
        Studio,
        Orders,
        Users,
    }

    public partial class MainForm : Form
    {
        #region Properties

        ViewMode ViewMode { get; set; } = ViewMode.Movies;

        ColumnHeader[] MovieModeListColumns => new ColumnHeader[] 
        {
            new ColumnHeader() { Text = "Title" },
            new ColumnHeader() { Text = "Year" },
            new ColumnHeader() { Text = "Genre" },
            new ColumnHeader() { Text = "IMDB" },
            new ColumnHeader() { Text = "Studio" },
            new ColumnHeader() { Text = "Country" },
            new ColumnHeader() { Text = "Price" },
            new ColumnHeader() { Text = "Actors" },
        };

        ColumnHeader[] ActorsModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Name" },
            new ColumnHeader() { Text = "Birth date" },
            new ColumnHeader() { Text = "Country" },
            new ColumnHeader() { Text = "Family status" },
        };

        ColumnHeader[] StudiosModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title" },
            new ColumnHeader() { Text = "Country" },
            new ColumnHeader() { Text = "Foundation date" },
            new ColumnHeader() { Text = "Production" },
        };

        ColumnHeader[] OrdersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Date" },
            new ColumnHeader() { Text = "Customer" },
            new ColumnHeader() { Text = "Movies" },
        };

        ColumnHeader[] UsersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Name" },
            new ColumnHeader() { Text = "E-mail" },
            new ColumnHeader() { Text = "Role" },
        };

        int ListViewOffset { get; set; } = 0;
        int ListViewLimit => 10;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        void Login()
        {
            using (var loginForm = new LoginForm())
            {
                var result = loginForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    SetViewMode(ViewMode.Movies, true);
                    RefreshListView();
                    UpdateControls();
                }
            }
        }

        void SetViewMode(ViewMode mode, bool forceUpdate = false)
        {
            if (mode != ViewMode || forceUpdate)
            {
                ViewMode = mode;

                ReinitControls();
                RefreshListView();
                UpdateControls();
            }
        }

        void RefreshListView()
        {
            if (!Program.DB.IsAuthorized)
            {
                return;
            }

            switch (ViewMode)
            {
                case ViewMode.Movies:
                    RefreshMoviesListView();
                    break;

                case ViewMode.Actors:
                    RefreshActorsListView();
                    break;

                case ViewMode.Studio:
                    RefreshStudiosListView();
                    break;

                case ViewMode.Orders:
                    RefreshOrdersListView();
                    break;

                case ViewMode.Users:
                    RefreshUsersListView();
                    break;
            }
        }

        void RefreshMoviesListView()
        {
            try
            {
                var movies = Program.DB.GetMovies(ListViewLimit, ListViewOffset, loadActors: true);

                m_listView.BeginUpdate();
                m_listView.Items.Clear();

                foreach(var m in movies)
                {
                    m_listView.Items.Add(new ListViewItem(new string[]
                    {
                        m.Title,
                        Utility.UIPrimitiveFormatting.Format(m.Year,"yyyy"),
                        m.Genre,
                        Utility.UIPrimitiveFormatting.FormatImdb(m.Imdb),
                        m.Studio.Title,
                        m.Country,
                        Utility.UIPrimitiveFormatting.FormatPrice(m.Price),
                        Utility.UIPrimitiveFormatting.FormatActorsList(m.Actors),
                    })
                    { Tag = m });
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void RefreshActorsListView()
        {
            try
            {
                var actors = Program.DB.GetActors(ListViewLimit, ListViewOffset);

                m_listView.BeginUpdate();
                m_listView.Items.Clear();

                foreach (var a in actors)
                {
                    m_listView.Items.Add(new ListViewItem(new string[]
                    {
                        Utility.UIPrimitiveFormatting.FormatActorName(a),
                        Utility.UIPrimitiveFormatting.Format(a.BirthDate,"d"),
                        a.Country,
                        Utility.UIPrimitiveFormatting.Format(a.FamilyStatus),
                    })
                    { Tag = a });
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void RefreshStudiosListView()
        {
            try
            {
                var studios = Program.DB.GetStudio(ListViewLimit, ListViewOffset);

                m_listView.BeginUpdate();
                m_listView.Items.Clear();

                foreach (var s in studios)
                {
                    m_listView.Items.Add(new ListViewItem(new string[]
                    {
                        s.Title,
                        s.Country,
                        Utility.UIPrimitiveFormatting.Format(s.FoundationDate,"yyyy"),
                        s.Production,
                    })
                    { Tag = s });
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void RefreshOrdersListView()
        {
            try
            {
                var filter = Program.DB.IsManagerMode ? null : new DB.Filters.CurrentUserOrderList();
                var orders = Program.DB.GetOrders(ListViewLimit, ListViewOffset, filter, loadMovies: true);

                m_listView.BeginUpdate();
                m_listView.Items.Clear();

                foreach (var o in orders)
                {
                    m_listView.Items.Add(new ListViewItem(new string[]
                    {
                        Utility.UIPrimitiveFormatting.Format(o.Date,"g"),
                        Utility.UIPrimitiveFormatting.FormatUserName(o.User),
                        Utility.UIPrimitiveFormatting.FormatMoviesList(o.Movies),
                    })
                    { Tag = o });
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void RefreshUsersListView()
        {
            try
            {
                var users = Program.DB.GetUsers(ListViewLimit, ListViewOffset);

                m_listView.BeginUpdate();
                m_listView.Items.Clear();

                foreach (var u in users)
                {
                    m_listView.Items.Add(new ListViewItem(new string[]
                    {
                        Utility.UIPrimitiveFormatting.FormatUserName(u),
                        u.EMail,
                        Utility.UIPrimitiveFormatting.Format(u.Role),
                    })
                    { Tag = u });
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void DeleteListViewItems(bool refreshList = true)
        {
            if (!Program.DB.IsAuthorized)
            {
                return;
            }

            switch (ViewMode)
            {
                case ViewMode.Movies:
                    DeleteMoviesListViewItems();
                    break;

                case ViewMode.Actors:
                    DeleteActorsListViewItems();
                    break;

                case ViewMode.Studio:
                    DeleteStudioListViewItems();
                    break;

                case ViewMode.Orders:
                    DeleteOrdersListViewItems();
                    break;

                case ViewMode.Users:
                    DeleteUsersListViewItems();
                    break;
            }

            if (refreshList)
            {
                RefreshListView();
                UpdateControls();
            }
        }

        void DeleteMoviesListViewItems()
        {
            try
            {
                var movies = m_listView.SelectedItems
                                       .Cast<ListViewItem>()
                                       .Select(lv => lv.Tag as Data.Movie)
                                       .ToList();

                Program.DB.DeleteMovies(movies);

                m_listView.BeginUpdate();

                foreach (var lv in m_listView.SelectedItems.Cast<ListViewItem>())
                {
                    m_listView.Items.Remove(lv);
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void DeleteActorsListViewItems()
        {
            try
            {
                var actors = m_listView.SelectedItems
                                       .Cast<ListViewItem>()
                                       .Select(lv => lv.Tag as Data.Actor)
                                       .ToList();

                Program.DB.DeleteActors(actors);

                m_listView.BeginUpdate();

                foreach (var lv in m_listView.SelectedItems.Cast<ListViewItem>())
                {
                    m_listView.Items.Remove(lv);
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void DeleteStudioListViewItems()
        {
            try
            {
                var studio = m_listView.SelectedItems
                                       .Cast<ListViewItem>()
                                       .Select(lv => lv.Tag as Data.Studio)
                                       .ToList();

                Program.DB.DeleteStudio(studio);

                m_listView.BeginUpdate();

                foreach (var lv in m_listView.SelectedItems.Cast<ListViewItem>())
                {
                    m_listView.Items.Remove(lv);
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void DeleteOrdersListViewItems()
        {
            try
            {
                var orders = m_listView.SelectedItems
                                       .Cast<ListViewItem>()
                                       .Select(lv => lv.Tag as Data.Order)
                                       .ToList();

                Program.DB.DeleteOrders(orders);

                m_listView.BeginUpdate();

                foreach (var lv in m_listView.SelectedItems.Cast<ListViewItem>())
                {
                    m_listView.Items.Remove(lv);
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void DeleteUsersListViewItems()
        {
            try
            {
                var users = m_listView.SelectedItems
                                       .Cast<ListViewItem>()
                                       .Select(lv => lv.Tag as Data.User)
                                       .ToList();

                Program.DB.DeleteUsers(users);

                m_listView.BeginUpdate();

                foreach (var lv in m_listView.SelectedItems.Cast<ListViewItem>())
                {
                    m_listView.Items.Remove(lv);
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void UpdateListViewItem(bool createNewItem, bool refreshList = true)
        {
            if (!Program.DB.IsAuthorized)
            {
                return;
            }

            var res = false;

            switch (ViewMode)
            {
                case ViewMode.Movies:
                    res = UpdateMoviesListViewItem(createNewItem);
                    break;

                case ViewMode.Actors:
                    res = UpdateActorsListViewItem(createNewItem);
                    break;

                case ViewMode.Studio:
                    res = UpdateStudioListViewItem(createNewItem);
                    break;

                case ViewMode.Orders:
                    res = UpdateOrdersListViewItem(createNewItem);
                    break;

                case ViewMode.Users:
                    res = UpdateUsersListViewItem(createNewItem);
                    break;
            }

            if (refreshList && res)
            {
                RefreshListView();
                UpdateControls();
            }
        }

        bool UpdateMoviesListViewItem(bool createNewItem)
        {
            var res = false;

            var selectedItem = m_listView.SelectedItems.Cast<ListViewItem>()?.FirstOrDefault()?.Tag as Data.Movie;
            if (createNewItem || (selectedItem != null))
            {
                using (var movieForm = new MovieForm())
                {
                    var isManagerMode = Program.DB.IsManagerMode;
                    movieForm.SetMode(createNewItem ? MovieFormMode.NewMovie :
                                     (isManagerMode ? MovieFormMode.EditMovie : MovieFormMode.ViewMovie), 
                                      selectedItem);

                    res = movieForm.ShowDialog(this) == DialogResult.OK;
                }
            }

            return res;
        }

        bool UpdateActorsListViewItem(bool createNewItem)
        {
            var res = false;

            var selectedItem = m_listView.SelectedItems.Cast<ListViewItem>()?.FirstOrDefault()?.Tag as Data.Actor;
            if (createNewItem || (selectedItem != null))
            {
#if DEBUG
                if (createNewItem)
                {
                    var a = new Data.Actor()
                    {
                        FirstName = "Sylvester",
                        SecondName = "Stallone",
                        BirthDate = new DateTime(1946, 7, 6),
                        Country = "USA",
                        FamilyStatus = Data.ActorFamilyStatus.Married,
                        AwardsDescription = "Favorite Movie Actor (1985); Best Supporting Actor (2015-2016)",
                    };

                    Program.DB.AddActors(new Data.Actor[] { a });
                    res = true;
                }
                else
                {
                    var a = selectedItem;
                    a.FirstName = string.Join("", a.FirstName.Reverse());
                    Program.DB.UpdateActors(new Data.Actor[] { a });
                    res = true;
                }
#endif
            }
         
            return res;
        }

        bool UpdateStudioListViewItem(bool createNewItem)
        {
            var res = false;

            var selectedItem = m_listView.SelectedItems.Cast<ListViewItem>()?.FirstOrDefault()?.Tag as Data.Studio;
            if (createNewItem || (selectedItem != null))
            {
                using (var studioForm = new StudioForm())
                {
                    var isManagerMode = Program.DB.IsManagerMode;
                    studioForm.SetMode(createNewItem ? StudioFormMode.NewStudio :
                                      (isManagerMode ? StudioFormMode.EditStudio : StudioFormMode.ViewStudio),
                                       selectedItem);

                    res = studioForm.ShowDialog(this) == DialogResult.OK;
                }
            }

            return res;
        }

        bool UpdateOrdersListViewItem(bool createNewItem)
        {
            var res = false;

            var selectedItem = m_listView.SelectedItems.Cast<ListViewItem>()?.FirstOrDefault()?.Tag as Data.Order;
            if (createNewItem || (selectedItem != null))
            {
#if DEBUG
                if (createNewItem)
                {
                    var o = new Data.Order()
                    {
                        Date = new DateTime(1976, 1, 1),
                        User = Program.DB.CurrentUser
                    };

                    Program.DB.AddOrders(new Data.Order[] { o });
                    res = true;
                }
                else
                {
                    var o = selectedItem;
                    o.Date = DateTime.Now;
                    Program.DB.UpdateOrders(new Data.Order[] { o });
                    res = true;
                }
#endif
            }

            return res;
        }

        bool UpdateUsersListViewItem(bool createNewItem)
        {
            var res = false;

            var selectedItem = m_listView.SelectedItems.Cast<ListViewItem>()?.FirstOrDefault()?.Tag as Data.User;
            if (createNewItem || (selectedItem != null))
            {
                using (var userForm = new UserForm())
                {
                    var isManagerMode = Program.DB.IsManagerMode;
                    userForm.SetMode(createNewItem ? UserFormMode.NewUser : 
                                    (isManagerMode ? UserFormMode.EditUser : UserFormMode.ViewUser),
                                     selectedItem);

                    res = userForm.ShowDialog(this) == DialogResult.OK;
                }
            }

            return res;
        }

        void ReinitControls()
        {
            switch (ViewMode)
            {
                case ViewMode.Movies:
                    ReinitMoviesModeControls();
                    break;

                case ViewMode.Actors:
                    ReinitActorsModeControls();
                    break;

                case ViewMode.Studio:
                    ReinitStudiosModeControls();
                    break;

                case ViewMode.Orders:
                    ReinitOrdersModeControls();
                    break;

                case ViewMode.Users:
                    ReinitUsersModeControls();
                    break;
            }

            UpdateControls();
        }

        void ReinitMoviesModeControls()
        {
            ReinitListView(MovieModeListColumns);
        }

        void ReinitActorsModeControls()
        {
            ReinitListView(ActorsModeListColumns);
        }

        void ReinitUsersModeControls()
        {
            ReinitListView(UsersModeListColumns);
        }

        void ReinitStudiosModeControls()
        {
            ReinitListView(StudiosModeListColumns);
        }

        void ReinitOrdersModeControls()
        {
            ReinitListView(OrdersModeListColumns);
        }

        void ReinitListView(ColumnHeader[] columns)
        {
            try
            {
                ListViewOffset = 0;

                m_listView.BeginUpdate();

                m_listView.Items.Clear();
                m_listView.Columns.Clear();
                m_listView.Columns.AddRange(columns);

                ExpandListViewColumns(false);
            }
            finally
            {
                m_listView.EndUpdate();
            }
        }

        void ExpandListViewColumns(bool byContent = true)
        {
            foreach(var c in m_listView.Columns.Cast<ColumnHeader>())
            {
                if (byContent)
                {
                    c.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                c.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        void UpdateControls()
        {
            var isManagerMode = Program.DB.IsManagerMode;
            var isAuthorized = Program.DB.IsAuthorized;
            var hasSelection = m_listView.SelectedItems.Count > 0;

            // Menu items
            {
                m_moviesToolStripMenuItem.Enabled = true && isAuthorized;
                m_moviesToolStripMenuItem.Visible = true;

                m_actorsToolStripMenuItem.Enabled = isManagerMode && isAuthorized;
                m_actorsToolStripMenuItem.Visible = isManagerMode;

                m_studiosToolStripMenuItem.Enabled = isManagerMode && isAuthorized;
                m_studiosToolStripMenuItem.Visible = isManagerMode;

                m_ordersToolStripMenuItem.Enabled = true && isAuthorized;
                m_ordersToolStripMenuItem.Visible = true;

                m_usersToolStripMenuItem.Enabled = isManagerMode && isAuthorized;
                m_usersToolStripMenuItem.Visible = isManagerMode;

                m_myBasketToolStripMenuItem.Enabled = !isManagerMode && isAuthorized;
                m_myBasketToolStripMenuItem.Visible = !isManagerMode;
            }

            // Toolstrip buttons
            {
                m_reportToolStripButton.Enabled = isAuthorized && isManagerMode;
                m_reportToolStripButton.Visible = isManagerMode;

                m_receiptToolStripButton.Enabled = isAuthorized && hasSelection;
                m_receiptToolStripButton.Visible = (ViewMode == ViewMode.Orders);

                m_prevToolStripButton.Enabled = isAuthorized && (ListViewOffset > 0);
                m_prevToolStripButton.Visible = true;

                m_nextToolStripButton.Enabled = isAuthorized && (m_listView.Items.Count >= ListViewLimit);
                m_nextToolStripButton.Visible = true;

                m_addNewToolStripButton.Enabled = isManagerMode && isAuthorized;
                m_addNewToolStripButton.Visible = isManagerMode;

                m_deleteToolStripButton.Enabled = isManagerMode && isAuthorized && hasSelection;
                m_deleteToolStripButton.Visible = isManagerMode;
            }
        }

        void BuildReceipt()
        {
            if (!Program.DB.IsAuthorized || (ViewMode != ViewMode.Orders))
            {
                return;
            }

            var orders = m_listView.SelectedItems.Cast<ListViewItem>()
                                                 .Select(lv => lv.Tag as Data.Order)
                                                 .ToList();
            if (orders.Any())
            {
                using (var reportForm = new OrdersReceiptForm() { Orders = orders })
                {
                    reportForm.ShowDialog(this);
                }
            }
        }

        void BuildReport()
        {
            if (!Program.DB.IsAuthorized)
            {
                return;
            }

            using (var reportForm = new OrdersReportForm())
            {
                reportForm.ShowDialog(this);
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                Login();

                if (!Program.DB.IsAuthorized)
                {
                    Close();
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnLogout(object sender, EventArgs e)
        {
            try
            {
                Program.DB.Logout();

                var res = MessageBox.Show(this, "You have been logged out. Do you want to login again?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Login();
                }

                if (!Program.DB.IsAuthorized)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnMoviesMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.Movies);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnActorsMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.Actors);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnStudioMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.Studio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnOrdersMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.Orders);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnUsersMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.Users);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnMyBasketMode(object sender, EventArgs e)
        {

        }

        private void OnDeleteSelected(object sender, EventArgs e)
        {
            try
            {
                if (m_listView.SelectedItems.Count > 0)
                {
                    var resp = MessageBox.Show(this, "Are you sure that you want to delete selected items?\nAll related with them data will be deleted also and you can not rollback this action!", 
                                                     "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resp == DialogResult.Yes)
                    {
                        DeleteListViewItems();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnAddNew(object sender, EventArgs e)
        {
            try
            {
                UpdateListViewItem(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnItemActivated(object sender, EventArgs e)
        {
            try
            {
                UpdateListViewItem(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnListViewSelectionChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void OnBuildReceipt(object sender, EventArgs e)
        {
            try
            {
                BuildReceipt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnBuildReport(object sender, EventArgs e)
        {
            try
            {
                BuildReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
