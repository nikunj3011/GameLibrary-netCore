using GameLibrary.Data.Entities;
using System.Collections.Generic;

namespace GameLibrary.Data
{
    public interface IGameRepository
    {
        IEnumerable<Library> GetGameLibraries();
        IEnumerable<Library> GetGameLibrariesByName(string name);

        IEnumerable<GameSystem> GetGameSystems();
        GameSystem GetGameSystemsById(int id);

        public bool SaveAll();
        
    }
}