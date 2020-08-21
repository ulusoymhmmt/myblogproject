using Microsoft.AspNetCore.Mvc;
using UlusoyBlogFront.Filters;

namespace UlusoyBlogFront.Areas.Admin.Controllers
{
    
     [Area("Admin")]
     
    
    public class HomeController : Controller
    {
      
        [JwtAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}