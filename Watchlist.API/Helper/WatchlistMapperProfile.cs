using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Watchlist.API.Helper
{
    public class WatchlistMapperProfile : Profile
    {
        public WatchlistMapperProfile()
        {
            CreateMap<Entities.Watchlist, Model.WatchlistModel>()
                .ForMember(dest => dest.MediaCount, opt => opt.MapFrom(x => x.Medias.Count()))
                .ForMember(dest => dest.Medias, opt => opt.MapFrom(x => x.Medias));

            CreateMap<Entities.WatchlistMedia, Guid>()
                .ConstructUsing(x => x.MediaId);

            CreateMap<Model.WatchlistMediaEntryForCreation, Entities.WatchlistMedia>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => Guid.NewGuid()));
        }
    }
}
