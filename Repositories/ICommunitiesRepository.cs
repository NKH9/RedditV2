﻿using Reddit.Models;

namespace Reddit.Repositories
{
    public interface ICommunitiesRepository
    {
        public Task<PagedList<Community>> GetCommunities(int pageNumber, int pageSize, string? searchTerm, string? sortTerm = null, bool isAscending = true);

    }
}