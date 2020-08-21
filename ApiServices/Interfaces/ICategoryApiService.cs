
using System.Collections.Generic;
using System.Threading.Tasks;
using UlusoyBlogFront.Models;

namespace UlusoyBlogFront.ApiServices.Interfaces{
    public interface ICategoryApiService
    {
       public Task<List<CategoryListModel>> GetAllAsync();
       public Task<List<CategoryWithBlogCountsModel>> GetAllWithBlogsCount();

        //for get name category
        public Task<CategoryListModel> GetByIdAsync(int id);
    }
}