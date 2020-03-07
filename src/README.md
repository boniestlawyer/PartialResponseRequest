[< home](../)
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
    +----------+       Parses        +----------+      Pass       +---------------+
    |  Parser  | ------------------> |  Tokens  | --------------> |  Interpreter  |
    +----------+                     +----------+                 +---------------+
         ^                                |
         | Pass                           | Pass
         |                                v
    +----------+    Serializes     +--------------+ 
    |  String  |<------------------|  Serializer  |
    +----------+                   +--------------+
                   
```

## Scopes

* [Fields](./fields.md)
* [Filters](./filters.md)
* [Partial response](./partial_response.md)
