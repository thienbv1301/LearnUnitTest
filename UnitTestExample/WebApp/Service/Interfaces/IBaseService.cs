using System.Threading.Tasks;

namespace WebApp.Service
{
    public interface IBaseService<TEntity, TDto>
    where TEntity : class
    where TDto : class
    {
        Task<TDto> CreateAsync(TDto dto);

        Task<TDto> UpdateAsync(TDto dto);

        Task DeleteAsync(object keyValues);

        TDto FindById(object keyValues);
    }

}
