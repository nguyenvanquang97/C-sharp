using System;
using System.Collections.Generic;

namespace Wini.SaleMultipleChannel.Lazop
{
    /// <summary>
    /// Lazada Open Platform style dictionary.
    /// </summary>
    public class LazopDictionary : Dictionary<string, string>
    {
        public LazopDictionary() { }

        public LazopDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        { }

        /// <summary>
        /// add a new key-value struct. key or value can't be null
        /// </summary>
        /// <param name="key">name</param>
        /// <param name="value">suport：string, int, long, double, bool, DateTime</param>
        public void Add(string key, object value)
        {
            string strValue;

            if (value == null)
            {
                strValue = null;
            }
            else if (value is string)
            {
                strValue = (string)value;
            }
            else if (value is DateTime?)
            {
                DateTime? dateTime = value as DateTime?;
                strValue = dateTime.Value.Ticks.ToString();
            }
            else if (value is int?)
            {
                strValue = (value as int?).Value.ToString();
            }
            else if (value is long?)
            {
                strValue = (value as long?).Value.ToString();
            }
            else if (value is double?)
            {
                strValue = (value as double?).Value.ToString();
            }
            else if (value is bool?)
            {
                strValue = (value as bool?).Value.ToString().ToLower();
            }
            else
            {
                strValue = value.ToString();
            }

            Add(key, strValue);
        }

        public new void Add(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                base[key] = value;
            }
        }

        public void AddAll(IDictionary<string, string> dict)
        {
            if (dict != null && dict.Count > 0)
            {
                IEnumerator<KeyValuePair<string, string>> kvps = dict.GetEnumerator();
                while (kvps.MoveNext())
                {
                    KeyValuePair<string, string> kvp = kvps.Current;
                    Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
}
