[< back](../)
# Fields Queries

## Syntax

**Selecting fields**
`field1,field2,field3`

**Selecting nested fields**
`field1{field2,field3}`

**Field parameters**
`field1{field2(top:3)}`

**Select all fields, but only few in nested**
`*,field2{field3}`

### Usage

```csharp
class Model {
    public int Field1 { get; set; }
    public Nested Field2 { get; set; }

    class Nested {
        public int Field3 { get; set; }
    }
}

// Parsing
var parser = new QueryParser();
List<FieldToken> result = parser.Parse("field1,field2{field3}");

// Or building tokens manually with strong type!
var builder = new FieldsBuilder<Model>();
List<FieldToken> result = builder
    .Field(x => x.Field1)
    .Nested(x => x.Field2, x => x.Field3)
    .Build();

// Interpreting
var interpreter = new QueryInterpreter(result);
interpreter.Includes("field1"); // returns if field1 should be included

var nested = interpreter.Visit("field2"); // returns a nested interpreter
nested.Includes("field3"); // returns if you need to include field2.field3 

// Interpreter could be wrapped to support strong type
var interpreter2 = new QueryInterpreter<Model>(interpreter);
interpreter2.Includes(x => x.Field1);
var nested = interpreter2.Visit(x => x.Field2);
nested.Includes(x => x.Field3);

// Serialize back into string query
var serializer = new FieldsSerializer();
string query = serializer.Serialize(result); // field1,field2{field3}
```