[![Build Status](https://dev.azure.com/benasradzevicius9404/PartialResponseRequest/_apis/build/status/Pack?branchName=master)](https://dev.azure.com/benasradzevicius9404/Partial%20Response%20Request/_build/latest?definitionId=8&branchName=master)
![Azure DevOps tests with custom labels](https://img.shields.io/azure-devops/tests/benasradzevicius9404/partialresponserequest/8?label=Tests)
![Coverage](https://img.shields.io/azure-devops/coverage/benasradzevicius9404/PartialResponseRequest/8/master?label=Coverage)
[![Nuget](https://img.shields.io/nuget/dt/PartialResponseRequest.AspNetCore.ResponsePruner)](https://www.nuget.org/packages/PartialResponseRequest.Core)
![Nuget](https://img.shields.io/nuget/v/PartialResponseRequest.Core)
![Nuget](https://img.shields.io/nuget/vpre/PartialResponseRequest.Core?label=nuget%20prerelease)

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
    services.AddHttpContextAccessor();
    services.AddPartialResponse();
}
```
[Read more](https://benasradzevicius9404.github.io/PartialResponseRequest/response-pruner.html)

### Fields Queries

[![Nuget](https://img.shields.io/nuget/dt/PartialResponseRequest.Fields)](https://www.nuget.org/packages/PartialResponseRequest.Fields)

Add a dependency using the NuGet package manager (console):
```
Install-Package PartialResponseRequest.Fields
```

Allows you to parse **fields** query for processing/interpreting and reacting based on it, like building a custom optimized SQL query:
```csharp
var parser = new FieldsQueryParser();
IEnumerable<FieldToken> fields = parser.Parse("id,name,photoUrl");
var interpreter = new FieldsQueryInterpreter(fields);

// Use the interpreter to build queries
if (interpreter.Includes("id"))
{
    // include id field
}
```
[Read more](https://benasradzevicius9404.github.io/PartialResponseRequest/fields.html)

### Filters Queries

[![Nuget](https://img.shields.io/nuget/dt/PartialResponseRequest.Filters)](https://www.nuget.org/packages/PartialResponseRequest.Filters)

Add a dependency using the NuGet package manager (console):
```
Install-Package PartialResponseRequest.Filters
```

Allows you to parse **filters** query for processing/interpreting and reacing based on it, like building custom SQL query where clauses:
```csharp
var parser = new FilterQueryParser();
IEnumerable<FilterToken> filters = parser.Parse("created(gt:2020-01-01)");
var interpreter = new FiltersQueriesInterpreter<MyFilters>(filters.ToList());

if (interpreter.FiltersBy(x => x.Created, out IOperatorsInterpreter<CreatedFilters> createdFilter))
{
    if (createdFilter.HasOperator(x => x.Gt, out OperatorValue value))
    {
        var parsedValue = DateTime.Parse(value.Value);
        Console.WriteLine(parsedValue.ToString());
    }
}
```
[Read more](https://benasradzevicius9404.github.io/PartialResponseRequest/filters.html)

## Documentation
View the documentation [here](https://benasradzevicius9404.github.io/PartialResponseRequest)

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
