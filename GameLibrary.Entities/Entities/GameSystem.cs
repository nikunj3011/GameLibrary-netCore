using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameLibrary.Data.Entities
{
    [Index(nameof(SystemName), IsUnique = true)]
    public class GameSystem
    {
        [Key]
        public int GameSystemID { get; set; }

        [Required]
        [Display(Name = "System Name")]
        [MinLength(2, ErrorMessage = "Name must be at lest 2 Characters")]
        [MaxLength(64, ErrorMessage = "Name can't be logner than 64 Characters")]
        public string SystemName { get; set; }

        public virtual ICollection<Games> GameLibrary { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        public StoreUser user { get; set; }

    }
}