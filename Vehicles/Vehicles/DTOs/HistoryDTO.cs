using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Vehicles.Entities;
using Vehicles.Models;

namespace Vehicles.DTOs
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        [Display(Name = "Vehículo")]
        public Vehicle Vehicle { get; set; }

        [JsonIgnore]
        [Display(Name = "Usuario")]
        public IdentityModel IdentityModel { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime Date { get; set; }

        [Display(Name = "Kilometraje")]
        public int Mileage { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [JsonIgnore]
        public ICollection<Detail> Details { get; set; }

       
    }
}
