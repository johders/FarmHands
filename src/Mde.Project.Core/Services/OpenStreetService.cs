﻿using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;
using System.Text.Json;

namespace Mde.Project.Core.Services
{
    public class OpenStreetService : IOpenStreetService
    {
        private readonly HttpClient _httpClient;
        public OpenStreetService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("OpenStreet");
        }
        public async Task<ResultModel<List<OpenStreetResult>>> GetCoordinatesFromAddressAsync(string address)
        {
            var result = new ResultModel<List<OpenStreetResult>>();

            try
            {
                var relativeUrl = $"search?format=json&q={Uri.EscapeDataString(address)}";
                var response = await _httpClient.GetAsync(relativeUrl);

                if (!response.IsSuccessStatusCode)
                {
                    result.Errors.Add("Error fetching API data");
                }

                var content = await response.Content.ReadAsStringAsync();
                var results = JsonSerializer.Deserialize<List<OpenStreetResult>>(content);

                if (results?.Count > 0)
                {
                    result.Data = results;
                }
                else
                {
                    result.Errors.Add("No locations found for address input.");
                }
            }
            catch (Exception ex)
            {
                    result.Errors.Add(ex.Message);
            
            }
            return result;
        }
    }
}
