using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Teacher
{
    public ulong id { get; set; }

    public ulong user_id { get; set; }

    public int employee_number { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<Class> _classes { get; set; } = new List<Class>();

    public virtual User user { get; set; } = null!;
}
