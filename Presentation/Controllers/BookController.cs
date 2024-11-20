using BookStoreAPI.Application.UseCases.Interfaces;
using BookStoreAPI.Domain.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class BookController : ControllerBase
    {
        private readonly IBookUseCase _bookUseCase;

        public BookController(IBookUseCase bookUseCase)
        {
            _bookUseCase = bookUseCase;
        }

        [HttpGet]
        [Route("books")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _bookUseCase.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route("booksByFilter")]
        public async Task<IActionResult> GetBooksByFilterAsync([FromQuery] string title, [FromQuery] DateTime? releaseDate)
        {
            var books = await _bookUseCase.GetBooksByFilterAsync(title, releaseDate);
            return Ok(books);
        }

        [HttpGet]
        [Route("books/{id}")]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            var book = await _bookUseCase.GetBookByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        [Route("books")]
        public async Task<IActionResult> AddBookAsync([FromBody] BookDTO bookDto)
        {
            var createdBook = await _bookUseCase.AddBookAsync(bookDto);
            return Ok(createdBook);
            
            //return CreatedAtAction(nameof(GetBookByIdAsync), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut]
        [Route("books/{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] BookDTO bookDto)
        {
            var updatedBook = await _bookUseCase.UpdateBookAsync(id, bookDto);
            return Ok(updatedBook);
        }

        [HttpDelete]
        [Route("books/{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            await _bookUseCase.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
