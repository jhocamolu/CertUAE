using System;
using System.IO;
using System.Security.Cryptography;
using UglyToad.PdfPig;
using CertUAE.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using Spire.Pdf;
using IronOcr;
using System.Linq;

namespace CertUAE.Utilities
{
    public class FileAnalysisUtils : IFileAnalysisUtils
    {
        public FileInfoData GetFileInfo(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            long fileSize = new FileInfo(filePath).Length;
            string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();

            var fileInfo = new FileInfo(filePath);

            string hash = "N/A";
            string hashType = "SHA256";

            try
            {
                using (var sha256 = SHA256.Create())
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
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
                CreatedAt = fileInfo.CreationTime,
                ModifiedAt = fileInfo.LastWriteTime,
                HashCode = fileInfo.GetHashCode(),
                Hash = hash,
                HashType = hashType
            };
        }

        public int? GetPdfPageCount(string filePath)
        {
            try
            {
                using (var document = UglyToad.PdfPig.PdfDocument.Open(filePath))
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

        public PdfMetadata GetPdfMetadata(string filePath)
        {
            var metadata = new PdfMetadata();
            try
            {
                using (var document = UglyToad.PdfPig.PdfDocument.Open(filePath))
                {
                    metadata.PageCount = document.NumberOfPages;

                    var info = document.Information;

                    metadata.Author = info.Author;
                    metadata.Title = info.Title;
                    metadata.Subject = info.Subject;
                    metadata.Keywords = info.Keywords;
                    metadata.Creator = info.Creator;
                    metadata.Producer = info.Producer;

                    if (DateTime.TryParse(info.CreationDate, out DateTime creationDate))
                    {
                        metadata.CreationDate = creationDate;
                    }
                    if (DateTime.TryParse(info.ModifiedDate, out DateTime modDate))
                    {
                        metadata.ModDate = modDate;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener metadatos de PDF {filePath}: {ex.Message}");
                metadata = null;
            }
            return metadata;
        }

        public string GetTiffMetadata(string filePath)
        {
            try
            {
                using (Image<Rgba32> image = Image.Load<Rgba32>(filePath))
                {
                    string formatName = image.Metadata.DecodedImageFormat?.Name ?? "N/A";
                    string dimensions = $"{image.Width}x{image.Height}";
                    string exifInfo = "";

                    var exifProfile = image.Metadata.ExifProfile;

                    if (exifProfile != null)
                    {
                        if (exifProfile.TryGetValue(ExifTag.DateTime, out SixLabors.ImageSharp.Metadata.Profiles.Exif.IExifValue<string> dateTimeValue))
                            exifInfo += $"Fecha y Hora: {dateTimeValue.Value} | ";

                        if (exifProfile.TryGetValue(ExifTag.Make, out SixLabors.ImageSharp.Metadata.Profiles.Exif.IExifValue<string> makeValue))
                            exifInfo += $"Fabricante: {makeValue.Value} | ";

                        if (exifProfile.TryGetValue(ExifTag.Model, out SixLabors.ImageSharp.Metadata.Profiles.Exif.IExifValue<string> modelValue))
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

        // NUEVOS MÉTODOS IMPLEMENTADOS
        public PdfConformanceLevel ValidatePdfDigitalSignature(string filePath)
        {
            try
            {
                Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                doc.LoadFromFile(filePath);
                // Obtener el nivel de conformidad PDF
                PdfConformanceLevel conformance = doc.Conformance;
                return conformance;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al validar PDF/A de {filePath}: {ex.Message}");
                return PdfConformanceLevel.None;
            }
        }



        public bool HasOcrText(string filePath)
        {
            try
            {
                // Creamos el OCR engine
                var ocr = new IronTesseract();
                // Determinamos cuántas páginas tiene el PDF
                int totalPages;
                using (var docs = UglyToad.PdfPig.PdfDocument.Open(filePath)) // O usar otro método para contar
                {
                    totalPages = docs.NumberOfPages;
                }
                List<int> paginas = new List<int>();
                if (totalPages > 10)
                {
                    paginas = Enumerable.Range(0, 10).ToList(); // 0-based: primeras 10 páginas
                }

                // Construimos el input pasando los índices
                using var pdfInput = paginas.Any()
                    ? new OcrPdfInput(filePath, PageIndices: paginas)
                    : new OcrPdfInput(filePath);

                var result = ocr.Read(pdfInput);
                bool tieneTexto = result.Pages.Any(p =>
                    p.Words.Any(w => !string.IsNullOrWhiteSpace(w.Text))
                );

                return tieneTexto;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al detectar OCR en {filePath}: {ex.Message}");
                return false;
            }
        }


    }
}
