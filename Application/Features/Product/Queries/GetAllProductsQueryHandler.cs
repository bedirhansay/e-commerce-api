using Application.DTOs;
using Application.Interface;
using Application.Interface.AutoMapper;
using core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Product.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomMapper _mapper;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork ,ICustomMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    

    public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    { 
        var products = await _unitOfWork.GetReadRepository<core.Entities.Product>
                ().GetAllAsync(include:x=> x.Include(x=>x.Brand));

        var brand = _mapper.Map<BrandDto, Brand>(new Brand());
        var map = _mapper.Map<GetAllProductsQueryResponse,core.Entities.Product>(products);

        
        foreach (var item in map)
            item.Price -= (item.Price * item.Discount)/100;
        return map;
    }
} 