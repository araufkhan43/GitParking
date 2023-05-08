using Parking.Business.Business.Interface;
using Parking.Model.Account;
using Parking.Model.ParkingZone;
using Parking.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Business
{

    public class AccountBusiness:IAccountBusiness
    {
        private IAccountRepository _iAccountRepository;
        public AccountBusiness(IAccountRepository _iAccountRepository)
        {
            this._iAccountRepository = _iAccountRepository;
        }

        public async Task Create(UserMaster pz)
        {
            await this._iAccountRepository.Create(pz);

        }
    }
}
