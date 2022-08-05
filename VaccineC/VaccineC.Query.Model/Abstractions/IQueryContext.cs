using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Model.Abstractions
{
    public interface IQueryContext
    {
        IQueryable<Example> AllExamples { get; }
    }
}
