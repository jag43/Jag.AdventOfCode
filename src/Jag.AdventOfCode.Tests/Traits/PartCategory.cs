using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Jag.AdventOfCode.Tests.Traits
{
    [TraitDiscoverer("Jag.AdventOfCode.Tests.Traits.PartDiscoverer", "Jag.AdventOfCode.Tests")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PartAttribute : Attribute, ITraitAttribute
    {
        public PartAttribute(int part)
        {
            Part = part;
        }

        public int Part { get; }
    }

    public class PartDiscoverer : ITraitDiscoverer
    {
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var part = traitAttribute.GetNamedArgument<int>("Part");
            yield return new KeyValuePair<string, string>("Category", $"Part{part}");
        }
    }
}