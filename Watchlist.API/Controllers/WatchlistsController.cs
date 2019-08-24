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
    public class WatchlistsController : ControllerBase
    {
        private IWatchlistsRepository _repo;
        private IMapper _mapper;
        private IConfiguration _configuration;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        public WatchlistsController(IWatchlistsRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration;
        }

        [HttpGet("{id}", Name = "GetWatchlist")]
        public async Task<IActionResult> GetWatchlist([FromRoute] Guid id, [FromQuery] Guid userId)
        {
            var watchlistEntity = await _repo.GetWatchlistByUserIdAsync(userId);

            if(watchlistEntity == null)
            {
                watchlistEntity = new Entities.Watchlist()
                {
                    Userid = userId,
                    Id = id,
                    Name = "WatchLaterList"
                };
            }

            if(!watchlistEntity.Id.Equals(id))
            {
                return BadRequest("Given watchlist does not belong to given user.");
            }

            var result = _mapper.Map<Model.WatchlistModel>(watchlistEntity);
            //result.Medias = _mapper.Map<IEnumerable<Guid>>(watchlistEntity.Medias).ToList();

            var link = new Model.LinkModel()
            {
                type = "GET",
                rel = "data",
                href = string.Format(_configuration["AppSettings:MediaServiceUrl"], watchlistEntity.MediaIdsToString())
            };

           
            result.Link.Add(link);

            return Ok(result);

        }
    }
}