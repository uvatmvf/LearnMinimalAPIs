using MinApi;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<HelloService>(new HelloService());

var app = builder.Build();
app.UseSwagger();

app.MapGet("/hello", (HttpContext context, HelloService helloService) => 
    helloService.SayHello(context.Request.Query["name"].ToString()));
app.UseSwaggerUI();

app.Run();
