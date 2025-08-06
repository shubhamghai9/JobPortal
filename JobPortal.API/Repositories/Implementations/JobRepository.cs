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

        public async Task<(List<Job> Jobs, int TotalCount)> GetFilteredJobsAsync(string? title, string? location, int pageNumber, int pageSize)
        {
            var query = _context.Jobs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(j => j.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(j => j.Location.Contains(location));

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
