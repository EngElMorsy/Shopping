using ECEMAPI.Extensions;
using ECEMAPI.Filters;
using ECEMCore;
using ECEMInfrastructure;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

//Log With Seriloh Libarary 
//log Event On Sytem With your Choice( Apply Configration AppSetting.json) 
// Consol/ File/ DataBase/ Seq)  
//Library 
//-- Serilog.AspNetCore// Read From Configration .Json
//and Log To file and Log to File With JsonFormatar 
//-- Serilog.Sinks.MSSqlServer// Read Configration form .Json
//To Log To SQl DataBase
//--Serilog.Sinks.Enrirch
// To enable to rich to Pc Name IP 
builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));





// Add services to the container.
// add Services of Dependency Injection
builder.Services.AddInfrastructureDependencies(builder.Configuration);
builder.Services.AddAppllicationServices();


//builder.Services.AddControllers();
// Add Golbal Filters
builder.Services.AddControllers(opt =>
{
    //opt.Filters.Add(new ValidationFilterAttribute());
    opt.Filters.Add<ValidationFilterAttribute>();
    // opt.Filters.Add(typeof(ValidationFilterAttribute));

}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Disable automatic validation
}); 

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

//Use MiddleWare Of InJectCorrelationId 
app.UseRequestContextLogging();
// User Log Serolog 
app.UseSerilogRequestLogging();
//Pipe line of MiddleWare For CustomExceptionHandler
app.UseCustomExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();
