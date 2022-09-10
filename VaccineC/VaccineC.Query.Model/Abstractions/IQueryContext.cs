using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Model.Abstractions
{
    public interface IQueryContext
    {
        IQueryable<Example> AllExamples { get; }
        IQueryable<PaymentForm> AllPaymentForms { get; }
        IQueryable<Resource> AllResources { get; }
        IQueryable<Company> AllCompanies { get; }
        IQueryable<User> AllUsers { get; }
        IQueryable<Person> AllPersons { get; }
        IQueryable<PersonsPhysical> AllPersonsPhysicals { get; }
        IQueryable<PersonsJuridical> AllPersonsJuridicals { get; }
        IQueryable<PersonPhone> AllPersonsPhones { get; }
        IQueryable<PersonAddress> AllPersonsAddresses { get; }
        IQueryable<UserResource> AllUserResources { get; }
        IQueryable<CompanyParameter> AllCompaniesParameters { get; }
        IQueryable<CompanySchedule> AllCompaniesSchedules { get; }
        IQueryable<Product> AllProducts { get; }
        IQueryable<ProductDoses> AllProductsDoses { get; }
        IQueryable<ProductSummaryBatch> AllProductsSummariesBatches { get; }

    }
}
