using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        public OrderController(IOrderDetailService orderDetailService, IOrderService orderService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        [HttpPost]
        [Route("{createBy}")]
        public async Task Post(string createBy, [FromBody] IDictionary<string, IEnumerable<OrderDetailDto>> dic)
        {
            var orderDetailsList = dic["orderDetails"];
            await _orderDetailService.SubmitAsync(createBy, orderDetailsList);
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            if (_orderService.OrderIsExist(id))
            {
                return Ok(_orderService.GetById(id));
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpGet()]
        public List<OrderDto> GetAll()
        {
            return _orderService.GetAll().ToList();
        }
    }
}
