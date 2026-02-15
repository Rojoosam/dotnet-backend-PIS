using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class role_user
{
    public ulong user_id { get; set; }

    public ulong role_id { get; set; }

    public virtual role role { get; set; } = null!;

    public virtual user user { get; set; } = null!;
}
