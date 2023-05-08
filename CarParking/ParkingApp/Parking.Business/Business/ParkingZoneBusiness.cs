using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Repository.Repository.Interface;
using Parking.Model;
using Parking.Model.ParkingZone;
using Parking.Business.Business.Interface;

namespace Parking.Business.Business
{
    public class ParkingZoneBusiness: IParkingZoneBusiness
    {
        private readonly IParkingZoneRepository _parkingZoneRepository;
        public ParkingZoneBusiness(IParkingZoneRepository _parkingZoneRepository) {
            this._parkingZoneRepository = _parkingZoneRepository;
        }
        public async Task Create(ParkingZone pz)
        {
            await this._parkingZoneRepository.Create(pz);
            
        }
        public async Task Update(ParkingZone pz)
        {
            await this._parkingZoneRepository.Update(pz);
        }
        public async Task<ParkingZone> GetParkingZoneById(int Id)
        {
            return await this._parkingZoneRepository.GetParkingZoneById(Id);
        }
        public List<ParkingZone> GetAll()
        {
            return this._parkingZoneRepository.GetAll();
        }
    }
}
