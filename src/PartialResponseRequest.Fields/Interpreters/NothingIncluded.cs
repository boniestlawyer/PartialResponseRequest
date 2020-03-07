namespace PartialResponseRequest.Fields.Interpreters
{
    public class NothingIncluded : IFieldsQueryInterpreter
    {
        public bool Includes(string fieldName)
        {
            return false;
        }

        public IFieldsQueryInterpreter Visit(string fieldName)
        {
            return this;
        }
    }
}
