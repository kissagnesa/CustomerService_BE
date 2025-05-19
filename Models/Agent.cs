using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Agent
{
    public int AgentId { get; set; }

    public string AgentName { get; set; } = null!;

    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();
}
