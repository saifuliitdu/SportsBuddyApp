using SportsBuddy.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBuddy.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}