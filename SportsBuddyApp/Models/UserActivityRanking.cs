using SportsBuddy.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBuddy.Models
{
    public class UserActivityRanking
    {
        [Key, Column(Order = 0)]
        public int ActivityId { get; set; }
        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public virtual RecretionalActivity Activity { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Rating Rating { get; set; }
    }
}

