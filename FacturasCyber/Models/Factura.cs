namespace FacturasCyber.Models
{
    // Modelo completo de Factura (lo que devuelve la API)
    public class Factura
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = "";
        public string? NIT { get; set; }
        public string? Direccion { get; set; }
        public List<DetalleFactura> Detalles { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "";
    }

    // Detalle de factura completo
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public string Producto { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    // DTO para CREAR/EDITAR facturas (lo que envías a la API)
    public class CrearFacturaDTO
    {
        public string Cliente { get; set; } = "";
        public string? NIT { get; set; }
        public string? Direccion { get; set; }
        public List<DetalleFacturaDTO> Detalles { get; set; } = new();
    }

    // DTO para detalles al crear/editar
    public class DetalleFacturaDTO
    {
        public string Producto { get; set; } = "";
        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; } = 0;
    }
}