using BookStoreAPI.Domain.ModelsDTO;
using FluentValidation;

namespace BookStoreAPI.Application.Validators
{
    public class OrderItemDTOValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemDTOValidator()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("ID книги обязательно.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Количество должно быть больше нуля.");
        }
    }
}
