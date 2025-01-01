using Application.Interface;
using MediatR;

namespace Application.Features.Product.Command.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest,Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {

        var product = await _unitOfWork.GetReadRepository<core.Entities.Product>()
            .GetAsync(x => x.Id == request.Id && !x.IsDeleted);

        if (product == null)
            throw new Exception("Product not found or already deleted");


        var productCategories = await _unitOfWork.GetReadRepository<core.Entities.ProductCategory>()
            .GetAllAsync(x => x.ProductId == product.Id);

        await _unitOfWork.GetWriteRepository<core.Entities.ProductCategory>()
            .HardDeleteRangeAsync(productCategories);


        product.IsDeleted = true;
        await _unitOfWork.GetWriteRepository<core.Entities.Product>().UpdateAsync(product);


        await _unitOfWork.SaveAsync();
        
        return Unit.Value;
    }
    
    
}