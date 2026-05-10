using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Program
{
    public int id { get; set; }

    public int level_id { get; set; }

    public string name { get; set; } = null!;

    public string? temario { get; set; }

    // Navigation properties
    public virtual EducationalLevel educational_levels { get; set; } = null!;
    public virtual ICollection<Student> students { get; set; } = new List<Student>();
    public virtual ICollection<Class> classes { get; set; } = new List<Class>();
}