using Application.DTOs;

namespace Application.Features.Product.Queries;

public class GetAllProductsQueryResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; } 
    public BrandDto Brand { get; set; }
}