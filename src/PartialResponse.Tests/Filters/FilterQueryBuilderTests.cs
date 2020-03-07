using FluentAssertions;
using PartialResponse.Filters.Builders;
using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using Xunit;

namespace PartialResponse.Tests
{
    public class SumFilter
    {
        public int Lt { get; set; }
        public int Lte { get; set; }
        public int Gt { get; set; }
        public int Gte { get; set; }
    }

    public class OrderFilter
    {
        public SumFilter Sum { get; set; }
    }

    public class FilterQueryBuilderTests
    {
        [Fact]
        public void ShouldGenerateFilterQueryCorrectly()
        {
            var builder = new FilterQueryBuilder<OrderFilter>();

            var result = builder.Filter(x => x.Sum, x => x
                .Operator(o => o.Gt, "5")
                .Operator(o => o.Lt, "10")).Build();

            result.Should().BeEquivalentTo(new List<FilterToken>()
            {
                new FilterToken("sum", new List<OperatorToken>()
                {
                    new OperatorToken("gt", "5"),
                    new OperatorToken("lt", "10")
                })
            });
        }

        [Fact]
        public void ShouldGenerateFilterQueryCorrectly_WhenOperatorsNotBuilt()
        {
            var builder = new FilterQueryBuilder<OrderFilter>();

            var result = builder.Filter(x => x.Sum).Build();

            result.Should().BeEquivalentTo(new List<FilterToken>()
            {
                new FilterToken("sum", new List<OperatorToken>())
            });
        }
    }
}
