using Elasticsearch;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var settings = new ConnectionSettings(new Uri("https://localhost:9200"))
    .BasicAuthentication("elastic", "yourStrongPassWord")
    .PrettyJson()
    .DefaultIndex("index");
settings.EnableApiVersioningHeader();
settings.ServerCertificateValidationCallback((o, cert, chain, errors) => true);
var client = new ElasticClient(settings);
builder.Services.AddSingleton<IElasticClient>(client);
var createIndexResponse = client.Indices.Create("index", index => index.Map<User>(x => x.AutoMap()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
