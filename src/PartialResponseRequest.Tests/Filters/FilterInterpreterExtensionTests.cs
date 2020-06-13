using FluentAssertions;
using PartialResponseRequest.Filters.Builders;
using PartialResponseRequest.Filters.Interpreters;
using Xunit;

namespace PartialResponseRequest.Tests.Filters
{
    public interface MyFilters
    {
        IdFilters Id { get; }
        IdFilters Id2 { get; }
    }

    public interface IdFilters
    {
        int Eq { get; }
    }

    public class FilterInterpreterExtensionTests
    {
        [Fact]
        public void FiltersByWithOutParam_DoesNotReturnNullIfFilterIsNotFound()
        {
            var interpreter = new FiltersQueryInterpreter<MyFilters>(new FiltersQueryBuilder<MyFilters>()
                .Filter(x => x.Id, x => x.Operator(o => o.Eq, "3")).Build());

            var idOps = interpreter.GetFilter(x => x.Id);
            var id2Ops = interpreter.GetFilter(x => x.Id2);

            interpreter.FiltersBy(x => x.Id, out var idOpsOut);
            interpreter.FiltersBy(x => x.Id2, out var id2OpsOut);

            idOps.HasOperator(x => x.Eq).Should().BeTrue();
            idOpsOut.HasOperator(x => x.Eq).Should().BeTrue();
            
            id2Ops.HasOperator(x => x.Eq).Should().BeFalse();
            id2OpsOut.HasOperator(x => x.Eq).Should().BeFalse();
        }
    }
}
