using FluentAssertions;
using PartialResponse.Fields.Builders;
using PartialResponse.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using Xunit;

namespace PartialResponse.Tests
{
    public class OrderItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }

    }

    public class Order
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public OrderItem PriorityItem { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class FieldsQueryBuilderTests
    {
        [Fact]
        public void CorrectlyBuildsFieldsQuery()
        {
            var builder = new FieldsQueryBuilder<Order>();
            builder
                .Everything()
                .Field(x => x.Name)
                .Field(x => x.Description)
                .Nested(x => x.PriorityItem, item => item.Everything())
                .NestedList(x => x.Items, items => items
                    .Everything()
                    .Field(item => item.Quantity, new Dictionary<string, string>()
                    {
                        { "discounted", "true" },
                        { "param2", "3" }
                    })
                    .Field(item => item.ItemId));

            var result = builder.Build();

            result.Should().BeEquivalentTo(new List<FieldToken>()
            {
                new FieldToken("*", new List<ParameterToken>(), new List<FieldToken>()),
                new FieldToken("name", new List<ParameterToken>(), new List<FieldToken>()),
                new FieldToken("description", new List<ParameterToken>(), new List<FieldToken>()),
                new FieldToken("priorityItem", new List<ParameterToken>(), new List<FieldToken>(){
                    new FieldToken("*", new List<ParameterToken>(), new List<FieldToken>())
                }),
                new FieldToken("items", new List<ParameterToken>(), new List<FieldToken>(){
                    new FieldToken("*", new List<ParameterToken>(), new List<FieldToken>()),
                    new FieldToken("quantity", new List<ParameterToken>(){
                        new ParameterToken("discounted", "true"),
                        new ParameterToken("param2", "3")
                    }, new List<FieldToken>()),
                    new FieldToken("itemId", new List<ParameterToken>(), new List<FieldToken>())
                }),
            });
        }
    }
}
