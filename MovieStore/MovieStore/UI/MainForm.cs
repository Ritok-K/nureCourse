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
                    }));
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
                    }));
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
                var studios = Program.DB.GetStudios(ListViewLimit, ListViewOffset);

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
                    }));
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
                    }));
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
                    }));
                }

                ExpandListViewColumns();
            }
            finally
            {
                m_listView.EndUpdate();
            }
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
    }
}
