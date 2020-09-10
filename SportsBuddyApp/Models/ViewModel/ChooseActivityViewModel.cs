using SportsBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddyApp.Models.ViewModel
{
    public class ChooseActivityViewModel
    {
        public RecretionalActivity Activity { get; set; }
        public bool IsChoosen { get; set; }
        public int Rating { get; set; }
    }
}
