using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using DMT.Domain.Interfaces;
using DMT.Domain.Models.ToFrontend;
using DMT.Domain.Models.ToShardApisAgent;
using DMT.Domain.Models.ToTaskProcessing;
using Microsoft.Net.Http.Headers;
using Task = DMT.Domain.Models.ToFrontend.Task;

namespace DMT.RequestsSender;

internal class RequestSender : IRequestSender
{
    private readonly HttpClient _httpClient;

    private const string RequestUriAllApis = "api/getall";
    private const string RequestUriApisForParameters = "api/parameters";
    private const string RequestUriApisWithParametersForResult = "api/data";

    public RequestSender()
    {
        string uriToShardApiAgent = "http://localhost:8000";
        
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(uriToShardApiAgent);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        
        
        
        // _httpClient = httpClient;
        
        
        
        // HttpResponseMessage response = httpClient.GetAsync("WeatherForecast").Result;  // Blocking call!
        // if (response.IsSuccessStatusCode)
        // {
        //     var products = response.Content.ReadAsStringAsync().Result;
        // }
        // else
        // {
        //     Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        // }
        
        
        // _httpClient = httpClient;
        //
        // _httpClient.BaseAddress = new Uri("https://api.github.com/");
        //
        // _httpClient.DefaultRequestHeaders.Add(
        //     HeaderNames.Accept, "application/vnd.github.v3+json");
        // _httpClient.DefaultRequestHeaders.Add(
        //     HeaderNames.UserAgent, "HttpRequestsSample");
    }

    // private string HttpGetResponseShardApiAgent(string requestUri)
    // {
    //     HttpResponseMessage response = _httpClient.GetAsync(requestUri).Result;
    //     if (response.IsSuccessStatusCode)
    //     {
    //         return response.Content.ReadAsStringAsync().Result;
    //     }
    //     throw new Exception($"{(int)response.StatusCode} ({response.ReasonPhrase})");
    // }

    private TOut? HttpPostResponseShardApiAgent<TIn, TOut>(string requestUri, TIn userTask)
    {
        var userTaskJson = new StringContent(
            JsonSerializer.Serialize(userTask),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        
        var response = _httpClient.PostAsync(requestUri, userTaskJson).Result;
        var products = ResponseShardApiAgent(response);

        TOut? result = JsonSerializer.Deserialize<TOut>(products);

        return result;
    }
    
    private string ResponseShardApiAgent(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return response.Content.ReadAsStringAsync().Result;
        
        throw new Exception($"{(int)response.StatusCode} ({response.ReasonPhrase})");
    }

    public ApiTasks? GetAllApis()
    {
        // string response = HttpGetResponseShardApiAgent(RequestUriAllApis);
        string requestUri = RequestUriAllApis;
        
        HttpResponseMessage response = _httpClient.GetAsync(requestUri).Result;
        string products = ResponseShardApiAgent(response);

        ApiTask[]? apiTasks = 
            JsonSerializer.Deserialize<ApiTask[]>(products);

        return new ApiTasks(apiTasks);
    }
    
    public UserTask<Task>? GetApisWithParameters(UserTask<string> userTask)
    {
        string requestUri = RequestUriApisForParameters;
        return HttpPostResponseShardApiAgent<UserTask<string>,UserTask<Task>>(requestUri,  userTask);
    }
    
    public UserTask<TaskData>? GetFinalResult(UserTask<TaskToResult> userTask)
    {
        string requestUri = RequestUriApisWithParametersForResult;
        return HttpPostResponseShardApiAgent<UserTask<TaskToResult>,UserTask<TaskData>>(requestUri,  userTask);
    }

    
    // public async Task CreateItemAsync(ApiWithoutParameters todoItem)
    // {
    //     var todoItemJson = new StringContent(
    //         JsonSerializer.Serialize(todoItem),
    //         Encoding.UTF8,
    //         MediaTypeNames.Application.Json); // using static System.Net.Mime.MediaTypeNames;
    //
    //     using var httpResponseMessage =
    //         await _httpClient.PostAsync("/api/TodoItems", todoItemJson);
    //
    //     httpResponseMessage.EnsureSuccessStatusCode();
    // }
    
    
}