using GameLibrary.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameLibrary.ViewModels
{
    public class GamesViewModel
    {
        public int GameLibraryID { get; set; }
        public string Name { get; set; }
         
        public int Rating { get; set; }
         
        public string DiscType { get; set; } 
    }
}