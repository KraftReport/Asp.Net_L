using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class UserAccessLog
{
    public long Id { get; set; }

    public DateTime? AccessTime { get; set; }

    public string? Email { get; set; }

    public string? ErrorMessage { get; set; }

    public string? Type { get; set; }
}
