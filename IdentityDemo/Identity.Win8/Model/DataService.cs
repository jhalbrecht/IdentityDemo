﻿using System;
using System.Collections.Generic;
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
        private Configuration _configuration;
        public DataService()
        {
            _configuration = new Configuration();
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
            return AccessToken;
        }
    }
}