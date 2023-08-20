
var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
 .SetBasePath(builder.Environment.ContentRootPath)
 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
 .Build();


builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(
   options =>
   {
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
   }
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
