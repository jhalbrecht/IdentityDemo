using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Demo.Pcl
{
    public interface IIdentityService
    {
        Task<string> RegisterUser(string username, string password);
        Task<string> LoginUser(string username, string password);
        Task<List<string>> GetValues();
    }
}
