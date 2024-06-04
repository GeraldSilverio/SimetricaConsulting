using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using TaskManagement.Core.Application.Dtos.Task;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Infraestructure.Persistence.Context;

namespace TaskManagement.Infraestructure.Persistence.Repositories
{
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {
        private readonly ApplicationContext _dbContext;

        public TasksRepository(ApplicationContext dbContext, IConfiguration configuration) : base(dbContext,
            configuration)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tasks>> GetAllAsync(string idUser)
        {
            try
            {
                var tasks = await _dbContext.Tasks.Where(x => x.IdUser == idUser).Where(x => x.IsActive == true).Include(x=> x.TaskStatus)
                    .ToListAsync();


                return tasks;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<Tasks> GetByIdAsync(int id)
        {
            try
            {
                var tasks = await _dbContext.Tasks.Where(x => x.IsActive == true).Include(x=> x.TaskStatus).FirstOrDefaultAsync(x => x.Id == id);
                return tasks;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}