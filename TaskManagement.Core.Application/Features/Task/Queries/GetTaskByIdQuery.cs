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

namespace TaskManagement.Core.Application.Features.Task.Queries
{
    public class GetTaskByIdQuery : IRequest<Response<TaskDto>>
    {
        public int Id { get; set; }
    }
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Response<TaskDto>>
    {
        private readonly IAccountService _accountService;
        private readonly ITasksRepository _tasksRepository;

        public GetTaskByIdQueryHandler(IAccountService accountService, ITasksRepository tasksRepository)
        {
            _accountService = accountService;
            _tasksRepository = tasksRepository;
        }

        public async Task<Response<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string idUser = _accountService.GetIdUser();
                var task = await _tasksRepository.GetByIdAsync(request.Id);
                if (task == null)
                {
                    throw new ApiException("Task not found", (int)HttpStatusCode.NotFound);
                }
                if (task.IdUser != idUser)
                {
                    throw new ApiException("This task wasn´t created by you", (int)HttpStatusCode.Forbidden);
                }

                return new Response<TaskDto>(task);

            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
