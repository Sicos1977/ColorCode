// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ColorCodeStandard.Common;
using ColorCodeStandard.Compilation;

namespace ColorCodeStandard.Parsing
{
    public class LanguageParser : ILanguageParser
    {
        private readonly ILanguageCompiler _languageCompiler;
        private readonly ILanguageRepository _languageRepository;

        public LanguageParser(ILanguageCompiler languageCompiler,
            ILanguageRepository languageRepository)
        {
            _languageCompiler = languageCompiler;
            _languageRepository = languageRepository;
        }

        public void Parse(string sourceCode,
            ILanguage language,
            Action<string, IList<Scope>> parseHandler)
        {
            if (string.IsNullOrEmpty(sourceCode))
                return;

            var compiledLanguage = _languageCompiler.Compile(language);

            Parse(sourceCode, compiledLanguage, parseHandler);
        }

        private void Parse(string sourceCode,
            CompiledLanguage compiledLanguage,
            Action<string, IList<Scope>> parseHandler)
        {
            var regexMatch = compiledLanguage.Regex.Match(sourceCode);

            if (!regexMatch.Success)
            {
                parseHandler(sourceCode, new List<Scope>());
            }
            else
            {
                var currentIndex = 0;

                while (regexMatch.Success)
                {
                    var sourceCodeBeforeMatch = sourceCode.Substring(currentIndex, regexMatch.Index - currentIndex);
                    if (!string.IsNullOrEmpty(sourceCodeBeforeMatch))
                        parseHandler(sourceCodeBeforeMatch, new List<Scope>());

                    var matchedSourceCode = sourceCode.Substring(regexMatch.Index, regexMatch.Length);
                    if (!string.IsNullOrEmpty(matchedSourceCode))
                    {
                        var capturedStylesForMatchedFragment =
                            GetCapturedStyles(regexMatch, regexMatch.Index, compiledLanguage);
                        var capturedStyleTree = CreateCapturedStyleTree(capturedStylesForMatchedFragment);
                        parseHandler(matchedSourceCode, capturedStyleTree);
                    }

                    currentIndex = regexMatch.Index + regexMatch.Length;
                    regexMatch = regexMatch.NextMatch();
                }

                var sourceCodeAfterAllMatches = sourceCode.Substring(currentIndex);
                if (!string.IsNullOrEmpty(sourceCodeAfterAllMatches))
                    parseHandler(sourceCodeAfterAllMatches, new List<Scope>());
            }
        }

        private static List<Scope> CreateCapturedStyleTree(IList<Scope> capturedStyles)
        {
            capturedStyles.SortStable((x, y) => x.Index.CompareTo(y.Index));

            var capturedStyleTree = new List<Scope>(capturedStyles.Count);
            Scope currentScope = null;

            foreach (var capturedStyle in capturedStyles)
            {
                if (currentScope == null)
                {
                    capturedStyleTree.Add(capturedStyle);
                    currentScope = capturedStyle;
                    continue;
                }

                AddScopeToNestedScopes(capturedStyle, ref currentScope, capturedStyleTree);
            }

            return capturedStyleTree;
        }

        private static void AddScopeToNestedScopes(Scope scope,
            ref Scope currentScope,
            ICollection<Scope> capturedStyleTree)
        {
            if (scope.Index >= currentScope.Index &&
                scope.Index + scope.Length <= currentScope.Index + currentScope.Length)
            {
                currentScope.AddChild(scope);
                currentScope = scope;
            }
            else
            {
                currentScope = currentScope.Parent;

                if (currentScope != null)
                    AddScopeToNestedScopes(scope, ref currentScope, capturedStyleTree);
                else
                    capturedStyleTree.Add(scope);
            }
        }


        private List<Scope> GetCapturedStyles(Match regexMatch,
            int currentIndex,
            CompiledLanguage compiledLanguage)
        {
            var capturedStyles = new List<Scope>();

            for (var i = 0; i < regexMatch.Groups.Count; i++)
            {
                var regexGroup = regexMatch.Groups[i];
                var styleName = compiledLanguage.Captures[i];

                if (regexGroup.Length == 0 || string.IsNullOrEmpty(styleName))
                    continue;
                foreach (Capture regexCapture in regexGroup.Captures)
                    AppendCapturedStylesForRegexCapture(regexCapture, currentIndex, styleName, capturedStyles);
            }

            return capturedStyles;
        }

        private void AppendCapturedStylesForRegexCapture(Capture regexCapture,
            int currentIndex,
            string styleName,
            ICollection<Scope> capturedStyles)
        {
            if (styleName.StartsWith(ScopeName.LanguagePrefix))
            {
                var nestedGrammarId = styleName.Substring(1);
                AppendCapturedStylesForNestedLanguage(regexCapture, regexCapture.Index - currentIndex, nestedGrammarId,
                    capturedStyles);
            }
            else
            {
                capturedStyles.Add(new Scope(styleName, regexCapture.Index - currentIndex, regexCapture.Length));
            }
        }

        private void AppendCapturedStylesForNestedLanguage(Capture regexCapture,
            int offset,
            string nestedLanguageId,
            ICollection<Scope> capturedStyles)
        {
            var nestedLanguage = _languageRepository.FindById(nestedLanguageId);

            if (nestedLanguage == null)
                throw new InvalidOperationException("The nested language was not found in the language repository.");

            var nestedCompiledLanguage = _languageCompiler.Compile(nestedLanguage);

            var regexMatch = nestedCompiledLanguage.Regex.Match(regexCapture.Value, 0, regexCapture.Value.Length);

            if (!regexMatch.Success)
                return;
            while (regexMatch.Success)
            {
                var capturedStylesForMatchedFragment = GetCapturedStyles(regexMatch, 0, nestedCompiledLanguage);
                var capturedStyleTree = CreateCapturedStyleTree(capturedStylesForMatchedFragment);

                foreach (var nestedCapturedStyle in capturedStyleTree)
                {
                    IncreaseCapturedStyleIndicies(capturedStyleTree, offset);
                    capturedStyles.Add(nestedCapturedStyle);
                }

                regexMatch = regexMatch.NextMatch();
            }
        }

        private static void IncreaseCapturedStyleIndicies(IList<Scope> capturedStyles,
            int amountToIncrease)
        {
            for (var i = 0; i < capturedStyles.Count; i++)
            {
                var scope = capturedStyles[i];

                scope.Index += amountToIncrease;

                if (scope.Children.Count > 0)
                    IncreaseCapturedStyleIndicies(scope.Children, amountToIncrease);
            }
        }
    }
}