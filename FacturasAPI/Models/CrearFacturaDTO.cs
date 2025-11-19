using System.ComponentModel.DataAnnotations;

namespace FacturasAPI.Models
{
    public class CrearFacturaDTO
    {
        [Required]
        public string Cliente { get; set; }

        public string NIT { get; set; }

        public string Direccion { get; set; }

        [Required]
        public List<DetalleFacturaDTO> Detalles { get; set; }
    }
}
