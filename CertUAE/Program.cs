using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CertUAE.Models; // Asegúrate de que tus modelos estén en este namespace
using CertUAE.Services;
using CertUAE.Utilities;
using Microsoft.Extensions.Configuration; // Necesario para IConfiguration

namespace CertUAE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configuración del Host para Inyección de Dependencias
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // Puedes cargar configuraciones desde appsettings.json si lo deseas
                    // Aunque la cadena de conexión se pedirá dinámicamente,
                    // esto es útil para otras configuraciones o valores por defecto.
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Registrar servicios
                    services.AddTransient<IConnectionService, ConnectionService>();
                    services.AddTransient<IFileAnalysisUtils, FileAnalysisUtils>();
                    services.AddTransient<IDatabaseService, DatabaseService>();
                    services.AddTransient<IFileScannerService, FileScannerService>();

                    // Configurar el DbContext dinámicamente
                    // Aquí es donde se construye la cadena de conexión y se pasa
                    // Esto se hará dentro de ConnectionService y luego se usará para configurar el DbContext
                    // para cada instancia.
                    // Para EF Core, la configuración del DbContext se hace típicamente una vez al inicio.
                    // Aquí, debido a la entrada dinámica, la pasaremos a DatabaseService.
                    // Se crea una instancia de DbContextOptions<CertDbContext> que luego es usada por DatabaseService.
                    services.AddScoped<CertDbContext>(provider =>
                    {
                        var connectionService = provider.GetRequiredService<IConnectionService>();
                        var connectionString = connectionService.GetConnectionString(); // Obtiene la cadena de conexión

                        // Si la cadena de conexión no se pudo obtener, se puede manejar aquí o en el servicio.
                        if (string.IsNullOrEmpty(connectionString))
                        {
                            Console.WriteLine("La cadena de conexión no fue proporcionada o es inválida. La aplicación no puede continuar.");
                            Environment.Exit(1); // Sale de la aplicación
                        }

                        var optionsBuilder = new DbContextOptionsBuilder<CertDbContext>();
                        // Importante: AutoDetect para MariaDB/MySQL
                        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                        return new CertDbContext(optionsBuilder.Options);
                    });
                })
                .Build();

            // Obtener el servicio principal y ejecutar
            using (var scope = host.Services.CreateScope())
            {
                var fileScannerService = scope.ServiceProvider.GetRequiredService<IFileScannerService>();
                fileScannerService.RunScanner();
            }

            Console.WriteLine("\n--- Proceso finalizado. Presiona cualquier tecla para salir ---");
            Console.ReadKey();
        }
    }
}