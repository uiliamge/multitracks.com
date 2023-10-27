using System;

namespace Domain
{
    public class SongFilter
    {
        public int? SongID { get; set; }
        public string Title { get; set; }
        public DateTime? DateCreation { get; set; }
        public int? AlbumID { get; set; }
        public int? ArtistID { get; set; }
        public decimal? Bpm { get; set; }
        public string TimeSignature { get; set; }
        public bool? Multitracks { get; set; }

    }
}
