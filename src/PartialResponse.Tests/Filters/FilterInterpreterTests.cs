using FluentAssertions;
using PartialResponse.Filters.Interpreters;
using PartialResponse.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using Xunit;

namespace PartialResponse.Tests
{
    public class OrderQuantityFilter
    {
        public int Lt { get; set; }
        public int Gt { get; set; }
    }

    public class OrderFilters
    {
        public OrderQuantityFilter Quantity { get; set; }
        public OrderQuantityFilter Quantity2 { get; set; }
    }

    public class FilterInterpreterTests
    {
        [Fact]
        public void CorrectlyInterpretsFilters()
        {
            var interpreter = new FilterInterpreter<OrderFilters>(new List<FilterToken>()
            {
                new FilterToken("quantity", new List<OperatorToken>(){
                    new OperatorToken("lt", "5") })
            });

            interpreter.HasFilter(x => x.Quantity2, (op) =>
            {
                Assert.True(false, "Quantity2 should not have been interpreted as existing");
            });

            bool called = false;
            interpreter.HasFilter(x => x.Quantity, (op) =>
            {
                called = true;
                string val = "";
                Type type = null;
                op
                    .HandleOperator(x => x.Lt, (v, t) => { val = v; type = t; })
                    .HandleOperator(x => x.Gt, (v, t) =>
                    {
                        Assert.True(false, "gt should not have been interpreted as existing operator");
                    });

                op.HasOperator("lt").Should().BeTrue();
                op.HasOperator("gt").Should().BeFalse();
                op.HasOperator(x => x.Lt).Should().BeTrue();
                op.HasOperator(x => x.Gt).Should().BeFalse();

                op.GetOperator(x => x.Lt).Should().BeEquivalentTo(new OperatorValue("5", typeof(int)));
                op.GetOperator("lt").Should().BeEquivalentTo(new OperatorValue("5", typeof(int)));

                val.Should().Be("5");
                type.Should().Be(typeof(int));
            });
            called.Should().BeTrue();
        }
    }
}
