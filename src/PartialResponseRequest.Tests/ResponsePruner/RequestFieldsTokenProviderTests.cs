using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using PartialResponseRequest.AspNetCore.ResponsePruner.RequestTokenProviders;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PartialResponseRequest.Tests.ResponsePruner;

public class RequestFieldsTokenProviderTests
{
    [Fact]
    public void ParsesFieldsQueryString()
    {
        var accessorMock = MockHttpAccessor("?fields=id");
        var provider = new RequestFieldsTokensProvider(accessorMock.Object);
        provider.Provide().Should().BeEquivalentTo(new List<FieldToken>()
        {
            new FieldToken("id", new List<ParameterToken>(), new List<FieldToken>())
        });
    }

    [Fact]
    public void ReturnsEmptyListForEmptyFieldsQuery()
    {
        var accessorMock = MockHttpAccessor("");
        var provider = new RequestFieldsTokensProvider(accessorMock.Object);
        provider.Provide().Should().BeEquivalentTo(new List<FieldToken>());
    }

    [Fact]
    public void ReturnsFromCacheFirst()
    {
        var cache = new List<FieldToken>()
        {
            new FieldToken("cached", new List<ParameterToken>(), new List<FieldToken>())
        };

        var accessorMock = MockHttpAccessor("?fields=id,randomField", cache);
        var provider = new RequestFieldsTokensProvider(accessorMock.Object);
        provider.Provide().Should().BeSameAs(cache);
    }

    [Fact]
    public void SetsCache()
    {
        var accessorMock = MockHttpAccessor("?fields=id,randomField");
        var provider = new RequestFieldsTokensProvider(accessorMock.Object);
        provider.Provide().Should().BeSameAs(accessorMock.Object.HttpContext.Items["field-tokens"] as List<FieldToken>);
    }

    private Mock<IHttpContextAccessor> MockHttpAccessor(string queryString, List<FieldToken>? fieldsCache = null)
    {

        var accessorMock = new Mock<IHttpContextAccessor>();
        var context = new DefaultHttpContext();

        if(fieldsCache != null)
        {
            context.Items["field-tokens"] = fieldsCache;
        }

        context.Request.QueryString = new QueryString(queryString);

        accessorMock.SetupGet(x => x.HttpContext).Returns(context);

        return accessorMock;
    }
}
