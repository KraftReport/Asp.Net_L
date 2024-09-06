using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class ChatMessage
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public string? Sender { get; set; }

    public string? VoiceUrl { get; set; }

    public long? RoomId { get; set; }

    public virtual ChatRoom? Room { get; set; }
}
