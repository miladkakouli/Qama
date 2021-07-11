using System;
using System.Text.RegularExpressions;

namespace Qama.Framework.Extensions.Validations
{
    public static class ParamValidations
    {
        public static bool IsValidSsn(this string value)
        {
            long sum = 0;
            if (value.Length != 10)
            {
                return false;
            }

            for (int i = 0; i < 9; i++)
            {
                sum += (Convert.ToInt16(value[i]) - 48) * (10 - i);
            }
            var r = sum % 11;
            if (r < 2)
            {
                if (Convert.ToInt16(value[9] - 48) != r)
                {
                    return false;
                }
            }
            else
            {
                if ((Convert.ToInt16(value[9]) - 48) != 11 - r)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsValidNationalCode(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var L = value.Length;

            if (L < 11 || Convert.ToInt64(value) == 0)
                return false;

            var c = Convert.ToInt64(value.Substring(10, 1));
            var d = Convert.ToInt64(value.Substring(9, 1)) + 2;

            int[] z = { 29, 27, 23, 19, 17 };
            long s = 0;
            for (var i = 0; i < 10; i++)
                s += (d + Convert.ToInt64(value.Substring(i, 1))) * z[i % 5];
            s %= 11;

            if (s == 10)
                s = 0;
            if (c != s)
                return false;
            return true;
        }
        public static bool IsValidMobile(this string value)
        {
            return Regex.IsMatch(value, "^(\\+989)([0-9]{9})$|^(989)([0-9]{9})$|^(09)([0-9]{9})$");
        }
        public static bool IsValidBrokerCode(this string value)
        {
            return Regex.IsMatch(value, "^\\d{3}$");
        }
        public static bool IsValidCardSerial(this string value)
        {
            return Regex.IsMatch(value, "^([\\d]{9})$|^([\\d]{1}[\\w][\\d]{8})$");
        }
        public static bool IsValidInclusiveCode(this string value)
        {
            long sum = 0;
            long digit;
            long addend;
            bool timesTwo = false;

            for (var i = value.Length - 1; i >= 0; i--)
            {
                digit = Int64.Parse(value.Substring(i, i + 1));
                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                    { addend -= 9; }
                }
                else { addend = digit; }
                sum += addend;
                timesTwo = !timesTwo;
            }

            long modulus = sum % 10;
            if (modulus == 0)
                return false;
            return true;
        }
        public static bool IsValidIp(this string value)
        {
            return Regex.IsMatch(value, "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
        }
        public static bool IsValidPersianDate(this string value)
        {
            return Regex.IsMatch(value, "^[\\d]{4}\\/[\\d]{2}\\/[\\d]{2}$");
        }
        public static bool IsValidPersianDateWithoutSlash(this string value)
        {
            return Regex.IsMatch(value, "^[\\d]{4}[\\d]{2}[\\d]{2}$");
        }
        public static bool IsValidText(this string value, long length)
        {
            return Regex.IsMatch(value, "^[0-9a-zA-Z]{" + length + "}$");
        }
        public static bool IsValidIsin(this string value)
        {
            return Regex.IsMatch(value, "^(IR)([0-9a-zA-Z]{10})$");
        }
        public static bool IsValidLegacyCode(this string value)
        {
            if (value.Length < 8)
                return false;
            var number = value.Substring(value.Length - 5, 5);
            return Regex.IsMatch(number, "^[0-9]{5}$");
        }
        public static bool IsValidIban(this string value)
        {
            if (value.Trim().Length != 26)
                return false;
            if (!Regex.IsMatch(value, "^[a-zA-Z0-9]*$"))
                return false;

            string t = value.Substring(0, 4);
            string y = value.Substring(4, 22);
            string u = y + t;
            u = u.Replace("A", "10")
                .Replace("B", "11")
                .Replace("C", "12")
                .Replace("D", "13")
                .Replace("E", "14")
                .Replace("F", "15")
                .Replace("G", "16")
                .Replace("H", "17")
                .Replace("I", "18")
                .Replace("J", "19")
                .Replace("K", "20")
                .Replace("L", "21")
                .Replace("M", "22")
                .Replace("N", "23")
                .Replace("O", "24")
                .Replace("P", "25")
                .Replace("Q", "26")
                .Replace("R", "27")
                .Replace("S", "28")
                .Replace("T", "29")
                .Replace("U", "30")
                .Replace("V", "31")
                .Replace("W", "32")
                .Replace("X", "33")
                .Replace("Y", "34")
                .Replace("Z", "35");

            decimal i = decimal.Parse(u);

            if (i % 97 != 1)
                return false;
            return true;
        }
    }
}
