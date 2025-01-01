using Application.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using core.Entities;

namespace Application.Features.Product.Command.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest,Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
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