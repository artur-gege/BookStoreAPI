using BookStoreAPI.Domain.Models;
using BookStoreAPI.Domain.ModelsDTO;
using System.Linq.Expressions;

namespace BookStoreAPI.Application.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book?>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<List<Book?>> GetBooksByFilterAsync(string title, DateTime? releaseDate);
        Task<Book> AddBookAsync(Book bookToCreate);
        Task<Book> UpdateBookAsync(Book bookToUpdate);
        Task DeleteBookAsync(int id);
        Task<bool> IsBookExistsAsync(BookDTO bookDto);
        Task SaveAsync();
    }
}
