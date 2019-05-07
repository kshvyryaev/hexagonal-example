using System.Collections.Generic;
using HexagonalExample.Api.Models.Commands.Authors;

namespace HexagonalExample.Api.Models.Commands.Books
{
    public class CreateBookCommand
    {
        public CreateBookCommand()
        {
            Authors = new List<CreateAuthorCommand>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<CreateAuthorCommand> Authors { get; set; }
    }
}
