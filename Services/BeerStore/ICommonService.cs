using BackendLearnUdemy.DataTransferObjects;

namespace BackendLearnUdemy.Services.BeerStore
{
    public interface ICommonService<T,TInsert,TUpdate>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TInsert beerInsertDTO);
        Task<T> Update(int id, TUpdate beerUpdateDTO);
        Task<T> Delete(int id);




    }
}
