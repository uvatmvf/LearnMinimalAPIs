using MinApi.Services;
using Newtonsoft.Json;
using RestSharp;

namespace TodoClient.Data;
public class TodoDataService
{
    private readonly string apiEndpoint = "https://localhost:7088/todoitems"; // TODO fix this 

    public Task<IRestResponse> Delete(TodoItem todo) =>
        Task.Run(() =>
        {
            var todoRestClient = new RestClient($"{apiEndpoint}/{todo.Id}");
            var restRequest = new RestRequest(Method.DELETE);
            return todoRestClient.Execute(restRequest);
        });

    public Task<IList<TodoItem>> GetAllAsync() =>
        Task.Run<IList<TodoItem>>(() =>
        {
            var todoRestClient = new RestClient($"{apiEndpoint}");
            var restRequest = new RestRequest(Method.GET);
            var response = todoRestClient.Execute(restRequest);
            return JsonConvert.DeserializeObject<List<TodoItem>>(response.Content);
        });
    public Task<IRestResponse> Post(string Title) =>
        Task.Run(() =>
        {
            var todoRestClient = new RestClient($"{apiEndpoint}");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(new TodoItem() { Title = Title }));
            var response = todoRestClient.Execute(request);
            return response;
        });

}