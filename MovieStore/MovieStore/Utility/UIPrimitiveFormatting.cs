using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Utility
{
    static class UIPrimitiveFormatting
    {
        internal const string c_NoData = "-";

        static internal string Format(DateTime? value, string format)
        {
            return value.HasValue ? $"{value.Value.ToString(format)}" : c_NoData;
        }

        static internal string Format(Data.ActorFamilyStatus? value)
        {
            return value.HasValue ? (value == Data.ActorFamilyStatus.Single ? "Single" : "Married") : c_NoData;
        }

        static internal string Format(Data.UserRole? value)
        {
            return value.HasValue ? (value == Data.UserRole.Manager? "Manager" : "Customer") : c_NoData;
        }

        static internal string FormatString(string text, int width)
        {
            if (text.Length > Math.Abs(width))
            {
                text = text.Substring(0, Math.Abs(width));
            }

            var res = string.Format($"{{0, {width}}}", text);

            return res;
        }

        static internal string FormatImdb(float? value)
        {
            return value.HasValue ? $"{value.Value:F2}" : c_NoData;
        }

        static internal string FormatPrice(int? value, string currencyPrefix = "$", string currencySuffix = "")
        {
            return value.HasValue ? $"{currencyPrefix}{value.Value/100:F2}{currencySuffix}" : c_NoData;
        }

        static internal string FormatStarActor(IEnumerable<Data.Actor> value)
        {
            return value?.FirstOrDefault().FirstName ?? c_NoData;
        }

        static internal string FormatList(IEnumerable<string> value, string separator = " ")
        {
            return (value?.Any() ?? false) ? string.Join(separator, value) : c_NoData;
        }

        static internal string FormatActorsList(IEnumerable<Data.Actor> value, string separator = ". ")
        {
            return FormatList(value?.Select(v => string.Join(' ', new string[] { v.FirstName, v.SecondName }).Trim(' ')), separator);
        }

        static internal string FormatMoviesList(IEnumerable<Data.Movie> value, string separator = "; ")
        {
            return FormatList(value?.Select(v => v.Title), separator);
        }

        static internal string FormatUserName(Data.User value)
        {
            return (value != null) ? string.Join(' ', new string[] { value.FirstName, value.SecondName }).Trim(' ') : c_NoData;
        }

        static internal string FormatActorName(Data.Actor value)
        {
            return (value != null) ? string.Join(' ', new string[] { value.FirstName, value.SecondName }).Trim(' ') : c_NoData;
        }
    }
}
