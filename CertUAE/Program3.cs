using CertUAE.Models;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

using System.Security.Cryptography;

using UglyToad.PdfPig; // Para PDF

public class FileScanner2
{
    public static void Main2(string[] args)
    {
        Console.Write("Por favor, introduce la ruta del directorio a escanear: ");
        string targetDirectory = Console.ReadLine();

        if (!Directory.Exists(targetDirectory))
        {
            Console.WriteLine($"Error: La ruta '{targetDirectory}' no es un directorio válido.");
            return;
        }

        ProcessDirectory(targetDirectory);

    }

    public static void ProcessDirectory(string targetDirectory)
    {
        Dictionary<string, FileInfoData> pdfFiles = new Dictionary<string, FileInfoData>();
        int tiffCountInDir = 0;
        List<string> pdfCsvLines = new List<string>
        {
            "Nombre,Ruta,Tamaño KB,Tamaño MB,Tamaño GB,Cantidad Páginas,Cantidad TIFFs en Carpeta,Diferencia TIFFs vs Páginas"
        };

        List<string> tiffCsvLines = new List<string>
        {
            "Nombre,Ruta,Tamaño KB,Tamaño MB,Tamaño GB,Alto,Ancho"
        };


        Console.WriteLine($"\n--- Escaneando directorio: {targetDirectory} ---\n");

        foreach (string filePath in Directory.EnumerateFiles(targetDirectory))
        {
            FileInfoData fileData = GetFileInfo(filePath);

            Console.WriteLine($"Archivo: {fileData.Name}");
            Console.WriteLine($"  Ruta: {fileData.Path}");
            Console.WriteLine($"  Tamaño: {fileData.SizeBytes} bytes ({fileData.SizeBytes / (1024.0 * 1024.0):F2} MB)");
            Console.WriteLine($"  Hash ({fileData.HashType}): {fileData.Hash}");
            Console.WriteLine($"  Extensión: {fileData.Extension}");

            if (fileData.Extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"  Tipo: PDF");
                int? pageCount = GetPdfPageCount(filePath);
                Console.WriteLine($"  Páginas: {(pageCount.HasValue ? pageCount.ToString() : "N/A (Error al leer)")}");
                if (pageCount.HasValue)
                {
                    long sizeKB = fileData.SizeBytes / 1024;
                    double sizeMB = fileData.SizeBytes / (1024.0 * 1024.0);
                    double sizeGB = fileData.SizeBytes / (1024.0 * 1024.0 * 1024.0);

                    int diff = pageCount.Value - tiffCountInDir;

                    pdfCsvLines.Add(
                        $"\"{fileData.Name}\",\"{fileData.Path}\",{sizeKB},{sizeMB:F2},{sizeGB:F4},{pageCount.Value},{tiffCountInDir},{diff}"
                    );
                }

                pdfFiles[fileData.Name] = fileData;
                pdfFiles[fileData.Name].PdfPageCount = pageCount;
            }
            else if (fileData.Extension.Equals(".tiff", StringComparison.OrdinalIgnoreCase) || fileData.Extension.Equals(".tif", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"  Tipo: TIFF");
                string tiffMetadata = GetTiffMetadata(filePath);
                Console.WriteLine($"  Metadatos TIFF: {tiffMetadata}");
                tiffCountInDir++;
                try
                {
                    using (Image image = Image.Load(filePath))
                    {
                        long sizeKB = fileData.SizeBytes / 1024;
                        double sizeMB = fileData.SizeBytes / (1024.0 * 1024.0);
                        double sizeGB = fileData.SizeBytes / (1024.0 * 1024.0 * 1024.0);

                        tiffCsvLines.Add(
                            $"\"{fileData.Name}\",\"{fileData.Path}\",{sizeKB},{sizeMB:F2},{sizeGB:F4},{image.Height},{image.Width}"
                        );
                    }
                }
                catch
                {
                    // Opcional: puedes registrar el error aquí
                }

            }
            else
            {
                Console.WriteLine($"  Tipo: Otro (no PDF/TIFF)");
            }
            Console.WriteLine(new string('-', 30));
        }

