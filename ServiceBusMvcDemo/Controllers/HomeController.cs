using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiceBusMvcDemo.Models;
using Azure.Messaging.ServiceBus;

namespace ServiceBusMvcDemo.Controllers;

public class HomeController : Controller
{
     private readonly string _connectionString;
    private readonly string _queueName;

    
    public HomeController(IConfiguration config)
    {
        _connectionString = config["AzureServiceBus:ConnectionString"];
        _queueName = config["AzureServiceBus:QueueName"];
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
        public async Task<IActionResult> Send(string message)
        {
            await SendMessageAsync(message);
            ViewBag.Status = "Message sent successfully!";
            return View("Index");
        }

        public async Task<IActionResult> Receive()
        {
            var messages = await ReceiveMessagesAsync();
            return View("Messages", messages);
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task SendMessageAsync(string message)
        {
            await using var client = new ServiceBusClient(_connectionString);
            var sender = client.CreateSender(_queueName);

            var busMessage = new ServiceBusMessage(message);
            await sender.SendMessageAsync(busMessage);
        }

        public async Task<List<string>> ReceiveMessagesAsync()
        {
            var messages = new List<string>();

            await using var client = new ServiceBusClient(_connectionString);
            var receiver = client.CreateReceiver(_queueName);

            var received = await receiver.ReceiveMessagesAsync(maxMessages: 5);

            foreach (var msg in received)
            {
                messages.Add(msg.Body.ToString());
                await receiver.CompleteMessageAsync(msg); // Remove from queue
            }

            return messages;
        }
}