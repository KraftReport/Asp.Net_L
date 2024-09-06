using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class Comment
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public DateTime? LocalDateTime { get; set; }

    public long? PostId { get; set; }

    public long? UserId { get; set; }

    public virtual ICollection<Mention> Mentions { get; set; } = new List<Mention>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Post? Post { get; set; }

    public virtual ICollection<React> Reacts { get; set; } = new List<React>();

    public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();

    public virtual User? User { get; set; }
}
