using System.Net.Http.Headers;
using DMT.Domain.Interfaces;
using DMT.Domain.Models.ToFrontend;
using DMT.Domain.Models.ToShardApisAgent;
using DMT.Domain.Models.ToTaskProcessing;
using DMT.RequestsSender;
using DMT.TaskProcessing;
using Microsoft.AspNetCore.Mvc;
using Task = DMT.Domain.Models.ToFrontend.Task;

namespace DMT.WebApi.Controllers;


[ApiController]
[Route("apis")]
public class ApiController : ControllerBase
{
    // private readonly HttpClient _httpClient;
    private readonly IRequestSender _requestSender;
    private readonly ILogger<ApiController> _logger;
    
    public ApiController(IRequestSender requestSender, ILogger<ApiController> logger)
    {
        _requestSender = requestSender;
        _logger = logger;
        
        // _httpClient = new HttpClient();
        // _httpClient.BaseAddress = new Uri("https://localhost:7291/");
        // _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    [HttpGet]
    public ActionResult<ApiTasks> GetAllApis()
    {
        return _requestSender.GetAllApis();
    }
    
    [HttpPost("/apisWithParameters")]
    public UserTask<Task>? GetApisWithParameters(UserTask<string> userTask)
    {
        try
        {
            var apisWithParameters = _requestSender.GetApisWithParameters(userTask);
            return apisWithParameters;
        }
        catch (Exception e)
        {
            _logger.LogError("Add Student: There is already exist Student with that id");
            // throw new AlreadyExistenceException("There is already exist Student with that id");
            throw new Exception();
        }
    }
    
    [HttpPost("/getResult")]
    public FileResult Get(UserTask<TaskToResult> userTask)
    {
        try
        {
            var result = _requestSender.GetFinalResult(userTask);
            
            string fileName = $"{result.User_id}_{result.Task_id}.csv";
            // string fileName = "test.csv";
            // byte[] fileBytes = CsvFileWorker.CreateCsvTest();
            var fileBytes = DataLinking.GetLinkingData(result);

            return File(fileBytes, "text/csv", fileName);
        }
        catch (Exception e)
        {
            _logger.LogError("Add Student: There is already exist Student with that id");
            // throw new AlreadyExistenceException("There is already exist Student with that id");
            throw new Exception();
        }
    }
}