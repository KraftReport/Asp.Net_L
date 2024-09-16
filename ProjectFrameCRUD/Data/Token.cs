using System;
using System.Collections.Generic;

namespace ProjectFrameCRUD.Data;

public partial class Token
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string AccessToken { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }= DateTime.Now;

    public virtual User User { get; set; } = null!;
}
