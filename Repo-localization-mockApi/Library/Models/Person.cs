using System;
using System.Collections.Generic;

namespace Library.Models;
public partial class Person
{
    public int? Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? Avatar { get; set; }
}