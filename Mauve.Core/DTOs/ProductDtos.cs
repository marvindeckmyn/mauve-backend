using System.ComponentModel.DataAnnotations;

namespace Mauve.Core.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool IsActive { get; set; }
    public string MainImage { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedAt { get; set; }
    // Don't expose SupplierInfo here - private business data
}

public class CreateProductDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public string Category { get; set; }

    public string MainImage { get; set; }
    public List<string> Tags { get; set; }
}