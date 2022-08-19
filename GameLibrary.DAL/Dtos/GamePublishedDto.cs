using GameLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameLibrary.ViewModels
{
    public class GamePublishedDto
    { 
        public string Name { get; set; }
        public int GameLibraryID { get; set; }

        public virtual GameSystem GameSystems { get; set; }

        public int Rating { get; set; }

        public string DiscType { get; set; }
        public string Event { get; set; }

        public DateTime CreationDate { get; set; }

    }
}