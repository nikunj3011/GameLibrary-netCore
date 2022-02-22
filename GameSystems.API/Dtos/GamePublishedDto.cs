using GameLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSystems.Dtos
{
    public class GamePublishedDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int GameLibraryID { get; set; }

        public int GameSystemID { get; set; }
        public GameSystem GameSystems { get; set; }

        public int Rating { get; set; }

        public string DiscType { get; set; }

        public DateTime CreationDate { get; set; }
        public string Event { get; set; }

    }
}