using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Vehicles.Entities;

namespace Vehicles.DTOs
{
    public class VehiclePhotoDTO
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }

        [JsonIgnore]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Foto")]
        public string ImageId { get; set; }

    }
}
