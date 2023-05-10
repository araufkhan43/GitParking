using Microsoft.EntityFrameworkCore;
using Parking.Model.ParkingArea;
using Parking.Model.ParkingZone;
using Parking.Repository.Context;
using Parking.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Repository.Repository
{
    public class ParkingSpaceRepository: IParkingSpaceRepository
    {
        private ParkingContext _parkingcontext;
        public ParkingSpaceRepository(ParkingContext _parkingcontext)
        {
            this._parkingcontext = _parkingcontext;

        }
        public List<ParkingArea> GetAllParkingSlot(int Id)
        {
            var query = (from zone in _parkingcontext.ParkingZones
                         join
                       space in _parkingcontext.ParkingSpace on zone.Id equals space.Parking_Zone_Id
                       where zone.Id == Id 
                         select new ParkingArea
                         {
                             Id=space.Id,
                             Parking_Space_Title = space.Parking_Space_Title,
                             Parking_Zone_Title = zone.Parking_Zone_Title,
                             Is_Available = space.Is_Available,
                             Parking_Zone_Id=space.Parking_Zone_Id
                         }).ToList();
            return query;
        }
    }
}
