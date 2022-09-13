using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class HttpManager
{


    private static readonly string accessToken = "77560b18-6977-4faf-9210-15d17a9c6ebf";
    private static readonly string host = "localhost";
    private static readonly int port = 3001;

    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> HttpGet(string url)
    {
        if (!client.DefaultRequestHeaders.Contains("access_token"))
            client.DefaultRequestHeaders.Add("access_token", accessToken);
        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        return await response.Content.ReadAsStringAsync();

    }


    public static async Task<string> HttpPost(string url)
    {
        if (!client.DefaultRequestHeaders.Contains("access_token"))
            client.DefaultRequestHeaders.Add("access_token", accessToken);
        using HttpResponseMessage response = await client.PostAsync(url, null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> HttpPost(string url, object payload = null)
    {
        if (!client.DefaultRequestHeaders.Contains("access_token"))
            client.DefaultRequestHeaders.Add("access_token", accessToken);
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }


    public static async Task<string> HttpPost(string url, Dictionary<string, string> payload)
    {
        if (!client.DefaultRequestHeaders.Contains("access_token"))
            client.DefaultRequestHeaders.Add("access_token", accessToken);
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }




    public static string BuildUrl(string path)
    {
        UriBuilder Url = new UriBuilder();
        Url.Host = host;
        Url.Port = port;
        Url.Path = path;
        return Url.ToString();
    }
}