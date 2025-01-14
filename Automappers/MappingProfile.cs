using AutoMapper;
using BackendLearnUdemy.DataTransferObjects;
using BackendLearnUdemy.Models;

namespace BackendLearnUdemy.Automappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BeerInsertDTO, Beer>();//Since names are the same, works right away
            CreateMap<Beer, BeerDTO>().ForMember(dto => dto.Id,
                                                 m => m.MapFrom(b => b.BeerId));
            CreateMap<BeerUpdateDTO,Beer>();
        }
    }
}
