using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieStore.Reports
{
    class OrdersReport : IReport
    {
        string FileName { get; init; }
        DateTime FromDate { get; init; } = DateTime.Now;
        DateTime ToDate { get; init; } = DateTime.Now;

        internal OrdersReport(string fileName, DateTime fromDate, DateTime toDate)
        {
            FileName = fileName;
            FromDate = fromDate.Date;
            ToDate = toDate.Date + new TimeSpan(23, 59, 59);
        }

        public void Build()
        {
            const int width = 50;
            const string Total = "Total: ";
            var totalWidth = width - Total.Length;
            var delimeter = new string('-', width);


            var filter = new DB.Filters.OrderFilter();
            filter.WithDatePeriod(FromDate, ToDate);

            var orders = Program.DB.GetOrders(filter: filter, loadMovies: true);

            using (var textStream = new StreamWriter(FileName, false, Encoding.UTF8))
            {
                textStream.WriteLine($"Orders Report");
                textStream.WriteLine($"Period: {Utility.UIPrimitiveFormatting.Format(FromDate, "d")} - {Utility.UIPrimitiveFormatting.Format(ToDate, "d")}");
                textStream.WriteLine($"{delimeter}");

                int total = 0;
                foreach (var o in orders)
                {
                    var orderPrice = o.Movies?.Select(m => m.Price).Aggregate((p, c) => p + c) ?? 0;
                    total += orderPrice;

                    var orderTitle = $"Order #{o.Id} ";
                    var priceWidth = width - orderTitle.Length;

                    textStream.WriteLine($"{orderTitle}{FormatString(Utility.UIPrimitiveFormatting.FormatPrice(orderPrice), priceWidth)}");
                }

                textStream.WriteLine($"{delimeter}");
                textStream.WriteLine($"{Total}{FormatString(Utility.UIPrimitiveFormatting.FormatPrice(total), totalWidth)}");
                textStream.WriteLine();
                textStream.WriteLine();
            }
        }

        static string FormatString(string text, int width)
        {
            return Utility.UIPrimitiveFormatting.FormatString(text, width);
        }
    }
}
