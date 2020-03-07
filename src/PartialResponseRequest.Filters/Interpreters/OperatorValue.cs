using System;

namespace PartialResponseRequest.Filters.Interpreters
{
    public class OperatorValue
    {
        public string Value { get; }
        public Type Type { get; }

        public OperatorValue(string value, Type type)
        {
            Value = value;
            Type = type;
        }
    }
}
