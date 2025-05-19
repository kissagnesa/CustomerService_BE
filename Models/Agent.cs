using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Agent
{
    public int AgentId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]

    public virtual ICollection<Call> ? Calls { get; set; } = new List<Call>();
}
