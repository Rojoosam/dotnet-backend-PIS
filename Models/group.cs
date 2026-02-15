using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class group
{
    public ulong id { get; set; }

    public string name { get; set; } = null!;

    public ulong academic_term_id { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual academic_term academic_term { get; set; } = null!;

    public virtual ICollection<group_class> group_classes { get; set; } = new List<group_class>();
}
