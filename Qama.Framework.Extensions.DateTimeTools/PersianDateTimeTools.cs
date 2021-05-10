using System;

namespace Qama.Framework.Extensions.DateTimeTools
{
    public static class PersianDateTimeTools
    {
        public static int ToPersianShortDateInt(this DateTime dateTime) =>
            new MD.PersianDateTime.Standard.PersianDateTime(dateTime).ToShortDateInt();

        public static string ToPersianShortDateString(this DateTime dateTime) =>
            new MD.PersianDateTime.Standard.PersianDateTime(dateTime).ToShortDateString();

        public static string ToPersianDateTimeString(this DateTime dateTime) =>
            new MD.PersianDateTime.Standard.PersianDateTime(dateTime).ToString();
    }
}
