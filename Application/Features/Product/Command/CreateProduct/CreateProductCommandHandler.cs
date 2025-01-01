using Application.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Product.Rules;
using core.Entities;
using Product = core.Entities.Product;
namespace Application.Features.Product.Command.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest,Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductRules _productRules;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork ,ProductRules productRules)
    {
        _unitOfWork = unitOfWork;
        _productRules = productRules;
    }

    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {

        IList<core.Entities.Product> products = await _unitOfWork.GetReadRepository<core.Entities.Product>()
            .GetAllAsync();
        
        await _productRules.ProductTitleMustNotBeSame(products, request.Title);
        
        
        core.Entities.Product product = new(request.Title,
            request.Description, request.BrandId, request.Price, request.Discount);
        
        await _unitOfWork.GetWriteRepository<core.Entities.Product>().AddAsync(product);

        var result = await _unitOfWork.SaveAsync();
        if (result > 0)
        {
            foreach (var categoryId in request.CategoryIds)
            {
                await _unitOfWork.GetWriteRepository<ProductCategory>().
                    AddAsync(new ProductCategory { ProductId = product.Id, CategoryId = categoryId });
            }

            if (await _unitOfWork.SaveAsync() > 0)
            {
                
            }
         
        }
        return Unit.Value;
    }
}