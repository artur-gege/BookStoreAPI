using BookStoreAPI.Domain.ModelsDTO;
using FluentValidation;

namespace BookStoreAPI.Application.Validators
{
    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Название книги обязательно.");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Автор обязательно.");
            RuleFor(x => x.ReleaseDate).NotEmpty().WithMessage("Дата выхода обязательна.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Цена должна быть больше нуля.");
        }
    }
}
