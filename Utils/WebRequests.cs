﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.Utils;
public static class WebRequests
{
    internal class Release
    {
        public string? tag_name { get; set; }
    }

    public static async Task<bool> NewReleaseAvaiable(string currentVersion)
    {
        if (string.IsNullOrEmpty(currentVersion)) throw new ArgumentException(nameof(currentVersion));

        using var handler = new HttpClientHandler();
        handler.UseDefaultCredentials = true;

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

        HttpResponseMessage response = await client.GetAsync(Paths.LatestReleaseApi);
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            string? latest = JsonConvert.DeserializeObject<IEnumerable<Release>>(data)?.FirstOrDefault()?.tag_name;
            if (string.CompareOrdinal(latest, 0, currentVersion, 0, 5) > 0)
            {
                Console.WriteLine($"Release {latest} is newer than current version {currentVersion}");
                return true;
            }
            Console.WriteLine($"Release {latest} is not newer than current version {currentVersion}");
            return false;
        }

        Console.WriteLine($"Bad response: {response.StatusCode}");
        return false;
    }
}
