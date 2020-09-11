using SportsBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddyApp.Interface
{
    public interface IRegionService
    {
        bool AddRegion(Region region);
        Region GetRegionById(int Id);
        IEnumerable<Region> GetAllRegions();
        bool UpdateRegion(Region region);
        bool DeleteRegion(int Id);
    }
}
