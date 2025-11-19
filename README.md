# 🧾 Sistema de Facturas Cyber- Frontend Blazor

Sistema completo de gestión de facturas con diseño cyberpunk desarrollado en **Blazor Server** con .NET 6.0+.

![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?style=for-the-badge&logo=blazor&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-6.0+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-10.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

## 📋 Tabla de Contenidos

- [Características](#-características)
- [Tecnologías](#-tecnologías)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Configuración](#-configuración)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Uso](#-uso)
- [Módulos](#-módulos)
- [Solución de Problemas](#-solución-de-problemas)
- [Capturas de Pantalla](#-capturas-de-pantalla)

## ✨ Características

### 🎨 Diseño
- ✅ **Interfaz Cyberpunk** con colores rojo y naranja
- ✅ **Diseño responsivo** adaptable a dispositivos móviles
- ✅ **Animaciones fluidas** y transiciones suaves
- ✅ **Vista de tiquete** tipo factura física

### 🔧 Funcionalidades
- ✅ **Crear facturas** con múltiples productos
- ✅ **Editar facturas** existentes (búsqueda por ID o NIT)
- ✅ **Eliminar facturas** con confirmación (búsqueda por NIT)
- ✅ **Buscar facturas** por nombre de cliente
- ✅ **Cálculo automático** de IVA (19%) y totales
- ✅ **Validaciones** en tiempo real
- ✅ **Manejo de errores** detallado con mensajes claros

### 💻 Técnicas
- ✅ **Blazor Server** con renderizado interactivo
- ✅ **Comunicación HTTP** con API REST
- ✅ **Arquitectura de servicios** para lógica de negocio
- ✅ **Inyección de dependencias**
- ✅ **Logging en consola** para depuración

## 🛠️ Tecnologías

### Framework Principal
- **Blazor Server** - Framework de UI interactivo
- **.NET 6.0+** - Plataforma de desarrollo
- **C# 10.0** - Lenguaje de programación

### Bibliotecas y Dependencias
- `System.Net.Http.Json` - Serialización JSON
- `Microsoft.JSInterop` - Interoperabilidad con JavaScript
- `Microsoft.AspNetCore.Components.WebAssembly` - Componentes web

### Estilos y Diseño
- **CSS3** - Estilos personalizados
- **Google Fonts** - Tipografías (Orbitron, Share Tech Mono)
- **Flexbox & Grid** - Layouts responsivos

## 📦 Requisitos Previos

### Software Necesario
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) o superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/) / [VS Code](https://code.visualstudio.com/) / [Rider](https://www.jetbrains.com/rider/)
- [Git](https://git-scm.com/) (opcional)

### API Backend
Este proyecto requiere que la **API REST de Facturas** esté ejecutándose. Por defecto, espera que la API esté en:
```
http://localhost:5294
```

Asegúrate de que el backend esté corriendo antes de iniciar el frontend.

## 🚀 Instalación

### 1. Clonar o Descargar el Proyecto

```bash
# Opción 1: Clonar repositorio
git clone https://github.com/tu-usuario/FacturasCyberpunk.git
cd FacturasCyberpunk

# Opción 2: Descargar ZIP y extraer
```

### 2. Restaurar Dependencias

```bash
dotnet restore
```

### 3. Compilar el Proyecto

```bash
dotnet build
```

## ⚙️ Configuración

### Configurar URL de la API

Abre `Program.cs` y actualiza la URL del backend si es necesario:

```csharp
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5294") // ← Cambiar aquí
});
```

### Configurar Puerto de Blazor (Opcional)

Edita `Properties/launchSettings.json`:

```json
{
  "profiles": {
    "FacturasCyberpunk": {
      "commandName": "Project",
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5062",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## 📂 Estructura del Proyecto

```
FacturasCyberpunk/
│
├── 📁 Components/
│   ├── 📁 Layout/
│   │   ├── MainLayout.razor              # Layout principal
│   │   └── MainLayout.razor.css          # Estilos del layout
│   │
│   ├── 📁 Pages/
│   │   ├── Crear.razor                   # Página: Crear facturas
│   │   ├── Editar.razor                  # Página: Editar facturas
│   │   ├── Eliminar.razor                # Página: Eliminar facturas
│   │   ├── Buscar.razor                  # Página: Buscar facturas
│   │   └── Test.razor                    # Página: Diagnóstico
│   │
│   ├── App.razor                         # Componente raíz
│   ├── Routes.razor                      # Configuración de rutas
│   └── _Imports.razor                    # Imports globales
│
├── 📁 Models/
│   └── Factura.cs                        # DTOs y modelos
│
├── 📁 Services/
│   └── FacturasService.cs                # Servicio de API
│
├── 📁 wwwroot/
│   └── 📁 css/
│       └── app.css                       # Estilos globales
│
├── Program.cs                            # Punto de entrada
├── appsettings.json                      # Configuración
└── FacturasCyberpunk.csproj             # Archivo del proyecto
```

## 🎮 Uso

### Iniciar la Aplicación

```bash
# Terminal 1: Iniciar API (Backend)
cd FacturasAPI
dotnet run

# Terminal 2: Iniciar Blazor (Frontend)
cd FacturasCyberpunk
dotnet run
```

### Acceder a la Aplicación

Abre tu navegador en:
```
http://localhost:5062
```

### Página de Diagnóstico

Para verificar que todo funciona correctamente:
```
http://localhost:5062/test
```

## 📚 Módulos

### 1️⃣ Crear Factura (`/`)

**Ruta:** `/` o `/crear`

**Funcionalidad:**
- Ingresar datos del cliente (Nombre, NIT, Dirección)
- Agregar productos dinámicamente
- Especificar cantidad y precio de cada producto
- Cálculo automático de subtotal, IVA y total
- Vista previa del tiquete al crear

**Validaciones:**
- Cliente es obligatorio
- Mínimo 1 producto
- Cantidad > 0
- Precio > 0

### 2️⃣ Editar Factura (`/editar`)

**Ruta:** `/editar`

**Funcionalidad:**
- Buscar factura por **ID** o **NIT**
- Editar todos los campos (cliente, productos, etc.)
- Agregar o eliminar productos
- Guardar cambios con confirmación

**Búsqueda:**
- Por ID numérico: `1`, `2`, `3`...
- Por NIT: `123456789`

### 3️⃣ Eliminar Factura (`/eliminar`)

**Ruta:** `/eliminar`

**Funcionalidad:**
- Buscar facturas por **NIT** o **Cédula**
- Ver lista de facturas encontradas
- Seleccionar factura visualmente
- Botón de basura 🗑️ en cada factura
- Confirmación antes de eliminar

**Seguridad:**
- Doble confirmación antes de eliminar
- Muestra resumen de la factura a eliminar

### 4️⃣ Buscar Factura (`/buscar`)

**Ruta:** `/buscar`

**Funcionalidad:**
- Buscar por **nombre del cliente**
- Ver lista de resultados
- Seleccionar factura para ver detalles completos
- Vista de tiquete con toda la información

**Resultados:**
- Muestra todas las coincidencias
- Click en factura para ver detalles

### 5️⃣ Diagnóstico (`/test`)

**Ruta:** `/test`

**Funcionalidad:**
- Verificar interactividad de Blazor
- Probar conexión con API
- Diagnóstico de problemas
- Tests de binding y eventos

## 🐛 Solución de Problemas

### Problema: Los botones no responden

**Causa:** Falta `@rendermode InteractiveServer`

**Solución:**
```razor
@page "/tu-pagina"
@rendermode InteractiveServer  ← Agregar esta línea
@inject HttpClient Http
```

### Problema: Error de conexión con API

**Síntomas:**
- `HttpRequestException`
- "No se pudo conectar al servidor"

**Solución:**
1. Verifica que la API esté corriendo:
   ```bash
   cd FacturasAPI
   dotnet run
   ```

2. Verifica la URL en `Program.cs`:
   ```csharp
   BaseAddress = new Uri("http://localhost:5294")
   ```

3. Verifica CORS en la API:
   ```csharp
   app.UseCors("AllowBlazor");
   ```

### Problema: Error 404 al navegar

**Causa:** Rutas mal configuradas

**Solución:**
1. Verifica que `Routes.razor` esté correctamente configurado
2. Verifica que las páginas tengan `@page "/ruta"`
3. Reinicia la aplicación

### Problema: Cambios no se reflejan

**Solución:**
```bash
# Limpiar y reconstruir
dotnet clean
dotnet build
dotnet run
```

### Problema: Warning de HTTPS redirect

**Causa:** `app.UseHttpsRedirection()` activo sin puerto HTTPS

**Solución:**
Comenta la línea en `Program.cs`:
```csharp
// app.UseHttpsRedirection();
```

## 📸 Capturas de Pantalla

### Página Principal - Crear Factura
![Crear Factura](docs/screenshots/crear.png)

### Vista de Tiquete
![Tiquete](docs/screenshots/tiquete.png)

### Editar Factura
![Editar](docs/screenshots/editar.png)

### Buscar Factura
![Buscar](docs/screenshots/buscar.png)

## 🔗 Enlaces Útiles

- [Documentación de Blazor](https://docs.microsoft.com/es-es/aspnet/core/blazor/)
- [.NET Documentation](https://docs.microsoft.com/es-es/dotnet/)
- [Blazor University](https://blazor-university.com/)

## 📝 Notas Importantes

### Renderizado Interactivo
Este proyecto usa **Blazor Server** con renderizado interactivo. Es **esencial** que todas las páginas incluyan:
```razor
@rendermode InteractiveServer
```

### CORS
La API debe tener CORS configurado para aceptar peticiones desde:
```
http://localhost:5062
```

### Base de Datos
Este frontend **requiere** que la API tenga una base de datos configurada y funcionando.

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 👤 Autor

**Pablo Reyes**
- GitHub: [@pablo2240](https://github.com/pablo2240)
- Email: reyestorrespablo22@gmail.com

## 🙏 Agradecimientos

- Comunidad de Blazor y .NET
- Tipografías de Google Fonts (Orbitron, Share Tech Mono)

---

⭐ Si te gustó este proyecto, dale una estrella en GitHub!

**Versión:** 1.0.0  
**Última actualización:** Noviembre 2025
