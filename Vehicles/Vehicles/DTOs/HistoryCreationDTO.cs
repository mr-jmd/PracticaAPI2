using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Vehicles.Entities;

namespace Vehicles.DTOs
{
    public class HistoryCreationDTO
    {
        public int VehicleId { get; set; }

        [JsonIgnore]
        [Display(Name = "Vehículo")]
        public Vehicle Vehicle { get; set; }

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

        //[Display(Name = "Total Mano de Obra")]
        //[DisplayFormat(DataFormatString = "{0:C2}")]
        //public int TotalLabor => Details == null ? 0 : Details.Sum(x => x.LaborPrice);

        //[Display(Name = "Total Repuestos")]
        //[DisplayFormat(DataFormatString = "{0:C2}")]
        //public int TotalSpareParts => Details == null ? 0 : Details.Sum(x => x.SparePartsPrice);

        //[Display(Name = "Total")]
        //[DisplayFormat(DataFormatString = "{0:C2}")]
        //public int Total => Details == null ? 0 : Details.Sum(x => x.TotalPrice);
    }
}
