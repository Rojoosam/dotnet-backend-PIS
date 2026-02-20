using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Group_class
{
    public ulong id { get; set; }

    public ulong group_id { get; set; }

    public ulong class_id { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual Class _class { get; set; } = null!;

    public virtual Group group { get; set; } = null!;
}
