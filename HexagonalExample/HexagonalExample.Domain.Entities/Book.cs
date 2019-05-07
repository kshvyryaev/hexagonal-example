using System;
using System.Collections.Generic;

namespace HexagonalExample.Domain.Entities
{
    public class Book
    {
        #region Constructors

        public Book()
        {
            Authors = new List<Author>();
        }

        #endregion Constructors

        #region Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Author> Authors { get; set; }

        #endregion Properties

        #region Methods

        public static string GetKeyForCache(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return nameof(Book) + id;
        }

        public string GetKeyForCache()
        {
            return GetKeyForCache(Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Book book) || book.Authors.Count != Authors.Count)
            {
                return false;
            }

            for (int i = 0; i < book.Authors.Count; i++)
            {
                Author authorOnComparison = book.Authors[i];
                Author currentAuthor = Authors[i];

                if (!authorOnComparison.Equals(currentAuthor))
                {
                    return false;
                }
            }

            return book.Id == Id && book.Name == Name && book.Description == Description;
        }

        public override int GetHashCode()
        {
            int hash = Id.GetHashCode() ^ Name.GetHashCode() ^ Description.GetHashCode();

            foreach (var author in Authors)
            {
                hash ^= author.GetHashCode();
            }

            return hash;
        }

        #endregion Methods
    }
}
