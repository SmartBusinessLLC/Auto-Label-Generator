using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Addin
{
    class AsParseLabel
    {
        private const string PatternFindText = @"""(\\.|[^""\\])+""|""""";
        private const string ReplacePattern = "";
        private const string PatternFindLabel = "\\@{1}([a-zA-Z0-9])+\\:{0,1}([a-zA-Z0-9])+";
        private const string PatternNotLetters = "[^a-zA-Z-]";
        public const int MaxLabelIdSize = 50;
        private static bool[] _lookup;

        public static void fillLookupArray()
        {
            _lookup = new bool[65536];
            for (char c = '0'; c <= '9'; c++) _lookup[c] = true;
            for (char c = 'A'; c <= 'Z'; c++) _lookup[c] = true;
            for (char c = 'a'; c <= 'z'; c++) _lookup[c] = true;
        }

        public List<string> ParseString(string source)
        {
            List<string> mylist = new List<string>();

            if (!string.IsNullOrEmpty(source))
            {
                Regex r = new Regex(PatternFindText, RegexOptions.IgnoreCase);
                Match m = r.Match(source);

                while (m.Success)
                {
                    string text = m.ToString();
                    text = text.Substring(1, text.Length - 2);

                    if (text.Length != 0 && LabelManager.IsPrefixed(text))
                    {
                        mylist.Add(text);
                    }

                    m = m.NextMatch();
                }
            }

            return mylist;
        }

        public static string TrimLabelId(string text)
        {
            if (text.Length > MaxLabelIdSize)
            {
                return text.Substring(0, MaxLabelIdSize);
            }

            return text;
        }
        public static Boolean IsLabelFormat(string source)
        {
            Boolean ret = false;

            Regex rgx = new Regex(PatternFindLabel);
            Match m = rgx.Match(source);
            if (m.Success)
            {
                ret = true;
            }

            return ret;
        }

        public static string CreateLabelIdString(string source, bool camel)
        {
            string ret = source;

            if (!string.IsNullOrEmpty(ret))
            {
                //CamelStyle
                if(camel)
                {
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    ret = textInfo.ToTitleCase(ret);
                }

                //LeftOnly letters and numbers
                Regex rgx = new Regex(PatternFindText);
                ret = rgx.Replace(ret, "");

                ret = TrimLabelId(ret);
                ret = ret.Replace(" ", string.Empty);

                ret = RemoveSpecialCharacters(ret);
            }

            return ret;
        }
        public static string RemoveSpecialCharacters(string str)
        {
            fillLookupArray();
            char[] buffer = new char[str.Length];
            int index = 0;
            foreach (char c in str)
            {
                if (_lookup[c])
                {
                    buffer[index] = c;
                    index++;
                }
            }
            return new string(buffer, 0, index);
        }
    }
}