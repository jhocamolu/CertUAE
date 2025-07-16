using CsvHelper.Configuration;
using CertUAE.Models; // Make sure to include the namespace for ColumnSchemaInfo

public sealed class ColumnSchemaInfoMap : ClassMap<ColumnSchemaInfo>
{
    public ColumnSchemaInfoMap()
    {
        Map(m => m.TableName).Name("Tabla");
        Map(m => m.ColumnName).Name("Columna");
        Map(m => m.NetDataType).Name("Tipo de Dato (.NET)");
        Map(m => m.SqlDataType).Name("Tipo de Dato (SQL)");
        Map(m => m.IsNullable).Name("Permite Nulos");
        Map(m => m.IsPrimaryKey).Name("Es Clave Primaria");
        Map(m => m.IsForeignKey).Name("Es Clave Foránea");
        Map(m => m.ForeignKeyTable).Name("Tabla Foránea");
        Map(m => m.ForeignKeyColumn).Name("Columna Foránea");
        Map(m => m.MaxLength).Name("Longitud Máxima");
        Map(m => m.Description).Name("Descripción");
        // Add other properties as needed
    }
}