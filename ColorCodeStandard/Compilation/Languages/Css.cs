// Copyright (c) Microsoft Corporation.  All rights reserved.   

using System.Collections.Generic;
using ColorCodeStandard.Common;

namespace ColorCodeStandard.Compilation.Languages
{
    public class Css : ILanguage
    {
        public string Id => LanguageId.Css;

        public string Name => "CSS";

        public string CssClassName => "css";

        public string FirstLinePattern => null;

        public IList<LanguageRule> Rules =>
            new List<LanguageRule>
            {
                new LanguageRule(
                    @"(?msi)(?:(\s*/\*.*?\*/)|(([a-z0-9#. \[\]=\"":_-]+)\s*(?:,\s*|{))+(?:(\s*/\*.*?\*/)|(?:\s*([a-z0-9 -]+\s*):\s*([a-z0-9#,<>\?%. \(\)\\\/\*\{\}:'\""!_=-]+);?))*\s*})",
                    new Dictionary<int, string>
                    {
                        {3, ScopeName.CssSelector},
                        {5, ScopeName.CssPropertyName},
                        {6, ScopeName.CssPropertyValue},
                        {4, ScopeName.Comment},
                        {1, ScopeName.Comment}
                    })
            };

        public override string ToString()
        {
            return Name;
        }
    }
}