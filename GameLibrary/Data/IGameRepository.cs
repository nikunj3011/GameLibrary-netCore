﻿using GameLibrary.Data.Entities;
using System.Collections.Generic;

namespace GameLibrary.Data
{
    public interface IGameRepository
    {
        IEnumerable<Library> GetGameLibraries();
        IEnumerable<Library> GetGameLibrariesByName(string name);
        public bool SaveAll();
    }
}