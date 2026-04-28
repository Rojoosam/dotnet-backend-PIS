using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Teacher
{
    public int id { get; set; }

    public int user_id { get; set; }

    public string employee_number { get; set; } = null!;

    // Navigation properties
    public virtual User user { get; set; } = null!;
    public virtual ICollection<Class> classes { get; set; } = new List<Class>();
}
