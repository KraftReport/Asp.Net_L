﻿using System;
using System.Collections.Generic;

namespace ProjectFrameCRUD.Data;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
