using GameLibrary.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace GameLibrary.Data
{
    public class GameSeeder
    {
        private readonly GameContext _ctx;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<StoreUser> userManager;

        public GameSeeder(GameContext ctx, IWebHostEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();
            StoreUser storeUser = await userManager.FindByEmailAsync("a@a.com");
            if (storeUser == null)
            {
                storeUser = new StoreUser()
                {
                    FirstName = "Nikunj",
                    LastName = "Rathod",
                    Email = "a@a.com",
                    UserName = "nrathod"
                };
                var result = await userManager.CreateAsync(storeUser, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user");
                }
            }
            if (!_ctx.GameLibraries.Any())
            {
                //create data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/file.json");
                var json = File.ReadAllText(filepath);
                //var games = JsonSerializer.DeserializeObject<IEnumerable<Games>>(json);
                var GameSystem = new GameSystem { CreationDate = DateTime.Today, GameLibrary = null, SystemName = "PS4",
                    GameSystemID = 1
                    /*new List<Games>() { new Games() { library = Games.First() }*/
                    /*new Library { GameSystemID=1, CreationDate=DateTime.Now, Description="abcd", DiscType="Digital", GameLibraryID=1, GameSystems=null, Name="Spiderman", Rating=4}*/
                };
                GameSystem.user = storeUser;
                _ctx.GameSystems.AddRange(GameSystem);
                _ctx.SaveChanges();
            }
        }
    }
}

