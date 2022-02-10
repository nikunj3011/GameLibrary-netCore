using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameLibrary.APIMessageBusControllers
{
    public interface IMessageBusClient
    {
        void Publish<T>(T gamePublishedDto);
    }
}