using System.Collections.Generic;
using System.Threading.Tasks;
using UlusoyBlogFront.Models;

namespace UlusoyBlogFront.ApiServices.Interfaces
{
    public interface IBloggApiService
    {
       public Task<List<BloggListModel>> GetAllAsync();

       public Task <BloggListModel> GetByIdAsync(int id);
       
       public Task<List<BloggListModel>> GetAllByCategoryIdAsync(int id);
       public Task AddAsync(BloggAddModel model);// adminler icin ekleme islemi
        
    }
}