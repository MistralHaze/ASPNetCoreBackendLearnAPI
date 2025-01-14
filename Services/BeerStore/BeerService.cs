using AutoMapper;
using BackendLearnUdemy.DataTransferObjects;
using BackendLearnUdemy.Models;
using BackendLearnUdemy.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackendLearnUdemy.Services.BeerStore
{
    public class BeerService : ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO>
    {
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;

        public List<string> errors { get; }

        public BeerService(IRepository<Beer> beerRepository,
                           IMapper mapper)
        {
            errors  = new List<string>();
            _beerRepository = beerRepository;
            _mapper = mapper;
        }   

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            var beers= await _beerRepository.Get();

            return beers.Select(b => _mapper.Map<BeerDTO>(b));   
        }

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _beerRepository.GetByID(id);

            if (beer is null) return null;

            BeerDTO beerDTO = _mapper.Map<BeerDTO>(beer);

            return beerDTO; 
        }
        public async Task<BeerDTO> Add(BeerInsertDTO beerInsertDTO)
        {
            Beer beerToInsert = _mapper.Map<Beer>(beerInsertDTO);

            await _beerRepository.Add(beerToInsert);
            await _beerRepository.Save();

            BeerDTO completeBeerDTO = _mapper.Map<BeerDTO>(beerToInsert);

            return completeBeerDTO;
        }

        public async Task<BeerDTO> Update(int id, BeerUpdateDTO beerUpdateDTO)
        {
            Beer? beer = await _beerRepository.GetByID(id);

            if (beer is null) return null;

           beer= _mapper.Map<BeerUpdateDTO, Beer>(beerUpdateDTO,beer);

            _beerRepository.Update(beer);
            await _beerRepository.Save();

            BeerDTO beerDTO = _mapper.Map<BeerDTO>(beer);

            return beerDTO;

        }

        public async Task<BeerDTO> Delete(int id)
        {
            Beer? beer = await _beerRepository.GetByID(id);
            if (beer is null) return null;

            _beerRepository.Delete(beer);
            await _beerRepository.Save();

            BeerDTO beerDTO = _mapper.Map<BeerDTO>(beer);


            return beerDTO;
        }

        public bool Validate(BeerInsertDTO dto)
        {
            if (_beerRepository.Search(b => Equals(dto.Name, b.Name)).Count()>0){
                errors.Add("Beer name is already in use");
                return false;
            }
            return true;
        }

        public bool Validate(BeerUpdateDTO beerUpdateDTO)
        {
            if (_beerRepository.Search(b => Equals(beerUpdateDTO.Name, b.Name)&& beerUpdateDTO.Id!=b.BeerId).Count() > 0)
            {
                errors.Add("Beer name is already in use");
                return false;
            }
            return true;
        }
    }
}
