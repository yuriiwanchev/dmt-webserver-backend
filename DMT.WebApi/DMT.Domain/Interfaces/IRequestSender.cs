using DMT.Domain.Models.ToFrontend;
using DMT.Domain.Models.ToShardApisAgent;
using DMT.Domain.Models.ToTaskProcessing;
using Task = DMT.Domain.Models.ToFrontend.Task;

namespace DMT.Domain.Interfaces;

public interface IRequestSender
{
    ApiTasks? GetAllApis();
    UserTask<Task>? GetApisWithParameters(UserTask<string> userTask);
    UserTask<TaskData>? GetFinalResult(UserTask<TaskToResult> userTask);
}