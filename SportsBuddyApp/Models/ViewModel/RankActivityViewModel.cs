﻿using SportsBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddyApp.Models.ViewModel
{
    public class RankActivityViewModel
    {
        public RecretionalActivity Activity { get; set; }
        public int ActivityId { get; set; }
        public int Rating { get; set; }
    }
}
