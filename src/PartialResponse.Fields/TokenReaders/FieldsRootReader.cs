using PartialResponse.Core.Enumeration;
using PartialResponse.Core.TokenReaders;
using PartialResponse.Core.TokenReaders.Utils;
using PartialResponse.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponse.Fields.TokenReaders
{
    public class FieldsRootReader : ITokenReader<List<FieldToken>>
    {
        private readonly FieldsReadersCollection readers;

        public FieldsRootReader(FieldsReadersCollection readers)
        {
            this.readers = readers;
        }

        public List<FieldToken> Read(LookupEnumerator<char> enumerator)
        {
            return readers.Get<ListOfTokensReader>().Read(enumerator, readers.Get<FieldTokenReader>(), new HashSet<char>() { '\0' }, ',').ToList();
        }
    }
}
