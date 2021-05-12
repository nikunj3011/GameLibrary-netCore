using GameLibrary.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameLibrary.Data
{
    public interface IGameRepository
    {
        IEnumerable<Games> GetGameLibraries();
        IEnumerable<Games> GetGamesByName(string username, bool includeItems);
        Task<IEnumerable<Games>> GetGamesAsync();

        IEnumerable<GameSystem> GetGameLibrariesByName(string name, bool includeItems);
        IEnumerable<GameSystem> GetGameSystems(bool includeItems);
        GameSystem GetGameSystemsById(int id);

        public bool SaveAll();
        void AddEntity(object model); //add any type of data
        Games GetGameById(int gameid);
    }
}