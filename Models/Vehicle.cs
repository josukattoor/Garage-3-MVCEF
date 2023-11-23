using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        [Required(ErrorMessage = "Personal number is required")]
        [RegularExpression(@"^\d{8}-\d{4}$", ErrorMessage = "Personal number should be in format yyyymmdd-nnnn")]
        public string PersonalNumber { get; set; }
        public DateTime ArrivalTime { get; set; }
        public Member Member { get; set; }
        public int? MemberID { get; set; }

        public VehicleType VehicleType { get; set; }


        public int VehicleTypeID { get; set; }

    }
}
