using System;
using System.Collections.Generic;

namespace community_hub.Models;

public partial class Resource
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public string? Raw { get; set; }

    public string? Video { get; set; }

    public long? PostId { get; set; }

    public virtual Post? Post { get; set; }
}
