using System.ComponentModel.DataAnnotations;

namespace SportsBuddy.Models
{
    public class ActivityType
    {
        [Key]
        public int ActivityTypeId { get; set; }
        public string ActivityTypeName { get; set; }
    }
}