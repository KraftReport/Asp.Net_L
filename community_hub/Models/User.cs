using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class User
{
    public long Id { get; set; }

    public string? BannedReason { get; set; }

    public string? RejectReason { get; set; }

    public string? RemovedReason { get; set; }

    public string? Dept { get; set; }

    public string? Division { get; set; }

    public string? Dob { get; set; }

    public ulong Done { get; set; }

    public long? DoorLogNum { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public string? Hobby { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsDeleted { get; set; }

    public ulong IsRejected { get; set; }

    public ulong IsRemoved { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public ulong Pending { get; set; }

    public string? Phone { get; set; }

    public string? Photo { get; set; }

    public string? Role { get; set; }

    public string? StaffId { get; set; }

    public string? Team { get; set; }

    public string? IsOn { get; set; }

    public ulong FullyPermitted { get; set; }

    public int RejectedCount { get; set; }

    public virtual ICollection<Background> Backgrounds { get; set; } = new List<Background>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Mention> Mentions { get; set; } = new List<Mention>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();

    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<React> Reacts { get; set; } = new List<React>();

    public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();

    public virtual ICollection<Share> Shares { get; set; } = new List<Share>();

    public virtual ICollection<UserChatRoom> UserChatRooms { get; set; } = new List<UserChatRoom>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}
