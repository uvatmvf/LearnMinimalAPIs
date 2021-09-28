using RestSharp;

namespace TodoClient.Data;
public class TodoDataService
{
    public IRestResponse Post(string Title)
    {
        var todoRestClient = new RestClient("http://localhost/todoitems");
        var request = new RestRequest(Method.POST);
        request.AddJsonBody(new Dictionary<string, string>() { { "Title", Title } });
        return todoRestClient.Execute(request);
    }
}