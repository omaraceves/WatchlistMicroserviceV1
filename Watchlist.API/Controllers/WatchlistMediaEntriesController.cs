using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Watchlist.API.DataServices;

namespace Watchlist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistMediaEntriesController : ControllerBase
    {
        private IWatchlistsRepository _repo;
        private IMapper _mapper;
        private IConfiguration _configuration;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        public WatchlistMediaEntriesController(IWatchlistsRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> AddWatchlistMediaEntry([FromBody] Model.WatchlistMediaEntryForCreation entry)
        {
            //Check if watchlist belongs to user
            var watchlistEntity = await _repo.GetWatchlistByUserIdAsync(entry.UserId);
            bool isNewList = false;

            if (watchlistEntity == null)
            {
                watchlistEntity = new Entities.Watchlist()
                {
                    Userid = entry.UserId,
                    Id = entry.WatchlistId,
                    Name = "WatchLaterList"
                };

                isNewList = true;
            }
            else if (!watchlistEntity.Id.Equals(entry.WatchlistId))
            {
                return BadRequest("Given watchlist does not belong to given user");
            }

            //Check if the entry exists
            var watchlistEntryEntity = await _repo.GetWatchListMediaEntryAsync(entry.WatchlistId, entry.MediaId);

            if (watchlistEntryEntity != null)
            {
                return NoContent(); //If entry exists it means the operation was done successfully, just return No content.
            }
            else
            {
                watchlistEntryEntity = _mapper.Map<Entities.WatchlistMedia>(entry); //If entry doesn't exists, map entry to a new entity
            }

            //Add to watchlist
            watchlistEntity.Medias.Add(watchlistEntryEntity);
            _repo.UpdateWatchlist(watchlistEntity);
            var success = await _repo.SaveChangesAsync();

            if (!success)
            {
                return new StatusCodeResult(500);
            }

            //Map Watchlist with updated entry       
            var listResult = _mapper.Map<Model.WatchlistModel>(watchlistEntity);
            var link = new Model.LinkModel()
            {
                type = "GET",
                rel = "data",
                href = string.Format(_configuration["AppSettings:MediaServiceUrl"], watchlistEntity.MediaIdsToString())
            };
            listResult.Link.Add(link);

            //Return result
            if (isNewList)
            {
                return Created($"api/watchlists/{watchlistEntity.Id}", listResult);
            }
            else
            {
                return Ok(listResult);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveWatchlistMediaEntry([FromBody] Model.WatchlistMediaEntryForCreation entry)
        {
            //Validate user and watchlist
            var watchlistEntity = await _repo.GetWatchlistByUserIdAsync(entry.UserId);

            if (watchlistEntity == null)
            {
                return NotFound("Watchlist not found for given user.");
            }
            else if (!watchlistEntity.Id.Equals(entry.WatchlistId))
            {
                return BadRequest("Given watchlist does not belong to given user");
            }

            //Remove from watchlist
            watchlistEntity.Medias.RemoveAll(x => x.MediaId == entry.MediaId);
            _repo.UpdateWatchlist(watchlistEntity);

            var success = await _repo.SaveChangesAsync();
            if (!success)
            {
                return new StatusCodeResult(500);
            }

            //Map Watchlist with updated entry       
            var listResult = _mapper.Map<Model.WatchlistModel>(watchlistEntity);
            var link = new Model.LinkModel()
            {
                type = "GET",
                rel = "data",
                href = string.Format(_configuration["AppSettings:MediaServiceUrl"], watchlistEntity.MediaIdsToString())
            };
            listResult.Link.Add(link);

            //Return result
            return Ok(listResult);
        }
    }
}