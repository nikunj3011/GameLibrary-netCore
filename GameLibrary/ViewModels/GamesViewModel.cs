using GameLibrary.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameLibrary.ViewModels
{
    public class GamesViewModel
    {
        public int GameLibraryID { get; set; }

        [Required]
        [Display(Name = "Game Name")]
        [MinLength(2, ErrorMessage = "Name must be at lest 2 Characters")]
        [MaxLength(64, ErrorMessage = "Name can't be logner than 64 Characters")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //[Required]
        //[Display(Name = "Game System")]
        //public int GameSystemID { get; set; }
        //public virtual GameSystem GameSystems { get; set; }

        [Required]
        [Display(Name = "Rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 seconds")]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Disc Type")]
        public string DiscType { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}