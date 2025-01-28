using System;
using System.Collections.Generic;

namespace Company_Personnel_Tracking_App.DataBase;

public partial class Salary
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int Amount { get; set; }

    public int MonthId { get; set; }

    public int Year { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Month Month { get; set; } = null!;
}
