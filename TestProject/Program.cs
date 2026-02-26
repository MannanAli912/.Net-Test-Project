//using Microsoft.EntityFrameworkCore;
//using TestProject.Data;
//using TestProject.Interfaces;
//using TestProject.Services;

//var builder = WebApplication.CreateBuilder(args);

//// 1. SERVICES CONFIGURATION
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer(); // Swagger ke liye zaroori hai
//builder.Services.AddSwaggerGen(); // Swagger UI ka generator

//// Database & DI
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddScoped<IRegistrationService, RegistrationService>();

//var app = builder.Build();

//// 2. MIDDLEWARE PIPELINE
//// Is section ko dhyan se update karein
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger(); // Swagger spec generate karega
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//        options.RoutePrefix = string.Empty; // Isse project run hote hi Swagger khulega
//    });
//}

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();


using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using TestProject.Interfaces;
using TestProject.Middleware;
using TestProject.Services;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();  

 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Koperasi Tentera API v1");
        options.RoutePrefix = "swagger"; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();