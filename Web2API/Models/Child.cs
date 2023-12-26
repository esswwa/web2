using System;
using System.Collections.Generic;

namespace Web2API.Models;

public partial class Child
{
    public int Idchild { get; set; }

    public string? Gender { get; set; }

    public string? Name { get; set; }

    public DateOnly? Date { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
