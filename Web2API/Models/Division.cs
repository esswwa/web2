using System;
using System.Collections.Generic;

namespace Web2API.Models;

public partial class Division
{
    public int IdDivision { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
