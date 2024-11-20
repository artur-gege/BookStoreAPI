using BookStoreAPI.Domain.Models;
using BookStoreAPI.Domain.ModelsDTO;

namespace BookStoreAPI.Application.UseCases.Interfaces
{
    public interface IBookUseCase
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<List<Book>> GetBooksByFilterAsync(string title, DateTime? releaseDate);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(BookDTO bookDto);
        Task<Book> UpdateBookAsync(int id, BookDTO bookDto);
        Task DeleteBookAsync(int id);
    }
}
