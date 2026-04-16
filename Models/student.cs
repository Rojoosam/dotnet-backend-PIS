using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Student
{
    public int id { get; set; }

    public int user_id { get; set; }

    public int program_id { get; set; }

    public string enrollment_number { get; set; } = null!;

    public DateOnly birth_date { get; set; }

    // Navigation properties
    public virtual User user { get; set; } = null!;
    public virtual Program program { get; set; } = null!;
    public virtual ICollection<Enrollment> enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<Submission> submissions { get; set; } = new List<Submission>();
}
