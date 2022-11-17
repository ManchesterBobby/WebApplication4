using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class ScheduledMeeting
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MeetingID")]
    public int MeetingId { get; set; }

    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("ScheduledMeeting")]
    public virtual Meeting Id1 { get; set; } = null!;

    [ForeignKey("Id")]
    [InverseProperty("ScheduledMeeting")]
    public virtual Employee IdNavigation { get; set; } = null!;
}
