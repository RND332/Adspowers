using Adspowers.AdspowersAPI.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Adspowers.AdspowersAPI
{
    internal class AdsPowersClient
    {
        private HttpClient client = new HttpClient();
        public string Id { get; private set; }
        public AdsPowersClient()
        {
            this.client.BaseAddress = new Uri("http://local.adspower.com:50325");
        }
        public async Task<string> GetStatus() 
        {
            var rawContent = await client.GetAsync("/status");
            var stringContent =  await rawContent.Content.ReadAsStringAsync();

            return stringContent;
        }
        public async Task<string> CreateAccount(string name, string domain_name) 
        {
            FingerprintConfig fngrprntobj = new FingerprintConfig()
            {
                language = new List<string>() { "en-US", "en", "zh-CN", "zh" },
                ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141",
                flash = "block",
                webrtc = "disabled",
            };

            UserProxyConfig userProxyConfig = new UserProxyConfig()
            {
                proxy_soft = "no_proxy",
            };

            CreateConfig createConfig = new CreateConfig()
            {
                name = name,
                group_id = "0",
                repeat_config = new List<string>() { "0" },
                country = "us",
                domain_name = domain_name,
                fingerprint_config = fngrprntobj,
                user_proxy_config = userProxyConfig
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/user/create");
            request.Content = new StringContent(JsonConvert.SerializeObject(createConfig),
                                                Encoding.UTF8,
                                                "application/json");

            var rawResponce = await this.client.SendAsync(request);
            var responce = await rawResponce.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responce);

            if (result.data.id == null)
            {
                throw new Exception((string)result.msg);
            }
            this.Id = result.data.id;
            return result.data.id;
        }
        public async Task<AdsBrowser> OpenBrowser() 
        {
            var rawResponce = await client.GetAsync($"/api/v1/browser/start?user_id={this.Id}");
            var stringContent = await rawResponce.Content.ReadAsStringAsync();
            var responce = JsonConvert.DeserializeObject<dynamic>(stringContent);

            return new AdsBrowser((string)responce.data.ws.puppeteer, (string)responce.data.ws.selenium);
        }
    }
    internal class AdsBrowser
    {
        public string Puppeteer { get; set; }
        public string Selenium { get; set; }

        public AdsBrowser(string puppeteer, string selenium)
        {
            Puppeteer = puppeteer;
            Selenium = selenium;
        }
    }
}
