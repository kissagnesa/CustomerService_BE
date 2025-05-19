using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Call
{
    public int CallId { get; set; }

    public DateTime CallDate { get; set; }

    public int AgentId { get; set; }

    public int CustomerId { get; set; }

    public int PhoneNumber { get; set; }

    public TimeSpan CallTime { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
