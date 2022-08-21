using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Command.Application.Commands.Resource
{
    public class AddResourceCommand : IRequest
    {
        public Guid ID;
        public string Name;
        public string UrlName;
        public DateTime Register;

        public AddResourceCommand(Guid id, string name, string urlName, DateTime register)
        {
            ID = id;
            Name = name;
            UrlName = urlName;
            Register = register;
        }
    }
}
