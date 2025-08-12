using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CertUAE.Models;
using CertUAE.Services;
using CertUAE.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; // Importar para logging
using System;
using System.Threading.Tasks;
using System.IO; // Importar para Directory.Exists y Directory.CreateDirectory

namespace CertUAE
{
    public class Program
    {
        public static async Task Main(string[] args)
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
                            Environment.Exit(1); // Sale de la aplicación si no hay cadena de conexión
                        }

                        var optionsBuilder = new DbContextOptionsBuilder<CertDbContext>();
                        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                        return new CertDbContext(optionsBuilder.Options);
                    });
                })
                .Build();

            // Obtener una instancia del logger para la clase Program
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            bool continueRunning = true;
            while (continueRunning) // Bucle principal que permite el reinicio automático
            {
                try
                {
                    using (var scope = host.Services.CreateScope())
                    {
                        // Lógica de selección de funcionalidad
                        Console.WriteLine("\n--- Selecciona una opción ---");
                        Console.WriteLine("1. Escanear archivos y generar reportes.");
                        Console.WriteLine("2. Generar diccionario de datos de la base de datos.");
                        Console.WriteLine("9. Finalizar."); // Opción para salir del programa
                        Console.Write("Tu opción: ");
                        string option = Console.ReadLine();

                        switch (option)
                        {
                            case "1":
                                var fileScannerService = scope.ServiceProvider.GetRequiredService<IFileScannerService>();
                                fileScannerService.RunScanner(); // Asumiendo que RunScanner() es síncrono o maneja su propia asincronía
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
                            case "9":
                                continueRunning = false; // Establece la bandera para salir del bucle
                                Console.WriteLine("Saliendo de la aplicación.");
                                break;
                            default:
                                Console.WriteLine("Opción inválida. Por favor, intenta de nuevo.");
                                break;
                        }
                    }
                    if (continueRunning) // Solo muestra este mensaje si no se ha elegido salir
                    {
                        Console.WriteLine($"Escaneo/Proceso finalizado: {DateTime.Now.ToString(format: "yyyy-MM-dd HH:mm")}");
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey(); // Espera una tecla antes de limpiar la consola
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    // Registra la excepción utilizando el logger
                    logger.LogError(ex, "Ocurrió un error inesperado en la aplicación. El programa intentará reiniciar.");

                    // Informa al usuario sobre el error
                    Console.WriteLine($"\n¡ERROR! Ocurrió un error inesperado: {ex.Message}");
                    Console.WriteLine("El programa intentará reiniciar en 5 segundos...");
                    await Task.Delay(5000); // Espera 5 segundos antes de volver a intentar
                    Console.Clear(); // Limpia la consola para un "reinicio" visual
                }
            }

            Console.WriteLine("\n--- Proceso finalizado. Presiona cualquier tecla para salir ---");
            Console.ReadKey(); // Espera la entrada del usuario antes de que la consola se cierre
        }
    }
}
