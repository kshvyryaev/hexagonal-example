using System.Collections.Generic;

namespace HexagonalExample.Infrastructure.Data.EF.Models
{
    public class BookEFModel : EFBaseModel
    {
        public BookEFModel()
        {
            Authors = new List<AuthorEFModel>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<AuthorEFModel> Authors { get; set; }
    }
}
