namespace PartialResponse.Fields.TokenReaders.Tokens
{
    public class ParameterToken
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public ParameterToken(string key, string value)
        {
            Name = key;
            Value = value;
        }
    }
}
