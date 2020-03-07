using PartialResponseRequest.Core.TokenReaders;
using System;
using System.Collections.Generic;

namespace PartialResponseRequest.Core.Enumeration
{
    public static class LookupEnumeratorExtensions
    {
        public static void EnsureExpectedChar(this LookupEnumerator<char> enumerator, ISet<char> chars)
        {
            if (chars != null && chars.Contains(enumerator.Current))
            {
                throw new UnexpectedCharException(enumerator.Index, enumerator.Current);
            }
        }

        public static LookupEnumerator<char> IfNext(this LookupEnumerator<char> enumerator, char next, Action<LookupEnumerator<char>> ifTrue, Action<LookupEnumerator<char>> ifFalse = null)
        {
            if (enumerator.Finished)
            {
                return enumerator;
            }

            if (enumerator.GetNext() == next)
            {
                ifTrue(enumerator);
            }
            else
            {
                ifFalse?.Invoke(enumerator);
            }
            return enumerator;
        }
    }
}
