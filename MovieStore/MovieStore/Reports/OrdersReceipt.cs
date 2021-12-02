using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieStore.Reports
{
    class OrdersReceipt : IReport
    {
        IEnumerable<int> Ids { get; init; }
        string FileName { get; init; }

        internal OrdersReceipt(IEnumerable<int> ids, string fileName)
        {
            Ids = ids;
            FileName = fileName;
        }

        public void Build()
        {
            const int width = 50;
            const string Total = "Total: ";
            const string Date = "Date: ";
            const string Customer = "Customer: ";

            var titleWidth = width - (Utility.UIPrimitiveFormatting.FormatPrice(0).Length + 2);
            var totalWidth = width - Total.Length;
            var dateWidth = width - Date.Length;
            var customerWidth = width - Customer.Length;
            var delimeter = new string('-', width);

            var filter = new DB.Filters.OrderFilter();
            filter.WithIds(Ids);

            var orders = Program.DB.GetOrders(filter: filter, loadMovies: true);

            using (var textStream = new StreamWriter(FileName, false, Encoding.UTF8))
            {
                foreach (var o in orders)
                {
                    textStream.WriteLine($"Receipt #{o.Id}");
                    textStream.WriteLine($"{Customer}{FormatString(Utility.UIPrimitiveFormatting.FormatUserName(o.User), customerWidth)}");
                    textStream.WriteLine($"{Date}{FormatString(Utility.UIPrimitiveFormatting.Format(o.Date, "g"), dateWidth)}");
                    textStream.WriteLine($"{delimeter}");

                    int total = 0;
                    if (o.Movies?.Any() ?? false)
                    {
                        foreach (var m in o.Movies)
                        {
                            textStream.WriteLine($"{FormatString(m.Title, -titleWidth)} {Utility.UIPrimitiveFormatting.FormatPrice(m.Price)}");
                            total += m.Price;
                        }
                    }
                    
                    textStream.WriteLine($"{delimeter}");
                    textStream.WriteLine($"{Total}{FormatString(Utility.UIPrimitiveFormatting.FormatPrice(total), totalWidth)}");
                    textStream.WriteLine();
                    textStream.WriteLine();
                }
            }
        }

        static string FormatString(string text, int width)
        {
            return Utility.UIPrimitiveFormatting.FormatString(text, width);
        }
    }
}
