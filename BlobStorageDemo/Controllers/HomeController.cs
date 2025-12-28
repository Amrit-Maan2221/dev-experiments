using Azure.Storage.Blobs;          // Import Azure Blob Storage SDK for working with blobs
using Azure.Storage.Blobs.Models;   // Import models like BlobItem
using Microsoft.AspNetCore.Mvc;     // Import ASP.NET Core MVC functionality

namespace BlobStorageDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;  // To read configuration from appsettings.json
        private readonly string _connectionString;      // Storage account connection string
        private readonly string _containerName;         // Name of the blob container

        // Constructor to initialize configuration and read blob storage settings
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["AzureBlobStorage:ConnectionString"];  // Read connection string
            _containerName = _configuration["AzureBlobStorage:ContainerName"];        // Read container name
        }

        // Method to create or get a reference to the blob container
        private async Task<BlobContainerClient> GetContainerClient()
        {
            var containerClient = new BlobContainerClient(_connectionString, _containerName); // Create container client
            await containerClient.CreateIfNotExistsAsync(); // Create container if it does not exist (private by default)
            return containerClient; // Return the container client for further operations
        }

        // Action to list all blobs in the container
        public async Task<IActionResult> Index()
        {
            var containerClient = await GetContainerClient(); // Get container client
            var blobs = new List<string>(); // List to hold blob names

            // Iterate over all blobs in the container asynchronously
            await foreach (BlobItem blob in containerClient.GetBlobsAsync())
            {
                blobs.Add(blob.Name); // Add blob name to the list
            }

            return View(blobs); // Pass the list to the view for display
        }

        // Action to upload a file to the blob container
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0) // Check if file is selected and not empty
            {
                var containerClient = await GetContainerClient(); // Get container client
                var blobClient = containerClient.GetBlobClient(file.FileName); // Get blob client for the specific file

                // Open file stream and upload the file
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, overwrite: true); // Upload and overwrite if file exists
                }
            }

            return RedirectToAction("Index"); // Redirect back to the list page
        }

        // Action to download a blob from the container
        public async Task<IActionResult> Download(string name)
        {
            var containerClient = await GetContainerClient(); // Get container client
            var blobClient = containerClient.GetBlobClient(name); // Get blob client for the specific file

            var memoryStream = new MemoryStream(); // Create a memory stream to hold the downloaded file
            await blobClient.DownloadToAsync(memoryStream); // Download blob content into memory
            memoryStream.Position = 0; // Reset stream position to start

            return File(memoryStream, "application/octet-stream", name); // Return file to browser for download
        }
    }
}
