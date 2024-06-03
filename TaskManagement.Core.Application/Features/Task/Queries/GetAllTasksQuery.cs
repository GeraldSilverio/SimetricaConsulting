using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Application.Dtos.Task;
using TaskManagement.Core.Application.Exceptions;
using TaskManagement.Core.Application.Interfaces;
using TaskManagement.Core.Application.Interfaces.Repositories;
using TaskManagement.Core.Application.Wrappers;
using TaskManagement.Core.Domain.Settings;

namespace TaskManagement.Core.Application.Features.Task.Queries
{
    public class GetAllTasksQuery : IRequest<Response<List<TaskDto>>>
    {

    }

    public class GetAllATasksQueryHandler : IRequestHandler<GetAllTasksQuery, Response<List<TaskDto>>>
    {
        private readonly IAccountService _accountService;
        private readonly ITasksRepository _tasksRepository;

        public GetAllATasksQueryHandler(IAccountService accountService, ITasksRepository tasksRepository)
        {
            _accountService = accountService;
            _tasksRepository = tasksRepository;
        }

        public async Task<Response<List<TaskDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string idUser = _accountService.GetIdUser();
                var tasks = await _tasksRepository.GetAllAsync(idUser);

                if (tasks.Count == 0)
                {
                    throw new ApiException("Tasks not found", (int)HttpStatusCode.NoContent);
                }

                return new Response<List<TaskDto>>(tasks);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
