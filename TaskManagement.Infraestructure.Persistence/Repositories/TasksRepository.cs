using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Application.Dtos.Task;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Infraestructure.Persistence.Context;

namespace TaskManagement.Infraestructure.Persistence.Repositories
{
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {

        public TasksRepository(ApplicationContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
        {

        }
        public async Task<List<TaskDto>> GetAllAsync(string idUser)
        {
            try
            {
                using var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));

                const string query = @"SELECT T.Id,T.Name, TS.Name as Status,TS.Id as IdTaskStatus,T.IdUser 
                FROM TASK T INNER JOIN TaskStatus TS on T.IdTaskStatus = TS.Id
                WHERE T.IsActive = 1 AND T.IdUser = @idUser";

                var tasks = await connection.QueryAsync<TaskDto>(query, new {idUser});
                return tasks.ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<TaskDto> GetByIdAsync(int id)
        {
            try
            {
                using var connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));

                const string query = @"SELECT T.Id,T.Name, TS.Name as Status,TS.Id as IdTaskStatus,T.IdUser 
                FROM TASK T INNER JOIN TaskStatus TS on T.IdTaskStatus = TS.Id
                WHERE T.Id = @id and T.IsActive = 1";

                var task = await connection.QueryFirstOrDefaultAsync<TaskDto>(query, new {id});
                return task;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }


    }
}
