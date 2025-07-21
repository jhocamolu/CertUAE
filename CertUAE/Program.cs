using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CertUAE.Models;
using CertUAE.Services;
using CertUAE.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CertUAE
{
    public class Program
    {
        public static async Task Main(string[] args) // Cambiado a async Task Main
        {
            // Configuración del Host para Inyección de Dependencias
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Registrar servicios
                    services.AddTransient<IConnectionService, ConnectionService>();
                    services.AddTransient<IFileAnalysisUtils, FileAnalysisUtils>();
                    services.AddTransient<IDatabaseService, DatabaseService>();
                    services.AddTransient<IFileScannerService, FileScannerService>();
                    services.AddTransient<IDataDictionaryService, DataDictionaryService>(); // Nuevo servicio

                    // Configurar el DbContext dinámicamente
                    services.AddScoped<CertDbContext>(provider =>
                    {
                        var connectionService = provider.GetRequiredService<IConnectionService>();
                        var connectionString = connectionService.GetConnectionString(); // Obtiene la cadena de conexión

                        if (string.IsNullOrEmpty(connectionString))
                        {
                            Console.WriteLine("La cadena de conexión no fue proporcionada o es inválida. La aplicación no puede continuar.");
                            Environment.Exit(1);
                        }

                        var optionsBuilder = new DbContextOptionsBuilder<CertDbContext>();
                        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                        return new CertDbContext(optionsBuilder.Options);
                    });
                })
                .Build();

            do
            {

                using (var scope = host.Services.CreateScope())
                {
                    // Lógica de selección de funcionalidad
                    Console.WriteLine("\n--- Selecciona una opción ---");
                    Console.WriteLine("1. Escanear archivos y generar reportes.");
                    Console.WriteLine("2. Generar diccionario de datos de la base de datos.");
                    Console.Write("Tu opción: ");
                    string option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            var fileScannerService = scope.ServiceProvider.GetRequiredService<IFileScannerService>();
                            fileScannerService.RunScanner();
                            break;
                        case "2":
                            var dataDictionaryService = scope.ServiceProvider.GetRequiredService<IDataDictionaryService>();
                            Console.Write("Introduce la ruta para guardar el diccionario de datos (ej: C:\\temp\\reports): ");
                            string outputPath = Console.ReadLine();
                            if (!Directory.Exists(outputPath))
                            {
                                Console.WriteLine($"Creando directorio de salida: {outputPath}");
                                Directory.CreateDirectory(outputPath);
                            }
                            await dataDictionaryService.GenerateDataDictionaryCsv(outputPath); // Llamada asíncrona
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Saliendo.");
                            break;
                    }
                }
                Console.WriteLine($"Escaneo directorio finalizo: {DateTime.Now.ToString(format: "yyyy-MM-dd HH:mm")}");
                Console.WriteLine("0. Regresar Menu.");
                Console.WriteLine("9. Finalizar.");
            } while ("0" == Console.ReadLine());
            Console.WriteLine("\n--- Proceso finalizado. Presiona cualquier tecla para salir ---");
            Console.ReadKey();
        }
    }
}

