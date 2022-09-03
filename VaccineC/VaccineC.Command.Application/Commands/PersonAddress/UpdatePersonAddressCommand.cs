using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonAddress
{
    public class UpdatePersonAddressCommand : IRequest<IEnumerable<PersonAddressViewModel>>
    {
        public Guid ID;
        public Guid PersonID;
        public string AddressType;
        public string PublicPlace;
        public string District;
        public string AddressNumber;
        public string Complement;
        public string AddressCode;
        public string? ReferencePoint;
        public string City;
        public string State;
        public string Country;
        public DateTime Register;

        public UpdatePersonAddressCommand(Guid id, Guid personID, string addressType, string publicPlace, string district, string addressNumber, string complement, string addressCode, string? referencePoint, string city, string state, string country, DateTime register)
        {
            ID = id;
            PersonID = personID;
            AddressType = addressType;
            PublicPlace = publicPlace;
            District = district;
            AddressNumber = addressNumber;
            Complement = complement;
            AddressCode = addressCode;
            ReferencePoint = referencePoint;
            City = city;
            State = state;
            Country = country;
            Register = register;
        }
    }
}
