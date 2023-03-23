using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library.Models;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    [JsonIgnore]
    public virtual ICollection<Import> Imports { get; } = new List<Import>();

    public virtual ICollection<Repository> IdRepositories { get; } = new List<Repository>();
}
