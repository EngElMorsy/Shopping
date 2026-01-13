using ECEMAPI.Extensions;
using ECEMCore;
using ECEMInfrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// add Services of Dependency Injection
builder.Services.AddInfrastructureDependencies(builder.Configuration);
builder.Services.AddAppllicationServices();




builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi(); 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Pipe line of MiddleWare For CustomExceptionHandler
app.UseCustomExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
