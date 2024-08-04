using AdminPanelBackend.Repositories;
using AdminPanelBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminPanelBackend.Services
{
    public class ConfigService
    {
        private readonly ConfigRepository _configRepository;

        public ConfigService(ConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public async Task<List<Config>> GetAllAsync()
        {
            return await _configRepository.GetAllAsync();
        }

        public async Task<Config> GetByIdAsync(string id)
        {
            return await _configRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Config config)
        {
            await _configRepository.AddAsync(config);
        }

        public async Task UpdateAsync(string id, Config configIn)
        {
            await _configRepository.UpdateAsync(id, configIn);
        }

        public async Task RemoveAsync(string id)
        {
            await _configRepository.RemoveAsync(id);
        }
    }
}
