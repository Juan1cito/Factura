using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FacturasAPI.Models
{
    // Modelo principal de Factura
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroFactura { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(100)]
        public string Cliente { get; set; }

        [StringLength(20)]
        public string NIT { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();

        public decimal Subtotal { get; set; }

        public decimal IVA { get; set; }

        public decimal Total { get; set; }

        [StringLength(20)]
        public string Estado { get; set; } = "Activa";
    }
}