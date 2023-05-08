using Parking.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.Repository.Interface
{
    public interface IAccountRepository
    {
        Task Create(UserMaster obj);
    }
}
