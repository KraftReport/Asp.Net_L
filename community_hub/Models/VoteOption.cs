using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class VoteOption
{
    public long Id { get; set; }

    public string? Type { get; set; }

    public long? EventId { get; set; }

    public ulong IsDeleted { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();
}
