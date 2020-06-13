using PartialResponseRequest.Fields;
using PartialResponseRequest.Fields.Builders;
using PartialResponseRequest.Fields.Interpreters;
using PartialResponseRequest.Fields.Serializers;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using PartialResponseRequest.Filters;
using PartialResponseRequest.Filters.Builders;
using PartialResponseRequest.Filters.Interpreters;
using PartialResponseRequest.Filters.Serializers;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponseRequest.Sandbox
{
    public interface IdFilters
    {
        int Lt { get; set; }
        int Gt { get; set; }
    }
    public interface StatusFilters
    {
        string In { get; set; }
    }

    public interface OrderFilters
    {
        IdFilters Id { get; set; }
        StatusFilters Status { get; set; }
    }


    public class FiltersUsage
    {
        public static void Run()
        {
            // Parsing
            var parser = new FiltersQueryParser();
            IEnumerable<FilterToken> result = parser.Parse("id(gt:1,lt:10),status(in:\"1,2,3\")");

            // Or building tokens manually with strong type
            var builder = new FiltersQueryBuilder<OrderFilters>();
            List<FilterToken> builderResult = builder
                .Filter(x => x.Id, x => x
                    .Operator(o => o.Gt, "1")
                    .Operator(o => o.Lt, "10"))
                .Filter(x => x.Status, x => x
                    .Operator(o => o.In, "\"1,2,3\""))
                .Build();

            // Interpreting
            var interpreter = new FiltersQueryInterpreter<OrderFilters>(result);
            if (interpreter.FiltersBy(x => x.Id, out var idOperators))
            {
                if (idOperators.HasOperator(x => x.Gt, out var gtValue))
                {
                    Console.WriteLine("Id should be after: {0}", gtValue.Value);
                }

                if (idOperators.HasOperator(x => x.Lt, out var ltValue))
                {
                    Console.WriteLine("Id should be before: {0}", ltValue.Value);
                }
            }

            if (interpreter.FiltersBy(x => x.Status, out var statusOperators))
            {
                if (statusOperators.HasOperator(x => x.In, out var inValue))
                {
                    Console.WriteLine("Status should in: {0}", inValue.Value);
                }
            }

            // Serialize back into string query
            var serializer = new FiltersQuerySerializer();
            string query = serializer.Serialize(result); // id(gt:1,lt:10),status(in:1,2,3)
        }
    }
}
