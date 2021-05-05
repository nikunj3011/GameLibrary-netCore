using GameLibrary.Data.Entities;
using System.Collections.Generic;

namespace GameLibrary.Data
{
    public interface IGameRepository
    {
        IEnumerable<Games> GetGameLibraries();
        IEnumerable<Games> GetGameLibrariesByName(string name);

        IEnumerable<GameSystem> GetGameSystems();
        GameSystem GetGameSystemsById(int id);

        public bool SaveAll();
        void AddEntity(object model); //add any type of data
        Games GetGameById(int gameid);
    }
}