﻿using System;
using System.Collections.Generic;

namespace hub_app.Models;

public partial class Member
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}
