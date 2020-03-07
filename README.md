# Partial response

A simple set of libraries that enables you to parse, interpret, serialize and build simple queries for **fields** and **filters** that could be used in building flexible REST endpoints (but not limited to API):
`https://my-app.com/animals?fields=id,photoUrl,guardian{name}&filters=id(gt:5)`


## Quick start

### Response pruner

[![Nuget](https://img.shields.io/nuget/dt/PartialResponseRequest.AspNetCore.ResponsePruner)](https://www.nuget.org/packages/PartialResponseRequest.AspNetCore.ResponsePruner)

Add a dependency using the NuGet package manager (console): 
```
Install-Package PartialResponseRequest.AspNetCore.ResponsePruner
```

Use `services.AddPartialResponse()` to add a custom json output formatter, that will search for `?fields=...` query string that would be used to prune the response for the client.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddPartialResponse();
}
```
[Read more](./src/PartialResponseRequest.AspNetCore.ResponsePruner)

### Fields Queries

[![Nuget](https://img.shields.io/nuget/dt/PartialResponseRequest.Fields)](https://www.nuget.org/packages/PartialResponseRequest.Fields)

Add a dependency using the NuGet package manager (console):
```
Install-Package PartialResponseRequest.Fields
```

Allows you to parse **fields** query for processing/interpreting and reacting based on it, like building a custom optimized SQL query:
```csharp
var parser = new FieldsQueryParser();
List<FieldToken> fields = parser.Parse("id,name,photoUrl");
var interpreter = new FieldsInterpreter(fields);

// Use the interpreter to build queries
if(interpreter.Includes("id")) {
    // include id field
}
```
[Read more](./src/PartialResponseRequest.Fields)

### Filters Queries

[![Nuget](https://img.shields.io/nuget/dt/PartialResponseRequest.Filters)](https://www.nuget.org/packages/PartialResponseRequest.Filters)

Add a dependency using the NuGet package manager (console):
```
Install-Package PartialResponseRequest.Filters
```

Allows you to parse **filters** query for processing/interpreting and reacing based on it, like building custom SQL query where clauses:
```csharp
var parser = new FiltersQueryParser();
List<FilterToken> filters = parser.Parse("created(gt:2020-01-01)");
var interpreter = new FiltersInterpreter<MyFilters>(filters);

// Use the interpreter to build queries
interpreter.HasField(x => x.Created, op => {
    op.HandleOperator(o => o.Gt, (value, type) => {
        // Do something for with the operator value
    });
})
```
[Read more](./src/PartialResponseRequest.Filters)

## Documentation
View the documentation [here](./src)

## Cake build tasks
https://cakebuild.net/

**Build the project**
`build.ps1 --target=Build [--configuration=Release]`

**Run tests**
`build.ps1 --target=Test [--outputDirectory=./output]`

**Run a test coverage report**
`build.ps1 --target=Report [--testResultsDirectory=./test-results/]`

**Create nuget packages**
`build.ps1 --target=Pack [--outputDirectory=./output] [--configuration=Release] [--package-version=1.0.0.0]`
