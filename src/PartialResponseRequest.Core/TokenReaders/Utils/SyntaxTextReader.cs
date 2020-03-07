using PartialResponseRequest.Core.Enumeration;
using System.Collections.Generic;

namespace PartialResponseRequest.Core.TokenReaders.Utils
{
    public class SyntaxTextReader
    {
        public IEnumerable<char> Read(LookupEnumerator<char> enumerator, ISet<char> stopSymbols, ISet<char> unexpectedSymbols = null)
        {
            enumerator.EnsureExpectedChar(unexpectedSymbols);
            yield return enumerator.Current;

            while (!enumerator.Finished && !stopSymbols.Contains(enumerator.GetNext()) && enumerator.GetNext() != '\0')
            {
                enumerator.MoveNext();
                enumerator.EnsureExpectedChar(unexpectedSymbols);
                yield return enumerator.Current;
            };
        }
    }
}
