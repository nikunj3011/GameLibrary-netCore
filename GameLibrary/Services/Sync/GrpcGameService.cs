using GameLibrary.Data;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Services.Sync
{
    public class GrpcGameService : GrpcGame.GrpcGameBase
    {
        private readonly IGameRepository _gameRepository;

        public GrpcGameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }   

        public override Task<GameResponseModel> GetAllGames(GetAllRequest request, ServerCallContext context)
        {
            var response = new GameResponseModel();
            var games = _gameRepository.GetGameLibraries();
            foreach (var game in games)
            {
                var grpcGame = new GrpcGameModel();
                grpcGame.Name = game.Name;
                grpcGame.Rating = game.Rating;
                grpcGame.DiscType = game.DiscType;
                response.Game.Add(grpcGame);
            }
            return Task.FromResult(response);
        }
    }
}
