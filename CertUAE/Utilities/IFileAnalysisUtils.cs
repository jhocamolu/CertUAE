using System.IO; // Para Stream
using CertUAE.Models; // Para FileInfoData y PdfMetadata
using Spire.Pdf;

namespace CertUAE.Utilities
{
    public interface IFileAnalysisUtils
    {
        FileInfoData GetFileInfo(string filePath);
        int? GetPdfPageCount(string filePath); // Mantener para obtener solo el conteo si es necesario
        PdfMetadata GetPdfMetadata(string filePath); // Nuevo método para metadatos PDF
        string GetTiffMetadata(string filePath);

        PdfConformanceLevel ValidatePdfDigitalSignature(string filePath);// SELLADO
        bool HasOcrText(string filePath); // OCR
    }
}
