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

        public void AddEntity(object model)
        {
            gameContext.Add(model);
        }

        public Games GetGameById(int gameid)
        {
            return gameContext.GameLibraries.Include(p => p.GameSystems)/*.ThenInclude(p=>p.GameSystemID)*/
                .Where(p => p.GameLibraryID == gameid).FirstOrDefault();
        }

        public IEnumerable<Games> GetGameLibraries()
        {
            try
            {
                logger.LogInformation("Get All products");
                return gameContext.GameLibraries.Include(p => p.GameSystems).OrderBy(p => p.Name).ToList();
            }
            catch(Exception ex)
            {
                logger.LogError( $"Failed to get products: {ex}");
                return null;
            }
            
        }
        public async Task<IEnumerable<Games>> GetGamesAsync(bool includeGameSystem)
        {  
            try
            {
                logger.LogInformation("Get All games");
                if (includeGameSystem)
                {
                    return await gameContext.GameLibraries
                     .Include(t => t.GameSystems)
                     .OrderBy(t => t.Name)
                                    .ToArrayAsync();
                }
                else
                {
                    return await gameContext.GameLibraries 
                     .OrderBy(t => t.Name)
                                    .ToArrayAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get games: {ex}");
                return null;
            }
        }

        public async Task<Games> GetGameAsync(string game)
        {
           // return await .ToList();
            var query = gameContext.GameLibraries.Include(p => p.GameSystems).Where(p => p.Name == game);
            try
            {
                logger.LogInformation("Get All games");
                return await query.SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get games: {ex}");
                return null;
            }
        }
        public async Task<Games> GetGameAsync(int game)
        {
            // return await .ToList();
            var query = gameContext.GameLibraries.Include(p => p.GameSystems).Where(p => p.GameLibraryID == game);
            try
            {
                logger.LogInformation("Get All games");
                return await query.SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get games: {ex}");
                return null;
            }
        }

        public async Task<IEnumerable<Games>> SearchRatingGameAsync(int rating, bool includeSystemName)
        {
            //var query = gameContext.GameLibraries.Include(p => p.GameSystems).Where(p => p.Name== game);
            try
            {
                logger.LogInformation("Get All games");
                if (includeSystemName)
                {
                    return await gameContext.GameLibraries.Include(p => p.GameSystems).Where(p => p.Rating == rating)
                                    .ToArrayAsync();
                }
                else
                {
                    return await gameContext.GameLibraries 
                                    .Where(p => p.Rating == rating)
                                    .ToArrayAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get games: {ex}");
                return null;
            }
        }

        public async Task<IEnumerable<Games>> SearchNameGameAsync(string name, bool includeSystemName)
        {
            //var query = gameContext.GameLibraries.Include(p => p.GameSystems).Where(p => p.Name== game);
            try
            {
                logger.LogInformation("Get All games");
                if (includeSystemName)
                {
                    return await gameContext.GameLibraries.Include(p => p.GameSystems)
                                    .Where(p => p.Name.Contains(name))
                                    .ToArrayAsync();
                }
                else
                {
                    return await gameContext.GameLibraries
                                    .Where(p => p.Name.Contains(name))
                                    .ToArrayAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get games: {ex}");
                return null;
            }
        }

        public IEnumerable<Games> GetGameLibrariesByName(string name)
        {
            return gameContext.GameLibraries.Include(p => p.GameSystems).Where(p => p.Name == name).ToList();
        }

        public IEnumerable<GameSystem> GetGameLibrariesByName(string name, bool includeItems)
        {
            //return gameContext.GameSystems.Include(p => p).Where(p => p.Name == name).ToList();
            if (includeItems)
            {
                return gameContext.GameSystems.Where(p=>p.user.UserName==name).Include(p => p.GameLibrary)/*.ThenInclude(p=>p.GameSystemID)*/
                                .ToList();
            }
            else
            {
                return gameContext.GameSystems.ToList();
            }
        }

        public IEnumerable<Games> GetGamesByName(string username, bool includeItems)
        {
            //if (includeItems)
            //{
            //    return gameContext.GameLibraries.ToList();
            //}
            //else
            //{
            //return gameContext.GameLibraries.Where(p => p.Name == username).Include(p => p.GameSystems).ToList();

            return gameContext.GameLibraries
            .Include(l => l.GameSystems).ToList(); 
          
        }

        public IEnumerable<GameSystem> GetGameSystems(bool includeItems)
        {
            if (includeItems)
            {
                return gameContext.GameSystems.Include(p => p.GameLibrary)/*.ThenInclude(p=>p.GameSystemID)*/
                                .ToList();
            }
            else
            {
                return gameContext.GameSystems.ToList();
            }
            
        }

        public GameSystem GetGameSystemsById(int id)
        {
            return gameContext.GameSystems.Include(p => p.GameLibrary)/*.ThenInclude(p=>p.GameSystemID)*/
                .Where(p => p.GameSystemID == id).FirstOrDefault();
        }

        public bool SaveAll()
        {
            return gameContext.SaveChanges()>0;
        }

        
    }
}
