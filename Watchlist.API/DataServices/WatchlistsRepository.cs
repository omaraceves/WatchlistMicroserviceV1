using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watchlist.API.Context;
using Watchlist.API.Entities;

namespace Watchlist.API.DataServices
{
    public class WatchlistsRepository : IWatchlistsRepository
    {
        private WatchlistContext _context;

        public WatchlistsRepository(WatchlistContext context)
        {
            _context = context;
        }


        #region Watchlists
        public void AddWatchlist(Entities.Watchlist watchlistToAdd)
        {
            _context.Watchlists.AddAsync(watchlistToAdd);
        }

        public async Task<Entities.Watchlist> GetWatchlistAsync(Guid id)
        {
            var watchlist = await _context.Watchlists.Include(x => x.Medias).FirstOrDefaultAsync(x => x.Id == id);

            if (watchlist == null)
            {
                return null;
            }

            return watchlist;
        }

        public Task<Entities.Watchlist> GetWatchlistByUserIdAsync(Guid userId)
        {
            return _context.Watchlists.Include(x => x.Medias).Where(x => x.Userid == userId)
                .FirstOrDefaultAsync() ?? null;
        }

        public Task<IEnumerable<Entities.Watchlist>> GetWatchlistsAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateWatchlist(Entities.Watchlist watchlistToUpdate)
        {
            _context.Watchlists.Update(watchlistToUpdate);
        }

        #endregion

        #region Watchlist entries
        public async Task<bool> AddWatchlistMediaEntryAsync(WatchlistMedia entryToAdd)
        {
            var watchList = await _context.Watchlists.FirstOrDefaultAsync(w => w.Id == entryToAdd.WatchlistId);

            if (watchList == null)
            {
                return false;
            }

            entryToAdd.Id = Guid.NewGuid();
            _context.WatchlistsMedias.Add(entryToAdd);

            return true;
        }

        public async Task<IEnumerable<WatchlistMedia>> GetWatchlistMediaEntriesAsync(Guid watchlistId)
        {
            return await _context.WatchlistsMedias.Where(x => x.WatchlistId == watchlistId)
                .ToListAsync() ?? null;
        }

        public async Task<WatchlistMedia> GetWatchListMediaEntryAsync(Guid watchlaterId, Guid mediaId)
        {
            var entry = await _context.WatchlistsMedias.FirstOrDefaultAsync(x => x.MediaId == mediaId && x.WatchlistId == watchlaterId);
            if (entry == null)
            {
                return entry;
            }

            return entry;
        }

        public async Task<bool> RemoveWatchlistEntryAsync(Guid watchlaterId, Guid mediaId)
        {
            var entryToRemove = await _context.WatchlistsMedias.FirstOrDefaultAsync(x => x.MediaId == mediaId && x.WatchlistId == watchlaterId);

            if (entryToRemove == null)
            {
                return false;
            }

            _context.WatchlistsMedias.Remove(entryToRemove);

            return true;
        }

        #endregion

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync() > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
