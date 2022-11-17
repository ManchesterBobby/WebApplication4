using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [StringLength(30)]
    public string? Description { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual ScheduledMeeting? ScheduledMeeting { get; set; }
}
