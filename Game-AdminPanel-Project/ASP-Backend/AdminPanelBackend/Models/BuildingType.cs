using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdminPanelBackend.Models
{
    public class BuildingType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }
    }
}
