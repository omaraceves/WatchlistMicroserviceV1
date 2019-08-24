using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchlist.API.Entities;

namespace Watchlist.API.DataServices
{
    public interface IWatchlistsRepository
    {
        Task<IEnumerable<Entities.Watchlist>> GetWatchlistsAsync();

        Task<Entities.Watchlist> GetWatchlistAsync(Guid id);

        Task<Entities.Watchlist> GetWatchlistByUserIdAsync(Guid id);

        void AddWatchlist(Entities.Watchlist watchlistToAdd);

        void UpdateWatchlist(Entities.Watchlist watchlistToAdd);

        Task<bool> AddWatchlistMediaEntryAsync(WatchlistMedia entryToAdd);

        Task<bool> RemoveWatchlistEntryAsync(Guid watchlaterId, Guid mediaId);

        Task<WatchlistMedia> GetWatchListMediaEntryAsync(Guid watchlaterId, Guid mediaId);

        Task<IEnumerable<WatchlistMedia>> GetWatchlistMediaEntriesAsync(Guid watchlistId);

        Task<bool> SaveChangesAsync();
    }
}
