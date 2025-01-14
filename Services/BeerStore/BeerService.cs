using BackendLearnUdemy.DataTransferObjects;
using BackendLearnUdemy.Models;
using BackendLearnUdemy.Repository;
using Microsoft.EntityFrameworkCore;

namespace BackendLearnUdemy.Services.BeerStore
{
    public class BeerService : ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO>
    {
        private IRepository<Beer> _beerRepository;

        public BeerService(IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }   

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            var beers= await _beerRepository.Get();

            return beers.Select(b =>
                new BeerDTO
                {
                    Id = b.BeerId,
                    Name = b.Name,
                    Alcohol = b.Alcohol,
                    BrandId = b.BrandId
                });   
        }

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _beerRepository.GetByID(id);

            if (beer is null) return null;

            var beerDTO = new BeerDTO
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };
            return beerDTO; 
        }
        public async Task<BeerDTO> Add(BeerInsertDTO beerInsertDTO)
        {
            var beerToInsert = new Beer
            {
                Name = beerInsertDTO.Name,
                Alcohol = beerInsertDTO.Alcohol,
                BrandId = beerInsertDTO.BrandId
            };

            await _beerRepository.Add(beerToInsert);
            await _beerRepository.Save();

            var completeBeerDTO = new BeerDTO
            {
                Id = beerToInsert.BeerId,
                Name = beerToInsert.Name,
                Alcohol = beerToInsert.Alcohol,
                BrandId = beerToInsert.BrandId
            };
            return completeBeerDTO;
        }

        public async Task<BeerDTO> Update(int id, BeerUpdateDTO beerUpdateDTO)
        {
            Beer? beer = await _beerRepository.GetByID(id);

            if (beer is null) return null;

            beer.Name = beerUpdateDTO.Name;
            beer.Alcohol = beerUpdateDTO.Alcohol;
            beer.BrandId = beerUpdateDTO.BrandId;

            _beerRepository.Update(beer);
            await _beerRepository.Save();

            var completeBeerDTO = new BeerDTO
            {
                Id = beerUpdateDTO.Id,
                Name = beerUpdateDTO.Name,
                Alcohol = beerUpdateDTO.Alcohol,
                BrandId = beerUpdateDTO.BrandId
            };

            return completeBeerDTO;

        }

        public async Task<BeerDTO> Delete(int id)
        {
            Beer? beer = await _beerRepository.GetByID(id);
            if (beer is null) return null;

            _beerRepository.Delete(beer);
            _beerRepository.Save();


            var beerDTO = new BeerDTO
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            };

            return beerDTO;
        }

    }
}
