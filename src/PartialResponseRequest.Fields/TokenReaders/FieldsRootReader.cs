using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders;
using PartialResponseRequest.Core.TokenReaders.Utils;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponseRequest.Fields.TokenReaders
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
