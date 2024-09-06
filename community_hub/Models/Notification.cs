using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class Notification
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public long? CommentId { get; set; }

    public long? EventId { get; set; }

    public long? MentionId { get; set; }

    public long? PostId { get; set; }

    public long? ReactId { get; set; }

    public long? ReplyId { get; set; }

    public long? ShareId { get; set; }

    public long? UserId { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Mention? Mention { get; set; }

    public virtual Post? Post { get; set; }

    public virtual React? React { get; set; }

    public virtual Reply? Reply { get; set; }

    public virtual Share? Share { get; set; }

    public virtual User? User { get; set; }
}
