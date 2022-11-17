using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class Room
{
    [Key]
    [Column("RoomID")]
    public int RoomId { get; set; }

    [StringLength(30)]
    public string? Description { get; set; }

    [InverseProperty("Room")]
    public virtual ICollection<Meeting> Meetings { get; } = new List<Meeting>();
}
