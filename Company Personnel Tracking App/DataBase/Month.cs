using System;
using System.Collections.Generic;

namespace Company_Personnel_Tracking_App.DataBase;

public partial class Month
{
    public int Id { get; set; }

    public string MonthName { get; set; } = null!;

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();
}
