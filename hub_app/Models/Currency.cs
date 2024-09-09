using System;
using System.Collections.Generic;

namespace hub_app.Models;

public partial class Currency
{
    public int Id { get; set; }

    public string PlateType { get; set; } = null!;

    public int PlateCount { get; set; }

    public int MemberId { get; set; }
}
