using Parking.Business.Business.Interface;
using Parking.Model.ParkingArea;
using Parking.Model.ParkingZone;
using Parking.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Business.Business
{
    public class ParkingSpaceBusiness: IParkingSpaceBusiness
    {
        private readonly IParkingSpaceRepository _parkingSpaceRepository;
        public ParkingSpaceBusiness(IParkingSpaceRepository _parkingSpaceRepository)
        {
            this._parkingSpaceRepository = _parkingSpaceRepository;
        }

        public  List<ParkingArea> GetAllParkingSlot(int Id)
        {
            return this._parkingSpaceRepository.GetAllParkingSlot(Id);
        }
    }
}
