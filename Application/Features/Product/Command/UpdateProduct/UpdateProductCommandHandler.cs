using Application.Interface;
using Application.Interface.AutoMapper;
using MediatR;

namespace Application.Features.Product.Command.UpdateProduct;

public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommandRequest,Unit>

{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ICustomMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
       var product = await _unitOfWork.GetReadRepository<core.Entities.Product>()
           .GetAsync(x => x.Id == request.Id && !x.IsDeleted);
       
       var map = _mapper.Map<core.Entities.Product, UpdateProductCommandRequest>(request);
       
       var productCategories = 
           await _unitOfWork.GetReadRepository<core.Entities.ProductCategory>()
               .GetAllAsync(x => x.ProductId == product.Id);

       await _unitOfWork.GetWriteRepository<core.Entities.ProductCategory>()
           .HardDeleteRangeAsync(productCategories);

       foreach (var c in request.CategoryIds)
           await _unitOfWork.GetWriteRepository<core.Entities.ProductCategory>()
               .AddAsync(new(){CategoryId = c, ProductId = product.Id });

       await _unitOfWork.GetWriteRepository<core.Entities.Product>().UpdateAsync(product);
           await _unitOfWork.SaveAsync();
           
           return Unit.Value;
    }
}