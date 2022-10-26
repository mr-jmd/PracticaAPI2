using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Vehicles.Entities;

namespace Vehicles.DTOs
{
    public class DetailDTO
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public int ProcedureId { get; set; }

        [Display(Name = "Historia")]
        [JsonIgnore]
        public History History { get; set; }

        [Display(Name = "Procedimiento")]
        [JsonIgnore]
        public Procedure Procedure { get; set; }

        [Display(Name = "Precio Mano de Obra")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LaborPrice { get; set; }

        [Display(Name = "Precio Repuestos")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SparePartsPrice { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice => LaborPrice + SparePartsPrice;

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
}
