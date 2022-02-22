using AutoMapper;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using GameSystems.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameSystems.EventProcessing
{

    public class EventProcessor : IEventProcessor
    {
        private IServiceScopeFactory _scopedFactory;

        public EventProcessor(IServiceScopeFactory scopedFactory)
        {
            _scopedFactory = scopedFactory;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            if(eventType == EventType.GamePublished)
            {
                //todo
                addGame(message);
            }
            else
            {
                
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("Determining Event");
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            if(eventType.Event == "Game_Published")
            {
                Console.WriteLine("Game Published Event");
                return EventType.GamePublished;
            }
            else
            {
                Console.WriteLine("Other Event");
                return EventType.Other;
            }
        }

        private async void addGame(string gamePublishedMessage)
        {
            using(var scope = _scopedFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IGameRepository>();

                var gamePublishedRepo = JsonSerializer.Deserialize<Dtos.GamePublishedDto>(gamePublishedMessage);
                try
                {
                    var game = new Games
                    {
                        Name = gamePublishedRepo.Name,
                        Description = "",
                        CreationDate = gamePublishedRepo.CreationDate,
                        DiscType = gamePublishedRepo.DiscType,
                        Rating = gamePublishedRepo.Rating,
                        GameSystemID = 1
                    };
                    repo.AddEntity(game);
                    if (await repo.SaveAll())
                    {
                        Console.WriteLine("Saved to db");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not add game to DB: " + ex.Message);
                }
            }
        }
    }

    enum EventType
    {
        GamePublished, 
        Other
    }
}
