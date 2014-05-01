using System.Threading.Tasks;
using Identity.Win8.Model;

namespace Identity.Win8.Design
{
    public class DesignDataService : IDataService
    {
        public Task<DataItem> GetData()
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            return Task.FromResult(item);
        }

        public Task<string> RegisterUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> LoginUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}