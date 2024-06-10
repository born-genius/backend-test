using Newtonsoft.Json;
using System.Net.Http.Headers;
using TechnicalTest.Core.Interfaces.ExternalServices;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Infrastructure.ExternalServices
{
    public class BankService : IBankService
    {
        public async Task<Bank> GetBanks(string url, string token = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Bank>(response);
                return result;
            };
        }
    }
}
