using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdSupplier { get; set; }

    public decimal? Price { get; set; }

    public string? Unit { get; set; }

    public virtual Supplier? IdSupplierNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<ProductImport> ProductImports { get; } = new List<ProductImport>();
}
