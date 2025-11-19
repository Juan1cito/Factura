using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DisableBrowserLink))]

public class DisableBrowserLink : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        // No hacer nada — BrowserLink no existe en .NET moderno
    }
}
