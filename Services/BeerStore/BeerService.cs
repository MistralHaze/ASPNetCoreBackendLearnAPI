using BackendLearnUdemy.DataTransferObjects;
using BackendLearnUdemy.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendLearnUdemy.Services.BeerStore
{
    public class BeerService : ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO>
    {
        private StoreContext _storeContext;

        public BeerService(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }   

        public async Task<IEnumerable<BeerDTO>> Get()
        {
            return await _storeContext.Beers.Select(b =>
                new BeerDTO
                {
                    Id = b.BeerId,
                    Name = b.Name,
                    Alcohol = b.Alcohol,
                    BrandId = b.BrandId
                }
            ).ToListAsync();
        }

        public async Task<BeerDTO> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

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
            //Tells EF there will be an insert.
            await _storeContext.Beers.AddAsync(beerToInsert);
            //Represent stored changes in the DB, This step will add the id (Beer ID)
            await _storeContext.SaveChangesAsync();

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

            Beer? beer = await _storeContext.Beers.FindAsync(id);

            if (beer is null) return null;

            beer.Name = beerUpdateDTO.Name;
            beer.Alcohol = beerUpdateDTO.Alcohol;
            beer.BrandId = beerUpdateDTO.BrandId;

            await _storeContext.SaveChangesAsync();

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
            Beer? beer = await _storeContext.Beers.FindAsync(id);
            if (beer is null) return null;

            _storeContext.Beers.Remove(beer);
            await _storeContext.SaveChangesAsync();


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
