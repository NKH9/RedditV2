using Microsoft.Extensions.Hosting;
using Reddit.Models;
using SQLitePCL;
using System.Linq.Expressions;

namespace Reddit.Repositories
{
    public class CommunitiesRepository : ICommunitiesRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunitiesRepository (ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task<PagedList<Community>> GetCommunities(int pageNumber, int pageSize, string? searchTerm, string? sortTerm = null, bool isAscending = true)
        {
            var communities = _context.Communities.AsQueryable();

            if (searchTerm != null)
            {
                communities = communities.Where(communitt => communitt.Name.Contains(searchTerm) || communitt.Description.Contains(searchTerm));
            }

            if (isAscending)
            {
                communities = communities.OrderBy(GetSortExpression(sortTerm));
            }
            else
            {
                communities = communities.OrderByDescending(GetSortExpression(sortTerm));

            }
            return await PagedList<Community>.CreateAsync(communities, pageNumber, pageSize);
        }

        private Expression<Func<Community, object>> GetSortExpression(string? sortTerm)
        {
            sortTerm = sortTerm?.ToLower();
            return sortTerm switch
            {
                "createdAt" => communitt => communitt.CreatedAt,
                "PostsCount" => communitt => communitt.Posts.Count(),
                "subscribersCount" => communitt => communitt.Subscribers.Count(),
                _ => communitt => communitt.Id
            };
        }
    }
}
