using FluentAssertions;
using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Filters;
using PartialResponseRequest.Filters.TokenReaders;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PartialResponseRequest.Tests
{
    public class FilterQueryParserTests
    {
        private readonly FiltersQueryParser root;

        public FilterQueryParserTests()
        {
            root = new FiltersQueryParser();
        }

        private List<FilterToken> Parse(string scan)
        {
            return root.Parse(scan).ToList();
        }

        [Fact]
        public void ParsesSingleFilter()
        {
            string scan = "sterile(eq:true)";
            List<FilterToken> filters = Parse(scan);

            filters.Count.Should().Be(1);
            filters[0].Field.Should().Be("sterile");
            filters[0].Operators[0].Type.Should().Be("eq");
            filters[0].Operators[0].Value.Should().Be("true");
        }

        [Fact]
        public void ParsesSingleFilterWithQuotedValue()
        {
            string scan = "name(eq:\"an, ({})'\")";
            List<FilterToken> filters = Parse(scan);

            filters.Count.Should().Be(1);
            filters[0].Field.Should().Be("name");
            filters[0].Operators[0].Type.Should().Be("eq");
            filters[0].Operators[0].Value.Should().Be("an, ({})'");
        }

        [Fact]
        public void ParsesMultipleFilterOperators()
        {
            string scan = "date(gt:2019-10-10,lt:2019-12-14)";
            List<FilterToken> filters = Parse(scan);

            filters.Count.Should().Be(1);
            filters[0].Field.Should().Be("date");
            filters[0].Operators[0].Type.Should().Be("gt");
            filters[0].Operators[0].Value.Should().Be("2019-10-10");
            filters[0].Operators[1].Type.Should().Be("lt");
            filters[0].Operators[1].Value.Should().Be("2019-12-14");
        }

        [Fact]
        public void ParsesMultipleFilters()
        {
            string scan = "date(gt:2019-10-10),id(in:\"1,2,3,4,5\")";
            List<FilterToken> filters = Parse(scan);

            filters.Count.Should().Be(2);
            filters[0].Field.Should().Be("date");
            filters[0].Operators[0].Type.Should().Be("gt");
            filters[0].Operators[0].Value.Should().Be("2019-10-10");
            filters[1].Field.Should().Be("id");
            filters[1].Operators[0].Type.Should().Be("in");
            filters[1].Operators[0].Value.Should().Be("1,2,3,4,5");
        }
    }
}
