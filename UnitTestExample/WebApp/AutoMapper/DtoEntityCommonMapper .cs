using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.AutoMapper
{
    public class DtoEntityCommonMapper : Profile
    {
        public DtoEntityCommonMapper()
        {
            #region Enity To Dto

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDetail, OrderDetailDto>();

            #endregion

            #region Dto to Entity

            CreateMap<OrderDto, Order>();
            CreateMap<OrderDetailDto, OrderDetail>();

            #endregion
        }
    }
}
