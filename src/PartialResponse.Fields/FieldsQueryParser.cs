using PartialResponse.Core.Enumeration;
using PartialResponse.Fields.TokenReaders;
using PartialResponse.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponse.Fields
{
    public class FieldsQueryParser
    {
        public IEnumerable<FieldToken> Parse(string code)
        {
            FieldsReadersCollection readers = new FieldsReadersCollection();
            FieldsRootReader root = readers.Get<FieldsRootReader>();

            LookupEnumerator<char> enumerator = new LookupEnumerator<char>(code.GetEnumerator());
            enumerator.MoveNext();

            return root.Read(enumerator);
        }
    }
}
