using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.GenericRepository;
using WebApp.Models;
using WebApp.UnitOfWork;

namespace WebApp.Service
{
    public class OrderService : BaseService<Order, OrderDto>, IOrderService
    {
        public OrderService(IUnitOfWorks unitOfWork) : base(unitOfWork)
        {
        }
        protected override IGenericRepository<Order> _reponsitory => _unitOfWork.OrderRepository;

        public async Task SubmitAsync(string createBy, IEnumerable<OrderDetailDto> orderDetails)
        {
            var orderDto = new OrderDto
            {
                CreatedDay = DateTime.Now,
                CreatedBy = createBy,
                Code = Guid.NewGuid().ToString("n").Substring(0, 6)
            };

            var orderEntity = DtoToEntity(orderDto);

            _unitOfWork.OrderRepository.Add(orderEntity);

            foreach (var details in orderDetails)
            {              
                var detailsEntity = Mapper.Map<OrderDetail>(details);
                _unitOfWork.OrderDetailRepository.Add(detailsEntity);
            }
            await _unitOfWork.SaveAsync();
        }
        public IEnumerable<OrderDto> GetAll()
        {
            return EntityToDto(_unitOfWork.OrderRepository.GetAll());
        }

        public OrderDto GetById(int id)
        {
            return EntityToDto(_unitOfWork.OrderRepository.FindById(id));
        }

        public bool OrderIsExist(int id)
        {
            var order = _unitOfWork.OrderRepository.FindById(id);
            if (order != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
