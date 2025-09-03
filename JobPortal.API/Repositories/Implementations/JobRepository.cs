using JobPortal.API.Data;
using JobPortal.API.Domain.Entities;
using JobPortal.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.API.Repositories.Implementations
{
    public class JobRepository : IJobRepository
    {
        private readonly JobPortalDbContext _context;

        public JobRepository(JobPortalDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Job> Jobs, int TotalCount)> GetFilteredJobsAsync(string? search, int pageNumber, int pageSize)
        {
            var query = _context.Jobs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchLower = search.ToLower();

                query = query.Where(j =>
                    j.Title.ToLower().Contains(searchLower) ||
                    j.Location.ToLower().Contains(searchLower));
            }

            int totalCount = await query.CountAsync();

            var jobs = await query
                .OrderByDescending(j => j.PostedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (jobs, totalCount);
        }

        public async Task<Job?> GetByIdAsync(int id) =>
            await _context.Jobs.FindAsync(id);

        public async Task<Job> AddAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<bool> UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Job job)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
