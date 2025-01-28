using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Personnel_Tracking_App.DataBase;

public partial class Permission
{
   [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }

    public int EmployeeId { get; set; }
    public int UserNo {  get; set; }

    public DateTime PermissionStartDate { get; set; }

    public DateTime PermissionEndDate { get; set; }

    public int PermissionState { get; set; }

    public string? PermissionExplanation { get; set; }

    public int PermissionAmount { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual PermissionState PermissionStateNavigation { get; set; } = null!;
}
