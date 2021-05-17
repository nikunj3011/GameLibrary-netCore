using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Data.Entities
{
    [Index(nameof(GameShopName), IsUnique = true)]
    public class GameShop
    {
        [Key]
        public int GameShopID { get; set; }

        [Required]
        public string GameShopName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int GameLibraryID { get; set; }
        public Games GameLibrary { get; set; }

        [Required]
        public int GameSystemID { get; set; }
        public GameSystem GameSystem { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
