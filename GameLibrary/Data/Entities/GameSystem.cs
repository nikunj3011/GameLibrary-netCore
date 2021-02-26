﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameLibrary.Data.Entities
{
    public class GameSystem
    {
        [Key]
        public int GameSystemID { get; set; }

        [Required]
        [Display(Name = "System Name")]
        [MinLength(2, ErrorMessage = "Name must be at lest 2 Characters")]
        [MaxLength(64, ErrorMessage = "Name can't be logner than 64 Characters")]
        public string SystemName { get; set; }

        public virtual ICollection<Library> GameLibrary { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

    }
}