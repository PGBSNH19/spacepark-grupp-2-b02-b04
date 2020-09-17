using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Collections.Generic;
using SpacePark.Models;

namespace SpacePark.Services
{
    public class Repository : IRepository
    
    {
        protected readonly SpaceParkContext _context;
        protected readonly ILogger _logger;

        public Repository(SpaceParkContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<T> Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> Delete<T>(int id)  where T : class
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            _context.Set<T>().Remove(entity);
            await Save();
            return entity;
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Update<T>(T entity)  where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
            return entity;
        }
    }
}