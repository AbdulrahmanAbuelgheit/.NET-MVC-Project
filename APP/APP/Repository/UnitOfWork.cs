using APP.Context;
using APP.Models;
using APP.Repository.Base;

namespace APP.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
         
        public UnitOfWork(AppDbContext context) 
        {
            _context = context;
            categories = new MainRepository<Category>(_context);
            items = new MainRepository<Item>(_context);
            employees = new EmpRepo(_context);
        }
        private readonly AppDbContext _context;

        public IRepository<Category> categories { get; private set; }

        public IRepository<Item> items  { get; private set; }

        public IEmpRepo employees { get; private set; }

        public int CommitChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
