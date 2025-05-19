using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();
}
