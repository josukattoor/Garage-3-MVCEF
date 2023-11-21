using System.ComponentModel.DataAnnotations;

namespace Garage_3_MVCEF.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string RegNumber { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }

        public string? Color { get; set; }

        public int? NumWheels { get; set; }
        public DateTime ArrivalTime { get; set; }
        public Member member { get; set; }
        public int MemberID { get; set; }

        public VehicleType VehicleType { get; set; }

        public int VehicleTypeID { get; set; }

    }
}
