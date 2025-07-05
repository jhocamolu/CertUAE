using CertUAE.Models;

using CsvHelper;
using CsvHelper.Configuration;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CertUAE.Services
{
    public class DataDictionaryService : IDataDictionaryService
    {
        private readonly CertDbContext _dbContext;

        public DataDictionaryService(CertDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task GenerateDataDictionaryCsv(string outputPath)
        {
            Console.WriteLine("\n--- Generando Diccionario de Datos ---");

            var columnSchemaInfos = new List<ColumnSchemaInfo>();

            try
            {
                // Iterar sobre todos los tipos de entidad (tablas) en el modelo de EF Core
                foreach (var entityType in _dbContext.Model.GetEntityTypes())
                {
                    // Obtener el nombre de la tabla mapeada
                    string tableName = entityType.GetTableName();
                    if (string.IsNullOrEmpty(tableName)) // Si no hay nombre de tabla (ej: vista sin mapeo directo), saltar
                    {
                        continue;
                    }

                    // Iterar sobre las propiedades (columnas) de la entidad
                    foreach (var property in entityType.GetProperties())
                    {
                        var columnInfo = new ColumnSchemaInfo
                        {
                            TableName = tableName,
                            ColumnName = property.GetColumnName(), // Nombre de la columna en la BD
                            NetDataType = property.ClrType.Name, // Tipo de dato C#
                            IsNullable = property.IsNullable,
                            IsPrimaryKey = property.IsPrimaryKey(),
                            MaxLength = property.GetMaxLength(), // Longitud máxima para strings
                            Description = "" // Deja un campo vacío para descripciones manuales
                        };

                        // Intentar obtener el tipo de dato SQL (puede ser más complejo y requerir un proveedor específico)
                        // Para Pomelo.EntityFrameworkCore.MySql, puedes intentar obtener el tipo de columna.
                        // property.GetColumnType() a veces devuelve el tipo SQL, pero no siempre es preciso o incluye la longitud.
                        // Para una mayor precisión del tipo SQL, podrías necesitar inspeccionar la base de datos directamente
                        // o confiar en la inferencia de EF Core.
                        columnInfo.SqlDataType = property.GetColumnType() ?? property.ClrType.Name;


                        // Verificar si es una clave foránea
                        var foreignKey = entityType.FindForeignKeys(property).FirstOrDefault();
                        if (foreignKey != null)
                        {
                            columnInfo.IsForeignKey = true;
                            columnInfo.ForeignKeyTable = foreignKey.PrincipalEntityType.GetTableName();
                            columnInfo.ForeignKeyColumn = foreignKey.PrincipalKey.Properties.First().GetColumnName();
                        }
                        else
                        {
                            columnInfo.IsForeignKey = false;
                            columnInfo.ForeignKeyTable = "";
                            columnInfo.ForeignKeyColumn = "";
                        }

                        columnSchemaInfos.Add(columnInfo);
                    }
                }

                // Configuración para CsvHelper
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    Encoding = System.Text.Encoding.UTF8,
                    ShouldQuote = args => true // Asegura que todos los campos se citen, útil para descripciones
                };

                // Escribir la información en un archivo CSV
                string filePath = Path.Combine(outputPath, "data_dictionary.csv");
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteHeader<ColumnSchemaInfo>();
                    await csv.NextRecordAsync();
                    await csv.WriteRecordsAsync(columnSchemaInfos);
                }

                Console.WriteLine($"✅ Diccionario de datos generado exitosamente en: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al generar el diccionario de datos: {ex.Message}");
                Console.WriteLine($"Detalle: {ex.StackTrace}");
            }
        }
    }
}
