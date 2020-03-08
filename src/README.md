[< home](../../../)
# Documentation

## Data flow

Data flow between components:
* **Parsers** converts a string into a list of tokens
* **Tokens** are wrapped with interpreters that are able to interpret given tokens
* **Builders** are used to manually build a list of tokens rather than parsing from string
* **Serializers** takes the tokens and converts them into the string query

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

* [Fields](./PartialResponseRequest.Fields)
* [Filters](./PartialResponseRequest.Filters)
* [Partial response](./PartialResponseRequest.AspNetCore.ResponsePruner)
