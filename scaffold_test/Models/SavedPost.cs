﻿using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class SavedPost
{
    public long Id { get; set; }

    public long? PostId { get; set; }

    public DateTime? SavedDate { get; set; }

    public long? SaverId { get; set; }
}
