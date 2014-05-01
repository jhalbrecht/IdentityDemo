using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppDevPro.Utility.Pcl;
using Identity.Demo.Pcl;
using Identity.Win8.Common;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;

namespace Identity.Win8.Model
{
    public class DataService : IDataService
    {
        public DataService()
        {
            Configuration _configuration = new Configuration();
            BaseAddress = _configuration.BaseAddress;
            Logger.Log(this, BaseAddress);
        }
        public async Task<DataItem> GetData()
        {
            // Use this to connect to the actual data service

            // Simulate by returning a DataItem
            var item = new DataItem("Welcome to Identity Demo");
            return item;
        }
        public string BaseAddress { get; set; }
        public string AccessToken { get; set; }
        public IDialogService DialogService
        {
            get { return ServiceLocator.Current.GetInstance<IDialogService>(); }
        }
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
                await client.PostAsync(String.Format("{0}api/Account/Register", BaseAddress), jsonBody);
            string result = await response.Content.ReadAsStringAsync();
            Logger.Log(this, result.ToString());
            return result;
        }

        public async Task<string> LoginUser(string username, string password)
        {
            var handler = new HttpClientHandler();
            handler.Credentials = new NetworkCredential(username, password);
            Logger.Log(this, handler.Credentials.ToString());
            var handlerClient = new HttpClient(handler);
            var content = new MultipartFormDataContent();
            string postString =
                String.Format("username={0}&amp;password={1}&amp;grant_type=password",
                    WebUtility.HtmlEncode(username), WebUtility.HtmlEncode(password));
            content.Add(new StringContent(postString));
            Logger.Log(this, postString);

            HttpResponseMessage response =
                await handlerClient.PostAsync(string.Format("{0}Token", BaseAddress), content);
            Logger.Log(this, "response", response.Content.ToString());

            // var jsonBody = new StringContent(JsonConvert.SerializeObject(regModel), Encoding.UTF8, "application/json"); // TODO dump that magic string!
            // HttpResponseMessage response = await client.PostAsync(String.Format("{0}Token", BaseAddress), jsonBody);
            string result = await response.Content.ReadAsStringAsync();
            Logger.Log(this, "result", result);
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(result);
            AccessToken = tokenResponse.AccessToken;
            Logger.Log(this, "tokenResponse.AccessToken", tokenResponse.AccessToken);
            return tokenResponse.AccessToken;
            // return true;
        }
    }
}