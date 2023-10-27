using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ArtistPostRequest
    {
        [MaxLength(100)]
        public string Title { get; set; }

        public string Biography { get; set; }

        [MaxLength(500)]
        public string ImageURL { get; set; }

        [MaxLength(500)]
        public string HeroURL { get; set; }

    }
}
