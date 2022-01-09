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

        AdvicedMovies,
    }

    public partial class MainForm : Form
    {
        #region Nested classes

        class SortColumn
        {
            internal string ColumnName { get; set; } = string.Empty;
            internal SortOrder Order { get; set; } = SortOrder.Ascending;

            internal bool IsEmpty => string.IsNullOrEmpty(ColumnName) || Order == SortOrder.None;
            internal bool IsAscending => Order == SortOrder.Ascending;
        }

        #endregion

        #region Properties

        ViewMode ViewMode { get; set; } = ViewMode.Movies;

        bool IsMoviesViewMode => (ViewMode == ViewMode.Movies) ||
                                 (ViewMode == ViewMode.TopMovies) ||
                                 (ViewMode == ViewMode.AdvicedMovies);

        bool IsUsersViewMode => (ViewMode == ViewMode.Users) ||
                                (ViewMode == ViewMode.TopUsers);

        bool IsActorsViewMode => (ViewMode == ViewMode.Actors);

        bool IsViewModeEditable => (ViewMode == ViewMode.Movies) ||
                                   (ViewMode == ViewMode.Actors) ||
                                   (ViewMode == ViewMode.Studio) ||
                                   (ViewMode == ViewMode.Orders) ||
                                   (ViewMode == ViewMode.Users);

        List<int> BasketList { get; set; } = new List<int>();

        ColumnHeader[] MovieModeListColumns => new ColumnHeader[] 
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Movie.Title), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Year", Name = nameof(Data.Movie.Year), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Genre", Name = nameof(Data.Movie.Genre), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "IMDB", Name = nameof(Data.Movie.Imdb), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Studio", Name = nameof(Data.Movie.Studio), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Movie.Country), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Price", Name = nameof(Data.Movie.Price), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Actors", Name = nameof(Data.Movie.Actors) },
        };

        SortColumn MovieModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Movie.Title), Order = SortOrder.Ascending };

        ColumnHeader[] ManagerTopMovieModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Movie.Title), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Year", Name = nameof(Data.Movie.Year), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "IMDB", Name = nameof(Data.Movie.Imdb), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.Movie.Income), Tag = typeof(Data.Movie) },
        };

        SortColumn ManagerTopMovieModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Movie.Income), Order = SortOrder.Descending };

        ColumnHeader[] CustomerTopMovieModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Movie.Title), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "Year", Name = nameof(Data.Movie.Year), Tag = typeof(Data.Movie) },
            new ColumnHeader() { Text = "IMDB", Name = nameof(Data.Movie.Imdb), Tag = typeof(Data.Movie) },
        };

        SortColumn CustomerTopMovieModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Movie.Imdb), Order = SortOrder.Descending };

        ColumnHeader[] ActorsModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Name", Name = nameof(Data.Actor.FirstName), Tag = typeof(Data.Actor) },
            new ColumnHeader() { Text = "Birth date", Name = nameof(Data.Actor.BirthDate), Tag = typeof(Data.Actor) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Actor.Country), Tag = typeof(Data.Actor) },
            new ColumnHeader() { Text = "Family status", Name = nameof(Data.Actor.FamilyStatus), Tag = typeof(Data.Actor) },
        };
        
        SortColumn ActorsModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Actor.FirstName), Order = SortOrder.Ascending };

        ColumnHeader[] StudiosModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Studio.Title), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Studio.Country), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Foundation date", Name = nameof(Data.Studio.FoundationDate), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Production", Name = nameof(Data.Studio.Production), Tag = typeof(Data.Studio) },
        };
        
        SortColumn StudiosModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Studio.Title), Order = SortOrder.Ascending };

        ColumnHeader[] ManagerTopStudiosModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Studio.Title), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Studio.Country), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Foundation date", Name = nameof(Data.Studio.FoundationDate), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Production", Name = nameof(Data.Studio.Production), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.Studio.Income), Tag = typeof(Data.Studio) },
        };
        
        SortColumn ManagerTopStudiosModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Studio.Income), Order = SortOrder.Descending };

        ColumnHeader[] CustomerTopStudiosModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Title", Name = nameof(Data.Studio.Title), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Country", Name = nameof(Data.Studio.Country), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Foundation date", Name = nameof(Data.Studio.FoundationDate), Tag = typeof(Data.Studio) },
            new ColumnHeader() { Text = "Production", Name = nameof(Data.Studio.Production), Tag = typeof(Data.Studio) },
        };
        
        SortColumn CustomerTopStudiosModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Studio.Title), Order = SortOrder.Ascending };

        ColumnHeader[] OrdersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "#", Name = nameof(Data.Order.Id), Tag = typeof(Data.Order) },
            new ColumnHeader() { Text = "Date", Name = nameof(Data.Order.Date), Tag = typeof(Data.Order) },
            new ColumnHeader() { Text = "Customer", Name = nameof(Data.Order.User), Tag = typeof(Data.Order) },
            new ColumnHeader() { Text = "Movies", Name = nameof(Data.Order.Movies) },
        };

        SortColumn OrdersModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Order.Id), Order = SortOrder.Ascending };

        ColumnHeader[] ManagerTopOrdersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "#", Name = nameof(Data.Order.Id), Tag = typeof(Data.Order) },
            new ColumnHeader() { Text = "Date", Name = nameof(Data.Order.Date), Tag = typeof(Data.Order) },
            new ColumnHeader() { Text = "Customer", Name = nameof(Data.Order.User), Tag = typeof(Data.Order) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.Order.Income), Tag = typeof(Data.Order) },
        };

        SortColumn ManagerTopOrdersModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Order.Income), Order = SortOrder.Descending };

        ColumnHeader[] CustomerTopOrdersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "#", Name = nameof(Data.Order.Id), Tag = typeof(Data.Order) },
            new ColumnHeader() { Text = "Date", Name = nameof(Data.Order.Date), Tag = typeof(Data.Order) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.Order.Income), Tag = typeof(Data.Order) },
        };
        
        SortColumn CustomerTopOrdersModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.Order.Income), Order = SortOrder.Descending };

        ColumnHeader[] UsersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Name", Name = nameof(Data.User.FirstName), Tag = typeof(Data.User) },
            new ColumnHeader() { Text = "E-mail", Name = nameof(Data.User.EMail), Tag = typeof(Data.User) },
            new ColumnHeader() { Text = "Role", Name = nameof(Data.User.Role), Tag = typeof(Data.User) },
        };
        
        SortColumn UsersModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.User.FirstName), Order = SortOrder.Ascending };

        ColumnHeader[] TopUsersModeListColumns => new ColumnHeader[]
        {
            new ColumnHeader() { Text = "Name", Name = nameof(Data.User.FirstName), Tag = typeof(Data.User) },
            new ColumnHeader() { Text = "E-mail", Name = nameof(Data.User.EMail), Tag = typeof(Data.User) },
            new ColumnHeader() { Text = "Income", Name = nameof(Data.User.Income), Tag = typeof(Data.User) },
            new ColumnHeader() { Text = "Last Order Date", Name = nameof(Data.User.LastOrder), Tag = typeof(Data.User) },
        };
        
        SortColumn TopUsersModeListSortColumnDefault = new SortColumn() { ColumnName = nameof(Data.User.Income), Order = SortOrder.Descending };

        int ListViewOffset { get; set; } = 0;
        int ListViewLimit => 20;
        SortColumn ListViewSortColumn { get; set; } = new SortColumn();

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        #region Methods

        bool CanClose()
        {
            var res = !(BasketList?.Any() ?? false);
            if (!res)
            {
                var resp = MessageBox.Show(this, "Your basket list is not empty. Do you want to continue?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                res = resp == DialogResult.Yes;
            }

            return res;
        }

        void Login()
        {
            using (var loginForm = new LoginForm())
            {
                var result = loginForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    BasketList.Clear();

                    SetViewMode(ViewMode.Movies, true);
                    RefreshListView();
                    UpdateControls();
                }
            }
        }

        void Logout()
        {
            Program.DB.Logout();

            BasketList.Clear();
        }

        void NavigateListViewPage(bool next)
        {
            if (!Program.DB.IsAuthorized)
            {
                return;
            }

            bool refresh = false;

            if (next)
            {
                if (m_listView.Items.Count >= ListViewLimit)
                {
                    var offset = ListViewOffset + ListViewLimit;

                    ListViewOffset = offset;
                    refresh = true;
                }
            }
            else
            {
                var offset = Math.Max(0, ListViewOffset - ListViewLimit);
                if (offset != ListViewOffset)
                {
                    ListViewOffset = offset;
                    refresh = true;
                }
            }

            if (refresh)
            {
                RefreshListView();
                UpdateControls();
            }
        }

        void AddToBasket()
        {
            if (!Program.DB.IsAuthorized || !IsMoviesViewMode)
            {
                return;
            }

            var movieIds = m_listView.SelectedItems?.Cast<ListViewItem>()
                                                    .Select(lv => lv.Tag as Data.Movie)
                                                    .Select(m => m.Id)
                                                    .ToList();
            if (movieIds?.Any() ?? false)
            {
                var tempList = new List<int>();
                tempList.AddRange(BasketList);
                tempList.AddRange(movieIds);
                tempList = tempList.Distinct().ToList();

                var wasEmpty = !BasketList.Any();
                if (!BasketList.SequenceEqual(tempList))
                {
                    BasketList = tempList;

                    UpdateControls();

                    if (wasEmpty)
                    {
                        var resp = MessageBox.Show(this, "Your basket is not empty now.\nDo you want open it to make an order?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resp == DialogResult.Yes)
                        {
                            MyBasket();
                        }
                    }
                }
            }
        }

        void MyBasket()
        {
            using(var myBasketForm = new MyBasketForm() { BasketList = BasketList })
            {
                var resp = myBasketForm.ShowDialog(this);
                if (resp == DialogResult.OK)
                {
                    BasketList.Clear();

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
                m_searchToolStripTextBox.Text = string.Empty;

                ReinitControls();
                RefreshListView();
                UpdateControls();
            }
        }

        void SetupSortFilter(DB.Filters.SortFilter filter)
        {
            if (!ListViewSortColumn.IsEmpty)
            {
                var index = m_listView.Columns.IndexOfKey(ListViewSortColumn.ColumnName);
                var column = (index >= 0) ? m_listView.Columns[index] : null;
                if (column?.Tag != null && !string.IsNullOrEmpty(column.Name))
                {
                    var modelType = column.Tag as Type;
                    var modelName = column.Name;

                    filter.Build(modelType, modelName, !ListViewSortColumn.IsAscending);
                }
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
                case ViewMode.AdvicedMovies:
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
            var filter = new DB.Filters.SortFilter();
            if (m_searchToolStripTextBox.Text.Length != 0)
            {
                var movieFilter = new DB.Filters.MovieFilter();
                movieFilter.WithTitleLike(m_searchToolStripTextBox.Text);
                movieFilter.WithStudioLike(m_searchToolStripTextBox.Text);

                filter = movieFilter;
            }

            SetupSortFilter(filter);

            var movies = (ViewMode == ViewMode.Movies) ? Program.DB.GetMovies(ListViewLimit, ListViewOffset, filter, loadActors: true) :
                         ((ViewMode == ViewMode.TopMovies) ? Program.DB.GetTopMovies(ListViewLimit, ListViewOffset, filter, loadActors: false) :
                                                             Program.DB.GetAdviceMovies(Program.DB.CurrentUser.Id, ListViewLimit, ListViewOffset, filter, loadActors: true));
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
            var filter = new DB.Filters.SortFilter();
            if (m_searchToolStripTextBox.Text.Length != 0)
            {
                var movieFilter = new DB.Filters.ActorFilter();
                movieFilter.WithFirstNameLike(m_searchToolStripTextBox.Text);
                movieFilter.WithSecondNameLike(m_searchToolStripTextBox.Text);

                filter = movieFilter;
            }

            SetupSortFilter(filter);

            var actors = Program.DB.GetActors(ListViewLimit, ListViewOffset, filter);
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
            var filter = new DB.Filters.SortFilter();
            SetupSortFilter(filter);

            var studio = (ViewMode == ViewMode.Studio) ? Program.DB.GetStudio(ListViewLimit, ListViewOffset, filter) :
                                                         Program.DB.GetTopStudio(ListViewLimit, ListViewOffset, filter);
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
            var filter = Program.DB.IsManagerMode ? new DB.Filters.SortFilter() :
                                                    new DB.Filters.CurrentUserOrderList();
            SetupSortFilter(filter);

            var orders = (ViewMode == ViewMode.Orders) ? Program.DB.GetOrders(ListViewLimit, ListViewOffset, filter, loadMovies: true) :
                                                         Program.DB.GetTopOrders(ListViewLimit, ListViewOffset, filter, loadMovies: false);
            var view = orders.Select(o => new Dictionary<string, string>()
                                          {
                                              { nameof(Data.Order.Id),     $"{o.Id}" },
                                              { nameof(Data.Order.Date),   Utility.UIPrimitiveFormatting.Format(o.Date,"g") },
                                              { nameof(Data.Order.User),   Utility.UIPrimitiveFormatting.FormatUserName(o.User) },
                                              { nameof(Data.Order.Movies), Utility.UIPrimitiveFormatting.FormatMoviesList(o.Movies) },
                                              { nameof(Data.Order.Income), Utility.UIPrimitiveFormatting.FormatPrice(o.Income)}
                                          }).ToList();

            PopulateListView(orders, view);
        }

        void RefreshUsersListView()
        {
            var filter = new DB.Filters.SortFilter();
            if (m_searchToolStripTextBox.Text.Length != 0)
            {
                var movieFilter = new DB.Filters.UserFilter();
                movieFilter.WithFirstNameLike(m_searchToolStripTextBox.Text);
                movieFilter.WithSecondNameLike(m_searchToolStripTextBox.Text);

                filter = movieFilter;
            }

            SetupSortFilter(filter);

            var users = (ViewMode == ViewMode.Users) ? Program.DB.GetUsers(ListViewLimit, ListViewOffset, filter) :
                                                       Program.DB.GetTopUsers(ListViewLimit, ListViewOffset, filter);

            var view = users.Select(u => new Dictionary<string, string>()
                                      {
                                          { nameof(Data.User.FirstName), Utility.UIPrimitiveFormatting.FormatUserName(u) },
                                          { nameof(Data.User.EMail),     u.EMail },
                                          { nameof(Data.User.Role),      Utility.UIPrimitiveFormatting.Format(u.Role) },
                                          { nameof(Data.User.Income),    Utility.UIPrimitiveFormatting.FormatPrice(u.Income)},
                                          { nameof(Data.User.LastOrder), Utility.UIPrimitiveFormatting.Format(u.LastOrder,"g")}
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
                case ViewMode.TopOrders:
                case ViewMode.TopStudio:
                case ViewMode.TopUsers:
                case ViewMode.AdvicedMovies:
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
                case ViewMode.TopOrders:
                case ViewMode.TopStudio:
                case ViewMode.TopUsers:
                case ViewMode.AdvicedMovies:
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
                using (var orderForm = new OrderForm())
                {
                    var isManagerMode = Program.DB.IsManagerMode;
                    orderForm.SetMode(selectedItem);

                    res = orderForm.ShowDialog(this) == DialogResult.OK;
                }
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
                case ViewMode.AdvicedMovies:
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
            var columns = (ViewMode == ViewMode.Movies || ViewMode == ViewMode.AdvicedMovies) ? MovieModeListColumns : 
                                                         (Program.DB.IsManagerMode ? ManagerTopMovieModeListColumns : CustomerTopMovieModeListColumns);
            var sortColumnt = (ViewMode == ViewMode.Movies || ViewMode == ViewMode.AdvicedMovies) ? MovieModeListSortColumnDefault :
                                                         (Program.DB.IsManagerMode ? ManagerTopMovieModeListSortColumnDefault : CustomerTopMovieModeListSortColumnDefault);
            ReinitListView(columns, sortColumnt);
        }

        void ReinitActorsModeControls()
        {
            ReinitListView(ActorsModeListColumns, ActorsModeListSortColumnDefault);
        }

        void ReinitUsersModeControls()
        {
            var columns = (ViewMode == ViewMode.Users) ? UsersModeListColumns : TopUsersModeListColumns;
            var sortColumnt = (ViewMode == ViewMode.Users) ? UsersModeListSortColumnDefault : TopUsersModeListSortColumnDefault;
            ReinitListView(columns, sortColumnt);
        }

        void ReinitStudiosModeControls()
        {
            var columns = (ViewMode == ViewMode.Studio) ? StudiosModeListColumns :
                                                         (Program.DB.IsManagerMode ? ManagerTopStudiosModeListColumns : CustomerTopStudiosModeListColumns);

            var sortColumn = (ViewMode == ViewMode.Studio) ? StudiosModeListSortColumnDefault :
                                                         (Program.DB.IsManagerMode ? ManagerTopStudiosModeListSortColumnDefault : CustomerTopStudiosModeListSortColumnDefault);

            ReinitListView(columns, sortColumn);
        }

        void ReinitOrdersModeControls()
        {
            var columns = (ViewMode == ViewMode.Orders) ? OrdersModeListColumns : 
                                                         (Program.DB.IsManagerMode ? ManagerTopOrdersModeListColumns : CustomerTopOrdersModeListColumns);

            var sortColumn = (ViewMode == ViewMode.Orders) ? OrdersModeListSortColumnDefault :
                                                           (Program.DB.IsManagerMode ? ManagerTopOrdersModeListSortColumnDefault : CustomerTopOrdersModeListSortColumnDefault);

            ReinitListView(columns, sortColumn);
        }

        void ReinitListView(ColumnHeader[] columns, SortColumn sortColumn)
        {
            ListViewOffset = 0;
            ListViewSortColumn = sortColumn;

            Utility.UIListView.InitColumns(m_listView, columns);

            ExpandListViewColumns(false);
        }

        void PopulateListView<T>(IList<T> model, IList<Dictionary<string, string>> view) where T : class
        {
            Utility.UIListView.PopulateItems(m_listView, model, view);
            
            var sortColumnIndex = m_listView.Columns.IndexOfKey(ListViewSortColumn.ColumnName);
            Utility.UIListView.SetSortIcon(m_listView, sortColumnIndex, ListViewSortColumn.Order);

            ExpandListViewColumns();
        }

        void ExpandListViewColumns(bool byContent = true)
        {
            Utility.UIListView.ExpandColumns(m_listView, byContent);
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

                m_myBasketToolStripMenuItem.Enabled = isAuthorized && !isBasketEmpty;
                m_myBasketToolStripMenuItem.Visible = true;
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

                m_searchToolStripTextBox.Enabled = isAuthorized;
                m_searchToolStripTextBox.Visible = IsMoviesViewMode || IsUsersViewMode || IsActorsViewMode;

                m_refreshToolStripButton.Enabled = isAuthorized;
                m_refreshToolStripButton.Visible = true;

                m_addNewToolStripButton.Enabled = isManagerMode && isAuthorized && IsViewModeEditable;
                m_addNewToolStripButton.Visible = isManagerMode;

                m_deleteToolStripButton.Enabled = isManagerMode && isAuthorized && IsViewModeEditable && hasSelection;
                m_deleteToolStripButton.Visible = isManagerMode;

                m_addToBasketToolStripButton.Enabled = isAuthorized && hasSelection;
                m_addToBasketToolStripButton.Visible = IsMoviesViewMode;

                m_basketToolStripButton.Enabled = isAuthorized && !isBasketEmpty;
                m_basketToolStripButton.Visible = true;
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

        #endregion

        #region Event handlers

        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                using(var welcomeForm = new WelcomeForm())
                {
                    var resp = welcomeForm.ShowDialog(this);
                    if(resp == DialogResult.OK)
                    {
                        BasketList.Clear();

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
            if (CanClose())
            {
                Close();
            }
        }

        private void OnLogout(object sender, EventArgs e)
        {
            try
            {
                if (CanClose())
                {
                    Logout();

                    var res = MessageBox.Show(this, "You have been logged out. Do you want to login again?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        Login();
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

        private void OnNextPage(object sender, EventArgs e)
        {
            try
            {
                NavigateListViewPage(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnPrevPage(object sender, EventArgs e)
        {
            try
            {
                NavigateListViewPage(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnRefreshPage(object sender, EventArgs e)
        {
            try
            {
                RefreshListView();
                UpdateControls();
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

        private void OnAdvicedMoviesMode(object sender, EventArgs e)
        {
            try
            {
                SetViewMode(ViewMode.AdvicedMovies);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnAddToBasket(object sender, EventArgs e)
        {
            try
            {
                AddToBasket();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnMyBasket(object sender, EventArgs e)
        {
            try
            {
                MyBasket();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                var column = (e.Column >= 0 && e.Column < m_listView.Columns.Count) ? m_listView.Columns[e.Column] : null;
                if (column?.Tag != null)
                {
                    if (column.Name.Equals(ListViewSortColumn.ColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        ListViewSortColumn.Order = ListViewSortColumn.IsAscending ? SortOrder.Descending : SortOrder.Ascending;
                    }
                    else
                    {
                        ListViewSortColumn = new SortColumn() { ColumnName = column.Name };
                    }

                    RefreshListView();
                    UpdateControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        #endregion
    }
}
