using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Class
{
    public int id { get; set; }

    public int period_id { get; set; }

    public int program_id { get; set; }

    public int teacher_id { get; set; }

    public string name { get; set; } = null!;

    public string? schedule_json { get; set; }

    // Navigation properties
    public virtual AcademicPeriod academic_period { get; set; } = null!;
    public virtual Program program { get; set; } = null!;
    public virtual Teacher teacher { get; set; } = null!;
    public virtual ICollection<Enrollment> enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<Assignment> assignments { get; set; } = new List<Assignment>();
}
