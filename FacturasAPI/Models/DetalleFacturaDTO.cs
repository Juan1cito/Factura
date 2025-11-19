using System.ComponentModel.DataAnnotations;

namespace FacturasAPI.Models
{
    public class DetalleFacturaDTO
    {
        [Required]
        public string Producto { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal PrecioUnitario { get; set; }
    }
}
