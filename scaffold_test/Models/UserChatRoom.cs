using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class UserChatRoom
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public long? RoomId { get; set; }

    public long? UserId { get; set; }

    public virtual ChatRoom? Room { get; set; }

    public virtual User? User { get; set; }
}
