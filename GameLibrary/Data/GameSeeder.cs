using GameLibrary.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Data
{
    public class GameSeeder
    {
        private readonly GameContext _ctx;
        private readonly IWebHostEnvironment _hosting;

        public GameSeeder(GameContext ctx, IWebHostEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        //public void Seed()
        //{
        //    _ctx.Database.EnsureCreated();
        //    if(!_ctx.GameLibraries.Any())
        //    {
        //        //create data
        //        var filepath = Path.Combine(_hosting.ContentRootPath,"Data/file.json");
        //        var json = File.ReadAllText(filepath);
        //        var games = JsonSerializer.DeserializeObject<IEnumerable<Library>>(json);
        //var GameSystem = new GameSystem { CreationDate = DateTime.Today, GameLibrary = new List<Library>() {new Library(){library=Library.First() };/*new Library { GameSystemID=1, CreationDate=DateTime.Now, Description="abcd", DiscType="Digital", GameLibraryID=1, GameSystems=null, Name="Spiderman", Rating=4}*/, GameSystemID = 1, SystemName = "PS4" };
        //        _ctx.GameLibraries.AddRange(games);
        //        _ctx.SaveChanges();
        //    }
        }
    }

