using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UlusoyBlogFront.ApiServices.Interfaces;

namespace UlusoyBlogFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBloggApiService _blogApiService;
        public HomeController(IBloggApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        ///on click category on the right side of index bu getwithblog counts dan ayri nameleri ve icerigi onunla cekiyorduk        
        //tiklandiginda bloglari ceker sikinti yok
        //sagdaki category e tiklandiginda indexte goruntuleme yapacagim icin burasi nullable cunku nullable olmassa 
        //sag tarafa tiklandiginda yeni bir root acmam gerekir boyle yapmamin sebebi tek ekranda olabildigince sey dondurmektir.
        public async Task<IActionResult> Index(int? categoryId)
        {
            //viewbag veri tasimak icin viewbag in icinde active category diye bir degisken tanimladim ve icine category id nin degerini salladim ve boylelikle onun uzerinden isler yapacagim.
            if (categoryId.HasValue)
            {

                ViewBag.ActiveCategory = categoryId;
                return View(await _blogApiService.GetAllByCategoryIdAsync((int)categoryId));
            }
            return View(await _blogApiService.GetAllAsync());
        }


        public async Task<IActionResult> BlogDetail(int id)
        {
            return View(await _blogApiService.GetByIdAsync(id));
        }
    }
}