using System;
using System.Collections.Generic;

namespace scaffold_test.Models;

public partial class UserSkill
{
    public long Id { get; set; }

    public string? Experience { get; set; }

    public long? SkillId { get; set; }

    public long? UserId { get; set; }

    public virtual Skill? Skill { get; set; }

    public virtual User? User { get; set; }
}
