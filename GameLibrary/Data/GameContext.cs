using GameLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Data
{
    public class GameContext: DbContext
    {
        public DbSet<Library> GameLibraries { get; set; }
        public DbSet<GameShop> GameShops { get; set; }
        public DbSet<GameSystem> GameSystems { get; set; }
    }
}
