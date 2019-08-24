using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Watchlist.API.Model
{
    public class WatchlistModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<Guid> Medias { get; set; } = new List<Guid>();
        public int MediaCount { get; set; }

        [DisplayName("Links")]
        public List<LinkModel> Link { get; set; } = new List<LinkModel>();
    }
}
