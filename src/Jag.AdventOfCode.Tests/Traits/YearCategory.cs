using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Jag.AdventOfCode.Tests.Traits
{
    [TraitDiscoverer("Jag.AdventOfCode.Tests.Traits.YearDiscoverer", "Jag.AdventOfCode.Tests")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class YearAttribute : Attribute, ITraitAttribute
    {
        public YearAttribute(int year)
        {
            Year = year;
        }

        public int Year { get; }
    }

    public class YearDiscoverer : ITraitDiscoverer
    {
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var year = traitAttribute.GetNamedArgument<int>("Year");
            yield return new KeyValuePair<string, string>("Category", $"Y{year}");
        }
    }
}