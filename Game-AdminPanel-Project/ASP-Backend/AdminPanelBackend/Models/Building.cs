using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdminPanelBackend.Models 
{
    public class Building
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("buildingType")]
        public string BuildingType { get; set; }

        [BsonElement("cost")]
        public int Cost { get; set; }

        [BsonElement("constructionTime")]
        public int ConstructionTime { get; set; }
    }
}
