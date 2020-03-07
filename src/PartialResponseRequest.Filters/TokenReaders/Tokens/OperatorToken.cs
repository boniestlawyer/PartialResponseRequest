namespace PartialResponseRequest.Filters.TokenReaders.Tokens
{
    public class OperatorToken
    {
        public string Type { get; private set; }
        public string Value { get; private set; }

        public OperatorToken(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
