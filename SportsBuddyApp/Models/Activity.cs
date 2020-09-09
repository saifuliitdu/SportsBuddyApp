using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBuddy.Models
{
    public class RecretionalActivity
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public ICollection<UserActivityRanking> UserActivityRankings { get; set; }
    }
}

