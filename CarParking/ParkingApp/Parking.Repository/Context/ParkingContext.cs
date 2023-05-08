using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Parking.Model.ParkingZone;
using Parking.Model.Account;

namespace Parking.Repository.Context
{
    public class ParkingContext:DbContext
    {
        public ParkingContext(DbContextOptions<ParkingContext> options):base (options) 
        {
        this.ChangeTracker.LazyLoadingEnabled= true;
        }
        public virtual DbSet<ParkingZone> ParkingZones { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
    }
}
