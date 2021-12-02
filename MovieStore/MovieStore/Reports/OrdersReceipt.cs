﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieStore.Reports
{
    class OrdersReceipt : IReport
    {
        IEnumerable<Data.Order> Orders { get; init; }
        string FileName { get; init; }

        internal OrdersReceipt(IEnumerable<Data.Order> orders, string fileName)
        {
            Orders = orders;
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

            using (var textStream = new StreamWriter(FileName, false, Encoding.UTF8))
            {
                foreach (var o in Orders)
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
