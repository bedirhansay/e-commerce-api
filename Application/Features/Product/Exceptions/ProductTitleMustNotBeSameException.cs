using Application.Bases;

namespace Application.Features.Product.Exceptions;

public class ProductTitleMustNotBeSameException:BaseExceptions
{
    public ProductTitleMustNotBeSameException():base("Product Title is the same")
    {
        
    }
}