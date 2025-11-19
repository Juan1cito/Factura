using System.Net.Http.Json;
using FacturasCyber.Models;

namespace FacturasCyber.Services
{
    public class FacturasService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FacturasService>? _logger;

        public FacturasService(HttpClient httpClient, ILogger<FacturasService>? logger = null)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // ==================== CREAR FACTURA ====================
        public async Task<(bool exito, Factura? factura, string mensaje)> CrearFacturaAsync(CrearFacturaDTO facturaDTO)
        {
            try
            {
                _logger?.LogInformation("📤 Intentando crear factura para cliente: {Cliente}", facturaDTO.Cliente);
                Console.WriteLine($"📤 POST {_httpClient.BaseAddress}api/facturas");

                var response = await _httpClient.PostAsJsonAsync("/api/facturas", facturaDTO);

                _logger?.LogInformation("📡 Respuesta recibida: {StatusCode}", response.StatusCode);
                Console.WriteLine($"📡 Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var factura = await response.Content.ReadFromJsonAsync<Factura>();

                    if (factura != null)
                    {
                        _logger?.LogInformation("✅ Factura creada exitosamente: {NumeroFactura}", factura.NumeroFactura);
                        Console.WriteLine($"✅ Factura creada: {factura.NumeroFactura}");
                        return (true, factura, "Factura creada exitosamente");
                    }

                    return (false, null, "Error al deserializar la respuesta");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var mensajeError = $"Error {(int)response.StatusCode}: {response.ReasonPhrase}. {errorContent}";

                    _logger?.LogError("❌ Error al crear factura: {Error}", mensajeError);
                    Console.WriteLine($"❌ {mensajeError}");

                    return (false, null, mensajeError);
                }
            }
            catch (HttpRequestException ex)
            {
                var mensaje = $"Error de conexión: No se pudo conectar al servidor. Verifique que la API esté ejecutándose en {_httpClient.BaseAddress}";
                _logger?.LogError(ex, "❌ Error de conexión HTTP");
                Console.WriteLine($"❌ HttpRequestException: {ex.Message}");
                return (false, null, mensaje);
            }
            catch (Exception ex)
            {
                var mensaje = $"Error inesperado: {ex.Message}";
                _logger?.LogError(ex, "❌ Error inesperado al crear factura");
                Console.WriteLine($"❌ Exception: {ex.GetType().Name} - {ex.Message}");
                return (false, null, mensaje);
            }
        }

        // ==================== OBTENER FACTURA POR ID ====================
        public async Task<(bool exito, Factura? factura, string mensaje)> ObtenerFacturaPorIdAsync(int id)
        {
            try
            {
                _logger?.LogInformation("🔍 Buscando factura con ID: {Id}", id);
                Console.WriteLine($"🔍 GET {_httpClient.BaseAddress}api/facturas/{id}");

                var response = await _httpClient.GetAsync($"/api/facturas/{id}");

                Console.WriteLine($"📡 Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var factura = await response.Content.ReadFromJsonAsync<Factura>();

                    if (factura != null)
                    {
                        _logger?.LogInformation("✅ Factura encontrada: {NumeroFactura}", factura.NumeroFactura);
                        Console.WriteLine($"✅ Factura encontrada: {factura.NumeroFactura}");
                        return (true, factura, "Factura encontrada");
                    }

                    return (false, null, "No se pudo deserializar la factura");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var mensaje = $"No se encontró ninguna factura con ID {id}";
                    _logger?.LogWarning("⚠️ {Mensaje}", mensaje);
                    Console.WriteLine($"⚠️ {mensaje}");
                    return (false, null, mensaje);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var mensaje = $"Error {(int)response.StatusCode}: {errorContent}";
                    _logger?.LogError("❌ {Mensaje}", mensaje);
                    Console.WriteLine($"❌ {mensaje}");
                    return (false, null, mensaje);
                }
            }
            catch (HttpRequestException ex)
            {
                var mensaje = $"Error de conexión con el servidor";
                _logger?.LogError(ex, "❌ Error de conexión");
                Console.WriteLine($"❌ HttpRequestException: {ex.Message}");
                return (false, null, mensaje);
            }
            catch (Exception ex)
            {
                var mensaje = $"Error inesperado: {ex.Message}";
                _logger?.LogError(ex, "❌ Error inesperado");
                Console.WriteLine($"❌ Exception: {ex}");
                return (false, null, mensaje);
            }
        }

