using Parking.Model.ParkingArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.Repository.Interface
{
    public interface IParkingSpaceRepository
    {
        //List<ParkingArea> GetAll();
        List<ParkingArea> GetAllParkingSlot(int Id);
    }
}
