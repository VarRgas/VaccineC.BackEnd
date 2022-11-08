using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineC.Query.Application.ViewModels
{
    public class UserResourceMenuViewModel
    {
        public List<UserResourceMenuInventoryViewModel> listInventory = new List<UserResourceMenuInventoryViewModel>();
        public List<UserResourceMenuManagementViewModel> listManagement = new List<UserResourceMenuManagementViewModel>();
        public List<UserResourceMenuOperationalViewModel> listOperational = new List<UserResourceMenuOperationalViewModel>();
        public List<UserResourceMenuRegistrationViewModel> listRegistration = new List<UserResourceMenuRegistrationViewModel>();
    }
}
