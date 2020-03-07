namespace PartialResponse.Fields.Interpreters
{
    public class EverythingIncluded : IFieldsQueryInterpreter
    {
        public bool Includes(string fieldName)
        {
            return true;
        }

        public IFieldsQueryInterpreter Visit(string fieldName)
        {
            return this;
        }
    }
}
