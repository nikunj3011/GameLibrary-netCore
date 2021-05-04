using GameLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly GameContext gameContext;
        private readonly ILogger<GameRepository> logger;

        public GameRepository(GameContext gameContext, ILogger<GameRepository> logger)
        {
            this.gameContext = gameContext;
            this.logger = logger;
        }
        public IEnumerable<Library> GetGameLibraries()
        {
            try
            {
                logger.LogInformation("Get All products");
                return gameContext.GameLibraries.OrderBy(p => p.Name).ToList();
            }
            catch(Exception ex)
            {
                logger.LogError( $"Failed to get products: {ex}");
                return null;
            }
            
        }

        public IEnumerable<Library> GetGameLibrariesByName(string name)
        {
            return gameContext.GameLibraries.Where(p => p.Name == name).ToList();
        }
         
        public bool SaveAll()
        {
            return gameContext.SaveChanges()>0;
        }
    }
}
