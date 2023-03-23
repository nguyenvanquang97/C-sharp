using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Import
{
    public int Id { get; set; }

    public DateTime? DateImport { get; set; }

    public int? IdUser { get; set; }

    public decimal? TotalMoney { get; set; }

    public int? IdRepository { get; set; }

    public virtual Repository? IdRepositoryNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<ProductImport> ProductImports { get; } = new List<ProductImport>();
}
