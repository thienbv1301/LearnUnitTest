using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.GenericRepository;
using WebApp.Models;
using WebApp.UnitOfWork;

namespace WebApp.Service
{
    public class OrderDetailService : BaseService<OrderDetail, OrderDetailDto>, IOrderDetailService
    {
        public OrderDetailService(IUnitOfWorks unitOfWork) : base(unitOfWork)
        {
        }

        protected override IGenericRepository<OrderDetail> _reponsitory => _unitOfWork.OrderDetailRepository;

        public async Task SubmitAsync(string createBy, IEnumerable<OrderDetailDto> orderDetails)
        {
            var orderDto = new OrderDto
            {
                CreatedDay = DateTime.Now,
                CreatedBy = createBy,
                Code = Guid.NewGuid().ToString("n").Substring(0, 6)
            };

            var orderEntity = Mapper.Map<Order>(orderDto);

            _unitOfWork.OrderRepository.Add(orderEntity);

            foreach (var details in orderDetails)
            {
                details.OrderCode = orderDto.Id;
                _unitOfWork.OrderDetailRepository.Add(DtoToEntity(details));
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
