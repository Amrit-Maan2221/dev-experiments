using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true)
    .AddUserSecrets<Program>();


IConfiguration config = builder.Build();

CosmosClient cosmosClient = new CosmosClient(config.GetValue<string>("CosmosDb:Endpoint"), config.GetValue<string>("CosmosDb:PrimaryKey"));