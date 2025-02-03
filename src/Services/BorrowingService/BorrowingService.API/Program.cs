using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//add services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
//minimal API
builder.Services.AddCarter();

//validation
builder.Services.AddValidatorsFromAssembly(assembly);

//exception
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Cấu hình MongoSettings từ appsetting.json
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

//Dang ki mongodb client
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    
    return new MongoClient(settings.ConnectionString);
});

//Dang ky mongodb 
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

//Async communication service
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());


// Đăng ký Health Check cho MongoDB
builder.Services
        .AddSingleton(sp => new MongoClient("mongodb://adminMongo123:adminMongo123@localhost:27018"))
        .AddHealthChecks()
        .AddMongoDb();

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();
