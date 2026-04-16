using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class role_user
{
    public int user_id { get; set; }

    public int role_id { get; set; }

    public virtual User user { get; set; } = null!;

    public virtual Role role { get; set; } = null!;
}
