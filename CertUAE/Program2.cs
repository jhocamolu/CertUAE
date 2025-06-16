using CertUAE.Models;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System.Security.Cryptography;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using UglyToad.PdfPig; // Para PDF

public class FileScanner3
{
    public static void Main3(string[] args)
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
        List<PdfReportRow> pdfReport = new List<PdfReportRow>();
        List<TiffReportRow> tiffReport = new List<TiffReportRow>();


        Console.WriteLine($"\n--- Escaneando directorio y subdirectorios: {targetDirectory} ---\n");

        foreach (var dir in Directory.GetDirectories(targetDirectory, "*", SearchOption.AllDirectories).Prepend(targetDirectory))
        {
            Console.WriteLine($"\n-- Carpeta: {dir} --");

            var files = Directory.GetFiles(dir);
            var pdfs = files.Where(f => f.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)).ToList();
            var tiffs = files.Where(f => f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var pdfPath in pdfs)
            {
                var fileData = GetFileInfo(pdfPath);
                int? pageCount = GetPdfPageCount(pdfPath);

                Console.WriteLine($"PDF: {fileData.Name} - Páginas: {pageCount}");

                int diff = pageCount.HasValue ? (pageCount.Value - tiffs.Count) : 0;

                pdfReport.Add(new PdfReportRow
                {
                    Nombre = fileData.Name,
                    Ruta = fileData.Path,
                    TamanoKB = fileData.SizeBytes / 1024,
                    TamanoMB = fileData.SizeBytes / (1024.0 * 1024.0),
                    TamanoGB = fileData.SizeBytes / (1024.0 * 1024.0 * 1024.0),
                    Paginas = pageCount.Value,
                    CantidadTiffs = tiffs.Count,
                    DiferenciaTiffsVsPaginas = diff
                });

                if (pageCount.HasValue)
                {
                    if (pageCount.Value == tiffs.Count)
                        Console.WriteLine($"✅ Coincidencia: {fileData.Name} tiene {pageCount} páginas y {tiffs.Count} TIFFs.");
                    else
                        Console.WriteLine($"❌ Diferencia: {fileData.Name} tiene {pageCount} páginas y {tiffs.Count} TIFFs.");
                }
            }

            foreach (var tiffPath in tiffs)
            {
                var fileData = GetFileInfo(tiffPath);
                try
                {
                    using (Image image = Image.Load(tiffPath))
                    {
                        tiffReport.Add(new TiffReportRow
                        {
                            Nombre = fileData.Name,
                            Ruta = fileData.Path,
                            TamanoKB = fileData.SizeBytes / 1024,
                            TamanoMB = fileData.SizeBytes / (1024.0 * 1024.0),
                            TamanoGB = fileData.SizeBytes / (1024.0 * 1024.0 * 1024.0),
                            Alto = image.Height,
                            Ancho = image.Width
                        });
                    }
                }
                catch
                {
                    Console.WriteLine($"⚠️ No se pudo leer imagen TIFF: {fileData.Path}");
                }
            }
        }

        string basePath = Path.Combine(targetDirectory, "export_resultados");
        Directory.CreateDirectory(basePath);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Encoding = Encoding.UTF8,
            ShouldQuote = _ => true
        };

        using (var writer = new StreamWriter(Path.Combine(basePath, "pdf_report.csv")))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteHeader<PdfReportRow>();
            csv.NextRecord();
            csv.WriteRecords(pdfReport);
        }

        using (var writer = new StreamWriter(Path.Combine(basePath, "tiff_report.csv")))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteHeader<TiffReportRow>();
            csv.NextRecord();
            csv.WriteRecords(tiffReport);
        }

        Console.WriteLine($"\n✅ Exportación completada en: {basePath}");
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
            using (var document = PdfDocument.Open(filePath))
            {
                return document.NumberOfPages;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer PDF {filePath}: {ex.Message}");
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
                        exifInfo += $"Fecha y Hora: {dateTimeValue.Value} | ";

                    if (exifProfile.TryGetValue(ExifTag.Make, out IExifValue<string> makeValue))
                        exifInfo += $"Fabricante: {makeValue.Value} | ";

                    if (exifProfile.TryGetValue(ExifTag.Model, out IExifValue<string> modelValue))
                        exifInfo += $"Modelo: {modelValue.Value} | ";

                    if (string.IsNullOrEmpty(exifInfo))
                        exifInfo = "(Sin metadatos EXIF detallados)";
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
