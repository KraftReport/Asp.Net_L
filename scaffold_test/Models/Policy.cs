﻿using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class Policy
{
    public long Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Rule { get; set; }

    public long? UserId { get; set; }

    public virtual User? User { get; set; }
}
