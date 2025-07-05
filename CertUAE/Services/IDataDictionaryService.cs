using System.Threading.Tasks;

namespace CertUAE.Services
{
    public interface IDataDictionaryService
    {
        Task GenerateDataDictionaryCsv(string outputPath);
    }
}
