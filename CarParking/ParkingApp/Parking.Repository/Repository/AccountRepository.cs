using Parking.Model.Account;
using Parking.Model.ParkingZone;
using Parking.Repository.Context;
using Parking.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.Repository.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private ParkingContext _parkingContext;
        public AccountRepository(ParkingContext _parkingContext)
        {
            this._parkingContext = _parkingContext;

        }
        public async Task Create(UserMaster pz)
        {
            try
            {
                this._parkingContext.UserMasters.Add(pz);
               
               await this._parkingContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
