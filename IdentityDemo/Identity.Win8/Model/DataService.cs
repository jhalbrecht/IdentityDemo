using Identity.Demo.Pcl;
using Identity.Win8.Common;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public List<string> Strings { get; set; } 
        public IDialogService DialogService
        {
            get { return ServiceLocator.Current.GetInstance<IDialogService>(); }
        }
    }
}