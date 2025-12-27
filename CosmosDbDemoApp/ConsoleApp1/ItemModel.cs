using Newtonsoft.Json;

namespace ConsoleApp1;

public class ItemModel
{
    [JsonProperty(propertyName: "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Description { get; set; }

    public string Type { get; set; }

    public int Quantity { get; set; }
}
