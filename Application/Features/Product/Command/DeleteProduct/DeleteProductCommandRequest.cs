using MediatR;

namespace Application.Features.Product.Command.DeleteProduct;

public class DeleteProductCommandRequest : IRequest<Unit>
{
    public int Id { get; set; } 
}