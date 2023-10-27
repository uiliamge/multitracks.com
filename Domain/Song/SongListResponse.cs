﻿using System;
using System.Collections.Generic;

namespace Domain
{
    public class SongListResponseRoot
    {
        public List<KeyValuePair<string, string>> Headers { get; set; }

        public List<SongListResponse> Items { get; set; }
    }

    public class SongListResponse
    {
        public int SongID { get; set; }
        public string Title { get; set; }
        public DateTime DateCreation { get; set; }
        public int AlbumID { get; set; }
        public int ArtistID { get; set; }
        public decimal Bpm { get; set; }
        public string TimeSignature { get; set; }
        public bool Multitracks { get; set; }
        public bool CustomMix { get; set; }
        public bool Chart { get; set; }
        public bool RehearsalMix { get; set; }
        public bool Patches { get; set; }
        public bool SongSpecificPatches { get; set; }
        public bool ProPresenter { get; set; }
    }
}
