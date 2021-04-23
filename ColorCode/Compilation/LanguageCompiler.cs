// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ColorCode.Common;

namespace ColorCode.Compilation
{
    public class LanguageCompiler : ILanguageCompiler
    {
        private static readonly Regex numberOfCapturesRegex = new Regex(@"(?x)(?<!\\)\((?!\?)", RegexOptions.Compiled);
        private readonly Dictionary<string, CompiledLanguage> _compiledLanguages;
        private readonly ReaderWriterLockSlim _compileLock;

        public LanguageCompiler(Dictionary<string, CompiledLanguage> compiledLanguages)
        {
            this._compiledLanguages = compiledLanguages;

            _compileLock = new ReaderWriterLockSlim();
        }

        public CompiledLanguage Compile(ILanguage language)
        {
            Guard.ArgNotNull(language, "language");

            if (string.IsNullOrEmpty(language.Id))
                throw new ArgumentException("The language identifier must not be null.", nameof(language));

            CompiledLanguage compiledLanguage;

            _compileLock.EnterReadLock();
            try
            {
                // for performance reasons we should first try with
                // only a read lock since the majority of the time
                // it'll be created already and upgradeable lock blocks
                if (_compiledLanguages.ContainsKey(language.Id))
                    return _compiledLanguages[language.Id];
            }
            finally
            {
                _compileLock.ExitReadLock();
            }

            _compileLock.EnterUpgradeableReadLock();
            try
            {
                if (_compiledLanguages.ContainsKey(language.Id))
                {
                    compiledLanguage = _compiledLanguages[language.Id];
                }
                else
                {
                    _compileLock.EnterWriteLock();

                    try
                    {
                        if (string.IsNullOrEmpty(language.Name))
                            throw new ArgumentException("The language name must not be null or empty.", nameof(language));

                        if (language.Rules == null || language.Rules.Count == 0)
                            throw new ArgumentException("The language rules collection must not be empty.", nameof(language));

                        compiledLanguage = CompileLanguage(language);

                        _compiledLanguages.Add(compiledLanguage.Id, compiledLanguage);
                    }
                    finally
                    {
                        _compileLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _compileLock.ExitUpgradeableReadLock();
            }

            return compiledLanguage;
        }

        private static CompiledLanguage CompileLanguage(ILanguage language)
        {
            var id = language.Id;
            var name = language.Name;
            Regex regex;
            IList<string> captures;

            CompileRules(language.Rules, out regex, out captures);

            return new CompiledLanguage(id, name, regex, captures);
        }

        private static void CompileRules(IList<LanguageRule> rules,
            out Regex regex,
            out IList<string> captures)
        {
            var regexBuilder = new StringBuilder();
            captures = new List<string>();

            regexBuilder.AppendLine("(?x)");
            captures.Add(null);

            CompileRule(rules[0], regexBuilder, captures, true);

            for (var i = 1; i < rules.Count; i++)
                CompileRule(rules[i], regexBuilder, captures, false);

            regex = new Regex(regexBuilder.ToString());
        }


        private static void CompileRule(LanguageRule languageRule,
            StringBuilder regex,
            ICollection<string> captures,
            bool isFirstRule)
        {
            if (!isFirstRule)
            {
                regex.AppendLine();
                regex.AppendLine();
                regex.AppendLine("|");
                regex.AppendLine();
            }

            regex.AppendFormat("(?-xis)(?m)({0})(?x)", languageRule.Regex);

            var numberOfCaptures = GetNumberOfCaptures(languageRule.Regex);

            for (var i = 0; i <= numberOfCaptures; i++)
            {
                string scope = null;

                foreach (var captureIndex in languageRule.Captures.Keys)
                    if (i == captureIndex)
                    {
                        scope = languageRule.Captures[captureIndex];
                        break;
                    }

                captures.Add(scope);
            }
        }

        private static int GetNumberOfCaptures(string regex)
        {
            return numberOfCapturesRegex.Matches(regex).Count;
        }
    }
}