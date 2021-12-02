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

        TopMovies,
        TopUsers,
        TopOrders,
        TopStudio,
    }

    public partial class MainForm : Form
    {
        #region Properties

        ViewMode ViewMode { get; set; } = ViewMode.Movies;

        bool IsViewModeEditable => ViewMode == ViewMode.Movies ||
                                   ViewMode == ViewMode.Actors ||
                                   ViewMode == ViewMode.Studio ||
                                   ViewMode == ViewMode.Orders ||
                                   ViewMode == ViewMode.Users;

        IList<int> BasketList { get; set; } = new List<int>();

        ColumnHeader[] MovieModeListColumns => new ColumnHeader[] 
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Movie.Title) },
            new ColumnHeader() { Text = "Year", Name = nameof(Data.Movie.Year) },
            new ColumnHeader() { Text = "Genre", Name = nameof(Data.Movie.Genre) },
            new ColumnHeader() { Text = "IMDB", Name = nameof(Data.Movie.Imdb) },
            new ColumnHeader() { Text = "Studio", Name = nameof(Data.Movie.Studio) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Movie.Country) },
            new ColumnHeader() { Text = "Price", Name = nameof(Data.Movie.Price) },
            new ColumnHeader() { Text = "Actors", Name = nameof(Data.Movie.Actors) },
        };

        ColumnHeader[] ManagerTopMovieModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Movie.Title) },
            new ColumnHeader() { Text = "Year", Name = nameof(Data.Movie.Year) },
            new ColumnHeader() { Text = "IMDB", Name = nameof(Data.Movie.Imdb) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.Movie.Income) },
        };

        ColumnHeader[] CustomerTopMovieModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Movie.Title) },
            new ColumnHeader() { Text = "Year", Name = nameof(Data.Movie.Year) },
            new ColumnHeader() { Text = "IMDB", Name = nameof(Data.Movie.Imdb) },
        };

        ColumnHeader[] ActorsModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Name", Name = nameof(Data.Actor.FirstName) },
            new ColumnHeader() { Text = "Birth date", Name = nameof(Data.Actor.BirthDate) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Actor.Country) },
            new ColumnHeader() { Text = "Family status", Name = nameof(Data.Actor.FamilyStatus) },
        };

        ColumnHeader[] StudiosModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Studio.Title) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Studio.Country) },
            new ColumnHeader() { Text = "Foundation date", Name = nameof(Data.Studio.FoundationDate) },
            new ColumnHeader() { Text = "Production", Name = nameof(Data.Studio.Production) },
        };

        ColumnHeader[] ManagerTopStudiosModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Studio.Title) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Studio.Country) },
            new ColumnHeader() { Text = "Foundation date", Name = nameof(Data.Studio.FoundationDate) },
            new ColumnHeader() { Text = "Production", Name = nameof(Data.Studio.Production) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.Studio.Income) },
        };

        ColumnHeader[] CustomerTopStudiosModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Studio.Title) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Studio.Country) },
            new ColumnHeader() { Text = "Foundation date", Name = nameof(Data.Studio.FoundationDate) },
            new ColumnHeader() { Text = "Production", Name = nameof(Data.Studio.Production) },
        };

        ColumnHeader[] OrdersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Date", Name = nameof(Data.Order.Date) },
            new ColumnHeader() { Text = "Customer", Name = nameof(Data.Order.User) },
            new ColumnHeader() { Text = "Movies", Name = nameof(Data.Order.Movies) },
        };

        ColumnHeader[] ManagerTopOrdersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Date", Name = nameof(Data.Order.Date) },
            new ColumnHeader() { Text = "Customer", Name = nameof(Data.Order.User) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.Order.Income) },
        };

        ColumnHeader[] CustomerTopOrdersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Date", Name = nameof(Data.Order.Date) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.Order.Income) },
        };

        ColumnHeader[] UsersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Name", Name = nameof(Data.User.FirstName) },
            new ColumnHeader() { Text = "E-mail", Name = nameof(Data.User.EMail) },
            new ColumnHeader() { Text = "Role", Name = nameof(Data.User.Role) },
        };

        ColumnHeader[] TopUsersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Name", Name = nameof(Data.User.FirstName) },
            new ColumnHeader() { Text = "E-mail", Name = nameof(Data.User.EMail) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.User.Income) },
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
                case ViewMode.TopMovies:
                    RefreshMoviesListView();
                    break;

                case ViewMode.Actors:
                    RefreshActorsListView();
                    break;

                case ViewMode.Studio:
                case ViewMode.TopStudio:
                    RefreshStudiosListView();
                    break;

                case ViewMode.Orders:
                case ViewMode.TopOrders:
                    RefreshOrdersListView();
                    break;

                case ViewMode.Users:
                case ViewMode.TopUsers:
                    RefreshUsersListView();
                    break;
            }
        }

        void RefreshMoviesListView()
        {
            var movies = (ViewMode == ViewMode.Movies) ? Program.DB.GetMovies(ListViewLimit, ListViewOffset, loadActors: true) :
                                                         Program.DB.GetTopMovies(ListViewLimit, ListViewOffset, loadActors: false);
            var view = movies.Select(m => new Dictionary<string, string>()
                                          {
                                              { nameof(Data.Movie.Title),     m.Title },
                                              { nameof(Data.Movie.Year),      Utility.UIPrimitiveFormatting.Format(m.Year,"yyyy") },
                                              { nameof(Data.Movie.Genre),     m.Genre },
                                              { nameof(Data.Movie.Imdb),      Utility.UIPrimitiveFormatting.FormatImdb(m.Imdb)},
                                              { nameof(Data.Movie.Studio),    m.Studio.Title},
                                              { nameof(Data.Movie.Country),   m.Country },
                                              { nameof(Data.Movie.Price),     Utility.UIPrimitiveFormatting.FormatPrice(m.Price) },
                                              { nameof(Data.Movie.Actors),    Utility.UIPrimitiveFormatting.FormatActorsList(m.Actors)},
                                              { nameof(Data.Movie.Income),    Utility.UIPrimitiveFormatting.FormatPrice(m.Income)}
                                          }).ToList();

            PopulateListView(movies, view);
        }

        void RefreshActorsListView()
        {
            var actors = Program.DB.GetActors(ListViewLimit, ListViewOffset);
            var view = actors.Select(a => new Dictionary<string, string>()
                                          {
                                              { nameof(Data.Actor.FirstName),   Utility.UIPrimitiveFormatting.FormatActorName(a) },
                                              { nameof(Data.Actor.BirthDate),   Utility.UIPrimitiveFormatting.Format(a.BirthDate,"d") },
                                              { nameof(Data.Actor.Country),     a.Country },
                                              { nameof(Data.Actor.FamilyStatus),Utility.UIPrimitiveFormatting.Format(a.FamilyStatus)},
                                          }).ToList();

            PopulateListView(actors, view);
        }

        void RefreshStudiosListView()
        {
            var studio = (ViewMode == ViewMode.Studio) ? Program.DB.GetStudio(ListViewLimit, ListViewOffset) :
                                                         Program.DB.GetTopStudio(ListViewLimit, ListViewOffset);
            var view = studio.Select(s => new Dictionary<string, string>()
                                          {
                                              { nameof(Data.Studio.Title),          s.Title },
                                              { nameof(Data.Studio.Country),        s.Country },
                                              { nameof(Data.Studio.FoundationDate), Utility.UIPrimitiveFormatting.Format(s.FoundationDate,"yyyy") },
                                              { nameof(Data.Studio.Production),     s.Production},
                                              { nameof(Data.Movie.Income),          Utility.UIPrimitiveFormatting.FormatPrice(s.Income)}
                                          }).ToList();

            PopulateListView(studio, view);
        }

        void RefreshOrdersListView()
        {
            var filter = Program.DB.IsManagerMode ? null : new DB.Filters.CurrentUserOrderList();
            var orders = (ViewMode == ViewMode.Orders) ? Program.DB.GetOrders(ListViewLimit, ListViewOffset, filter, loadMovies: true) :
                                                         Program.DB.GetTopOrders(ListViewLimit, ListViewOffset, filter, loadMovies: false);
            var view = orders.Select(o => new Dictionary<string, string>()
                                          {
                                              { nameof(Data.Order.Date),   Utility.UIPrimitiveFormatting.Format(o.Date,"g") },
                                              { nameof(Data.Order.User),   Utility.UIPrimitiveFormatting.FormatUserName(o.User) },
                                              { nameof(Data.Order.Movies), Utility.UIPrimitiveFormatting.FormatMoviesList(o.Movies) },
                                              { nameof(Data.Order.Income), Utility.UIPrimitiveFormatting.FormatPrice(o.Income)}
                                          }).ToList();

            PopulateListView(orders, view);
        }

        void RefreshUsersListView()
        {
            var users = (ViewMode == ViewMode.Users) ? Program.DB.GetUsers(ListViewLimit, ListViewOffset) :
                                                       Program.DB.GetTopUsers(ListViewLimit, ListViewOffset);

            var view = users.Select(u => new Dictionary<string, string>()
                                      {
                                          { nameof(Data.User.FirstName),Utility.UIPrimitiveFormatting.FormatUserName(u) },
                                          { nameof(Data.User.EMail),    u.EMail },
                                          { nameof(Data.User.Role),     Utility.UIPrimitiveFormatting.Format(u.Role) },
                                          { nameof(Data.User.Income),   Utility.UIPrimitiveFormatting.FormatPrice(u.Income)}
                                      }).ToList();

            PopulateListView(users, view);
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

                case ViewMode.TopMovies:
                    // do nothing
                    break;

                default:
                    Debug.Assert(false);
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

                case ViewMode.TopMovies:
                    // do nothing
                    break;

                default:
                    Debug.Assert(false);
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
                using (var actorForm = new ActorForm())
                {
                    var isManagerMode = Program.DB.IsManagerMode;
                    actorForm.SetMode(createNewItem ? ActorFormMode.NewActor :
                                     (isManagerMode ? ActorFormMode.EditActor : ActorFormMode.ViewActor),
                                      selectedItem);

                    res = actorForm.ShowDialog(this) == DialogResult.OK;
                }
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
                case ViewMode.TopMovies:
                    ReinitMoviesModeControls();
                    break;

                case ViewMode.Actors:
                    ReinitActorsModeControls();
                    break;

                case ViewMode.Studio:
                case ViewMode.TopStudio:
                    ReinitStudiosModeControls();
                    break;

                case ViewMode.Orders:
                case ViewMode.TopOrders:
                    ReinitOrdersModeControls();
                    break;

                case ViewMode.Users:
                case ViewMode.TopUsers:
                    ReinitUsersModeControls();
                    break;
            }

            UpdateControls();
        }

        void ReinitMoviesModeControls()
        {
            var columns = (ViewMode == ViewMode.Movies) ? MovieModeListColumns : 
                                                         (Program.DB.IsManagerMode ? ManagerTopMovieModeListColumns : CustomerTopMovieModeListColumns);
            ReinitListView(columns);
        }

        void ReinitActorsModeControls()
        {
            ReinitListView(ActorsModeListColumns);
        }

        void ReinitUsersModeControls()
        {
            var columns = (ViewMode == ViewMode.Users) ? UsersModeListColumns : TopUsersModeListColumns;
            ReinitListView(columns);
        }

        void ReinitStudiosModeControls()
        {
            var columns = (ViewMode == ViewMode.Studio) ? StudiosModeListColumns :
                                                         (Program.DB.IsManagerMode ? ManagerTopStudiosModeListColumns : CustomerTopStudiosModeListColumns);

            ReinitListView(columns);
        }

        void ReinitOrdersModeControls()
        {
            var columns = (ViewMode == ViewMode.Orders) ? OrdersModeListColumns : 
                                                         (Program.DB.IsManagerMode ? ManagerTopOrdersModeListColumns : CustomerTopOrdersModeListColumns);

            ReinitListView(columns);
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

        void PopulateListView<T>(IList<T> model, IList<Dictionary<string, string>> view) where T : class
        {
            try
            {
                m_listView.BeginUpdate();
                m_listView.Items.Clear();

                for (var i = 0; i < model.Count; i++)
                {
                    var m = model[i];
                    var v = view[i];

                    var lv = default(ListViewItem);
                    foreach (var c in m_listView.Columns.Cast<ColumnHeader>())
                    {
                        var value = Utility.UIPrimitiveFormatting.c_NoData;
                        v.TryGetValue(c.Name, out value);

                        if (lv==null)
                        {
                            lv = new ListViewItem() { Text = value, Name = c.Name };
                        }
                        else
                        {
                            lv.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = value, Name = c.Name });
                        }
                    }

                    if (lv == null)
                    {
                        lv = new ListViewItem() { Text = string.Empty };
                    }

                    lv.Tag = m;

                    m_listView.Items.Add(lv);
                }

                ExpandListViewColumns();
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
            var isBasketEmpty = !BasketList.Any();
            var hasSelection = m_listView.SelectedItems.Count > 0;

            // Menu items
            {
                m_moviesToolStripMenuItem.Enabled = isAuthorized;
                m_moviesToolStripMenuItem.Visible = true;

                m_topMoviesToolStripMenuItem.Enabled = isAuthorized;
                m_topMoviesToolStripMenuItem.Visible = true;

                m_actorsToolStripMenuItem.Enabled = isManagerMode && isAuthorized;
                m_actorsToolStripMenuItem.Visible = isManagerMode;

                m_studiosToolStripMenuItem.Enabled = isManagerMode && isAuthorized;
                m_studiosToolStripMenuItem.Visible = isManagerMode;

                m_ordersToolStripMenuItem.Enabled = isAuthorized;
                m_ordersToolStripMenuItem.Visible = true;

                m_topOrdersToolStripMenuItem.Enabled = isAuthorized;
                m_topOrdersToolStripMenuItem.Visible = true;

                m_usersToolStripMenuItem.Enabled = isManagerMode && isAuthorized;
                m_usersToolStripMenuItem.Visible = isManagerMode;

                m_topUsersToolStripMenuItem.Enabled = isManagerMode && isAuthorized;
                m_topUsersToolStripMenuItem.Visible = isManagerMode;

                m_myBasketToolStripMenuItem.Enabled = !isManagerMode && isAuthorized;
                m_myBasketToolStripMenuItem.Visible = !isManagerMode;
            }

            // Toolstrip buttons
            {
                m_reportToolStripButton.Enabled = isAuthorized && isManagerMode;
                m_reportToolStripButton.Visible = isManagerMode;

                m_receiptToolStripButton.Enabled = isAuthorized && hasSelection;
                m_receiptToolStripButton.Visible = (ViewMode == ViewMode.Orders) || (ViewMode == ViewMode.TopOrders);

                m_prevToolStripButton.Enabled = isAuthorized && (ListViewOffset > 0);
                m_prevToolStripButton.Visible = true;

                m_nextToolStripButton.Enabled = isAuthorized && (m_listView.Items.Count >= ListViewLimit);
                m_nextToolStripButton.Visible = true;

                m_addNewToolStripButton.Enabled = isManagerMode && isAuthorized && IsViewModeEditable;
                m_addNewToolStripButton.Visible = isManagerMode;

                m_deleteToolStripButton.Enabled = isManagerMode && isAuthorized && IsViewModeEditable && hasSelection;
                m_deleteToolStripButton.Visible = isManagerMode;

                m_addToBasketToolStripButton.Enabled = isAuthorized && hasSelection;
                m_addToBasketToolStripButton.Visible = (ViewMode == ViewMode.Movies) || (ViewMode == ViewMode.TopMovies);

                m_basketToolStripButton.Enabled = isAuthorized && !isBasketEmpty;
                m_basketToolStripButton.Visible = (ViewMode == ViewMode.Movies) || (ViewMode == ViewMode.TopMovies);
            }
        }

        void BuildReceipt()
        {
            if (!Program.DB.IsAuthorized || ((ViewMode != ViewMode.Orders) && (ViewMode != ViewMode.TopOrders)))
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
                using(var welcomeForm = new WelcomeForm())
                {
                    var resp = welcomeForm.ShowDialog(this);
                    if(resp == DialogResult.OK)
                    {
                        SetViewMode(ViewMode.Movies, true);
                        RefreshListView();
                        UpdateControls();
                    }
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

        private void OnTopMoviesMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.TopMovies);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnTopUsersMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.TopUsers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnTopStudioMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.TopStudio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnTopOrdersMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.TopOrders);
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
