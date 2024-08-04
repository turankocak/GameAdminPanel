namespace AdminPanelBackend.Settings
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ConfigsCollectionName { get; set; }
        public string BuildingCollectionName { get; set; }
        public string BuildingTypesCollectionName { get; set; }
    }
}
