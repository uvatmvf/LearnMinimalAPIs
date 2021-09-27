using MinApi;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<HelloService>(new HelloService());

var app = builder.Build();

app.MapGet("/hello", (HttpContext context, HelloService helloService) => 
    helloService.SayHello(context.Request.Query["name"].ToString()));

app.Run();
