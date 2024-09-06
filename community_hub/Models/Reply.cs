using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class Reply
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public DateTime? LocalDateTime { get; set; }

    public long? CommentId { get; set; }

    public long? UserId { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<React> Reacts { get; set; } = new List<React>();

    public virtual User? User { get; set; }
}
