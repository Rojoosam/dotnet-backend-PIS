using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Course
{
    public ulong id { get; set; }

    public string name { get; set; } = null!;

    public string code { get; set; } = null!;

    public int credits { get; set; }

    public string desciption { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<Class> _classes { get; set; } = new List<Class>();
}
