using MinApi.Services;
using Newtonsoft.Json;
using RestSharp;

namespace TodoClient.Data;
public class TodoDataService
{
    private readonly string apiEndpoint = "https://localhost:7088/todoitems"; // TODO fix this 
    
    public void Delete(TodoItem todo)
    {
        var todoRestClient = new RestClient($"{apiEndpoint}/{todo.Id}");
        var restRequest = new RestRequest(Method.DELETE);
        todoRestClient.Execute(restRequest);       
    }

    public IList<TodoItem> GetAll()
    {
        var todoRestClient = new RestClient($"{apiEndpoint}");
        var restRequest = new RestRequest(Method.GET);        
        var response = todoRestClient.Execute(restRequest);
        return JsonConvert.DeserializeObject<List<TodoItem>>(response.Content);
    }
    public IRestResponse Post(string Title)
    {
        var todoRestClient = new RestClient($"{apiEndpoint}");
        var request = new RestRequest(Method.POST);
        request.AddJsonBody(JsonConvert.SerializeObject(new TodoItem() { Title = Title }));
        var response = todoRestClient.Execute(request);
        return response;
    }

}