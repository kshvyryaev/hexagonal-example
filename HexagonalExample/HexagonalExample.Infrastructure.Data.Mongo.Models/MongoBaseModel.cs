using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HexagonalExample.Infrastructure.Data.Mongo.Models
{
    public abstract class MongoBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public virtual void SetMissedIdentifiers()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = ObjectId.GenerateNewId().ToString();
            }
        }
    }
}
