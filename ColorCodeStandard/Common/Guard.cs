﻿// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;

namespace ColorCodeStandard.Common
{
    internal static class Guard
    {
        public static void ArgNotNull(object arg, string paramName)
        {
            if (arg == null)
                throw new ArgumentNullException(paramName);
        }

        public static void ArgNotNullAndNotEmpty(string arg, string paramName)
        {
            if (arg == null)
                throw new ArgumentNullException(paramName);

            if (string.IsNullOrEmpty(arg))
                throw new ArgumentException($"The {paramName} argument value must not be empty.",
                    paramName);
        }

        public static void EnsureParameterIsNotNullAndNotEmpty<TKey, TValue>(IDictionary<TKey, TValue> parameter,
            string parameterName)
        {
            if (parameter == null || parameter.Count == 0)
                throw new ArgumentNullException(parameterName);
        }

        public static void ArgNotNullAndNotEmpty<T>(IList<T> arg, string paramName)
        {
            if (arg == null)
                throw new ArgumentNullException(paramName);

            if (arg.Count == 0)
                throw new ArgumentException($"The {paramName} argument value must not be empty.",
                    paramName);
        }
    }
}