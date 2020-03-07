using PartialResponse.Core.Enumeration;
using System.Collections.Generic;

namespace PartialResponse.Core.TokenReaders.Utils
{
    public class ListOfTokensReader
    {
        public IEnumerable<T> Read<T>(LookupEnumerator<char> enumerator, ITokenReader<T> reader, ISet<char> stopChars, char separator)
        {
            do
            {
                yield return reader.Read(enumerator);
                enumerator.IfNext(separator, e => { e.MoveNext(); e.MoveNext(); });

            } while (!enumerator.Finished && (stopChars == null || !stopChars.Contains(enumerator.GetNext())));
        }
    }
}
