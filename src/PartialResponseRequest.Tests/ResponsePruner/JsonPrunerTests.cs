using Newtonsoft.Json.Linq;
using PartialResponseRequest.AspNetCore.ResponsePruner.Pruners;
using PartialResponseRequest.Fields.Interpreters;
using PartialResponseRequest.Tests.CustomAssertions;
using PartialResponseRequest.Tests.Interpreters;
using PartialResponseRequest.Tests.ResponsePruner.Utils;
using System.Collections.Generic;
using Xunit;

namespace PartialResponseRequest.Tests.ResponsePruner
{

    public class JsonPrunerTests
    {
        private readonly JsonPruner pruner;

        public JsonPrunerTests()
        {
            pruner = new JsonPruner();
        }

        [Fact]
        public void DoesntDoAnything_IfEverythingIncludedInterpreterWasGiven()
        {
            var token = new JObject();
            token["circular"] = token;

            // If pruner would try to prune, it would get into an infinite loop due to circular reference
            pruner.Prune(token, new EverythingIncluded());
        }

        [Fact]
        public void PrunesAllNotIncludedFields()
        {
            var model = new FakeModel()
            {
                DontInclude = 999,
                Include = 1,
                Models = new List<FakeModel>()
                {
                    new FakeModel()
                    {
                        DontInclude = 1000,
                        Include = 2
                    }
                },
                Nested = new FakeModel()
                {
                    DontInclude = 1001,
                    Include = 3
                }
            };

            var jtokens = JToken.FromObject(model);
            pruner.Prune(jtokens, new IncludeListedPropertiesInterpreter(new[] { "Include", "Models", "Nested" }));

            jtokens.MyShould().Match(
                new JObject()
                {
                    { "Include", 1 },
                    { "Models", new JArray(){
                        new JObject()  {
                            { "Include", 2 },
                            { "Models", null },
                            { "Nested", null }
                        }
                        }
                    },
                    { "Nested", new JObject()
                        {
                            { "Include", 3 },
                            { "Models", null },
                            { "Nested", null }
                        }
                    }
                }
            );
        }
    }
}
