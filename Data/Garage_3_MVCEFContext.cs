using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage_3_MVCEF.Models;

namespace Garage_3_MVCEF.Data
{
    public class Garage_3_MVCEFContext : DbContext
    {
        public Garage_3_MVCEFContext (DbContextOptions<Garage_3_MVCEFContext> options)
            : base(options)
        {
        }

        public DbSet<Garage_3_MVCEF.Models.Member> Member { get; set; } = default!;
        public DbSet<Garage_3_MVCEF.Models.Vehicle> Vehicle { get; set; } = default!;
        public DbSet<Garage_3_MVCEF.Models.VehicleType> VehicleType { get; set; } = default!;
        public DbSet<Garage_3_MVCEF.Models.ParkingSpot> ParkingSpot { get; set; } = default!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Garage_3_MVCEFContext-151b413c-fa0c-4230-b912-47be3e80cc73;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
