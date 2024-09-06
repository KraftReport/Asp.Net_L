using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class Background
{
    public long Id { get; set; }

    public string? BackgroundUrl { get; set; }

    public long? UserId { get; set; }

    public virtual User? User { get; set; }
}
