using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Messaging.EventHubs.Consumer;
using System.Text;

// Connection + Hub
string STRS = "";
string NAME = "testhub";

// Default consumer group
string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

bool running = true;

while (running)
{
    Console.WriteLine("\n--- Event Hubs Demo ---");
    Console.WriteLine("Press 1 = Produce (Send Message)");
    Console.WriteLine("Press 2 = Consume (Read Messages)");
    Console.WriteLine("Press 0 = Quit");
    Console.Write("Enter choice: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await ProduceMessage();
            break;

        case "2":
            await ConsumeMessages();
            break;

        case "0":
            running = false;
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid option, try again.");
            break;
    }
}

async Task ProduceMessage()
{
    await using var producer = new EventHubProducerClient(STRS, NAME);

    Console.Write("Enter message to send: ");
    string msg = Console.ReadLine() ?? "Default message";

    using EventDataBatch batch = await producer.CreateBatchAsync();
    batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(msg)));

    await producer.SendAsync(batch);
    Console.WriteLine("Message sent successfully!");
}

async Task ConsumeMessages()
{
    await using var consumer = new EventHubConsumerClient(
        consumerGroup,
        STRS,
        NAME);

    Console.WriteLine("Listening for messages... (press Ctrl+C to stop)");

    await foreach (PartitionEvent ev in consumer.ReadEventsAsync())
    {
        string body = Encoding.UTF8.GetString(ev.Data.Body.ToArray());
        Console.WriteLine($"Received: {body}");
    }
}
