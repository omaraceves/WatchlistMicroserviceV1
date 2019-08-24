using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Watchlist.API.Entities
{
    [Table("WatchLists")]
    public class Watchlist
    {
        public Watchlist() { } 

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid Userid { get; set; }


        public string Name { get; set; }

        public List<WatchlistMedia> Medias { get; set; } = new List<WatchlistMedia>();

        public string MediaIdsToString()
        {
            StringBuilder s = new StringBuilder();

            foreach(var media in Medias)
            {
                s.Append(media.MediaId.ToString());
                s.Append(",");
            }

            s.Remove(s.Length - 1, 1);

            return s.ToString();
        }
    }
}
