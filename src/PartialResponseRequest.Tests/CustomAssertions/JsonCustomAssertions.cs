using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PartialResponseRequest.Tests.CustomAssertions;

public static class JsonCustomAssertionsExtensions
{
    public static JsonCustomAssertions MyShould(this JsonNode token)
    {
        return new JsonCustomAssertions(token);
    }
}


public class JsonCustomAssertions : ReferenceTypeAssertions<JsonNode, JsonCustomAssertions>
{
    protected override string Identifier => "JsonToken";

    public JsonCustomAssertions(JsonNode subject)
    {
        Subject = subject;
    }

    public AndConstraint<JsonCustomAssertions> Match(JsonNode jtoken, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => new {
                Source = JsonSerializer.Serialize(Subject, options: new JsonSerializerOptions { WriteIndented = true }),
                Target = JsonSerializer.Serialize(jtoken, options: new JsonSerializerOptions { WriteIndented = true })
            })
            .ForCondition(jsons => jsons.Source == jsons.Target)
            .FailWith("Expected \n{0} to match \n{1}.",
                given => given.Source, given => given.Target);

        return new AndConstraint<JsonCustomAssertions>(this);
    }
}
