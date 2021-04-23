using ColorCodeStandard.Formatting;

namespace ColorCodeStandard
{
    /// <summary>
    ///     Provides easy access to ColorCode's built-in formatters.
    /// </summary>
    public static class Formatters
    {
        /// <summary>
        ///     Gets the default formatter.
        /// </summary>
        /// <remarks>
        ///     The default formatter produces HTML with stylesheet references.
        /// </remarks>
        public static IFormatter Default => new HtmlClassFormatter();
    }
}