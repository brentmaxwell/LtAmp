using LtAmpDotNet.Models;
using System.Threading.Tasks;

namespace LtAmpDotNet.Services
{
    public interface IConfigService
    {
        public SettingsModel Load();

        public Task Save(SettingsModel model);
    }
}