using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class UserGroup
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public long? CommunityId { get; set; }

    public long? UserId { get; set; }

    public virtual Community? Community { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual User? User { get; set; }
}
