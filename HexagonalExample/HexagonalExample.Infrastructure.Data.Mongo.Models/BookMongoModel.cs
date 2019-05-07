using System.Collections.Generic;

namespace HexagonalExample.Infrastructure.Data.Mongo.Models
{
    public class BookMongoModel : MongoBaseModel
    {
        public BookMongoModel()
        {
            Authors = new List<AuthorMongoModel>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<AuthorMongoModel> Authors { get; set; }

        public override void SetMissedIdentifiers()
        {
            foreach (var author in Authors)
            {
                author.SetMissedIdentifiers();
            }

            base.SetMissedIdentifiers();
        }
    }
}
