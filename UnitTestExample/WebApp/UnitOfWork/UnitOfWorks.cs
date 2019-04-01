using System;
using System.Threading.Tasks;
using WebApp.GenericRepository;
using WebApp.Models;

namespace WebApp.UnitOfWork
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private ApplicationContext _context;

        public UnitOfWorks(ApplicationContext context)
        {
            _context = context;
            InitRepositories();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        public IGenericRepository<Order> OrderRepository { get; private set; }

        public IGenericRepository<OrderDetail> OrderDetailRepository { get; private set; }

        private void InitRepositories()
        {
            OrderRepository = new GenericRepository<Order>(_context);
            OrderDetailRepository = new GenericRepository<OrderDetail>(_context);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
