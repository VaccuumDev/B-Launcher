using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace BW_Launcher.Models;

public struct Version
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string linkLinux { get; set; }
    public string linkWindows { get; set; }

    public Version() { this.id = "0"; this.name = "error"; this.description = "ERROR"; this.linkLinux = ""; this.linkWindows = ""; }
}

public class JSONProcesser
{
    public static async Task<List<Version>> GetRemoteJsonAsync(string url)
    {
        try
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(url).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode) return new List<Version>();

            var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var stopwatch = Stopwatch.StartNew();
            var data = JsonSerializer.Deserialize<List<Version>>(jsonString);
            stopwatch.Stop();
            Log.Information($"Parsing took {stopwatch.ElapsedMilliseconds} ms");
            return data ?? new List<Version>();
        }
        catch (HttpRequestException) { return new List<Version> { new Version() }; }
        catch (TaskCanceledException) { return new List<Version> { new Version() }; }
        catch (JsonException) { return new List<Version> { new Version() }; }
        catch
        {
            return new List<Version>();
        }
        /*string jsonString;

        using HttpClient client = new HttpClient();
        Log.Information($"Http client created for {url}");

        HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
        Log.Information($"Url status {(int)response.StatusCode} {response.ReasonPhrase}");

        if (response.IsSuccessStatusCode)
        {
            jsonString = await response.Content.ReadAsStringAsync();
            Log.Information("Content received");
            //Log.Information(jsonString);

            var stopwatch = Stopwatch.StartNew();
            var data = JsonSerializer.Deserialize<List<Version>>(jsonString);
            stopwatch.Stop();
            Log.Information($"Parsing took {stopwatch.ElapsedMilliseconds} ms");

            if (data != null) return data;
            else return new List<Version>();
        }
        else
        {
            Log.Error("Failed to get content from URL");
            return new List<Version> { new Version() };
        }*/
    }
}