        // ==================== OBTENER TODAS LAS FACTURAS ====================
        public async Task<(bool exito, List<Factura> facturas, string mensaje)> ObtenerTodasAsync()
        {
            try
            {
                _logger?.LogInformation("📋 Obteniendo todas las facturas");
                Console.WriteLine($"📋 GET {_httpClient.BaseAddress}api/facturas");

                var response = await _httpClient.GetAsync("/api/facturas");

                Console.WriteLine($"📡 Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var facturas = await response.Content.ReadFromJsonAsync<List<Factura>>();

                    if (facturas != null)
                    {
                        _logger?.LogInformation("✅ Se obtuvieron {Count} facturas", facturas.Count);
                        Console.WriteLine($"✅ {facturas.Count} facturas obtenidas");
                        return (true, facturas, $"Se encontraron {facturas.Count} facturas");
                    }

                    return (true, new List<Factura>(), "No hay facturas");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var mensaje = $"Error {(int)response.StatusCode}: {errorContent}";
                    _logger?.LogError("❌ {Mensaje}", mensaje);
                    Console.WriteLine($"❌ {mensaje}");
                    return (false, new List<Factura>(), mensaje);
                }
            }
            catch (HttpRequestException ex)
            {
                var mensaje = "Error de conexión con el servidor";
                _logger?.LogError(ex, "❌ Error de conexión");
                Console.WriteLine($"❌ HttpRequestException: {ex.Message}");
                return (false, new List<Factura>(), mensaje);
            }
            catch (Exception ex)
            {
                var mensaje = $"Error inesperado: {ex.Message}";
                _logger?.LogError(ex, "❌ Error inesperado");
                Console.WriteLine($"❌ Exception: {ex}");
                return (false, new List<Factura>(), mensaje);
            }
        }

        // ==================== BUSCAR POR NOMBRE DE CLIENTE ====================
        public async Task<(bool exito, List<Factura> facturas, string mensaje)> BuscarPorClienteAsync(string cliente)
        {
            try
            {
                _logger?.LogInformation("🔍 Buscando facturas para cliente: {Cliente}", cliente);
                Console.WriteLine($"🔍 GET {_httpClient.BaseAddress}api/facturas/buscar?cliente={cliente}");

                var response = await _httpClient.GetAsync($"/api/facturas/buscar?cliente={cliente}");

                Console.WriteLine($"📡 Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var facturas = await response.Content.ReadFromJsonAsync<List<Factura>>();

                    if (facturas != null)
                    {
                        _logger?.LogInformation("✅ Se encontraron {Count} facturas", facturas.Count);
                        Console.WriteLine($"✅ {facturas.Count} facturas encontradas");
                        return (true, facturas, $"Se encontraron {facturas.Count} facturas");
                    }

                    return (true, new List<Factura>(), "No se encontraron facturas");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var mensaje = $"Error {(int)response.StatusCode}: {errorContent}";
                    _logger?.LogError("❌ {Mensaje}", mensaje);
                    Console.WriteLine($"❌ {mensaje}");
                    return (false, new List<Factura>(), mensaje);
                }
            }
            catch (HttpRequestException ex)
            {
                var mensaje = "Error de conexión con el servidor";
                _logger?.LogError(ex, "❌ Error de conexión");
                Console.WriteLine($"❌ HttpRequestException: {ex.Message}");
                return (false, new List<Factura>(), mensaje);
            }
            catch (Exception ex)
            {
                var mensaje = $"Error inesperado: {ex.Message}";
                _logger?.LogError(ex, "❌ Error inesperado");
                Console.WriteLine($"❌ Exception: {ex}");
                return (false, new List<Factura>(), mensaje);
            }
        }

