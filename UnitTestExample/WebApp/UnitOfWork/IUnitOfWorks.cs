using System;
using System.Threading.Tasks;
using WebApp.GenericRepository;
using WebApp.Models;

namespace WebApp.UnitOfWork
{
    public interface IUnitOfWorks : IDisposable
    {
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        Task SaveAsync();
    }
}
