using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class migration
{
    public uint id { get; set; }

    public string migration1 { get; set; } = null!;

    public int batch { get; set; }
}
