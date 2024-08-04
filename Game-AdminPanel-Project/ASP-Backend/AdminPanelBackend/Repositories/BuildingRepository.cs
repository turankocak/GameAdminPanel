using MongoDB.Driver;
using AdminPanelBackend.Models;
using AdminPanelBackend.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminPanelBackend.Repositories
{
    public class BuildingRepository
    {
        private readonly IMongoCollection<Building> _buildings;

        public BuildingRepository(IMongoDBSettings settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _buildings = database.GetCollection<Building>(settings.BuildingCollectionName);
        }
        
        public async Task<List<Building>> GetAllAsync()
        {
            return await _buildings.Find(_ => true).ToListAsync();
        }
        
        public async Task<Building> GetByIdAsync(string id)
        {
            return await _buildings.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Building building)
        {
            await _buildings.InsertOneAsync(building);
        }

        public async Task UpdateAsync(string id, Building buildingIn)
        {
            var updateDefinition = Builders<Building>.Update
                .Set(b => b.BuildingType, buildingIn.BuildingType)
                .Set(b => b.Cost, buildingIn.Cost)
                .Set(b => b.ConstructionTime, buildingIn.ConstructionTime);

            await _buildings.UpdateOneAsync(b => b.Id == id, updateDefinition);
        }
        
        public async Task RemoveAsync(string id)
        {
            await _buildings.DeleteOneAsync(b => b.Id == id);
        }
    }
}
