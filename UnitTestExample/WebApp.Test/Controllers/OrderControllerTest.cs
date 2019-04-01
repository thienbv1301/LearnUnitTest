using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Controllers;
using WebApp.GenericRepository;
using WebApp.Models;
using WebApp.Service;
using WebApp.UnitOfWork;
using Xunit;

namespace WebApp.Test.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IGenericRepository<Order>> orderRepositoryMock;
        private readonly Mock<IUnitOfWorks> unitOfWork;
        private Mock<IOrderDetailService> orderDetailServiceMock;
        private Mock<IOrderService> orderServiceMock;
        private OrderController controller;

        public OrderControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWorks>();
            orderRepositoryMock = new Mock<IGenericRepository<Order>>();
            orderDetailServiceMock = new Mock<IOrderDetailService>();
            orderServiceMock = new Mock<IOrderService>();
            controller = new OrderController(orderDetailServiceMock.Object, orderServiceMock.Object);
        }

        [Fact]
        public void GetTest_OrderIdPassed_ReturnRightOrder()
        {
            var mockOrderList = new List<OrderDto>
            {
                new OrderDto
                {
                    Id =1,
                    Code ="123",
                    CreatedBy ="1",
                    CreatedDay =DateTime.Now.Date
                },
                new OrderDto
                {
                    Id =2,
                    Code ="123",
                    CreatedBy ="2",
                    CreatedDay =DateTime.Now.Date
                }
            };
            orderServiceMock.Setup(o => o.GetById(1)).Returns(mockOrderList.Where(o=>o.Id==1).FirstOrDefault());
            orderServiceMock.Setup(o => o.OrderIsExist(1)).Returns(true);
            var result = controller.Get(1) as OkObjectResult;
            Assert.IsType<OrderDto>(result.Value);
            Assert.Equal(1, (result.Value as OrderDto).Id);
        }
        [Fact]
        public void GetTest_OrderIdPassed_ReturnOk()
        {
            var mockOrderList = new List<OrderDto>
            {
                new OrderDto
                {
                    Id =1,
                    Code ="123",
                    CreatedBy ="1",
                    CreatedDay =DateTime.Now.Date
                },
                new OrderDto
                {
                    Id =2,
                    Code ="123",
                    CreatedBy ="2",
                    CreatedDay =DateTime.Now.Date
                }
            };
            orderServiceMock.Setup(o => o.GetById(1)).Returns(mockOrderList.Where(o => o.Id == 1).FirstOrDefault());
            orderServiceMock.Setup(o => o.OrderIsExist(1)).Returns(true);
            var result = controller.Get(1);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetTest_OrderIdNotPassed_ReturnNotFoundResult()
        {
            orderServiceMock.Setup(o => o.OrderIsExist(1)).Returns(false);
            var result = controller.Get(1) ;
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
