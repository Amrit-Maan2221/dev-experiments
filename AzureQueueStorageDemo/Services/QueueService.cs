using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace AzureQueueStorageDemo.Services;
public class QueueService
{
    private readonly QueueClient _queueClient;

    public QueueService(IConfiguration config)
    {
        var conn = config["AzureQueue:ConnectionString"];
        var queue = config["AzureQueue:QueueName"];

        _queueClient = new QueueClient(conn, queue);
        _queueClient.CreateIfNotExists();
    }

    public async Task SendMessageAsync(string message)
    {
        await _queueClient.SendMessageAsync(message);
    }

    public async Task<List<string>> PeekMessagesAsync()
    {
        var result = new List<string>();
        PeekedMessage[] messages = await _queueClient.PeekMessagesAsync(10);

        foreach (var msg in messages)
        {
            result.Add(msg.Body.ToString());
        }

        return result;
    }

    public async Task<string?> ReceiveAndDeleteAsync()
    {
        var message = await _queueClient.ReceiveMessageAsync();

        if (message.Value == null)
            return null;

        string body = message.Value.Body.ToString();
        await _queueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);

        return body;
    }
}
