using GameLibrary.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameLibrary.Data
{
    public interface IGameRepository
    {
        IEnumerable<Games> GetGameLibraries();
        IEnumerable<Games> GetGamesByName(string username, bool includeItems);
        Task<IEnumerable<Games>> GetGamesAsync(bool includeGameSystem);
        Task<Games> GetGameAsync(string game);
        Task<Games> GetGameAsync(int game);
        Task<IEnumerable<Games>> SearchRatingGameAsync(int rating, bool includeSystemName);
        Games GetGameById(int gameid);
        Task<IEnumerable<Games>> SearchNameGameAsync(string name, bool includeSystemName);

        //Game System
        IEnumerable<GameSystem> GetGameLibrariesByName(string name, bool includeItems);
        IEnumerable<GameSystem> GetGameSystems(bool includeItems);
        GameSystem GetGameSystemsById(int id);
        Task<GameSystem[]> GetGameSystemByGameId(int gameid, int gameSystemId, bool includeSpeakers = false);
        Task<GameSystem[]> GetGameSystemsByGameName(string name);

        // General 
        void Delete<T>(T entity) where T : class;
        void AddEntity<T>(T entity) where T : class; 
        Task<bool> SaveAll();
    }
}