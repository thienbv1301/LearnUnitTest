using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Service
{
    public interface IOrderDetailService : IBaseService<OrderDetail, OrderDetailDto>
    {
        Task SubmitAsync(string createBy, IEnumerable<OrderDetailDto> orderDetails);
    }
}
