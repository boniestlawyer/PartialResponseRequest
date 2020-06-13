using FluentAssertions;
using PartialResponseRequest.Filters.Builders;
using PartialResponseRequest.Filters.Interpreters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PartialResponseRequest.Tests.Filters
{
    public interface IdFilters2
    { 
        int Eq { get; }
        int Neq { get; }
    }

    public class IOperatorsInterpreterExtensionsTests
    {
        [Fact]
        public void HasFilterWithOutParam_ReturnsAValueOrNull()
        {
            var interpreter = new OperatorsInterpreter<IdFilters2>(new OperatorsBuilder<IdFilters2>().Operator(x => x.Eq, "3").Build());


            var eqOps = interpreter.GetValue(x => x.Eq);

            interpreter.HasOperator(x => x.Eq, out var eqOpsOut);
            interpreter.HasOperator(x => x.Neq, out var neq2OpsOut);

            eqOps.Value.Should().Be(eqOpsOut.Value);

            neq2OpsOut.Should().BeNull();
        }
    }
}
