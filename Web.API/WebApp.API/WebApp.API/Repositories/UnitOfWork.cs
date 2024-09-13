using AutoMapper;
using WebApp.API.Data;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
      

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }


        public IMessageRepository MessageRepository => new MessageRepository(_context, _mapper);



        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool hasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

    }
}
