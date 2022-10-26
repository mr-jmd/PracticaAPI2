using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Vehicles.Entities;

namespace Vehicles.DTOs
{
    public class VehicleTypeDTO
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de vehículo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
