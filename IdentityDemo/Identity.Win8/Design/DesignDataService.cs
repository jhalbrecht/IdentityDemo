using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Win8.Model;

namespace Identity.Win8.Design
{
    public class DesignDataService : IDataService
    {
        public Task<DataItem> GetData()
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to Identity Demo [design]");
            return Task.FromResult(item);
        }
    }
}