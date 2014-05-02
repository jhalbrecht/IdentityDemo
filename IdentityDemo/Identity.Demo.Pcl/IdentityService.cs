using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppDevPro.Utility.Pcl;
using Newtonsoft.Json;

namespace Identity.Demo.Pcl
{
    public class IdentityService : IIdentityService
    {
        private Configuration _configuration;
        public IdentityService()
        {
            _configuration = new Configuration();
        }
        public string AccessToken { get; set; }
        public List<string> Strings { get; set; }

        public async Task<string> RegisterUser(string username, string password)
        {
            var regModel = new RegisterModel
            {
                // UserName = username,
                Email = username,
                Password = password,
                ConfirmPassword = password
            };
            var client = new HttpClient();
            var jsonBody = new StringContent(JsonConvert.SerializeObject(regModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response =
                await client.PostAsync(String.Format("{0}api/Account/Register", _configuration.BaseAddress), jsonBody);
            string result = await response.Content.ReadAsStringAsync();
            Logger.Log(this, result.ToString());
            client.Dispose();
            return result;
        }

        public async Task<string> LoginUser(string username, string password)
        {
            string tokenUrl = string.Format("{0}Token", _configuration.BaseAddress);
            var client = new HttpClient();

            // using postBody I got invalid grant type
            // using postData I got 200 OK! :-) TODO I wonder why? couldn't find a hex dump in fiddler composer
            //string postBody =
            //    String.Format("username={0}&amp;password={1}&amp;grant_type=password",
            //        WebUtility.HtmlEncode(username), WebUtility.HtmlEncode(password));
            //Logger.Log(this, postBody);
            // HttpContent content = new StringContent(postBody);

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("username", username));
            postData.Add(new KeyValuePair<string, string>("password", password));
            postData.Add(new KeyValuePair<string, string>("grant_type", "password"));

            Logger.Log(this, "postData", postData.ToString());
            HttpContent content = new FormUrlEncodedContent(postData);
            HttpResponseMessage response = await client.PostAsync(tokenUrl, content);
            Logger.Log(this, "response", response.Content.ToString());
            string result = await response.Content.ReadAsStringAsync();
            Logger.Log(this, "result", result);
            TokenResponseModel tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(result);
            AccessToken = tokenResponse.AccessToken;
            Logger.Log(this, "AccessToken", AccessToken);
            client.Dispose();
            await GetValues();
            return AccessToken;
        }

        public async Task<List<string>> GetValues()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", AccessToken));
            HttpResponseMessage response = await client.GetAsync(string.Format("{0}api/Values", _configuration.BaseAddress));
            var data = await response.Content.ReadAsStringAsync();
            Strings = await JsonConvert.DeserializeObjectAsync<List<string>>(data);
            Logger.Log(this, "strings");
            foreach (var s in Strings)
                Logger.Log(s);
            
            client.Dispose();
            return Strings;
        }
    }
}
