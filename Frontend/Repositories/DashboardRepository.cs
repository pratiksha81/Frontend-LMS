﻿using Frontend.Models;
using System.Text.Json;

namespace Frontend.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly HttpClient _httpClient;

        public DashboardRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<DashBoard> GetDashboardDataAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7178/GetDashboardData");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var dashboardData = JsonSerializer.Deserialize<DashBoard>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return dashboardData;
            }

            throw new HttpRequestException($"Failed to fetch dashboard data. Status code: {response.StatusCode}");
        }
        public async Task<List<OverdueBorrower>> GetOverdueBorrowersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7178/GetOverdueBorrowers");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var overdueBorrowers = JsonSerializer.Deserialize<List<OverdueBorrower>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return overdueBorrowers;
            }

            throw new HttpRequestException($"Failed to fetch overdue borrowers. Status code: {response.StatusCode}");
        }
    }
}
