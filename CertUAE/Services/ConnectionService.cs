using System;

namespace CertUAE.Services
{
    public class ConnectionService : IConnectionService
    {
        public string GetConnectionString()
        {
            Console.WriteLine("\n--- Configuración de la Conexión a la Base de Datos ---");
            Console.WriteLine("¿Cómo deseas proporcionar la cadena de conexión?");
            Console.WriteLine("1. Ingresar servidor, puerto, usuario y contraseña por separado.");
            Console.WriteLine("2. Ingresar la cadena de conexión completa.");
            Console.Write("Selecciona una opción (1 o 2): ");

            string choice = Console.ReadLine();
            string connectionString = string.Empty;

            if (choice == "1")
            {
                Console.Write("Servidor (ej: localhost): ");
                string server = Console.ReadLine();

                Console.Write("Puerto (ej: 3306): ");
                string port = Console.ReadLine();

                Console.Write("Usuario (ej: root): ");
                string uid = Console.ReadLine();

                Console.Write("Contraseña (dejar en blanco si no tiene): ");
                string pwd = Console.ReadLine();

                // Aquí usamos el nombre de la base de datos que ya sabemos del scaffolding
                string databaseName = "bd_limpia_snr";

                connectionString = $"Server={server};Port={port};Database={databaseName};Uid={uid};Pwd={pwd};";
            }
            else if (choice == "2")
            {
                Console.Write("Ingresa la cadena de conexión completa (ej: Server=localhost;Port=3306;Database=bd_limpia_snr;Uid=root;Pwd=;): ");
                connectionString = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Opción inválida. Se utilizará la cadena de conexión predeterminada (si existe) o la aplicación saldrá.");
                // Retorna null o string.Empty para que el Program.cs pueda manejar el error
                return string.Empty;
            }

            Console.WriteLine($"\nCadena de conexión generada: {connectionString}");
            return connectionString;
        }
    }
}
