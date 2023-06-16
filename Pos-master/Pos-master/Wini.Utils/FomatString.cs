using System;
using System.Text.RegularExpressions;

namespace Pos
{
    public static class FomatString
    {

        public static string UnicodeToAscii(string unicode)
        {
            unicode = Regex.Replace(unicode, "[á|à|ả|ã|ạ|â|ă|ấ|ầ|ẩ|ẫ|ậ|ắ|ằ|ẳ|ẵ|ặ]", "a", RegexOptions.IgnoreCase);
            unicode = Regex.Replace(unicode, "[é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ]", "e", RegexOptions.IgnoreCase);
            unicode = Regex.Replace(unicode, "[ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự]", "u", RegexOptions.IgnoreCase);
            unicode = Regex.Replace(unicode, "[í|ì|ỉ|ĩ|ị]", "i", RegexOptions.IgnoreCase);
            unicode = Regex.Replace(unicode, "[ó|ò|ỏ|õ|ọ|ô|ơ|ố|ồ|ổ|ỗ|ộ|ớ|ờ|ở|ỡ|ợ]", "o", RegexOptions.IgnoreCase);
            unicode = Regex.Replace(unicode, "[đ|Đ]", "d", RegexOptions.IgnoreCase);
            unicode = Regex.Replace(unicode, "[ý|ỳ|ỷ|ỹ|ỵ|Ý|Ỳ|Ỷ|Ỹ|Ỵ]", "y", RegexOptions.IgnoreCase);
            unicode = Regex.Replace(unicode, "[,|~|@|/|.|:|?|#|$|%|&|*|(|)|+|”|“|'|\"|!|`|–]", "", RegexOptions.IgnoreCase);
            //unicode = Regex.Replace(unicode, "[^A-Za-z0-9-]", "");
            return unicode;
        }
        public static string Slug(string unicode)
        {
            unicode = UnicodeToAscii(unicode);
            unicode = unicode.ToLower().Trim();
            unicode = Regex.Replace(unicode, @"\s+", " ");
            unicode = Regex.Replace(unicode, "[\\s]", "-");
            unicode = Regex.Replace(unicode, @"-+", "-");
            return unicode;
        }

        public static string Truncate(this string value, int maxLength)
        {
            return string.IsNullOrEmpty(value) ? value : value.Substring(0, Math.Min(value.Length, maxLength));
        }

        public static string SubString(this string input, int maxLength)
        {
            if (input.Length <= maxLength) return input;
            maxLength -= "...".Length;
            maxLength = input.Length < maxLength ? input.Length : maxLength;
            var isLastSpace = input[maxLength] == ' ';
            var part = input.Substring(0, maxLength);
            if (isLastSpace)
                return part + "...";
            var lastSpaceIndexBeforeMax = part.LastIndexOf(' ');
            if (lastSpaceIndexBeforeMax == -1)
                return part + "...";
            return input.Substring(0, lastSpaceIndexBeforeMax) + "...";
        }
        public static string Money(this decimal? price)
        {
            return (price ?? 0).MoneyBtc();
        }
        public static string MoneyBtc(this decimal price)
        {
            var str = price.ToString();
            if (str.Contains(","))
            {
                var a = 0;
                var stringpr = str.Split(',');
                if (stringpr.Length > 1)
                {
                    for (int i = stringpr[1].Length; i > 0; i--)
                    {
                        if (stringpr[1].Substring(i - 1, 1) != "0")
                        {
                            a = i;
                            break;
                        }
                    }
                    var b1 = int.Parse(stringpr[0]);
                    var b2 = string.Format("{0:0,0}", b1);
                    var temp = b2.StartsWith("0") ? int.Parse(b2).ToString() : b2;
                    var sub = stringpr[1].Substring(0, a);
                    if (!string.IsNullOrEmpty(sub)) temp = temp + "." + sub;
                    return temp;
                }
                else
                {
                    var b2 = string.Format("{0:0,0}", price);
                    var temp = b2.StartsWith("0") ? int.Parse(b2).ToString() : b2;
                    return temp;
                }
            }
            else
            {
                var a = 0;
                var stringpr = str.Split('.');
                if (stringpr.Length > 1)
                {
                    for (int i = stringpr[1].Length; i > 0; i--)
                    {
                        if (stringpr[1].Substring(i - 1, 1) != "0")
                        {
                            a = i;
                            break;
                        }
                    }
                    var b1 = decimal.Parse(stringpr[0]);
                    var b2 = string.Format("{0:0,0}", b1);
                    var temp = b2.StartsWith("0") ? int.Parse(b2).ToString() : b2;
                    var sub = stringpr[1].Substring(0, a);
                    if (!string.IsNullOrEmpty(sub)) temp = temp + "." + sub;
                    return temp;
                }
                else
                {
                    var b2 = string.Format("{0:0,0}", price);
                    var temp = b2.StartsWith("0") ? int.Parse(b2).ToString() : b2;
                    return temp;
                }
            }
        }
        public static string Quantity(this decimal? price, string fomat = "0:0.##")
        {
            return price == null ? "0" : string.Format("{" + fomat + "}", price);
        }
    }
}
