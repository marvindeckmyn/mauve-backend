using Mauve.Core.Interfaces;
using Mauve.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mauve.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var products = await _productRepository.GetPagedResponseAsync(pageNumber, pageSize);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        try {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return NotFound($"Product with ID {id} not found");

            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        try
        {
            if (product == null)
                return BadRequest("Product data is null");

            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            var createdProduct = await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
    {
        try
        {
            if (product == null)
                return BadRequest("Product data is null");
            
            if (id != product.Id)
                return BadRequest("ID mismatch");

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound($"Product with ID {id} not found");

            product.UpdatedAt = DateTime.UtcNow;
            product.CreatedAt = existingProduct.CreatedAt; // Prereserve original creation date

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateProductStatus(Guid id, [FromBody] bool isActive)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found");

            product.IsActive = isActive;
            product.UpdatedAt = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found");

            await _productRepository.DeleteAsync(product);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}