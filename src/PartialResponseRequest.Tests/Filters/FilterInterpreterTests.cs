using FluentAssertions;
using PartialResponseRequest.Filters.Interpreters;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using Xunit;

namespace PartialResponseRequest.Tests
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
            var interpreter = new FiltersQueryInterpreter<OrderFilters>(new List<FilterToken>()
            {
                new FilterToken("quantity", new List<OperatorToken>(){
                    new OperatorToken("lt", "5") })
            });

            interpreter.FiltersBy(x => x.Quantity).Should().BeTrue();
            interpreter.FiltersBy(x => x.Quantity2).Should().BeFalse();
            interpreter.GetFilter(x => x.Quantity2).HasOperator("lt").Should().BeFalse();

            var qOperators = interpreter.GetFilter(x => x.Quantity);
            qOperators.HasOperator(x => x.Lt).Should().BeTrue();
            qOperators.HasOperator(x => x.Gt).Should().BeFalse();

            qOperators.GetValue(x => x.Lt).Should().BeEquivalentTo(new OperatorValue("5", typeof(int)));
        }
    }
}
