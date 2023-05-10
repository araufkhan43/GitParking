using Parking.Model.ParkingArea;
using Parking.Model.ParkingZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Business.Interface
{
    public interface IParkingSpaceBusiness
    {
       // List<ParkingArea> GetAll();
        List<ParkingArea> GetAllParkingSlot(int Id);
    }
}
