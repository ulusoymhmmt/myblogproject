using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using UlusoyBlogFront.ApiServices.Interfaces;
using UlusoyBlogFront.Extensions;
using UlusoyBlogFront.Models;

namespace UlusoyBlogFront.ApiServices.Concrete{

    public class BloggApiManager : IBloggApiService
    {
        //burayla ilgili yorumlar icin categoryapimanager a bakabilirsin.
        private readonly HttpClient _httpClient;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        //Session a erismem gerekiyor cunku add metodunda kullanici bir blog ekleyecek ve ben onunda id sini bilgilerini yani imzasini atmak istiyorum bloga bu blogu kim yazmis yarabbi diyerekten gormem lazm. HttpContex e erisici kisacasi
        public BloggApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;  
            _httpClient.BaseAddress = new Uri("http://localhost:58992/api/blogs/"); 
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<BloggListModel>> GetAllAsync() 
            {
                        var responseMessage = await _httpClient.GetAsync("");
                        if(responseMessage.IsSuccessStatusCode){
                        return JsonConvert.DeserializeObject<List<BloggListModel>> (await responseMessage.Content.ReadAsStringAsync());
            }
                  return null;
        }

     

        public async Task<BloggListModel> GetByIdAsync(int id)
        {
               var responseMessage = await _httpClient.GetAsync($"{id}");
                if (responseMessage.IsSuccessStatusCode){
                    return JsonConvert.DeserializeObject<BloggListModel>(await responseMessage.Content.ReadAsStringAsync());
                }
                return null;
        }


        public async Task<List<BloggListModel>> GetAllByCategoryIdAsync (int id){
            var responseMessage = await _httpClient.GetAsync($"GetAllByCategoryId/{id}");
            if(responseMessage.IsSuccessStatusCode){
                 return JsonConvert.DeserializeObject<List<BloggListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task AddAsync(BloggAddModel model){
      
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if(model.Image.FileName != null){
                // var bytes = await System.IO.File.ReadAllBytesAsync(model.Image.FileName);// performansli fakat sikintili bende hata verdi 
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();


                ByteArrayContent byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);

                formData.Add(byteContent, nameof(BloggAddModel.Image.Name), model.Image.FileName);


            }
                var user =  _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
                 model.AppUserId = user.Id; 

                //buradaki olay web te form dan nasil veri database e gonderilir onu goruyoruz bakiniz stringcontent clasii ile string olarak tum datalari gonderiyoruz (image haric onu yukarida byte olarak gonderdik) daha sonra ise gidecegi prop adini veriyoruz yani bu prop adi bizim burdaki model ve database deki model le eslesen ad nameof(BloggAddMode.AppUserId vs vs)
            formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BloggAddModel.AppUserId));
            formData.Add(new StringContent(model.ShortDescription), nameof(BloggAddModel.ShortDescription));
            formData.Add(new StringContent(model.Description), nameof(BloggAddModel.Description));
            formData.Add(new StringContent(model.Title), nameof(BloggAddModel.Title));

                ///tabi biz bunlari gonderdik deee  appuser iin bilgiiisni de gondermemiz lazim yukari da ilk islem

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));

            await _httpClient.PostAsync("", formData);
            
        }
    

        
    }

}