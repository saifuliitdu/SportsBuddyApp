using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutOutWizWebApp.Models.ViewModel
{
    public class DropBoxImageViewModel
    {
        public long Bytes { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        public bool IsDir { get; set; }
        public string Link { get; set; }
        public string LinkType { get; set; }
        public string Name { get; set; }
        public string ThumbnailLink { get; set; }

    }
}
