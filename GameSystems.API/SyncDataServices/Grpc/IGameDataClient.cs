using GameSystems.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems.SyncDataServices.Grpc
{
    public interface IGameDataClient
    {
        IEnumerable<GamePublishedDto> ReturnAllPlatforms();
    }
}
