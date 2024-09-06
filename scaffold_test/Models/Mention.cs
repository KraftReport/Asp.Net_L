using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class Mention
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public long? PostedUserId { get; set; }

    public long? CommentId { get; set; }

    public long? PostId { get; set; }

    public long? UserId { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
