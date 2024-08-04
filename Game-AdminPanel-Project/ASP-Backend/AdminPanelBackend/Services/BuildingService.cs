using MongoDB.Driver;
using AdminPanelBackend.Models;
using AdminPanelBackend.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminPanelBackend.Services
{
    public class BuildingService
    {
        private readonly IMongoCollection<Building> _buildings;
        private readonly IMongoCollection<BuildingType> _buildingTypes;

        public BuildingService(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _buildings = database.GetCollection<Building>(settings.BuildingCollectionName);
            _buildingTypes = database.GetCollection<BuildingType>(settings.BuildingTypesCollectionName);
        }

        public async Task<List<Building>> GetAllAsync()
        {
            return await _buildings.Find(building => true).ToListAsync();
        }

        public async Task<Building> GetByIdAsync(string id)
        {
            return await _buildings.Find(building => building.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Building building)
        {
            await _buildings.InsertOneAsync(building);
        }

        public async Task UpdateAsync(string id, Building buildingIn)
        {
            await _buildings.ReplaceOneAsync(building => building.Id == id, buildingIn);
        }

        public async Task RemoveAsync(string id)
        {
            await _buildings.DeleteOneAsync(building => building.Id == id);
        }

        public async Task<List<BuildingType>> GetBuildingTypesAsync()
        {
            return await _buildingTypes.Find(type => true).ToListAsync();
        }
    }
}
