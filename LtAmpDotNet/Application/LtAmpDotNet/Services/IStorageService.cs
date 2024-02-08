using System.Threading.Tasks;

namespace LtAmpDotNet.Services
{
    public interface IStorageService
    {
        Task<bool> SaveAsync<T>(string fileName, T? data);

        Task<T?> LoadAsync<T>(string fileName);
    }
}