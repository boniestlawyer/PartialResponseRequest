using FluentAssertions;
using PartialResponseRequest.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace PartialResponseRequest.Tests.Core;

public class TestModel
{
    public int Property { get; set; }
    public int Method() { return 1; }
}

public class UtilsTests
{
    [Fact]
    public void ToPascalCase()
    {
        Utils.ToPascalCase("SomethingNotInPascal").Should().Be("somethingNotInPascal");
    }
    [Fact]
    public void ToPascalCase_EmptyString()
    {
        Utils.ToPascalCase("").Should().Be("");
    }

    [Fact]
    public void GetMemberName_ReturnsForProperty()
    {
        Expression<Func<TestModel, int>>  expression = x => x.Property;
        Utils.GetMemberName(expression).Should().Be("Property");
    }

    [Fact]
    public void GetMemberName_ThrowsForNull()
    {
        new Action(() => Utils.GetMemberName(null)).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetMemberName_ThrowsForMethods()
    {
        Expression<Func<TestModel, int>> expression = x => x.Method();
        new Action(() => Utils.GetMemberName(expression)).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetMemberName_ThrowsForNotLambda()
    {
        Expression expression = Expression.Constant(3);
        new Action(() => Utils.GetMemberName(expression)).Should().Throw<ArgumentException>();
    }
}
