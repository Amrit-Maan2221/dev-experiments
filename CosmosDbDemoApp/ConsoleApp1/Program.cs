using ConsoleApp1;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true)
                    .AddUserSecrets<Program>();


IConfiguration config = builder.Build();

string strCosmosEndpoint = config.GetValue<string>("CosmosDb:Endpoint") ?? "";
string strCosmosPrimaryKey = config.GetValue<string>("CosmosDb:PrimaryKey") ?? "";
string strCosmosDbname = config.GetValue<string>("CosmosDb:Database") ?? "";

CosmosClient cosmosClient = new(strCosmosEndpoint, strCosmosPrimaryKey);

Database db = cosmosClient.GetDatabase(strCosmosDbname);

Container container = db.GetContainer("items");
/*
ItemModel item = new ItemModel
{
    Name = "Laptop",
    Description = "Developer machine",
    Type = "electronics",   // Partition Key
    Quantity = 5
};

ItemModel item2 = new ItemModel
{
    Name = "Laptop2",
    Description = "Developer machine 2",
    Type = "electronics",   // Partition Key
    Quantity = 5
};


ItemResponse<ItemModel> response =
    await container.CreateItemAsync(item, new PartitionKey(item.Id));
ItemResponse<ItemModel> response2 =
    await container.CreateItemAsync(item2, new PartitionKey(item2.Id));

Console.WriteLine($"Item created with RU charge = {response.RequestCharge}");
Console.ReadLine();

ItemResponse<ItemModel> readItem =
    await container.ReadItemAsync<ItemModel>(item.Id, new PartitionKey(item.Id));

Console.WriteLine($"Read item: {readItem.Resource.Name}");
Console.WriteLine($"RU: {readItem.RequestCharge}");

Console.ReadLine();


var query = new QueryDefinition("SELECT * FROM c WHERE c.Type = @t")
    .WithParameter("@t", "electronics");

FeedIterator<ItemModel> result = container.GetItemQueryIterator<ItemModel>(query);

while (result.HasMoreResults)
{
    foreach (var doc in await result.ReadNextAsync())
        Console.WriteLine($"Found: {doc.Name} ({doc.Quantity})");
}

readItem.Resource.Quantity = 10;

await container.UpsertItemAsync(readItem.Resource, new PartitionKey(readItem.Resource.Id));

Console.WriteLine("Item updated");

await container.DeleteItemAsync<ItemModel>(readItem.Resource.Id, new PartitionKey(readItem.Resource.Id));

Console.WriteLine("Item deleted");



StoredProcedureExecuteResponse<string> spRes = await container.Scripts.ExecuteStoredProcedureAsync<string>(
    "helloWorld",
    new PartitionKey("laptop"),
    new dynamic[] { });


Console.WriteLine("helloWorld res : " + spRes.Resource);

Console.ReadLine();
*/
var itemsArray = new[]
{
    new { id = "1", name = "Laptop", qty = 5 },
    new { id = "2", name = "Mouse", qty = 10 }
};

var spResult = await container.Scripts.ExecuteStoredProcedureAsync<dynamic>(
    "sample",
    new PartitionKey("electronics"),   // must match the partition
    new object[] { itemsArray }
);


Console.WriteLine("Sample res : " + spResult.Resource);


Console.ReadLine();