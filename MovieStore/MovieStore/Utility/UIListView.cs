using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieStore.Utility
{
    static class UIListView
    {
        static internal void InitColumns(ListView listView, ColumnHeader[] columns, bool clearItem = true)
        {
            try
            {
                listView.BeginUpdate();

                if (clearItem)
                {
                    listView.Items.Clear();
                }

                listView.Columns.Clear();
                listView.Columns.AddRange(columns);
            }
            finally
            {
                listView.EndUpdate();
            }
        }

        static internal void ExpandColumns(ListView listView, bool byContent = true)
        {
            listView.BeginUpdate();

            foreach (var c in listView.Columns.Cast<ColumnHeader>())
            {
                if (byContent)
                {
                    c.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                c.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            listView.EndUpdate();
        }

        static internal void PopulateItems<T>(ListView listView, IList<T> model, IList<Dictionary<string, string>> view, bool clearItem = true) where T : class
        {
            try
            {
                listView.BeginUpdate();

                if (clearItem)
                {
                    listView.Items.Clear();
                }

                for (var i = 0; i < model.Count; i++)
                {
                    var m = model[i];
                    var v = view[i];

                    var lv = default(ListViewItem);
                    foreach (var c in listView.Columns.Cast<ColumnHeader>())
                    {
                        var value = UIPrimitiveFormatting.c_NoData;
                        v.TryGetValue(c.Name, out value);

                        if (lv == null)
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

                    listView.Items.Add(lv);
                }
            }
            finally
            {
                listView.EndUpdate();
            }
        }
    }
}
