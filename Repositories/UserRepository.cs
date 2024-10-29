using System.Linq.Expressions;
using Store.Repositories.IReqositories;
using Store.Data;
using static Store.Repositories.UserRepository;
using Store.Models.Models;

namespace Store.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepo
        {
            private readonly AppDbContext context;

            public UserRepository(AppDbContext context) : base(context)
            {
                this.context = context;
            }
        public void Add(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(AppUser entity)
        {
            throw new NotImplementedException();
        }

        public AppUser Get(Expression<Func<AppUser, bool>> expression, string? IncludeProperites = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppUser> GetAll(Expression<Func<AppUser, bool>>? expression = null, string? IncludeProperites = null)
        {
            throw new NotImplementedException();
        }

        public AppUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<AppUser> entities)
        {
            throw new NotImplementedException();
        }
    }
}
