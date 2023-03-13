using System;
using System.Collections.Generic;

namespace Test.Models;

public partial class Book
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdAuthor { get; set; }

    public int? IdCategory { get; set; }

    public virtual Author? IdAuthorNavigation { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }
}
