// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace ColorCodeStandard.Common
{
    public static class ExtensionMethods
    {
        public static void SortStable<T>(this IList<T> list,
            Comparison<T> comparison)
        {
            Guard.ArgNotNull(list, "list");

            var count = list.Count;

            for (var j = 1; j < count; j++)
            {
                var key = list[j];

                var i = j - 1;
                for (; i >= 0 && comparison(list[i], key) > 0; i--) list[i + 1] = list[i];

                list[i + 1] = key;
            }
        }

        public static Color HexToColor(this string str)
        {
            if (!str.StartsWith("#"))
                throw new ArgumentException($"'{str}' is not an hex string");

            var r = int.Parse(str.Substring(1,2), NumberStyles.HexNumber);
            var g = int.Parse(str.Substring(3,2), NumberStyles.HexNumber);
            var b = int.Parse(str.Substring(5,2), NumberStyles.HexNumber);
            return Color.FromArgb(r,g,b);
        }

        public static string ToHtmlColor(this Color color)
        {
            var r = (int) color.R;
            var g = (int) color.G;
            var b = (int) color.B;

            return $"#{r:X}{g:X}{b:X}";
        }
    }
}