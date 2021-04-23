// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using ColorCode.Common;

namespace ColorCode.Compilation.Languages
{
    public class Ashx : ILanguage
    {
        public string Id => LanguageId.Ashx;

        public string Name => "ASHX";

        public string CssClassName => "ashx";

        public string FirstLinePattern => null;

        public IList<LanguageRule> Rules =>
            new List<LanguageRule>
            {
                new LanguageRule(
                    @"(<%)(--.*?--)(%>)",
                    new Dictionary<int, string>
                    {
                        {1, ScopeName.HtmlServerSideScript},
                        {2, ScopeName.HtmlComment},
                        {3, ScopeName.HtmlServerSideScript}
                    }),
                new LanguageRule(
                    @"(?is)(?<=<%@.+?language=""c\#"".*?%>)(.*)",
                    new Dictionary<int, string>
                    {
                        {1, $"{ScopeName.LanguagePrefix}{LanguageId.CSharp}"}
                    }),
                new LanguageRule(
                    @"(?is)(?<=<%@.+?language=""vb"".*?%>)(.*)",
                    new Dictionary<int, string>
                    {
                        {1, $"{ScopeName.LanguagePrefix}{LanguageId.VbDotNet}"}
                    }),
                new LanguageRule(
                    @"(<%)(@)(?:\s+([a-zA-Z0-9]+))*(?:\s+([a-zA-Z0-9]+)(=)(""[^\n]*?""))*\s*?(%>)",
                    new Dictionary<int, string>
                    {
                        {1, ScopeName.HtmlServerSideScript},
                        {2, ScopeName.HtmlTagDelimiter},
                        {3, ScopeName.HtmlElementName},
                        {4, ScopeName.HtmlAttributeName},
                        {5, ScopeName.HtmlOperator},
                        {6, ScopeName.HtmlAttributeValue},
                        {7, ScopeName.HtmlServerSideScript}
                    })
            };

        public override string ToString()
        {
            return Name;
        }
    }
}