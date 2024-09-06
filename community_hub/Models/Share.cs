using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class Share
{
    public long Id { get; set; }

    public string? Caption { get; set; }

    public DateTime? Date { get; set; }

    public long? PostId { get; set; }

    public long? UserId { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
