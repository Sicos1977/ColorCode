using System.Drawing;
using ColorCode.Common;

namespace ColorCode.Styling.StyleSheets
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

        #region ColorFromHex
        private static Color ColorFromHex(string hex)
        {
            // ReSharper disable once PossibleNullReferenceException
            return (Color) new ColorConverter().ConvertFromString(hex);
        }
        #endregion

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
                new Style(ScopeName.HtmlTagDelimiter) {Foreground = ColorFromHex("#FF9D00"), CssClassName = "htmlTagDelimiter"},
                new Style(ScopeName.HtmlElementName) {Foreground = ColorFromHex("#5DF1FF"), CssClassName = "htmlElementName"},
                new Style(ScopeName.HtmlAttributeName) {Foreground = ColorFromHex("#EFDD22"), CssClassName = "htmlAttributeName"},
                new Style(ScopeName.HtmlAttributeValue) {Foreground = ColorFromHex("#3AD922"), CssClassName = "htmlAttributeValue"},
                new Style(ScopeName.HtmlOperator) {Foreground = Color.Red, CssClassName = "htmlOperator"},
                new Style(ScopeName.HtmlEntity) {Foreground = Color.White, CssClassName = "htmlEntity"},
                // XML
                new Style(ScopeName.XmlAttribute) {Foreground = ColorFromHex("#EFDD22"), CssClassName = "xmlAttribute"},
                new Style(ScopeName.XmlAttributeQuotes) {Foreground = ColorFromHex("#41A449"), CssClassName = "xmlAttributeQuotes"},
                new Style(ScopeName.XmlAttributeValue) {Foreground = ColorFromHex("#3AD922"), CssClassName = "xmlAttributeValue"},
                new Style(ScopeName.XmlCDataSection) {Foreground = Color.Gray, CssClassName = "xmlCDataSection"},
                new Style(ScopeName.XmlDelimiter) {Foreground = ColorFromHex("#FF9D00"), CssClassName = "xmlDelimiter"},
                new Style(ScopeName.XmlName) {Foreground = ColorFromHex("#5DF1FF"), CssClassName = "xmlName"},

                // Custom
                new Style(ScopeName.Keyword) {Foreground = ColorFromHex("#569CD6"), CssClassName = "keyword"},
                new Style(ScopeName.PreprocessorKeyword) {Foreground = ColorFromHex("#9B9B9B"), CssClassName = "preprocessorKeyword"},

                // Comments
                new Style(ScopeName.Comment) {Foreground = Color.Green, CssClassName = "comment"},
                new Style(ScopeName.XmlDocComment) {Foreground = Color.Green, CssClassName = "xmlDocComment"},
                new Style(ScopeName.XmlDocTag) {Foreground = Color.Green, CssClassName = "xmlDocTag"},
                new Style(ScopeName.XmlComment) {Foreground = Color.Green, CssClassName = "xmlComment"},
                new Style(ScopeName.ClassName) {Foreground = ColorFromHex("#4EC9B0"), CssClassName = "className"},

                new Style(ScopeName.String) {Foreground = ColorFromHex("#D69D85"), CssClassName = "string"},
                new Style(ScopeName.StringCSharpVerbatim) {Foreground = ColorFromHex("#D69D85"), CssClassName = "stringCSharpVerbatim"},
                
                // SQL
                new Style(ScopeName.SqlSystemFunction) {Foreground = ColorFromHex("#EF6273"), CssClassName = "sqlSystemFunction"},

                // CSS
                new Style(ScopeName.CssSelector) {Foreground = ColorFromHex("#3AD900"), CssClassName = "cssSelector"},
                new Style(ScopeName.CssPropertyName) {Foreground = ColorFromHex("#80FFBA"), CssClassName = "cssPropertyName"},
                new Style(ScopeName.CssPropertyValue) {Foreground = ColorFromHex("#EF6273"), CssClassName = "cssPropertyValue"},

                // PowerShell
                new Style(ScopeName.PowerShellAttribute) {Foreground = ColorFromHex("#EFDD22"), CssClassName = "powershellAttribute"},
                new Style(ScopeName.PowerShellOperator) {Foreground = Color.Red, CssClassName = "powershellOperator"},
                new Style(ScopeName.PowerShellType) {Foreground = ColorFromHex("#9EFFFF"), CssClassName = "powershellType"},
                new Style(ScopeName.PowerShellVariable) {Foreground = ColorFromHex("#5DF1FF"), CssClassName = "powershellVariable"},
            };
        }
        #endregion
    }
}