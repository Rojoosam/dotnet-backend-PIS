using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Assignment
{
    public int id { get; set; }

    public int class_id { get; set; }

    public string name { get; set; } = null!;

    public DateTime? duedate { get; set; }

    public int points { get; set; } = 0;

    public string? details { get; set; }

    // Navigation properties
    public virtual Class _class { get; set; } = null!;
    public virtual ICollection<Submission> submissions { get; set; } = new List<Submission>();
}
