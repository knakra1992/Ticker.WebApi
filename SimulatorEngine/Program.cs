using SimulatorEngine.Business.Hubs;
using SimulatorEngine.Business.Implementation;
using SimulatorEngine.Business.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSingleton<ISubscriptionHelper, SubscriptionHelper>()
    .AddSingleton<ITickerHelper, TickerHelper>()
    .AddSwaggerGen()
    .AddMemoryCache()
    .AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(60)
};

webSocketOptions.AllowedOrigins.Add("https://localhost:7278");
webSocketOptions.AllowedOrigins.Add("http://localhost:4200");

app.UseWebSockets(webSocketOptions);
app.UseCors(config =>
{
    config.AllowAnyOrigin();
    config.AllowAnyHeader();
    config.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<SourceHub>("api/subscribe");

app.Run();