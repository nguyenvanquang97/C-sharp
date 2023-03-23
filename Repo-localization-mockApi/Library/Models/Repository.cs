using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library.Models;
public partial class Repository
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    [JsonIgnore]
    public virtual ICollection<Import> Imports { get; } = new List<Import>();
    [JsonIgnore]
    public virtual ICollection<User> IdUsers { get; } = new List<User>();
}
