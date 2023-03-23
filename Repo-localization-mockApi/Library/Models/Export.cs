using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Export
{
    public int Id { get; set; }

    public DateTime? DateExport { get; set; }

    public int? IdUser { get; set; }

    public int? IdRepository { get; set; }

    public decimal? TotalMoney { get; set; }

    public virtual ICollection<ProductImportExport> ProductImportExports { get; } = new List<ProductImportExport>();
}
