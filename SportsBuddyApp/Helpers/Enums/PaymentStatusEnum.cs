using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutOutWizWebApp.Helpers.Enums
{
    public enum PaymentStatusEnum
    {
        Pending=1,
        Accepted=2,
        Declined=3,
        Expired=4,
        Due=5,
        Free=6
    }
}
