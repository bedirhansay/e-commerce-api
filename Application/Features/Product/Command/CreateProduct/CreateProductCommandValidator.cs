using FluentValidation;

namespace Application.Features.Product.Command.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Product title is required.")
                .WithName("Ürün adı")
                .MaximumLength(100).WithMessage("Product title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");

            RuleFor(x => x.BrandId)
                .GreaterThan(0).WithMessage("Brand ID must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be 0 or greater.")
                .LessThanOrEqualTo(100).WithMessage("Discount cannot exceed 100%.");

            RuleFor(x => x.CategoryIds)
                .NotNull().WithMessage("At least one category must be provided.")
                .Must(categories => categories.Any()).WithMessage("At least one category must be provided.");
        }
    }
}