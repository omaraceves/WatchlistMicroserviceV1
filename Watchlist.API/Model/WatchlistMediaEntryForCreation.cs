using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Watchlist.API.Model
{
    public class WatchlistMediaEntryForCreation
    {
        public Guid UserId { get; set; }
        public Guid WatchlistId { get; set; }
        public Guid MediaId { get; set; }
    }
}
