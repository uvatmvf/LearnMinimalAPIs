using Microsoft.EntityFrameworkCore;
using MinApi;
using MinApi.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<HelloService>(new HelloService());
builder.Services.AddDbContext<TodoDbContext>(options => options.UseInMemoryDatabase("TodoItems"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/hello", (HttpContext context, HelloService helloService) => 
    helloService.SayHello(context.Request.Query["name"].ToString()));

app.MapGet("/todoitems", async (http) =>
{
    var dbContext = http.RequestServices.GetService<TodoDbContext>();
    var todoItems = await dbContext.TodoItems.ToListAsync();

});

app.MapGet("/todoitems/{id}", async (http) =>
{
    if (!http.Request.RouteValues.TryGetValue("id", out var id))
    {
        http.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return;
    }
    var dbContext = http.RequestServices.GetService<TodoDbContext>();
    var todoItem = await dbContext.TodoItems.FindAsync(int.Parse(id.ToString()));
    if (todoItem == null)
    {
        http.Response.StatusCode = (int)HttpStatusCode.NotFound;
        return;
    }
    await http.Response.WriteAsJsonAsync(todoItem);
});

app.MapPost("/todoitems", async (http) =>
{
    var todoItem = await http.Request.ReadFromJsonAsync<TodoItem>();
    var dbContext = http.RequestServices.GetService<TodoDbContext>();
    dbContext.TodoItems.Add(todoItem);
    await dbContext.SaveChangesAsync();
    http.Response.StatusCode = (int)HttpStatusCode.NoContent;
});

app.MapPut("/todoitems/{id}", async (http) =>
{
    if (!http.Request.RouteValues.TryGetValue("id", out var id))
    {
        http.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return;
    }
    var dbContext = http.RequestServices.GetService<TodoDbContext>();
    var todoItem = await dbContext.TodoItems.FindAsync(int.Parse(id.ToString()));
    if (todoItem == null)
    {
        http.Response.StatusCode = (int)HttpStatusCode.NotFound;
        return;
    }
    var inputToDoItem = await http.Request.ReadFromJsonAsync<TodoItem>();
    todoItem.IsCompleted = inputToDoItem.IsCompleted;
    await dbContext.SaveChangesAsync();
    http.Response.StatusCode = (int)HttpStatusCode.NoContent;

});

app.MapDelete("/todoitems/{id}", async (http) =>
{
    if (!http.Request.RouteValues.TryGetValue("id", out var id))
    {
        http.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return;
    }
    var dbContext = http.RequestServices.GetService<TodoDbContext>();
    var todoItem = await dbContext.TodoItems.FindAsync(int.Parse(id.ToString()));
    if (todoItem == null)
    {
        http.Response.StatusCode = (int)HttpStatusCode.NotFound;
        return;
    }
    dbContext.TodoItems.Remove(todoItem);
    await dbContext.SaveChangesAsync();
    http.Response.StatusCode = (int)HttpStatusCode.NoContent;
});

app.UseSwagger();
app.UseSwaggerUI();
app.Run();
