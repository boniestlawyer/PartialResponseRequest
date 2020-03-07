using FluentAssertions;
using PartialResponseRequest.Fields.Interpreters;
using Xunit;

namespace PartialResponseRequest.Tests.Interpreters
{
    public class NothingIncludedInterpreterTests
    {
        readonly NothingIncluded interpreter;

        public NothingIncludedInterpreterTests()
        {
            interpreter = new NothingIncluded();
        }

        [Fact]
        public void Includes_AlwaysReturnsFalse()
        {
            interpreter.Includes("any-field").Should().BeFalse();
        }

        [Fact]
        public void Visit_ReturnsAnother_NothingIncludedInterpreter()
        {
            IFieldsQueryInterpreter result = interpreter.Visit("any-field");
            result.Should().BeOfType<NothingIncluded>();
        }
    }
}
