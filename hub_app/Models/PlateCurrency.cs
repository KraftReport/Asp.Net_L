using System;
using System.Collections.Generic;

namespace hub_app.Models;

public partial class PlateCurrency
{
    public int Id { get; set; }

    public string PlateType { get; set; } = null!;

    public int MemberId { get; set; }
}
