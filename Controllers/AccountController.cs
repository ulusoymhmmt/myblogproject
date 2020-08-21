using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UlusoyBlogFront.ApiServices.Interfaces;
using UlusoyBlogFront.Filters;
using UlusoyBlogFront.Models;

namespace UlusoyBlogFront.Controllers{
     public class AccountController: Controller{
         private readonly IAuthApiService _authApiService;
         public AccountController(IAuthApiService authApiService)
         {
             _authApiService=authApiService;
         }
         public IActionResult SignIn(){
             return View();
         }
         [HttpPost]
   
         public async Task<IActionResult> SignIn(AppUserLoginModel model){
             if(await _authApiService.SignIn(model)){
                 return RedirectToAction("Index","Blogg",new{@area="Admin"});
             }

             return View();
         }

     }
}