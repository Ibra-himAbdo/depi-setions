using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiCall
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var clinet = new HttpClient();

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                
            };

            var json = await clinet.GetStringAsync("https://dummyjson.com/products/1");

            Product? product = JsonSerializer.Deserialize<Product>(json, options);

            Console.WriteLine(product);
        }
    }

    class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - Title: {Title} - Description: {Description} - Category: {Category} - Price: {Price}";
        }
    }
}
