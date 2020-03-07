using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartialResponse.Tests.CustomAssertions
{
    public static class JsonCustomAssertionsExtensions
    {
        public static JsonCustomAssertions MyShould(this JToken token)
        {
            return new JsonCustomAssertions(token);
        }
    }


    public class JsonCustomAssertions : ReferenceTypeAssertions<JToken, JsonCustomAssertions>
    {
        protected override string Identifier => "JsonToken";

        public JsonCustomAssertions(JToken subject)
        {
            Subject = subject;
        }

        public AndConstraint<JsonCustomAssertions> Match(JToken jtoken, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => new {
                    Source = JsonConvert.SerializeObject(Subject, Formatting.Indented),
                    Target = JsonConvert.SerializeObject(jtoken, Formatting.Indented)
                })
                .ForCondition(jsons => jsons.Source == jsons.Target)
                .FailWith("Expected \n{0} to match \n{1}.",
                    given => given.Source, given => given.Target);

            return new AndConstraint<JsonCustomAssertions>(this);
        }
    }
}
