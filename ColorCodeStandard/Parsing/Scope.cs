// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using ColorCodeStandard.Common;

namespace ColorCodeStandard.Parsing
{
    public class Scope
    {
        public IList<Scope> Children { get; set; }
        public int Index { get; set; }
        public int Length { get; set; }
        public Scope Parent { get; set; }
        public string Name { get; set; }

        public Scope(string name,
            int index,
            int length)
        {
            Guard.ArgNotNullAndNotEmpty(name, "name");

            Name = name;
            Index = index;
            Length = length;
            Children = new List<Scope>();
        }

        public void AddChild(Scope childScope)
        {
            if (childScope.Parent != null)
                throw new InvalidOperationException("The child scope already has a parent.");

            childScope.Parent = this;

            Children.Add(childScope);
        }
    }
}