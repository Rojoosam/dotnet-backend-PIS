using System;

namespace SIADAL.Models;

public partial class Enrollment
{
    public int student_id { get; set; }

    public int class_id { get; set; }

    public DateTime enrolled_at { get; set; }

    // Navigation properties
    public virtual Student student { get; set; } = null!;
    public virtual Class _class { get; set; } = null!;
}
