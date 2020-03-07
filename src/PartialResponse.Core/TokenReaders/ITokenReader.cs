using PartialResponse.Core.Enumeration;

namespace PartialResponse.Core.TokenReaders
{
    public interface ITokenReader<T>
    {
        T Read(LookupEnumerator<char> enumerator);
    }
}
