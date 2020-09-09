using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutOutWizWebApp.Models.ViewModel
{
    public class CompletedOrder
    {
        public int OrderId { get; set; }
        public string OrderThumbnilImageUrl { get; set; }
        public string MainImageUrl { get; set; }
        public string ProductImagesIds { get; set; }
        public string Image { get; set; }
        public string OrderDetails { get; set; }
    }
}
