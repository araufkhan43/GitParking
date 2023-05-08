using Parking.Model.ParkingZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Business.Interface
{
    public interface IParkingZoneBusiness
    {
        Task Create(ParkingZone obj);
        Task Update(ParkingZone obj);
        List<ParkingZone> GetAll();
       // Task Delete(int Id);
        Task<ParkingZone> GetParkingZoneById(int Id);
    }
}
