using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class activity
{
    public ulong id { get; set; }

    public ulong class_id { get; set; }

    public ulong student_id { get; set; }

    public string name { get; set; } = null!;

    public decimal grade { get; set; }

    public decimal porcentage { get; set; }

    public string status { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual _class _class { get; set; } = null!;

    public virtual student student { get; set; } = null!;
}
