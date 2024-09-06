using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class Poll
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Type { get; set; }

    public long? EventId { get; set; }

    public long? UserId { get; set; }

    public long? VoteOptionId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User? User { get; set; }

    public virtual VoteOption? VoteOption { get; set; }
}
