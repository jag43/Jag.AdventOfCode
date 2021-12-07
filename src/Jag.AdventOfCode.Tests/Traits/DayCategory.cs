using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Jag.AdventOfCode.Tests.Traits
{
    [TraitDiscoverer("Jag.AdventOfCode.Tests.Traits.DayDiscoverer", "Jag.AdventOfCode.Tests")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class DayAttribute : Attribute, ITraitAttribute
    {
        public DayAttribute(int day)
        {
            Day = day;
        }

        public int Day { get; }
    }

    public class DayDiscoverer : ITraitDiscoverer
    {
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var day = traitAttribute.GetNamedArgument<int>("Day");
            yield return new KeyValuePair<string, string>("Category", $"D{day}");
        }
    }
}