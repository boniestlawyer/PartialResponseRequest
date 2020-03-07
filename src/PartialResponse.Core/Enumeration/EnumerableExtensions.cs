using System.Collections.Generic;
using System.Linq;

namespace PartialResponse.Core.Enumeration
{
    public static class EnumerableExtensions
    {
        public static string AsString(this IEnumerable<char> chars) => new string(chars.ToArray());
    }
}
