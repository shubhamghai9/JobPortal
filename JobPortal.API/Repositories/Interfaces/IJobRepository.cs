using JobPortal.API.Domain.Entities;

namespace JobPortal.API.Repositories.Interfaces
{
    public interface IJobRepository
    {
        Task<(List<Job> Jobs, int TotalCount)> GetFilteredJobsAsync(string? search, int pageNumber, int pageSize);
        Task<Job?> GetByIdAsync(int id);
        Task<Job> AddAsync(Job job);
        Task<bool> UpdateAsync(Job job);
        Task<bool> DeleteAsync(Job job);
    }
}
