using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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

        // https://stackoverflow.com/a/254139/987850
        static internal void SetSortIcon(ListView listViewControl, int columnIndex, SortOrder order)
        {
            IntPtr columnHeader = SendMessage(listViewControl.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
            for (int columnNumber = 0; columnNumber < listViewControl.Columns.Count; columnNumber++)
            {
                var columnPtr = new IntPtr(columnNumber);
                var item = new HDITEM
                {
                    mask = HDITEM.Mask.Format
                };

                if (SendMessage(columnHeader, HDM_GETITEM, columnPtr, ref item) == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                if (order != SortOrder.None && columnNumber == columnIndex)
                {
                    switch (order)
                    {
                        case SortOrder.Ascending:
                            item.fmt &= ~HDITEM.Format.SortDown;
                            item.fmt |= HDITEM.Format.SortUp;
                            break;
                        case SortOrder.Descending:
                            item.fmt &= ~HDITEM.Format.SortUp;
                            item.fmt |= HDITEM.Format.SortDown;
                            break;
                    }
                }
                else
                {
                    item.fmt &= ~HDITEM.Format.SortDown & ~HDITEM.Format.SortUp;
                }

                if (SendMessage(columnHeader, HDM_SETITEM, columnPtr, ref item) == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }
            }
        }

        #region Helper methods & types

        [StructLayout(LayoutKind.Sequential)]
        struct HDITEM
        {
            public Mask mask;
            public int cxy;
            [MarshalAs(UnmanagedType.LPTStr)] public string pszText;
            public IntPtr hbm;
            public int cchTextMax;
            public Format fmt;
            public IntPtr lParam;
            // _WIN32_IE >= 0x0300 
            public int iImage;
            public int iOrder;
            // _WIN32_IE >= 0x0500
            public uint type;
            public IntPtr pvFilter;
            // _WIN32_WINNT >= 0x0600
            public uint state;

            [Flags]
            public enum Mask
            {
                Format = 0x4,       // HDI_FORMAT
            };

            [Flags]
            public enum Format
            {
                SortDown = 0x200,   // HDF_SORTDOWN
                SortUp = 0x400,     // HDF_SORTUP
            };
        };

        const int LVM_FIRST = 0x1000;
        const int LVM_GETHEADER = LVM_FIRST + 31;

        const int HDM_FIRST = 0x1200;
        const int HDM_GETITEM = HDM_FIRST + 11;
        const int HDM_SETITEM = HDM_FIRST + 12;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, ref HDITEM lParam);

        #endregion
    }
}
