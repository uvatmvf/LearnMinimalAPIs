using MinApi.Services;
using RestSharp;

namespace TodoClient.Data;
public class TodoDataService
{
    public IRestResponse Post(string Title)
    {
        var todoRestClient = new RestClient("http://localhost/todoitems");
        var request = new RestRequest(Method.POST);
        request.AddJsonBody(new Dictionary<string, string>() { { "Title", Title } });
        var response = todoRestClient.Execute(request);
        return response;
    }

    public IList<TodoItem> GetAll()
    {
        var request = new List<TodoItem>();
        var todoRestClient = new RestClient("http://localhost/todoitems");
        var restRequest = new RestRequest(Method.GET);        
        var items = todoRestClient.Execute(restRequest);

        return request;
    }
}