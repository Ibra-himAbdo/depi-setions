using System.Net.Http.Json;
using System.Reflection;

HttpClient httpClient = new HttpClient();

httpClient.BaseAddress = new Uri("https://api.groq.com/openai");

httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "YOUR_TOKEN_HERE");

var request = new {
    model = "",
    input = ""
};

HttpResponseMessage? result = await httpClient.PostAsJsonAsync("/v1/responses", request);

if(result.IsSuccessStatusCode) //200 - 299 
{
    var content = await result.Content.ReadAsStringAsync();
}
else if ((int)result.StatusCode == 401)
{
    
}

Console.WriteLine(result);
