﻿using PartialResponseRequest.AspNetCore.ResponsePruner.Pruners;
using PartialResponseRequest.Tests.CustomAssertions;
using PartialResponseRequest.Tests.ResponsePruner.Utils;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xunit;

namespace PartialResponseRequest.Tests.ResponsePruner;

public class WrapperAwareJsonPrunerTests
{
    private readonly WrapperAwareJsonPruner pruner;

    public WrapperAwareJsonPrunerTests()
    {
        pruner = new WrapperAwareJsonPruner("Data");
    }

    [Fact]
    public void PrunesAllNotIncludedFieldsByIgnoringAWrapper()
    {
        var model = new Wrapper<FakeModel>()
        {
            Data = new FakeModel()
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
            }
        };

        var jtokens = System.Text.Json.JsonSerializer.SerializeToNode(model)!;
        pruner.Prune(jtokens, new IncludeListedPropertiesInterpreter(new[] { "Include", "Models", "Nested" }));

        jtokens.MyShould().Match(
            new JsonObject()
            {
                { "Data", new JsonObject()
                    {
                        { "Include", 1 },
                        { "Models", new JsonArray(){
                            new JsonObject()  {
                                { "Include", 2 },
                                { "Models", null },
                                { "Nested", null }
                            }
                            }
                        },
                        { "Nested", new JsonObject()
                            {
                                { "Include", 3 },
                                { "Models", null },
                                { "Nested", null }
                            }
                        }
                    }
                }
            }
        );
    }
}
