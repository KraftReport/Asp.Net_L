using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class ChatRoom
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public ulong IsDeleted { get; set; }

    public string? Name { get; set; }

    public string? Photo { get; set; }

    public long? CommunityId { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual Community? Community { get; set; }

    public virtual ICollection<UserChatRoom> UserChatRooms { get; set; } = new List<UserChatRoom>();
}
