namespace CertUAE.Models
{
    // Modelo para representar la información de una tabla en el diccionario de datos
    public class TableSchemaInfo
    {
        public string TableName { get; set; }
        public string Description { get; set; } // Para que el usuario pueda añadir descripciones
    }

    // Modelo para representar la información de una columna en el diccionario de datos
    public class ColumnSchemaInfo
    {
        public ColumnSchemaInfo() { } // Explicit parameterless constructor (good practice)

        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string NetDataType { get; set; }
        public string SqlDataType { get; set; }
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public int? MaxLength { get; set; }
        public string? Description { get; set; }
        public bool IsForeignKey { get; set; }
        public string ForeignKeyTable { get; set; }
        public string ForeignKeyColumn { get; set; }
    }
}
