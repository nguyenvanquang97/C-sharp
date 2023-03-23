using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class ProductImportExport
{
    public int Id { get; set; }

    public int? IdExport { get; set; }

    public int? IdProductImport { get; set; }

    public int? AmountSold { get; set; }

    public decimal? SalePrice { get; set; }

    public decimal? IntoMoney { get; set; }

    public virtual Export? IdExportNavigation { get; set; }

    public virtual ProductImport? IdProductImportNavigation { get; set; }
}
