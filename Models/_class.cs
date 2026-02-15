using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class _class
{
    public ulong id { get; set; }

    public ulong course_id { get; set; }

    public ulong academic_term_id { get; set; }

    public ulong teacher_id { get; set; }

    public string schedule { get; set; } = null!;

    public string room { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual academic_term academic_term { get; set; } = null!;

    public virtual ICollection<activity> activities { get; set; } = new List<activity>();

    public virtual course course { get; set; } = null!;

    public virtual ICollection<group_class> group_classes { get; set; } = new List<group_class>();

    public virtual teacher teacher { get; set; } = null!;
}
