using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Product
{
    public class AddProductCommand : IRequest<ProductViewModel>
    {
        public Guid ID;
        public Guid SbimVaccinesId;
        public string Situation;
        public string? Details;
        public decimal SaleValue;
        public DateTime Register;
        public string Name;
        public int MinimumStock;

        public AddProductCommand(Guid id, Guid sbimVaccinesId, string situation, string? details, decimal saleValue, DateTime register, string name, int minimumStock)
        {
            ID = id;
            SbimVaccinesId = sbimVaccinesId;
            Situation = situation;
            Details = details;
            SaleValue = saleValue;
            Register = register;
            Name = name;
            MinimumStock = minimumStock;
        }
    }
}
