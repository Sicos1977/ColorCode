﻿// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using ColorCodeStandard.Common;

namespace ColorCodeStandard.Compilation.Languages
{
    public class Java : ILanguage
    {
        public string Id => LanguageId.Java;

        public string Name => "Java";

        public string CssClassName => "java";

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
                    @"'[^\n]*?(?<!\\)'",
                    new Dictionary<int, string>
                    {
                        {0, ScopeName.String}
                    }),
                new LanguageRule(
                    @"(?s)(""[^\n]*?(?<!\\)"")",
                    new Dictionary<int, string>
                    {
                        {0, ScopeName.String}
                    }),
                new LanguageRule(
                    @"\b(abstract|assert|boolean|break|byte|case|catch|char|class|const|continue|default|do|double|else|enum|extends|false|final|finally|float|for|goto|if|implements|import|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|strictfp|super|switch|synchronized|this|throw|throws|transient|true|try|void|volatile|while)\b",
                    new Dictionary<int, string>
                    {
                        {0, ScopeName.Keyword}
                    })
            };

        public override string ToString()
        {
            return Name;
        }
    }
}