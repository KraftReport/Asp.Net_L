using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class Community
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public ulong IsActive { get; set; }

    public string? Name { get; set; }

    public string? OwnerName { get; set; }

    public string? GroupAccess { get; set; }

    public virtual ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}
