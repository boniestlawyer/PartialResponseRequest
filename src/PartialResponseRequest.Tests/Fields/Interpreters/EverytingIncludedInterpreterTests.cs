using FluentAssertions;
using PartialResponseRequest.Fields.Interpreters;
using Xunit;

namespace PartialResponseRequest.Tests.Interpreters;

public class EverytingIncludedInterpreterTests
{
    readonly EverythingIncluded interpreter;

    public EverytingIncludedInterpreterTests()
    {
        interpreter = new EverythingIncluded();
    }

    [Fact]
    public void Includes_AlwaysReturnsTrue()
    {
        interpreter.Includes("any-field").Should().BeTrue();
    }

    [Fact]
    public void Visit_ReturnsAnother_EverythingIncludedInterpreter()
    {
        IFieldsQueryInterpreter result = interpreter.Visit("any-field");
        result.Should().BeOfType<EverythingIncluded>();
    }
}