        // ==================== BUSCAR POR NIT ====================
        public async Task<(bool exito, List<Factura> facturas, string mensaje)> BuscarPorNITAsync(string nit)
        {
            try
            {
                _logger?.LogInformation("🔍 Buscando facturas con NIT: {NIT}", nit);
                Console.WriteLine($"🔍 Buscando por NIT: {nit}");

                // Obtener todas y filtrar por NIT
                var (exito, todasFacturas, mensaje) = await ObtenerTodasAsync();

                if (!exito)
                {
                    return (false, new List<Factura>(), mensaje);
                }

                var facturasFiltradas = todasFacturas
                    .Where(f => !string.IsNullOrEmpty(f.NIT) &&
                                f.NIT.Contains(nit, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                _logger?.LogInformation("✅ Se encontraron {Count} facturas con ese NIT", facturasFiltradas.Count);
                Console.WriteLine($"✅ {facturasFiltradas.Count} facturas con NIT {nit}");

                return (true, facturasFiltradas, $"Se encontraron {facturasFiltradas.Count} facturas");
            }
            catch (Exception ex)
            {
                var mensaje = $"Error al buscar por NIT: {ex.Message}";
                _logger?.LogError(ex, "❌ Error al buscar por NIT");
                Console.WriteLine($"❌ Exception: {ex}");
                return (false, new List<Factura>(), mensaje);
            }
        }

        // ==================== ACTUALIZAR FACTURA ====================
        public async Task<(bool exito, string mensaje)> ActualizarFacturaAsync(int id, CrearFacturaDTO facturaDTO)
        {
            try
            {
                _logger?.LogInformation("📝 Actualizando factura ID: {Id}", id);
                Console.WriteLine($"📝 PUT {_httpClient.BaseAddress}api/facturas/{id}");

                var response = await _httpClient.PutAsJsonAsync($"/api/facturas/{id}", facturaDTO);

                Console.WriteLine($"📡 Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    _logger?.LogInformation("✅ Factura actualizada exitosamente");
                    Console.WriteLine($"✅ Factura {id} actualizada");
                    return (true, "Factura actualizada exitosamente");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var mensaje = $"Error {(int)response.StatusCode}: {errorContent}";
                    _logger?.LogError("❌ {Mensaje}", mensaje);
                    Console.WriteLine($"❌ {mensaje}");
                    return (false, mensaje);
                }
            }
            catch (HttpRequestException ex)
            {
                var mensaje = "Error de conexión con el servidor";
                _logger?.LogError(ex, "❌ Error de conexión");
                Console.WriteLine($"❌ HttpRequestException: {ex.Message}");
                return (false, mensaje);
            }
            catch (Exception ex)
            {
                var mensaje = $"Error inesperado: {ex.Message}";
                _logger?.LogError(ex, "❌ Error inesperado");
                Console.WriteLine($"❌ Exception: {ex}");
                return (false, mensaje);
            }
        }

        // ==================== ELIMINAR FACTURA ====================
        public async Task<(bool exito, string mensaje)> EliminarFacturaAsync(int id)
        {
            try
            {
                _logger?.LogInformation("🗑️ Eliminando factura ID: {Id}", id);
                Console.WriteLine($"🗑️ DELETE {_httpClient.BaseAddress}api/facturas/{id}");

                var response = await _httpClient.DeleteAsync($"/api/facturas/{id}");

                Console.WriteLine($"📡 Status: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    _logger?.LogInformation("✅ Factura eliminada exitosamente");
                    Console.WriteLine($"✅ Factura {id} eliminada");
                    return (true, "Factura eliminada exitosamente");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var mensaje = $"No se encontró la factura con ID {id}";
                    _logger?.LogWarning("⚠️ {Mensaje}", mensaje);
                    Console.WriteLine($"⚠️ {mensaje}");
                    return (false, mensaje);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var mensaje = $"Error {(int)response.StatusCode}: {errorContent}";
                    _logger?.LogError("❌ {Mensaje}", mensaje);
                    Console.WriteLine($"❌ {mensaje}");
                    return (false, mensaje);
                }
            }
            catch (HttpRequestException ex)
            {
                var mensaje = "Error de conexión con el servidor";
                _logger?.LogError(ex, "❌ Error de conexión");
                Console.WriteLine($"❌ HttpRequestException: {ex.Message}");
                return (false, mensaje);
            }
            catch (Exception ex)
            {
                var mensaje = $"Error inesperado: {ex.Message}";
                _logger?.LogError(ex, "❌ Error inesperado");
                Console.WriteLine($"❌ Exception: {ex}");
                return (false, mensaje);
            }
        }

        // ==================== VERIFICAR CONEXIÓN ====================
        public async Task<(bool conectado, string mensaje)> VerificarConexionAsync()
        {
            try
            {
                Console.WriteLine($"🔌 Verificando conexión a: {_httpClient.BaseAddress}api/facturas");

                var response = await _httpClient.GetAsync("/api/facturas");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"✅ Conexión exitosa - Status: {response.StatusCode}");
                    return (true, $"Conectado correctamente a {_httpClient.BaseAddress}");
                }
                else
                {
                    Console.WriteLine($"⚠️ Respuesta inesperada - Status: {response.StatusCode}");
                    return (false, $"El servidor respondió con código {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Error de conexión: {ex.Message}");
                return (false, $"No se pudo conectar a {_httpClient.BaseAddress}. Verifique que la API esté ejecutándose.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return (false, $"Error al verificar conexión: {ex.Message}");
            }
        }
    }
}