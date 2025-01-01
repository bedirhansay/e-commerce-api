using Application.Features.Product.Command.CreateProduct;
using Application.Features.Product.Command.DeleteProduct;
using Application.Features.Product.Command.UpdateProduct;
using Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Prensentation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    // GET: Tüm ürünleri listeleme
    [HttpGet]
    public async Task<ActionResult> GetAllProducts()
    {
        var response = await _mediator.Send(new GetAllProductsQueryRequest());
        return Ok(response);
    }

    // POST: Yeni ürün oluşturma
    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok(new { Message = "Product created successfully!" });
    }

    // PUT: Mevcut ürünü güncelleme
    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok(new { Message = "Product updated successfully!" });
    }

    // DELETE: Ürünü silme
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] int id)
    {
        var request = new DeleteProductCommandRequest { Id = id };
        await _mediator.Send(request);
        return Ok(new { Message = "Product deleted successfully!" });
    }
    
    
}