﻿namespace VaccineC.Query.Model.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Situation { get; set; }
        public string FunctionUser { get; set; }
        public DateTime Register { get; set; }
        public Guid PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
