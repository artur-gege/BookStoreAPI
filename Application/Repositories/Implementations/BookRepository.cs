using BookStoreAPI.Application.Repositories.Interfaces;
using BookStoreAPI.Domain.Models;
using BookStoreAPI.Domain.ModelsDTO;
using BookStoreAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Application.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        protected readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>?> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> AddBookAsync(Book bookToCreate)
        {
            _context.Books.Add(bookToCreate);
            await SaveAsync();
            return bookToCreate;
        }
        public async Task<Book> UpdateBookAsync(Book bookToUpdate)
        {
            _context.Entry(bookToUpdate).State = EntityState.Modified;
            await SaveAsync();
            return bookToUpdate;
        }

        public async Task DeleteBookAsync(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);

            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                await SaveAsync();
            }
        }

        public async Task<List<Book>?> GetBooksByFilterAsync(string title, DateTime? releaseDate)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (releaseDate.HasValue)
            {
                query = query.Where(b => b.ReleaseDate == releaseDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> IsBookExistsAsync(BookDTO bookDto)
        {
            return await _context.Books.AnyAsync(b => b.Title == bookDto.Title && b.Author == bookDto.Author);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