        Console.WriteLine("\n--- Informe Comparativo ---\n");
        Console.WriteLine($"Cantidad total de archivos TIFF encontrados en '{targetDirectory}': {tiffCountInDir}\n");

        if (pdfFiles.Count == 0)
        {
            Console.WriteLine("No se encontraron archivos PDF para comparar.");
        }
        else
        {
            foreach (var entry in pdfFiles)
            {
                string pdfName = entry.Key;
                FileInfoData pdfData = entry.Value;

                if (pdfData.PdfPageCount.HasValue)
                {
                    if (tiffCountInDir == pdfData.PdfPageCount.Value)
                    {
                        Console.WriteLine($"Coincidencia: El PDF '{pdfName}' tiene {pdfData.PdfPageCount.Value} páginas, que es igual a la cantidad de archivos TIFF ({tiffCountInDir}).");
                    }
                    else
                    {
                        Console.WriteLine($"NO Coincidencia: El PDF '{pdfName}' tiene {pdfData.PdfPageCount.Value} páginas, pero la cantidad de archivos TIFF es {tiffCountInDir}.");
                    }
                }
                else
                {
                    Console.WriteLine($"No se pudo determinar el número de páginas del PDF '{pdfName}' para la comparación.");
                }
            }
        }
        string basePath = Path.Combine(targetDirectory, "export_resultados");
        Directory.CreateDirectory(basePath);

        File.WriteAllLines(Path.Combine(basePath, "pdf_report.csv"), pdfCsvLines, Encoding.UTF8);
        File.WriteAllLines(Path.Combine(basePath, "tiff_report.csv"), tiffCsvLines, Encoding.UTF8);

        Console.WriteLine($"✅ Exportación completada en: {basePath}");
    }

    public static FileInfoData GetFileInfo(string filePath)
    {
        string fileName = Path.GetFileName(filePath);
        long fileSize = new FileInfo(filePath).Length;
        string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();

        string hash = "N/A";
        string hashType = "SHA256";

        try
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        catch (Exception ex)
        {
            hash = $"Error calculando hash: {ex.Message}";
            hashType = "N/A";
        }

        return new FileInfoData
        {
            Name = fileName,
            Path = filePath,
            SizeBytes = fileSize,
            Extension = fileExtension,
            Hash = hash,
            HashType = hashType
        };
    }

    public static int? GetPdfPageCount(string filePath)
    {
        try
        {
            // Usando PdfPig
            using (var document = PdfDocument.Open(filePath))
            {
                return document.NumberOfPages;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer PDF {filePath}: {ex.Message}"); // Descomentar para depurar
            return null;
        }
    }

    public static string GetTiffMetadata(string filePath)
    {
        try
        {
            using (Image image = Image.Load(filePath))
            {
                string formatName = image.Metadata.DecodedImageFormat?.Name ?? "N/A";
                string dimensions = $"{image.Width}x{image.Height}";

                string exifInfo = "";
                var exifProfile = image.Metadata.ExifProfile;

                if (exifProfile != null)
                {
                    if (exifProfile.TryGetValue(ExifTag.DateTime, out IExifValue<string> dateTimeValue))
                    {
                        exifInfo += $"Fecha y Hora: {dateTimeValue.Value} | ";
                    }
                    if (exifProfile.TryGetValue(ExifTag.Make, out IExifValue<string> makeValue))
                    {
                        exifInfo += $"Fabricante: {makeValue.Value} | ";
                    }
                    if (exifProfile.TryGetValue(ExifTag.Model, out IExifValue<string> modelValue))
                    {
                        exifInfo += $"Modelo: {modelValue.Value} | ";
                    }

                    if (string.IsNullOrEmpty(exifInfo))
                    {
                        exifInfo = "(Sin metadatos EXIF detallados)";
                    }
                }
                else
                {
                    exifInfo = "(Sin perfil EXIF)";
                }

                return $"Formato: {formatName} | Dimensiones: {dimensions} | EXIF: {exifInfo.TrimEnd(' ', '|')}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

}
