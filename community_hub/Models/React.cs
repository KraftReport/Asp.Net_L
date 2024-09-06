using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class React
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Type { get; set; }

    public long? CommentId { get; set; }

    public long? EventId { get; set; }

    public long? PostId { get; set; }

    public long? ReplyId { get; set; }

    public long? UserId { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Post? Post { get; set; }

    public virtual Reply? Reply { get; set; }

    public virtual User? User { get; set; }
}
