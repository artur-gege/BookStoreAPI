using BookStoreAPI.Domain.ModelsDTO;
using FluentValidation;

namespace BookStoreAPI.Application.Validators
{
    public class CreateOrderDTOValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidator()
        {
            RuleFor(x => x.OrderItems).NotEmpty().WithMessage("Заказ должен содержать хотя бы один товар.");
            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemDTOValidator()); // Вложенная валидация
        }
    }
}
