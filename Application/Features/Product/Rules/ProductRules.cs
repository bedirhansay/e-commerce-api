using Application.Bases;
using Application.Features.Product.Exceptions;

namespace Application.Features.Product.Rules;

public class ProductRules: BaseRules
{
    public Task ProductTitleMustNotBeSame(IList<core.Entities.Product> products, string productTitle)
    {
        if( products.Any(x=>x.Title== productTitle) ) throw new ProductTitleMustNotBeSameException();
        return Task.CompletedTask;
    }
}