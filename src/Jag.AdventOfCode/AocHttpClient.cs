using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Jag.AdventOfCode.Config;

namespace Jag.AdventOfCode
{
    public class AocHttpClient
    {
        private readonly HttpClient httpClient;

        public AocHttpClient(HttpClient httpClient, AocHttpConfig aocHttpConfig)
        {
            this.httpClient = httpClient;
            
            httpClient.BaseAddress = aocHttpConfig.BaseAddress;
            httpClient.DefaultRequestHeaders.Add("Cookie", $"session={aocHttpConfig.SessionCookie}");
        }

        public bool IsConfigured => httpClient != null && httpClient.DefaultRequestHeaders.Contains("Cookie");

        public async Task<string> GetInput(int year, int day)
        {
            var response = await httpClient.GetAsync($"/{year}/day/{day}/input");
            var content = await response.Content.ReadAsStringAsync();

            GuardAgainstNonSuccessStatusCode(response, content);

            // Convert to windows line endings for consistency with copy pasted values
            return string.Join(Environment.NewLine,
                content
                    .Split(Environment.NewLine)
                    .Select(s => s.Replace("\n", Environment.NewLine)));
        }

        public async Task<SubmitResponse> SubmitAnswerAsync(int year, int day, int part, string answer)
        {
            var response = await httpClient.PostAsync($"/{year}/day/{day}/answer", CreateFormContent(part, answer));
            var content = await response.Content.ReadAsStringAsync();

            GuardAgainstNonSuccessStatusCode(response, content);

            if (content.Contains("That's not the right answer"))
            {
                return SubmitResponse.Incorrect;
            }

            if (content.Contains("You gave an answer too recently"))
            {
                return SubmitResponse.Timeout;
            }

            if (content.Contains("You don't seem to be solving the right level"))
            {
                return SubmitResponse.AlreadySolved;
            }

            if (content.Contains("That's the right answer!"))
            {
                return SubmitResponse.Correct;
            }

            var ex = new NotImplementedException("Unable to translate response: " + content);
            ex.Data["Response"] = response;
            throw ex;
        }

        private HttpContent CreateFormContent(int part, string answer)
        {
            return new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() 
            {
                new KeyValuePair<string, string>("answer", answer),
                new KeyValuePair<string, string>("level", part.ToString())
            };
        }

        private void GuardAgainstNonSuccessStatusCode(HttpResponseMessage response, string content)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Response is not success. Url: {response.RequestMessage.RequestUri}, ResponseContent: {content}");
            }
        }
    }
}