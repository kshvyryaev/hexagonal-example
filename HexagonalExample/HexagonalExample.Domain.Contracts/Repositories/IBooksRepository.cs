using System.Collections.Generic;
using System.Threading.Tasks;
using HexagonalExample.Domain.Entities;

namespace HexagonalExample.Domain.Contracts.Repositories
{
    public interface IBooksRepository
    {
        Task<Book> CreateAsync(Book book);

        Task<Book> GetAsync(string id);

        Book Get(string id);

        Task<IReadOnlyCollection<Book>> GetAllAsync();

        Task<Book> UpdateAsync(Book book);

        Task<bool> DeleteAsync(string id);
    }
}
