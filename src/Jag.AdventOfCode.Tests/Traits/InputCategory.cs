using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Jag.AdventOfCode.Tests.Traits
{
    [TraitDiscoverer("Jag.AdventOfCode.Tests.Traits.InputDiscoverer", "Jag.AdventOfCode.Tests")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class InputAttribute : Attribute, ITraitAttribute
    {
        public InputAttribute(bool test = false)
        {
            Test = test;
        }

        public bool Test { get; }
    }
    
    public class InputDiscoverer : ITraitDiscoverer
    {
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var test = traitAttribute.GetNamedArgument<bool>("test")
                ? "Test"
                :"";
            yield return new KeyValuePair<string, string>("Category", $"Input{test}");
        }
    }
}