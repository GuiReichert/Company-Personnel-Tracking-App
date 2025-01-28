using System;
using System.Collections.Generic;

namespace Company_Personnel_Tracking_App.DataBase;

public partial class TaskState
{
    public int Id { get; set; }

    public string StateName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
