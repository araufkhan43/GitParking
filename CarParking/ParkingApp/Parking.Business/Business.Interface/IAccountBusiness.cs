using Parking.Model.Account;
using Parking.Model.ParkingZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Business.Interface
{
    public interface IAccountBusiness
    {
        Task Create(UserMaster obj);
    }
}
