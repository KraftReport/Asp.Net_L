using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class Background
{
    public long Id { get; set; }

    public string? BackgroundUrl { get; set; }

    public long? UserId { get; set; }

    public virtual User? User { get; set; }
}
