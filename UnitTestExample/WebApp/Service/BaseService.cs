using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.GenericRepository;
using WebApp.UnitOfWork;

namespace WebApp.Service
{
    public abstract class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
       where TEntity : class
       where TDto : class
    {
        protected readonly IUnitOfWorks _unitOfWork;

        protected abstract IGenericRepository<TEntity> _reponsitory { get; }

        public BaseService(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = DtoToEntity(dto);

            _reponsitory.Add(entity);

            await _unitOfWork.SaveAsync();

            return EntityToDto(entity);
        }

        public virtual async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = DtoToEntity(dto);

            _reponsitory.Update(entity);

            await _unitOfWork.SaveAsync();

            return EntityToDto(entity);

        }

        public virtual async Task DeleteAsync(object id)
        {
            var entity =  _reponsitory.FindById(id);

            if (entity == null) throw new Exception("Not found entity object with id: " + id);

            _reponsitory.Delete(entity);

            await _unitOfWork.SaveAsync();
        }

        public virtual  TDto FindById(object id)
        {
            return EntityToDto( _reponsitory.FindById(id));
        }

        protected TDto EntityToDto(TEntity entity)
        {
            return Mapper.Map<TDto>(entity);
        }

        protected TEntity DtoToEntity(TDto dto)
        {
            return Mapper.Map<TEntity>(dto);
        }

        protected TEntity DtoToEntity(TDto dto, TEntity entity)
        {
            return Mapper.Map(dto, entity);
        }

        protected IEnumerable<TDto> EntityToDto(IEnumerable<TEntity> entities)
        {
            return Mapper.Map<IEnumerable<TDto>>(entities);
        }

        protected IEnumerable<TEntity> DtoToEntity(IEnumerable<TDto> dto)
        {
            return Mapper.Map<IEnumerable<TEntity>>(dto);
        }

    }
}
