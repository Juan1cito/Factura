using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FacturasAPI.Models;
using FacturasAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly FacturasContext _context;

        public FacturasController(FacturasContext context)
        {
            _context = context;
        }

        // GET: api/facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            var facturas = await _context.Facturas
                .Include(f => f.Detalles)
                .ToListAsync();

            return Ok(facturas);
        }

        // GET: api/facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (factura == null)
            {
                return NotFound(new { mensaje = "Factura no encontrada" });
            }

            return Ok(factura);
        }

        // POST: api/facturas
        [HttpPost]
        public async Task<ActionResult<Factura>> CrearFactura([FromBody] CrearFacturaDTO facturaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var factura = new Factura
            {
                NumeroFactura = $"FAC-{DateTime.Now:yyyyMMdd}-{await _context.Facturas.CountAsync() + 1:0000}",
                Fecha = DateTime.Now,
                Cliente = facturaDTO.Cliente,
                NIT = facturaDTO.NIT,
                Direccion = facturaDTO.Direccion,
                Detalles = new List<DetalleFactura>()
            };

            // Agregar detalles
            foreach (var detalleDTO in facturaDTO.Detalles)
            {
                var detalle = new DetalleFactura
                {
                    Producto = detalleDTO.Producto,
                    Cantidad = detalleDTO.Cantidad,
                    PrecioUnitario = detalleDTO.PrecioUnitario
                };
                factura.Detalles.Add(detalle);
            }

            // Calcular totales
            factura.Subtotal = factura.Detalles.Sum(d => d.Subtotal);
            factura.IVA = factura.Subtotal * 0.19m;
            factura.Total = factura.Subtotal + factura.IVA;

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFactura), new { id = factura.Id }, factura);
        }

        // PUT: api/facturas/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Factura>> ActualizarFactura(int id, [FromBody] CrearFacturaDTO facturaDTO)
        {
            var factura = await _context.Facturas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (factura == null)
            {
                return NotFound(new { mensaje = "Factura no encontrada" });
            }

            factura.Cliente = facturaDTO.Cliente;
            factura.NIT = facturaDTO.NIT;
            factura.Direccion = facturaDTO.Direccion;

            // Eliminar detalles antiguos
            _context.DetallesFactura.RemoveRange(factura.Detalles);
            factura.Detalles.Clear();

            // Agregar nuevos detalles
            foreach (var detalleDTO in facturaDTO.Detalles)
            {
                var detalle = new DetalleFactura
                {
                    FacturaId = factura.Id,
                    Producto = detalleDTO.Producto,
                    Cantidad = detalleDTO.Cantidad,
                    PrecioUnitario = detalleDTO.PrecioUnitario
                };
                factura.Detalles.Add(detalle);
            }

            // Recalcular totales
            factura.Subtotal = factura.Detalles.Sum(d => d.Subtotal);
            factura.IVA = factura.Subtotal * 0.19m;
            factura.Total = factura.Subtotal + factura.IVA;

            await _context.SaveChangesAsync();

            return Ok(factura);
        }

        // DELETE: api/facturas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null)
            {
                return NotFound(new { mensaje = "Factura no encontrada" });
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Factura eliminada exitosamente" });
        }

        // GET: api/facturas/buscar?cliente=nombre
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Factura>>> BuscarFacturas([FromQuery] string cliente)
        {
            var facturas = await _context.Facturas
                .Include(f => f.Detalles)
                .Where(f => f.Cliente.Contains(cliente))
                .ToListAsync();

            return Ok(facturas);
        }
    }
}