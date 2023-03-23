using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library.Models;

public partial class ProductImport
{
    public int Id { get; set; }

    public int IdProduct { get; set; }

    public int IdImport { get; set; }

    public decimal? Price { get; set; }

    public int? Amount { get; set; }

    public decimal? IntoMoney { get; set; }

    [JsonIgnore]
    public virtual Import IdImportNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ProductImportExport> ProductImportExports { get; } = new List<ProductImportExport>();
}
