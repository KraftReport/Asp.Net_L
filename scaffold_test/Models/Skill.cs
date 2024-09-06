using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class Skill
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}
