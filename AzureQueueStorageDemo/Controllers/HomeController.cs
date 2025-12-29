using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AzureQueueStorageDemo.Models;
using AzureQueueStorageDemo.Services;
namespace AzureQueueStorageDemo.Controllers;

public class HomeController : Controller
{
    private readonly QueueService _queue;
    public HomeController(QueueService queue)
    {
        _queue = queue;
    }

    public async Task<IActionResult> Index()
    {
        var messages = await _queue.PeekMessagesAsync();
        return View(messages);
    }

    [HttpPost]
    public async Task<IActionResult> Send(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
            await _queue.SendMessageAsync(text);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Receive()
    {
        TempData["Received"] = await _queue.ReceiveAndDeleteAsync();
        return RedirectToAction("Index");
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
}

