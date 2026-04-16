using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class User
{
    public int id { get; set; }

    public string email { get; set; } = null!;

    public string password_hash { get; set; } = null!;

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public bool is_active { get; set; } = true;

    public DateTime created_at { get; set; }

    // Navigation properties
    public virtual ICollection<role_user> role_users { get; set; } = new List<role_user>();
    public virtual Student? student { get; set; }
    public virtual Teacher? teacher { get; set; }
}
