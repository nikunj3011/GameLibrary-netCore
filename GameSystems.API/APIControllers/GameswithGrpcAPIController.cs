using AutoMapper;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using GameSystems.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Controllers
{
    //[Route("/api/s/[Controller]")]
    //[ApiController]
    public static class  GameswithGrpc
    {
       
        public static void Prep(IApplicationBuilder applicationBuilder)
        {
            //add it to database
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    var grpcClient = serviceScope.ServiceProvider.GetService<IGameDataClient>();
                    var games = grpcClient.ReturnAllPlatforms();
                    //return games;
                }
        }
    }
}
