using CertUAE.Models;

using CsvHelper;
using CsvHelper.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure; // Required for GetService<IDesignTimeModel>()
using Microsoft.EntityFrameworkCore.Metadata; // Required for IReadOnlyModel, IReadOnlyProperty

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
        private readonly IReadOnlyModel _designTimeModel; // New field for design-time model

        public DataDictionaryService(CertDbContext dbContext)
        {
            _dbContext = dbContext;
            // Get the design-time model via GetService<IDesignTimeModel>()
            _designTimeModel = _dbContext.GetService<IDesignTimeModel>().Model;
        }

        public async Task GenerateDataDictionaryCsv(string outputPath)
        {
            Console.WriteLine("\n--- Generando Diccionario de Datos ---");

            var columnSchemaInfos = new List<ColumnSchemaInfo>();

            try
            {
                // Iterate over all entity types (tables) in the design-time model
                // Use _designTimeModel instead of _dbContext.Model
                foreach (var entityType in _designTimeModel.GetEntityTypes())
                {
                    // Obtener el nombre de la tabla mapeada
                    string tableName = entityType.GetTableName();
                    if (string.IsNullOrEmpty(tableName))
                    {
                        continue;
                    }

                    // Iterar sobre las propiedades (columnas) de la entidad
                    foreach (var property in entityType.GetProperties())
                    {
                        var columnInfo = new ColumnSchemaInfo
                        {
                            TableName = tableName,
                            ColumnName = property.GetColumnName(),
                            NetDataType = property.ClrType.Name,
                            IsNullable = property.IsNullable,
                            IsPrimaryKey = property.IsPrimaryKey(),
                            MaxLength = property.GetMaxLength(),
                            // Correctly get the comment from the design-time property
                            Description = property.GetComment()
                        };

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
                    Delimiter = ";",
                    Encoding = System.Text.Encoding.UTF8,
                    ShouldQuote = args => true
                };

                // IMPORTANT: If you are still using CsvHelper v1.0.0.0 and facing issues
                // with DefaultClassMap, consider the previous recommendation to create
                // an explicit ColumnSchemaInfoMap and register it here:
                // config.RegisterClassMap<ColumnSchemaInfoMap>();

                string filePath = Path.Combine(outputPath, "data_dictionary.csv");
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, config))
                {
                    // If you registered a map, simply call WriteRecords
                    // If not, and you're relying on auto-mapping (and it works with a newer CsvHelper version),
                    // you might need to call WriteHeader<ColumnSchemaInfo>() if you want custom headers
                    // before WriteRecords. But often, WriteRecords is sufficient with auto-mapping.

                    // For auto-mapping with default headers:
                    csv.WriteHeader<ColumnSchemaInfo>(); // Explicitly write headers if not using a map
                    await csv.NextRecordAsync(); // Move to the next line after headers
                    await csv.WriteRecordsAsync(columnSchemaInfos);


                    // If you used the explicit map as suggested in the previous response:
                    // csv.WriteRecords(columnSchemaInfos); // This will write headers and records based on the map
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