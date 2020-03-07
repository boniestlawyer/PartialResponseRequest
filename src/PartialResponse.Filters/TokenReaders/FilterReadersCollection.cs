using PartialResponse.Core.TokenReaders.Utils;
using System;
using System.Collections.Generic;

namespace PartialResponse.Filters.TokenReaders
{
    public class FilterReadersCollection
    {
        private readonly Dictionary<Type, object> readers = new Dictionary<Type, object>();

        public FilterReadersCollection()
        {
            Add(new FilterOperatorsTokenReader(this));
            Add(new FilterRootReader(this));
            Add(new FilterTokenReader(this));
            Add(new OperatorTokenReader(this));
            Add(new SyntaxTextReader());
            Add(new ListOfTokensReader());
        }

        private void Add<T>(T value) => readers.Add(typeof(T), value);

        public T Get<T>() => (T)readers[typeof(T)];
    }
}
