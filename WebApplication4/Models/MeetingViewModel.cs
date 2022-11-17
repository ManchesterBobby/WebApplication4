using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class MeetingViewModel
    {
        [StringLength(30)]
        [Required]
        public string Description { get; set; } = null!;

        public int RoomId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime MeetingTime { get; set; }

    }
}
