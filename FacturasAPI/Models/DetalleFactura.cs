using System.ComponentModel.DataAnnotations;

namespace FacturasAPI.Models
{
    public class DetalleFactura
    {
        [Key]
        public int Id { get; set; }

        public int FacturaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
