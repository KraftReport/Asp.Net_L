using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class Invitation
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public ulong IsAccepted { get; set; }

    public ulong IsInvited { get; set; }

    public ulong IsRemoved { get; set; }

    public long? RecipientId { get; set; }

    public long? SenderId { get; set; }

    public long? CommunityId { get; set; }

    public ulong IsRequested { get; set; }

    public virtual Community? Community { get; set; }
}
