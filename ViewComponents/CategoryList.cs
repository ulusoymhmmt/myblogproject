using Microsoft.AspNetCore.Mvc;
using UlusoyBlogFront.ApiServices.Interfaces;

namespace UlusoyBlogFront.ViewComponents
{
    ///bir viewcomponent in component olabilmesi icin viewComponent sinifindan turemesi gerekir.
    public class CategoryList : ViewComponent{

        private readonly ICategoryApiService _catergoryApiService;
        public CategoryList(ICategoryApiService categoryApiService)
        {
            _catergoryApiService = categoryApiService;
        }
        public IViewComponentResult Invoke(){ //IViewComponenResul Invoke
            
            return View(_catergoryApiService.GetAllWithBlogsCount().Result);//Result burada su islemi yapiyor result olmadigi surece calistirma bekle async seklinde metot yazamadigimiz icin result i kullaniyoruz.
            
        }


    }
}