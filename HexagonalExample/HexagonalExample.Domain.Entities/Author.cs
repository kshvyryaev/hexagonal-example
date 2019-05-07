namespace HexagonalExample.Domain.Entities
{
    public class Author
    {
        #region Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Author author))
            {
                return false;
            }

            return author.Id == Id && author.Name == Name && author.Surname == Surname;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode() ^ Surname.GetHashCode();
        }

        #endregion Methods
    }
}
