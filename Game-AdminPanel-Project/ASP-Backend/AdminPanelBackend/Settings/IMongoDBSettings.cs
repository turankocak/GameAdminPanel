namespace AdminPanelBackend.Settings
{
    public interface IMongoDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ConfigsCollectionName { get; set; }
        string BuildingCollectionName { get; set; }
        string BuildingTypesCollectionName { get; set; }
    }
}
