using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Watchlist.API.Entities
{
    [Table("WatchListsMedias")]
    public class WatchlistMedia
    {
        [Key]
        public Guid Id { get; set; }
        public Guid WatchlistId { get; set; }
        public Watchlist WatchList { get; set; }
        public Guid MediaId { get; set; }
    }
}
