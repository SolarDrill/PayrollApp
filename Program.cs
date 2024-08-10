using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using payroll.Data;
using ServiceReference1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payroll API", Version = "v1" });
});

// Manually create the SOAP client
var soapClient = new ServiceReference1.SSWSSoapClient(ServiceReference1.SSWSSoapClient.EndpointConfiguration.SSWSSoap);
builder.Services.AddSingleton(soapClient);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();


// Configure the HTTP request pipeline.

 app.UseSwagger();
 app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
