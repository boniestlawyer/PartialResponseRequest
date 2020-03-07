using FluentAssertions;
using PartialResponse.Filters.Serializers;
using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using Xunit;

namespace PartialResponse.Tests
{
    public class FilterSerializerTests
    {
        [Fact]
        public void SerializesFilterTokensCorrectly()
        {
            var serializer = new FiltersSerializer();

            var result = serializer.Serialize(new List<FilterToken>()
            {
                new FilterToken("sum", new List<OperatorToken>()
                {
                    new OperatorToken("gt", "5"),
                    new OperatorToken("lt", "10")
                })
            });

            result.Should().Be("sum(gt:5,lt:10)");
        }
    }
}
