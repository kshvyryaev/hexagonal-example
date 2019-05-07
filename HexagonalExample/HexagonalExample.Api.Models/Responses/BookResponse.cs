using System.Collections.Generic;

namespace HexagonalExample.Api.Models.Responses
{
    public class BookResponse
    {
        public BookResponse()
        {
            Authors = new List<AuthorResponse>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<AuthorResponse> Authors { get; set; }
    }
}
