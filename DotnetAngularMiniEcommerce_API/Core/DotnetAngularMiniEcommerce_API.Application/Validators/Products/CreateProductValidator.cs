using DotnetAngularMiniEcommerce_API.Application.ViewModels.Products;
using FluentValidation;

namespace DotnetAngularMiniEcommerce_API.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator() {

            RuleFor(product => product.Name)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Lüfen ürün adını 5 ile 150 karakter arasında giriniz.");

            RuleFor(product => product.Stock)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Lütfen stok bilgisini boş geçmeyiniz")
                .Must(stock => stock >= 0)
                    .WithMessage("Stok bilgisi negatif olamaz!");

            RuleFor(product => product.Price)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz.")
                .Must(s => s >= 0)
                    .WithMessage("Fiyat bilgisi negatif olamaz!");

        }
    }
}
