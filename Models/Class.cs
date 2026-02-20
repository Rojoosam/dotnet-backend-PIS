using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Class
{
    public ulong id { get; set; }

    public ulong course_id { get; set; }

    public ulong academic_term_id { get; set; }

    public ulong teacher_id { get; set; }

    public string schedule { get; set; } = null!;

    public string room { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual Academic_term academic_term { get; set; } = null!;

    public virtual ICollection<Activity> activities { get; set; } = new List<Activity>();

    public virtual Course course { get; set; } = null!;

    public virtual ICollection<Group_class> group_classes { get; set; } = new List<Group_class>();

    public virtual Teacher teacher { get; set; } = null!;
}
