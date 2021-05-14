using GameLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.ViewModels
{
    public class GameSystemAPIViewModel
    { 
        public int GameSystemAPIID { get; set; }

        [Required]/*(ErrorMessage = "Error from api")]*/
        [MinLength(2, ErrorMessage = "Name must be at lest 2 Characters api")]
        [MaxLength(64, ErrorMessage = "Name can't be logner than 64 Characters api")]
        public string SystemName { get; set; }

        public virtual ICollection<GamesViewModel> GameLibrary { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        public StoreUser user { get; set; }

    }
}
