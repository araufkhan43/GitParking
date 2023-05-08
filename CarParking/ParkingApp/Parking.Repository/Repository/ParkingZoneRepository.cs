using Microsoft.EntityFrameworkCore;
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
    public class ParkingZoneRepository: IParkingZoneRepository
    {
        private ParkingContext _parkingcontext;
        public ParkingZoneRepository(ParkingContext _parkingcontext)
        {
            this._parkingcontext = _parkingcontext;

        }
        public async Task Create(ParkingZone pz)
        {
            this._parkingcontext.ParkingZones.Add(pz);
            await this._parkingcontext.SaveChangesAsync();
        }
        public async Task Update(ParkingZone pz)
        {
            this._parkingcontext.ParkingZones.Update(pz);
            await this._parkingcontext.SaveChangesAsync();
        }
        public async Task<ParkingZone> GetParkingZoneById(int Id)
        {
          return await  this._parkingcontext.ParkingZones.FirstOrDefaultAsync(z => z.Id == Id);
        }
        public List<ParkingZone> GetAll()
        {
            return this._parkingcontext.ParkingZones.ToList();
        }
    }
}
