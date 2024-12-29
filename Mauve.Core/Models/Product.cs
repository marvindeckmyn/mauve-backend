using Mauve.Core.Common;

namespace Mauve.Core.Models;

public class Product : BaseEntity {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Style { get; set; } // e.g., "Grunge", "Streetwear", ...
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool IsActive { get; set; }
    public string MainImage { get; set; }
    public List<string> AdditionalImages { get; set; } = new();
    public List<ProductVariant> Variants { get; set; } = new();
    public SupplierInfo SupplierInfo { get; set; }
    public List<string> Tags { get; set; } = new();
}

public class ProductVariant : BaseEntity
{
    public Guid ProductId { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string SKU { get; set; }
    public int StockLevel { get; set; }
    public Product Product { get; set; }
    public decimal? VariantPrice { get; set; } // Optional variant-specific price
}

public class SupplierInfo : BaseEntity
{
    public Guid ProductId { get; set; }
    public string SupplierSKU { get; set; }
    public decimal SupplierPrice { get; set; }
    public int ProcessingTime { get; set; }
    public DateTime LastSyncTime { get; set; }
    public bool IsInStock { get; set; }
    public string SupplierName { get; set; }
    public Product Product { get; set; }
}