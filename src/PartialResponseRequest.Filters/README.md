[< back](../README.md)
# Filters Queries

## Syntax
`id(gt:1,lt:10),status(in:1,2,3)`

### Usage

```csharp
public class IdFilters
{
    public int Lt { get; set; }
    public int Gt { get; set; }
}
public class StatusFilters
{
    public string In { get; set; }
}

public class OrderFilters
{
    public IdFilters Id { get; set; }
    public StatusFilters Status { get; set; }
}

// Parsing
var parser = new FilterQueryParser();
List<FilterToken> result = parser.Parse("id(gt:1,lt:10),status(in:1,2,3)");

// Or building tokens manually with strong type
var builder = new FilterBuilder<OrderFilters>();
List<FilterToken> result = builder
    .Filter(x => x.Id, x => x
        .Operator(o => o.Gt, "1")
        .Operator(o => o.Lt, "10"))
    .Filter(x => x.Status, x => x
        .Operator(o => o.In, "1,2,3"))
    .Build();

// Interpreting
var interpreter = new FilterInterpreter<OrderFilters>(result);
interpreter.HasFilter(x => x.Id, op => {
    // Action format of handling an operator
    op.HandleOperator(o => o.Gt, (value, type) => {
        // Do something for id filter and it's Gt filter
        // value = 1, type = int
    });

    // Or check and get an operator value
    if(op.HasOperator(o => o.Gt)) {
        OperatorValue operator = op.GetOperator(o => o.Gt);
    }
});

// Serialize back into string query
var serializer = new FiltersSerializer();
string query = serializer.Serialize(result); // id(gt:1,lt:10),status(in:1,2,3)
```