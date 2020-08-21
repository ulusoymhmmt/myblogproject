using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UlusoyBlogFront.ApiServices.Interfaces;
using UlusoyBlogFront.Models;


namespace UlusoyBlogFront.ApiServices.Concrete{

    public class CategoryApiManager : ICategoryApiService
    {
        //startupdaki burayla ilgili yorum satirindan sonra artik http client i (hem bizim modelimizi interface imizi goren) burada kullanabiliriz e ozaman bu class bu isleri yapacaksa http client tan ilk olarak nesnemi almaliyim dimi ctor la beraber.
        private readonly HttpClient _httpclient;
        public CategoryApiManager(HttpClient httpClient)
        {
            _httpclient = httpClient;
            _httpclient.BaseAddress = new Uri("http://localhost:58992/api/categories/"); //tabi hangi url i yonetecek bu manager onuda yonetmemiz gerekiyor.Bunu base adrese burayi gondererek hallediyoruz.

        }
        public async Task<List<CategoryListModel>> GetAllAsync()
        {
          var responseMessage = await _httpclient.GetAsync(""); //bu metodu bir nesnenin icine aldik cunku baska seyleride kontrol edecegiz icinde status codelari ona gore asagida birseyler yapacagiz. 
          
              if(responseMessage.IsSuccessStatusCode)
              {
              return JsonConvert.DeserializeObject<List<CategoryListModel>> (await responseMessage.Content.ReadAsStringAsync());
              }

          return null; //succes degilse de null don
        }


        public async Task<List<CategoryWithBlogCountsModel>> GetAllWithBlogsCount(){
            var responseMessage = await _httpclient.GetAsync("GetWithBlogsCount");
            if (responseMessage.IsSuccessStatusCode){
                return JsonConvert.DeserializeObject<List<CategoryWithBlogCountsModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        ////gets categories
        public async Task<CategoryListModel> GetByIdAsync(int id){
            var responseMessage = await _httpclient.GetAsync($"{id}");
            if(responseMessage.IsSuccessStatusCode){
                return JsonConvert.DeserializeObject<CategoryListModel>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

    }
}