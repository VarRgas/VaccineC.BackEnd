using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Model.Models
{
    public class UserResource
    {
        public Guid ID { get; set; }
        public Guid UsersId { get; set; }
        public Guid ResourcesId { get; set; }
        public DateTime Register { get; set; }
    }
}
