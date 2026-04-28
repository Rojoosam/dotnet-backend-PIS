using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class Role
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    // Navigation properties
    public virtual ICollection<role_user> role_users { get; set; } = new List<role_user>();
}
