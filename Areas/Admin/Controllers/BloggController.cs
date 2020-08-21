using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UlusoyBlogFront.ApiServices.Interfaces;
using UlusoyBlogFront.Filters;
using UlusoyBlogFront.Models;

namespace UlusoyBlogFront.Areas.Admin.Controllers{

   [Area("Admin")]
    public class BloggController : Controller {
       
            //burada kaldik
        private readonly IBloggApiService _bloggApiService;
        
        
        public BloggController(IBloggApiService bloggApiService)
        {
            _bloggApiService = bloggApiService;
            
        }

         [JwtAuthorize]
        public async Task <IActionResult> Index(){
            return View(await _bloggApiService.GetAllAsync());
        }

        [JwtAuthorize]
        public IActionResult Create (){
            return View(new BloggAddModel());
        }

        [HttpPost]
     
        public async Task <IActionResult> Create(BloggAddModel model){
            if(ModelState.IsValid){
                await _bloggApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);//valid degilse modeli geri don sayfayi yani
        }


    }
}