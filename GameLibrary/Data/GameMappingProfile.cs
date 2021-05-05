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
        public GameMappingProfile()
        {
            CreateMap<GameSystem, GameSystemAPIViewModel>()
                .ForMember(p=>p.GameSystemAPIID, ex=>ex.MapFrom(p=>p.GameSystemID))
                .ReverseMap();
        }
    }
}
