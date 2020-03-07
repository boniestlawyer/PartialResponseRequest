[< back](../)
# Partial response
Register a custom json output formatter that is able to prune json results based on given `?fields=...` query string value. Internally it uses `FieldsQueryParser` and `FieldsQueryInterpreter` to parse and iterpret the query.

If your result is:
```json
{
    "id": 1,
    "name": "John",
    "age": 65,
    "friends": [
        {
            "id": 2,
            "name": "Tim",
            "age": 23
        }
    ]
}
```
given a query of `?fields=id,friends{id,name}` would result in:
```json
{
    "id": 1,
    "friends": [
        {
            "id": 2,
            "name": "Tim"
        }
    ]
}
```


## Usage

Use `services.AddPartialResponse()` to add a custom json output formatter, that will search for `?fields=...` query string that would be used to prune the response for the client.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...

    services.AddPartialResponse();

    // ...
}
```

Extension also allows some of customization options:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...

    // It also supports customization by providing options
    services.AddPartialResponse(options =>
    {
        // By default, JsonPruner implementation is used, but
        // if you API wraps the responses, you can override the default
        // with WrapperAwareJsonPruner and prividing the field name which needs
        // to be unwrapped. Ex: For { page: 1, data: [ { name: "item" } ] }
        // root wrapper will be ignored and we'll start prunning from [ { name: "item" } ]
        options.Pruner = c => new WrapperAwareJsonPruner("data");

        // You can configure a custom IRequestFieldsTokensProvider implementation,
        // which is responsible for providing requested fields
        // By default, we'll search for `?fields=` query string in current request.
        options.RequestFieldsTokensProvider = c => new YourImplementation();
    });

    // ...
}
```