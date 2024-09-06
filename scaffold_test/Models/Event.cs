using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class Event
{
    public long Id { get; set; }

    public sbyte? Access { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    public string? EventType { get; set; }

    public ulong IsDeleted { get; set; }

    public string? Location { get; set; }

    public string? Photo { get; set; }

    public DateTime? StartDate { get; set; }

    public string? Title { get; set; }

    public long? UserId { get; set; }

    public long? UserGroupId { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();

    public virtual ICollection<React> Reacts { get; set; } = new List<React>();

    public virtual User? User { get; set; }

    public virtual UserGroup? UserGroup { get; set; }

    public virtual ICollection<VoteOption> VoteOptions { get; set; } = new List<VoteOption>();
}
