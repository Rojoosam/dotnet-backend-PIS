using System;

namespace SIADAL.Models;

public partial class Submission
{
    public int id { get; set; }

    public int assignment_id { get; set; }

    public int student_id { get; set; }

    public DateTime submitted_at { get; set; }

    public int? grade { get; set; }

    public string? file_url { get; set; }

    // Navigation properties
    public virtual Assignment assignment { get; set; } = null!;
    public virtual Student student { get; set; } = null!;
}
