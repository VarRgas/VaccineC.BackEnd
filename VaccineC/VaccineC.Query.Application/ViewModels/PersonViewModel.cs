﻿namespace VaccineC.Query.Application.ViewModels
{
    public class PersonViewModel
    {
        public Guid ID { get; set; }
        public string PersonType { get; set; }
        public string Name { get; set; }
        public DateTime? CommemorativeDate { get; set; }
        public string? Email { get; set; }
        public string? ProfilePic { get; set; }
        public string? Details { get; set; }
        public DateTime Register { get; set; }
        public string? PersonBudgetResponsible { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public PersonsPhysicalViewModel? PersonsPhysical { get; set; }
        public PersonsJuridicalViewModel? PersonsJuridical { get; set; }
        public PersonAddressViewModel? PersonPrincipalAddress { get; set; }
        public PersonPhoneViewModel? PersonPrincipalPhone { get; set; }
    }
}
