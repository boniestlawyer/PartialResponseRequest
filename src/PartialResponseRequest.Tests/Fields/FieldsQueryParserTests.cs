using FluentAssertions;
using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Fields;
using PartialResponseRequest.Fields.TokenReaders;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PartialResponseRequest.Tests
{
    public class FieldsQueryParserTests
    {
        private readonly FieldsQueryParser root;

        public FieldsQueryParserTests()
        {
            root = new FieldsQueryParser();
        }

        private List<FieldToken> Parse(string scan)
        {
            return root.Parse(scan).ToList();
        }


        [Fact]
        public void ParsesSingleField()
        {
            string scan = "animals";
            var result = Parse(scan);

            result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>()),
            });
        }

        [Fact]
        public void ParsesSingleField_WithParameter()
        {
            var result = Parse("animals(page:1)");

            result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(){
                    new ParameterToken("page", "1")
                }, new List<FieldToken>()),
            });
        }

        [Fact]
        public void ParsesSingleField_WithNestedField()
        {
            var result = Parse("animals{name}");

            result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>(){
                    new FieldToken("name", new List<ParameterToken>(), new List<FieldToken>()),
                })
            });
        }

        [Fact]
        public void ParsesSingleField_WithNestedField_AndParameter()
        {
            var result = Parse("animals{name(page:1)}");

            result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>(){
                    new FieldToken("name", new List<ParameterToken>(){
                        new ParameterToken("page", "1")
                    }, new List<FieldToken>()),
                })
            });
        }

        [Fact]
        public void ParsesSingleField_WithMultipleNestedFields()
        {
            var result = Parse("animals{name,description}");

            result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>(){
                    new FieldToken("name", new List<ParameterToken>(), new List<FieldToken>()),
                    new FieldToken("description", new List<ParameterToken>(), new List<FieldToken>())
                })
            });
        }

        [Fact]
        public void ParsesSingleField_WithMultipleNestedFields_AndParameter()
        {
            var result = Parse("animals{name(page:1),description}");

            result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>() {
                    new FieldToken("name", new List<ParameterToken>(){
                        new ParameterToken("page", "1"),
                    }, new List<FieldToken>()),
                    new FieldToken("description", new List<ParameterToken>(), new List<FieldToken>())
                }),
            });
        }

    [Fact]
    public void ParsesSingleField_WithMultipleNestedFields_AndMultipleParameters()
    {
        var result = Parse("animals{name(page:1,itemsPerPage:10),description}");

        result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>(){
                    new FieldToken("name", new List<ParameterToken>(){
                        new ParameterToken("page", "1"),
                        new ParameterToken("itemsPerPage", "10"),
                    }, new List<FieldToken>()),
                    new FieldToken("description", new List<ParameterToken>(), new List<FieldToken>())
                })
            });
    }

    [Fact]
    public void ParsesSingleField_WithMultipleNestedFields_AndMultipleParameters_OnMultipleFields()
    {
        var result = Parse("animals{name(page:1,itemsPerPage:10),description(page:1)}");

        result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>(){
                    new FieldToken("name", new List<ParameterToken>(){
                        new ParameterToken("page", "1"),
                        new ParameterToken("itemsPerPage", "10"),
                    }, new List<FieldToken>()),
                    new FieldToken("description", new List<ParameterToken>() {
                        new ParameterToken("page", "1")
                    }, new List<FieldToken>())
                })
            });
    }

    [Fact]
    public void ParsesSingleField_WithMultipleParameters()
    {
        var result = Parse("animals(page:1,itemsPerPage:10)");

        result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(){
                    new ParameterToken("page", "1"),
                    new ParameterToken("itemsPerPage", "10"),
                }, new List<FieldToken>())
            });
    }

    [Fact]
    public void ParsesMultipleFields()
    {
        var result = Parse("animals,users");

        result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>()),

                new FieldToken("users", new List<ParameterToken>(){}, new List<FieldToken>())
            });
    }

    [Fact]
    public void ParsesMultipleFields_WithParameter()
    {
        var result = Parse("animals(page:1),users");

        result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(){
                    new ParameterToken("page", "1"),
                }, new List<FieldToken>()),

                new FieldToken("users", new List<ParameterToken>(){}, new List<FieldToken>())
            });
    }

    [Fact]
    public void ParsesMultipleFields_WithMultipleParameters()
    {
        var result = Parse("animals(page:1,itemsPerPage:10),users");

        result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(){
                    new ParameterToken("page", "1"),
                    new ParameterToken("itemsPerPage", "10"),
                }, new List<FieldToken>()),

                new FieldToken("users", new List<ParameterToken>(){}, new List<FieldToken>())
            });
    }

    [Fact]
    public void ParsesMultipleFields_WithMultipleParameters_OnMultipleFields()
    {
        var result = Parse("animals(page:1,itemsPerPage:10),users(page:1)");

        result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(){
                    new ParameterToken("page", "1"),
                    new ParameterToken("itemsPerPage", "10"),
                }, new List<FieldToken>()),

                new FieldToken("users", new List<ParameterToken>(){
                    new ParameterToken("page", "1")}, new List<FieldToken>())
            });
    }

    [Fact]
    public void ParsesField_WithMultipleNestedFields()
    {
        var result = Parse("animals{registration{date,description},identity{type,sex}}");

        result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("animals", new List<ParameterToken>(), new List<FieldToken>()
                {
                    new FieldToken("registration", new List<ParameterToken>(), new List<FieldToken>()
                    {
                        new FieldToken("date", new List<ParameterToken>(), new List<FieldToken>()),
                        new FieldToken("description", new List<ParameterToken>(), new List<FieldToken>())
                    }),
                    new FieldToken("identity", new List<ParameterToken>(), new List<FieldToken>()
                    {
                        new FieldToken("type", new List<ParameterToken>(), new List<FieldToken>()),
                        new FieldToken("sex", new List<ParameterToken>(), new List<FieldToken>())
                    })
                })
            });
    }
}
}
