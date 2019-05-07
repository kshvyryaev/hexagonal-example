namespace HexagonalExample.Infrastructure.Data.Mongo.Models
{
    public class AuthorMongoModel : MongoBaseModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
