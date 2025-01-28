using System;
using System.Collections.Generic;

namespace Company_Personnel_Tracking_App.DataBase;

public partial class PermissionState
{
    public int Id { get; set; }

    public string StateName { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
