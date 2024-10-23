using Reddit.Models;
namespace Reddit.Repositories
{
    public interface IUsersRepository
    {
        public Task<PagedList<User>> GetUsers(int page, int pageSize, string? sortKey, string? SearchKey, bool? isAscending = true);
    }
}