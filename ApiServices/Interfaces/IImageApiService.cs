using System.Threading.Tasks;

namespace UlusoyBlogFront.ApiServices.Interfaces{
    public interface IImageApiService
    {
         Task<string> GetBlogImageByIdAsync(int id);
    } 
        
}