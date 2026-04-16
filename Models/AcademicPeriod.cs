using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class AcademicPeriod
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public DateOnly start_date { get; set; }

    public DateOnly end_date { get; set; }

    public bool is_active { get; set; } = false;

    // Navigation properties
    public virtual ICollection<Class> classes { get; set; } = new List<Class>();
}
