using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Fields.TokenReaders;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Fields
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
