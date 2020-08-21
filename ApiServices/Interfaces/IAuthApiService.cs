using System.Threading.Tasks;
using UlusoyBlogFront.Models;

namespace UlusoyBlogFront.ApiServices.Interfaces
{
    public interface IAuthApiService
    {
         Task<bool> SignIn(AppUserLoginModel model);
    }
}