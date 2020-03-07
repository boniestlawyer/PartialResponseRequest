using PartialResponseRequest.Core.TokenReaders.Utils;
using System;
using System.Collections.Generic;

namespace PartialResponseRequest.Fields.TokenReaders
{
    public class FieldsReadersCollection
    {
        private readonly Dictionary<Type, object> readers = new Dictionary<Type, object>();

        public FieldsReadersCollection()
        {
            Add(new FieldTokenReader(this));
            Add(new FieldPropertiesTokenReader(this));
            Add(new FieldNestedFieldsTokenReader(this));
            Add(new PropertyTokenReader(this));
            Add(new FieldsRootReader(this));
            Add(new ListOfTokensReader());
            Add(new SyntaxTextReader());
        }

        private void Add<T>(T value) => readers.Add(typeof(T), value);

        public T Get<T>() => (T)readers[typeof(T)];
    }
}
