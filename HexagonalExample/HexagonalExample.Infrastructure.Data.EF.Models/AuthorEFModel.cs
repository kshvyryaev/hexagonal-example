namespace HexagonalExample.Infrastructure.Data.EF.Models
{
    public class AuthorEFModel : EFBaseModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int BookId { get; set; }

        public BookEFModel Book { get; set; }
    }
}
