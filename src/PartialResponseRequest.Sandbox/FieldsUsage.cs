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
    public class Model
    {
        public int Field1 { get; set; }
        public Nested Field2 { get; set; }

        public class Nested
        {
            public int Field3 { get; set; }
        }
    }

    public class FieldsUsage
    {
		public static void Run()
        {
            // Parsing
            var parser = new FieldsQueryParser();
            IEnumerable<FieldToken> result = parser.Parse("field1,field2{field3}");

            // Or building tokens manually with strong type!
            var builder = new FieldsQueryBuilder<Model>();
            IEnumerable<FieldToken> builderResult = builder
                .Field(x => x.Field1)
                .Nested(x => x.Field2, n => n.Field(f => f.Field3))
                .Build();

            // Interpreting
            var interpreter = new FieldsQueryInterpreter(result);
            interpreter.Includes("field1"); // returns if field1 should be included

            var nested = interpreter.Visit("field2"); // returns a nested interpreter
            nested.Includes("field3"); // returns if you need to include field2.field3 

            // Interpreter could be wrapped to support strong type
            var strongTypeInterpreter = new FieldsQueryInterpreter<Model>(interpreter);
            strongTypeInterpreter.Includes(x => x.Field1);

            var strongTypeNested = strongTypeInterpreter.Visit(x => x.Field2);
            strongTypeNested.Includes(x => x.Field3);

            // Serialize back into string query
            var serializer = new FieldsQuerySerializer();
            string query = serializer.Serialize(result); // field1,field2{field3}
        }
    }
}
