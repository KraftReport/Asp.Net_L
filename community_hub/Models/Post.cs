using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class Post
{
    public long Id { get; set; }

    public string? Access { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? Description { get; set; }

    public ulong IsDeleted { get; set; }

    public string? PostType { get; set; }

    public string? Url { get; set; }

    public long? UserId { get; set; }

    public long? UserGroupId { get; set; }

    public ulong Hide { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Mention> Mentions { get; set; } = new List<Mention>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<React> Reacts { get; set; } = new List<React>();

    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();

    public virtual ICollection<Share> Shares { get; set; } = new List<Share>();

    public virtual User? User { get; set; }

    public virtual UserGroup? UserGroup { get; set; }
}
