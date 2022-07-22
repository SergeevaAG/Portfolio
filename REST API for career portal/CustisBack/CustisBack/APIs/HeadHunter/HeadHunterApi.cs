using System.Net.Http.Headers;
using System.Text;
using CustisCareers.APIs.HeadHunter;
using CustisCareers.APIs.HeadHunter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustisBack.APIs.HeadHunter
{
    public class HeadHunterApi
    {
        /// <summary>
        /// Get information about access rights
        /// https://github.com/hhru/api/blob/master/docs/me.md
        /// </summary>
        /// <returns>Response in JSON object</returns>
        public JObject GetMe()
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(HeadHunterOptions.TokenType, HeadHunterOptions.AccessToken);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "api-custis-agent");
            var response = httpClient.GetAsync($"https://{HeadHunterConstants.HeadHunterApiDomain}/me").Result;

            var responseStr = response.Content.ReadAsStringAsync().Result;
            var responseObj = JsonConvert.DeserializeObject<JObject>(responseStr);
            return responseObj;
        }

        /// <summary>
        /// Search vacancies
        /// https://github.com/hhru/api/blob/master/docs/vacancies.md#search
        /// </summary>
        /// <param name="employerId">company ID</param>
        /// <param name="page">Page number</param>
        /// <param name="perPage">Number of vacancies to be requested</param>
        /// <param name="parameters">Other parameters, which will be appended to query</param>
        /// <returns>VacanciesResponse object</returns>
        public VacanciesResponse GetVacancies(int? employerId, int page = 1, int perPage = 100, KeyValuePair<string,string>[] parameters = null)
        {
            if (perPage > 100) throw new Exception("per_page limited to 100");
            
            var sb = new StringBuilder();
            if(employerId != null)
                sb.AppendFormat("employer_id={0}&", employerId);
            
            if(parameters != null)
                foreach (var option in parameters)
                    sb.AppendFormat("{0}={1}&", option.Key, option.Value);
            
            
            var queryStr = "?" + sb.ToString().TrimEnd('&');

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(HeadHunterOptions.TokenType, HeadHunterOptions.AccessToken);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "api-custis-agent");
            var response = httpClient.GetAsync($"https://{HeadHunterConstants.HeadHunterApiDomain}/vacancies" + queryStr).Result;
            
            var responseStr = response.Content.ReadAsStringAsync().Result;
            var responseObj = JsonConvert.DeserializeObject<VacanciesResponse>(responseStr);
            return responseObj;
        }
        
        /// <summary>
        /// Generates new access token
        /// https://github.com/hhru/api/blob/master/docs/authorization_for_application.md#get-client-auth
        /// </summary>
        /// <returns>modified HeadHunterOptions</returns>
        public void GenerateAccessToken()
        {
            using var httpClient = new HttpClient();
            using var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new ("grant_type", "client_credentials"),
                new ("client_id", HeadHunterOptions.ClientId),
                new ("client_secret", HeadHunterOptions.Secret),
            });
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var response = httpClient.PostAsync($"https://{HeadHunterConstants.HeadHunterDomain}/oauth/token", content).Result;

            var responseStr = response.Content.ReadAsStringAsync().Result;
            var responseObj = JsonConvert.DeserializeObject<JObject>(responseStr);
            if (responseObj == null)
                throw new Exception("Unable to generate headhunter access token (invalid response): " + responseStr);
            
            HeadHunterOptions.AccessToken = responseObj.Value<string>("access_token");
            HeadHunterOptions.TokenType = "Bearer";
            
            if (HeadHunterOptions.AccessToken == null)
                throw new Exception("Unable to generate headhunter access token: " + responseStr);
        }
    }
}