using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]

    public virtual ICollection<Call> ? Calls { get; set; } = new List<Call>();
}
