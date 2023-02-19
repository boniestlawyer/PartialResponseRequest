using FluentAssertions;
using PartialResponseRequest.Fields.Serializers;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using Xunit;

namespace PartialResponseRequest.Tests;

public class FieldsSerializerTests
{
    [Fact]
    public void SerializesFieldsTokensCorrectly()
    {
        var serializer = new FieldsQuerySerializer();

        var result = serializer.Serialize(new List<FieldToken>()
        {
            new FieldToken("*", new List<ParameterToken>(), new List<FieldToken>()),
            new FieldToken("name", new List<ParameterToken>(), new List<FieldToken>()),
            new FieldToken("description", new List<ParameterToken>(), new List<FieldToken>()),
            new FieldToken("items", new List<ParameterToken>(), new List<FieldToken>(){
                new FieldToken("*", new List<ParameterToken>(), new List<FieldToken>()),
                new FieldToken("quantity", new List<ParameterToken>(){
                    new ParameterToken("discounted", "true"),
                    new ParameterToken("param2", "3")
                }, new List<FieldToken>()),
                new FieldToken("itemId", new List<ParameterToken>(), new List<FieldToken>())
            }),
        });

        result.Should().Be("*,name,description,items{*,quantity(discounted:true,param2:3),itemId}");
    }
}
