using AutoMapper;
using GameLibrary.Data.Entities;
using GameLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Data
{
    public class GameMappingProfile: Profile
    {
        //add mapping profile for automapper and map model with viewmodels
        public GameMappingProfile()
        {
            CreateMap<GameSystem, GameSystemAPIViewModel>()
                .ForMember(p=>p.GameSystemAPIID, ex=>ex.MapFrom(p=>p.GameSystemID))
                .ReverseMap();

            CreateMap<Games, GamesViewModel>()
                //.ForMember(c=>c.SystemName, o=>o.MapFrom(m=>m.GameSystems.SystemName))
                .ReverseMap();
        }
    }
}
