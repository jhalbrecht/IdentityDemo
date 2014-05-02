using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Win8.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}