// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;

namespace ColorCode.Common
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly Dictionary<string, ILanguage> _loadedLanguages;
        private readonly ReaderWriterLockSlim _loadLock;

        public LanguageRepository(Dictionary<string, ILanguage> loadedLanguages)
        {
            _loadedLanguages = loadedLanguages;
            _loadLock = new ReaderWriterLockSlim();
        }

        public IEnumerable<ILanguage> All => _loadedLanguages.Values;

        public ILanguage FindById(string languageId)
        {
            Guard.ArgNotNullAndNotEmpty(languageId, "languageId");

            ILanguage language = null;

            _loadLock.EnterReadLock();

            try
            {
                if (_loadedLanguages.ContainsKey(languageId))
                    language = _loadedLanguages[languageId];
            }
            finally
            {
                _loadLock.ExitReadLock();
            }

            return language;
        }

        public void Load(ILanguage language)
        {
            Guard.ArgNotNull(language, "language");

            if (string.IsNullOrEmpty(language.Id))
                throw new ArgumentException("The language identifier must not be null or empty.", "language");

            _loadLock.EnterWriteLock();

            try
            {
                _loadedLanguages[language.Id] = language;
            }
            finally
            {
                _loadLock.ExitWriteLock();
            }
        }
    }
}