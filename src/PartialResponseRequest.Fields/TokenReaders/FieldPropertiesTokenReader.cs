using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders.Utils;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponseRequest.Fields.TokenReaders
{
    public class FieldPropertiesTokenReader
    {
        private readonly HashSet<char> stopChars = new HashSet<char>(new[] { ')' });

        private readonly FieldsReadersCollection readers;

        public FieldPropertiesTokenReader(FieldsReadersCollection readers)
        {
            this.readers = readers;
        }

        public List<ParameterToken> Read(LookupEnumerator<char> enumerator)
        {
            enumerator.MoveNext();

            List<ParameterToken> properties = readers.Get<ListOfTokensReader>().Read(enumerator, readers.Get<PropertyTokenReader>(), stopChars, ',').ToList();

            enumerator.MoveNext();

            return properties;
        }
    }
}
