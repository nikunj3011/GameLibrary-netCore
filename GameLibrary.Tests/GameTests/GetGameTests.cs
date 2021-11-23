using GameLibrary.Data;
using GameLibrary.Data.Entities;
using System;
using Xunit;

namespace GameLibrary.Tests.GameTests
{
    public class GetGameTests
    {
        [Fact]
        public void ShouldGetGames()
        {
            //arrange
            var newgame = new Games
            {
                Name = "",
                CreationDate = DateTime.Today,
                Description = "aa",
                DiscType = "Physical",
                Rating = 5,
                GameSystemID = 1
            };

            GameRepository gameAPIController = new GameRepository();

            //act
            Games result = newgame;
            //assert
            Assert.NotNull(result);
        }
    }
}
