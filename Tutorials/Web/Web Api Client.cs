using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tutorials.Web;

// Taken from https://docs.microsoft.com/en-gb/dotnet/csharp/tutorials/console-webapiclient
// Shows how to send HTTP requests, deserialize JSON responses, configure deserialization with attributes.
static class WebApiExample
{
    static readonly HttpClient client = new();

    public static async Task Test()
    {
        var repositories = await ProcessRepositories();
        int descLength = 15;
        string format = "{0, -" + descLength + "} {1}";

        foreach (var repo in repositories)
        {
            Console.WriteLine(format, "Name:", repo.Name);
            DisplayDescription(repo.Description, format);
            Console.WriteLine(format, "Url:", repo.GitHubHomeUrl);
            Console.WriteLine(format, "Homepage:", repo.Homepage);
            Console.WriteLine(format, "Watchers:", repo.Watchers);
            Console.WriteLine();
        }
    }

    static void DisplayDescription(string desc, string format)
    {
        var lines = desc is null ? new List<string>() { "" } : SplitDescriptionIntoLines(desc);
        var lineName = "Description:";
        for (int i = 0; i < lines.Count; i++)
            Console.WriteLine(format, i == 0 ? lineName : "", lines[i]);
    }

    static List<string> SplitDescriptionIntoLines(string desc)
    {
        var words = desc.Split(' ');
        string line = string.Empty;
        List<string> lines = new();
        foreach (var word in words)
        {
            line += word + " ";
            if (line.Length > 70)
            {
                lines.Add(line);
                line = "";
            }
        }

        // make sure that lines shorter than 70 get added to the list as well
        if (line != string.Empty)
            lines.Add(line);

        return lines;
    }

    static async Task<List<Repository>> ProcessRepositories()
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
        return await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
    }
}

class Repository
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("html_url")]
    public Uri GitHubHomeUrl { get; set; }

    [JsonPropertyName("homepage")]
    public Uri Homepage { get; set; }

    [JsonPropertyName("watchers")]
    public int Watchers { get; set; }
}