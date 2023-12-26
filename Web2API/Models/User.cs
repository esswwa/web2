using System;
using System.Collections.Generic;

namespace Web2API.Models;

public partial class User
{
    public int Iduser { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? Post { get; set; }

    public DateOnly? Dateb { get; set; }

    public int? Salary { get; set; }

    public string? Adress { get; set; }

    public int? Number { get; set; }

    public int? ChildId { get; set; }

    public int? DivisionId { get; set; }

    public virtual Child? Child { get; set; }

    public virtual Division? Division { get; set; }
}
