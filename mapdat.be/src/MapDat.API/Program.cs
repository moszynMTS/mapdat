
using MapDat.Persistance.MongoSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();


builder.Services.AddApiServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "MapDatAPI");
});


app.UseHttpsRedirection();


app.UseCors();

app.MapControllers();


app.Run();