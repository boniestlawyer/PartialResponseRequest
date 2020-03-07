using PartialResponse.Fields.Interpreters;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponse.Tests.ResponsePruner.Utils
{
    public class IncludeListedPropertiesInterpreter : IFieldsQueryInterpreter
    {
        private readonly List<string> include;

        public IncludeListedPropertiesInterpreter(IEnumerable<string> include)
        {
            this.include = include.ToList();
        }

        public bool Includes(string fieldName)
        {
            return include.Contains(fieldName);
        }

        public IFieldsQueryInterpreter Visit(string fieldName)
        {
            return this;
        }
    }
}
