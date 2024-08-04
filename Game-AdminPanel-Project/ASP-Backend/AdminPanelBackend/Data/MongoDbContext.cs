using MongoDB.Driver;
using AdminPanelBackend.Models; 
using Microsoft.EntityFrameworkCore;


namespace AdminPanelBackend.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("ConfigDatabase");
        }

        public IMongoCollection<Config> Configs => _database.GetCollection<Config>("Configs");
    }
}
