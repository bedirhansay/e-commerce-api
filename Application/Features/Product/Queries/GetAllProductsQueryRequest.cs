using MediatR;

namespace Application.Features.Product.Queries;

public class GetAllProductsQueryRequest:IRequest<IList<GetAllProductsQueryResponse>>
{
    
}