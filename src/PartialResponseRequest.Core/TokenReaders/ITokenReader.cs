using PartialResponseRequest.Core.Enumeration;

namespace PartialResponseRequest.Core.TokenReaders
{
    public interface ITokenReader<T>
    {
        T Read(LookupEnumerator<char> enumerator);
    }
}
