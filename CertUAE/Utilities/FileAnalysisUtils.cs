using System;
using System.IO;
using System.Security.Cryptography;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using UglyToad.PdfPig; // Para PDF
using CertUAE.Models; // Para FileInfoData y PdfMetadata

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

        public PdfMetadata GetPdfMetadata(string filePath)
        {
            var metadata = new PdfMetadata();
            try
            {
                using (var document = PdfDocument.Open(filePath))
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
    }
}
