using AutoMapper;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Mapper
{
    public class QueryModelMapper : Profile
    {
        public QueryModelMapper()
        {
            CreateMap<Example, ExampleViewModel>();
            CreateMap<User, LoginViewModel>();
            CreateMap<PaymentForm, PaymentFormViewModel>();
            CreateMap<Resource, ResourceViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<Person, PersonViewModel>();
            CreateMap<UserResource, UserResourceViewModel>();
            CreateMap<Company, CompanyViewModel>();
            CreateMap<CompaniesParameters, CompaniesParametersViewModel>();
        }
    }
}
