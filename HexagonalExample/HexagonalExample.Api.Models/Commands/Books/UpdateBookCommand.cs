using System.Collections.Generic;
using HexagonalExample.Api.Models.Commands.Authors;

namespace HexagonalExample.Api.Models.Commands.Books
{
    public class UpdateBookCommand
    {
        public UpdateBookCommand()
        {
            Authors = new List<UpdateAuthorCommand>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<UpdateAuthorCommand> Authors { get; set; }
    }
}
