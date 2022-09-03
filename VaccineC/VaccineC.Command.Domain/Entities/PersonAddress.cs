using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccineC.Command.Domain.Entities
{
    public class PersonAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid ID { get; set; }

        [Column("personId")]
        public Guid PersonID { get; set; }

        [Column("addressType", TypeName = "varchar(1)")]
        public string AddressType { get; set; }

        [Column("publicPlace", TypeName = "varchar(255)")]
        public string PublicPlace { get; set; }

        [Column("district", TypeName = "varchar(255)")]
        public string District { get; set; }

        [Column("addressNumber", TypeName = "varchar(10)")]
        public string AddressNumber { get; set; }

        [Column("complement", TypeName = "varchar(255)")]
        public string Complement { get; set; }

        [Column("addressCode", TypeName = "varchar(9)")]   
        public string AddressCode { get; set; }

        [Column("referencePoint", TypeName = "varchar(255)")]
        public string? ReferencePoint { get; set; }

        [Column("city", TypeName = "varchar(255)")]
        public string City { get; set; }

        [Column("state", TypeName = "varchar(2)")]
        public string State { get; set; }

        [Column("country", TypeName = "varchar(255)")]
        public string Country { get; set; }

        [Column("register", TypeName = "datetime")]
        public DateTime Register { get; set; }

        public PersonAddress(Guid id, Guid personId, string addressType, string publicPlace, string district, string addressNumber, string complement, string addressCode, string referencePoint, string city, string state, string country, DateTime register)
        {
            ID = id;
            PersonID = personId;
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
        public PersonAddress()
        {

        }

        public void SetPersonId(Guid personId)
        {
            PersonID = personId;
        }

        public void SetAddressType(string addressType)
        {
            AddressType = addressType;
        }

        public void SetPublicPlace(string publicPlace)
        {
            PublicPlace = publicPlace;
        }

        public void SetDistrict(string district)
        {
            District = district;
        }

        public void SetAddressNumber(string addressNumber)
        {
            AddressNumber = addressNumber;
        }

        public void SetComplement(string complement)
        {
            Complement = complement;
        }

        public void SetAddressCode(string addressCode)
        {
            AddressCode = addressCode;
        }

        public void SetReferencePoint(string referencePoint)
        {
            ReferencePoint = referencePoint;
        }

        public void SetCity(string city)
        {
            City = city;
        }

        public void SetState(string state)
        {
            State = state;
        }

        public void SetCountry(string country)
        {
            Country = country;
        }

        public void SetRegister(DateTime register)
        {
            Register = register;
        }
    }
}
