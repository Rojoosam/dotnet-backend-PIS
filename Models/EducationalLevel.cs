using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class EducationalLevel
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    // Navigation properties
    public virtual ICollection<Program> programs { get; set; } = new List<Program>();
}
