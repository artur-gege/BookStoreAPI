using AutoMapper;
using BookStoreAPI.Application.CustomExceptions;
using BookStoreAPI.Application.Repositories.Interfaces;
using BookStoreAPI.Application.UseCases.Interfaces;
using BookStoreAPI.Domain.Models;
using BookStoreAPI.Domain.ModelsDTO;
using FluentValidation;

namespace BookStoreAPI.Application.UseCases.Implementations
{
    public class BookUseCase : IBookUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<BookDTO> _bookValidator;

        public BookUseCase(IBookRepository bookRepository, IMapper mapper, IValidator<BookDTO> bookValidator)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _bookValidator = bookValidator;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books;
        }

        public async Task<List<Book>> GetBooksByFilterAsync(string title, DateTime? releaseDate)
        {
            var books = await _bookRepository.GetBooksByFilterAsync(title, releaseDate);
            return books;
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                throw new BookNotFoundException($"Книга с ID {id} не найдена.");
            }

            return existingBook;
        }

        public async Task<Book> AddBookAsync(BookDTO bookDto)
        {
            var validationResult = _bookValidator.Validate(bookDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _bookRepository.IsBookExistsAsync(bookDto))
            {
                throw new BookAlreadyExistsException($"Книга с таким названием и автором уже существует.");
            }

            var book = _mapper.Map<Book>(bookDto);

            var createdBook = await _bookRepository.AddBookAsync(book);

            return createdBook;
        }


        public async Task<Book> UpdateBookAsync(int id, BookDTO bookDto)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                throw new BookNotFoundException($"Книга с ID {id} не найдена.");
            }

            var validationResult = _bookValidator.Validate(bookDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var bookToUpdate = _mapper.Map<Book>(bookDto);

            var updatedBook = await _bookRepository.UpdateBookAsync(bookToUpdate);

            return updatedBook;
        }

        public async Task DeleteBookAsync(int id)
        {
            var bookToDelete = await _bookRepository.GetBookByIdAsync(id);

            if (bookToDelete == null)
            {
                throw new BookNotFoundException($"Книга с ID {id} не найдена.");
            }

            await _bookRepository.DeleteBookAsync(id);
        }
    }
}
