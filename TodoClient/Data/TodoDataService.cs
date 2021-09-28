using MinApi.Services;
using Newtonsoft.Json;
using RestSharp;

namespace TodoClient.Data;
public class TodoDataService
{
    private readonly string apiAddress = "https://localhost:7088"; // TODO fix this 
    public IRestResponse Post(string Title)
    {
        var todoRestClient = new RestClient($"{apiAddress}/todoitems");
        var request = new RestRequest(Method.POST);
        request.AddJsonBody(JsonConvert.SerializeObject(new TodoItem() { Title = Title }));
        var response = todoRestClient.Execute(request);
        return response;
    }

    public IList<TodoItem> GetAll()
    {
        var todoRestClient = new RestClient($"{apiAddress}/todoitems");
        var restRequest = new RestRequest(Method.GET);        
        var response = todoRestClient.Execute(restRequest);
        return JsonConvert.DeserializeObject<List<TodoItem>>(response.Content);
    }
}