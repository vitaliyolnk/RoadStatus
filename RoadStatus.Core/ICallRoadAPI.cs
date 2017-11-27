using RoadStatus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus.Core
{
   public interface ICallRoadAPI
    {
        Task<RoadCorridor> GetRoadStatus(string roadId);
    }
}
