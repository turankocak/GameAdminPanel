using MongoDB.Driver;
using AdminPanelBackend.Models;
using AdminPanelBackend.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminPanelBackend.Repositories
{
    public class ConfigRepository
    {
        private readonly IMongoCollection<Config> _configsCollection;

        public ConfigRepository(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _configsCollection = database.GetCollection<Config>(settings.ConfigsCollectionName);
        }

        public async Task<List<Config>> GetAllAsync()
        {
            
            return await _configsCollection.Find(config => true).ToListAsync();
        }

        public async Task<Config> GetByIdAsync(string id)
        {
            
            return await _configsCollection.Find(config => config.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Config config)
        {
            
            await _configsCollection.InsertOneAsync(config);
        }

        public async Task UpdateAsync(string id, Config configIn)
        {
            
            await _configsCollection.ReplaceOneAsync(config => config.Id == id, configIn);
        }

        public async Task RemoveAsync(string id)
        {
            
            await _configsCollection.DeleteOneAsync(config => config.Id == id);
        }
    }
}
