//
// DarkModeStyleSheet.cs
//
// Author: Kees van Spelde <sicos2002@hotmail.com>
//
// Copyright (c) 2021 Magic-Sessions. (www.magic-sessions.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System.Drawing;
using ColorCodeStandard.Common;

namespace ColorCodeStandard.Styling.StyleSheets
{
    /// <summary>
    /// Add's a dark style to the formatting
    /// </summary>
    public class DarkModeStyleSheet : IStyleSheet
    {
        /// <summary>
        /// Gets the dictionary of styles for the style sheet.
        /// </summary>
        public StyleDictionary Styles { get; }

        #region Constructor
        /// <summary>
        /// Add's a dark style to the formatting
        /// </summary>
        public DarkModeStyleSheet()
        {
            Styles = new StyleDictionary
            {
                new Style(ScopeName.PlainText) {Foreground = Color.White, CssClassName = "plainText"},
                // HTML
                new Style(ScopeName.HtmlServerSideScript) {Foreground = Color.Yellow, CssClassName = "htmlServerSideScript"},
                new Style(ScopeName.HtmlComment) {Foreground = Color.Green, CssClassName = "htmlComment"},
                new Style(ScopeName.HtmlTagDelimiter) {Foreground = "#FF9D00".HexToColor(), CssClassName = "htmlTagDelimiter"},
                new Style(ScopeName.HtmlElementName) {Foreground = "#5DF1FF".HexToColor(), CssClassName = "htmlElementName"},
                new Style(ScopeName.HtmlAttributeName) {Foreground = "#EFDD22".HexToColor(), CssClassName = "htmlAttributeName"},
                new Style(ScopeName.HtmlAttributeValue) {Foreground = "#3AD922".HexToColor(), CssClassName = "htmlAttributeValue"},
                new Style(ScopeName.HtmlOperator) {Foreground = Color.Red, CssClassName = "htmlOperator"},
                new Style(ScopeName.HtmlEntity) {Foreground = Color.White, CssClassName = "htmlEntity"},
                // XML
                new Style(ScopeName.XmlAttribute) {Foreground = "#EFDD22".HexToColor(), CssClassName = "xmlAttribute"},
                new Style(ScopeName.XmlAttributeQuotes) {Foreground = "#41A449".HexToColor(), CssClassName = "xmlAttributeQuotes"},
                new Style(ScopeName.XmlAttributeValue) {Foreground = "#3AD922".HexToColor(), CssClassName = "xmlAttributeValue"},
                new Style(ScopeName.XmlCDataSection) {Foreground = Color.Gray, CssClassName = "xmlCDataSection"},
                new Style(ScopeName.XmlDelimiter) {Foreground = "#FF9D00".HexToColor(), CssClassName = "xmlDelimiter"},
                new Style(ScopeName.XmlName) {Foreground = "#5DF1FF".HexToColor(), CssClassName = "xmlName"},

                // Custom
                new Style(ScopeName.Keyword) {Foreground = "#569CD6".HexToColor(), CssClassName = "keyword"},
                new Style(ScopeName.PreprocessorKeyword) {Foreground = "#9B9B9B".HexToColor(), CssClassName = "preprocessorKeyword"},

                // Comments
                new Style(ScopeName.Comment) {Foreground = Color.Green, CssClassName = "comment"},
                new Style(ScopeName.XmlDocComment) {Foreground = Color.Green, CssClassName = "xmlDocComment"},
                new Style(ScopeName.XmlDocTag) {Foreground = Color.Green, CssClassName = "xmlDocTag"},
                new Style(ScopeName.XmlComment) {Foreground = Color.Green, CssClassName = "xmlComment"},
                new Style(ScopeName.ClassName) {Foreground = "#4EC9B0".HexToColor(), CssClassName = "className"},

                new Style(ScopeName.String) {Foreground = "#D69D85".HexToColor(), CssClassName = "string"},
                new Style(ScopeName.StringCSharpVerbatim) {Foreground = "#D69D85".HexToColor(), CssClassName = "stringCSharpVerbatim"},
                
                // SQL
                new Style(ScopeName.SqlSystemFunction) {Foreground = "#EF6273".HexToColor(), CssClassName = "sqlSystemFunction"},

                // CSS
                new Style(ScopeName.CssSelector) {Foreground = "#3AD900".HexToColor(), CssClassName = "cssSelector"},
                new Style(ScopeName.CssPropertyName) {Foreground = "#80FFBA".HexToColor(), CssClassName = "cssPropertyName"},
                new Style(ScopeName.CssPropertyValue) {Foreground = "#EF6273".HexToColor(), CssClassName = "cssPropertyValue"},

                // PowerShell
                new Style(ScopeName.PowerShellAttribute) {Foreground = "#EFDD22".HexToColor(), CssClassName = "powershellAttribute"},
                new Style(ScopeName.PowerShellOperator) {Foreground = Color.Red, CssClassName = "powershellOperator"},
                new Style(ScopeName.PowerShellType) {Foreground = "#9EFFFF".HexToColor(), CssClassName = "powershellType"},
                new Style(ScopeName.PowerShellVariable) {Foreground = "#5DF1FF".HexToColor(), CssClassName = "powershellVariable"},
            };
        }
        #endregion
    }
}