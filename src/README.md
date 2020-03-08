[< home](../../../)
# Documentation

## Data flow

Data flow between components:
* **Parsers** converts a string into a list of tokens
* **Tokens** are wrapped with interpreters that are able to interpret given tokens
* **Builders** are used to manually build a list of tokens rather than parsing from string
* **Serializers** takes the tokens and converts them into the string query
* **Interpreter** wraps tokens for interpretation. For example `FieldsQueryInterpreter` will interpret field token of `*` as "everything is included within the object"

```
                                    +-----------+
                                    |  Builder  |
                                    +-----------+
                                          |
                                          | Build()
                                          v          
    +----------+       Parse()       +----------+      Pass       +---------------+
    |  Parser  | ------------------> |  Tokens  | --------------> |  Interpreter  |
    +----------+                     +----------+                 +---------------+
         ^                                |
         | Pass                           | Pass
         |                                v
    +----------+    Serialize()    +--------------+ 
    |  String  |<------------------|  Serializer  |
    +----------+                   +--------------+
                   
```

## Scopes

* [Response Pruner](./PartialResponseRequest.AspNetCore.ResponsePruner)
* [Fields](./PartialResponseRequest.Fields)
* [Filters](./PartialResponseRequest.Filters)
