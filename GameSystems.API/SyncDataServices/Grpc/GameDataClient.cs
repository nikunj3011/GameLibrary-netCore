using GameLibrary;
using GameSystems.Dtos;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems.SyncDataServices.Grpc
{
    public class GameDataClient : IGameDataClient
    {
        private readonly IConfiguration _configuration;

        public GameDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<GamePublishedDto> ReturnAllPlatforms()
        {
            Console.WriteLine($"Calling Grpc server {_configuration["GrpcGame"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcGame"]);
            var client = new GrpcGame.GrpcGameClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllGames(request);
                var games = new List<GamePublishedDto>();
                foreach (var game in reply.Game)
                {
                    var a = new GamePublishedDto();
                    a.Name = game.Name;
                    a.Rating = game.Rating;
                    a.DiscType = game.DiscType;
                    a.Description = "";
                    games.Add(a);
                }
                return games;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cant find Grpc server" + ex.Message);
                return null;
            }
        }
    }
}
