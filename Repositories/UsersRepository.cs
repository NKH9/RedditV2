
using Reddit.Models;
using System.Linq.Expressions;

namespace Reddit.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext applcationDBContext)
        {
            _context = applcationDBContext;
        }

        public async Task<PagedList<User>> GetUsers(int page, int pageSize, string? sortKey, string? SearchKey, bool? isAscending)
        {
            var users = _context.Users.AsQueryable();

            //Filtering 
            if (SearchKey != null)
            {
                users = users.Where(p => p.Name.Contains(SearchKey) || p.Email.Contains(SearchKey));
            }
            
            

            // sorting 
            if (isAscending == true)
            {
                users = users.OrderBy(GetSortExpression(sortKey));
            }
            else
            {
                users = users.OrderByDescending(GetSortExpression(sortKey));
            }

            return await PagedList<User>.CreateAsync(users, page, pageSize);

        }
        public Expression<Func<User, object>> GetSortExpression(string? sortKey)
        {
            sortKey = sortKey?.ToLower();
            return sortKey switch
            {   "numbeerOfPosts" => user => user.Posts.Count(),
                _ => user => user.Id
            };
        }

               

    }
}
