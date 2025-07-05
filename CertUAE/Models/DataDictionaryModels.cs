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
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string SqlDataType { get; set; } // Tipo de dato SQL (ej: VARCHAR(255), INT, DATETIME)
        public string NetDataType { get; set; } // Tipo de dato C# (ej: string, int, DateTime)
        public bool IsNullable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string ForeignKeyTable { get; set; } // Tabla a la que apunta la clave foránea
        public string ForeignKeyColumn { get; set; } // Columna a la que apunta la clave foránea
        public int? MaxLength { get; set; } // Longitud máxima para cadenas
        public string Description { get; set; } // Para que el usuario pueda añadir descripciones
    }
}
