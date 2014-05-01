using System.Threading.Tasks;

namespace Identity.Win8.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
        Task<string> RegisterUser(string username, string password);
        Task<string> LoginUser(string username, string password);
    }
}