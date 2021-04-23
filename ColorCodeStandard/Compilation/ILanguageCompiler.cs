// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace ColorCodeStandard.Compilation
{
    public interface ILanguageCompiler
    {
        CompiledLanguage Compile(ILanguage language);
    }
}