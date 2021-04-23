// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using ColorCode.Common;

namespace ColorCode.Compilation.Languages
{
    public class JavaScript : ILanguage
    {
        public string Id => LanguageId.JavaScript;

        public string Name => "JavaScript";

        public string CssClassName => "javascript";

        public string FirstLinePattern => null;

        public IList<LanguageRule> Rules =>
            new List<LanguageRule>
            {
                new LanguageRule(
                    @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/",
                    new Dictionary<int, string>
                    {
                        {0, ScopeName.Comment}
                    }),
                new LanguageRule(
                    @"(//.*?)\r?$",
                    new Dictionary<int, string>
                    {
                        {1, ScopeName.Comment}
                    }),
                new LanguageRule(
                    @"'[^\n]*?'",
                    new Dictionary<int, string>
                    {
                        {0, ScopeName.String}
                    }),
                new LanguageRule(
                    @"""[^\n]*?""",
                    new Dictionary<int, string>
                    {
                        {0, ScopeName.String}
                    }),
                new LanguageRule(
                    @"\b(abstract|boolean|break|byte|case|catch|char|class|const|continue|debugger|default|delete|do|double|else|enum|export|extends|false|final|finally|float|for|function|goto|if|implements|import|in|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|super|switch|synchronized|this|throw|throws|transient|true|try|typeof|var|void|volatile|while|with)\b",
                    new Dictionary<int, string>
                    {
                        {1, ScopeName.Keyword}
                    })
            };

        public override string ToString()
        {
            return Name;
        }
    }
}