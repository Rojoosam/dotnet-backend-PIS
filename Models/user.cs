using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class User
{
    public ulong id { get; set; }

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public bool is_active { get; set; }

    public string email { get; set; } = null!;

    public DateTime? email_verified_at { get; set; }

    public string password { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<Student> students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> teachers { get; set; } = new List<Teacher>();
}
