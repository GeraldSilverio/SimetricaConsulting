using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Application.Dtos.Task;
using TaskManagement.Core.Domain.Entities;

namespace TaskManagement.Core.Application.Interfaces.Repositories
{
    public interface ITasksRepository : IBaseRepository<Tasks>, IDapperRepository<Tasks>
    {
    }
}
