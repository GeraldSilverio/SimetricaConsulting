using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Domain.Commons;
using TaskManagement.Infraestructure.Persistence.Context;

namespace TaskManagement.Infraestructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _dbContext;
        protected DbSet<T> Entities;
        protected readonly IConfiguration Configuration;

        public BaseRepository(ApplicationContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<T>();
            Configuration = configuration;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await Entities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity is AuditoryProperties entityModel)
                {
                    entityModel.IsActive = false;
                    Entities.Update(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task UpdateAsync(T entity, int id)
        {
            try
            {
                var entry = await Entities.FindAsync(id);
                _dbContext.Entry(entry).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
