using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class role
{
    public ulong id { get; set; }

    public string name { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }
}
