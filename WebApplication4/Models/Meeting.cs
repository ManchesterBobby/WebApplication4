using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class Meeting
{
    [Key]
    [Column("MeetingID")]
    public int MeetingId { get; set; }

    [StringLength(30)]
    [Required]
    public string Description { get; set; } = null!;

    [Column("RoomID")]
    public int RoomId { get; set; }

    [Column(TypeName = "datetime")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
    public DateTime MeetingTime { get; set; }

    [ForeignKey("RoomId")]
    [InverseProperty("Meetings")]
    public virtual Room Room { get; set; } = null!;

    [InverseProperty("Id1")]
    public virtual ScheduledMeeting? ScheduledMeeting { get; set; }
}
