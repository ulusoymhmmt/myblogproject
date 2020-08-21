using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UlusoyBlogFront.Models
{
    public class BloggAddModel
    {
        ///adminnin blog eklerken kullanacagi model
        //burada validasyon islemini hizlica data annotations lar ile yapabiliriz fluent validasyona hic gerek yok cunku bu on taraf oldugu icin bana yeterli bu sekilde       
        
        //data annotations bazi ozelliklerini kullandigimizda name gibi html taglerinde (tag-helper kullandigimi) tekrar name giremeyiz yani tag arasina text giremeyiz yada required dediysek required olarak belirtemeyiz tag tarafinda 
        [Display (Name="Resim  :")]
        public IFormFile Image { get; set; }

        [Required ( ErrorMessage= "Baslik Gereklidir")]
        [Display (Name = "Baslik")]
        public string Title { get; set; }

        [Required(ErrorMessage= "Aciklama Gereklidir.")]
        [Display (Name="Aciklama")]
        public  string  ShortDescription { get; set; }


        [Required(ErrorMessage= ("Icerik Gereklidir."))]
        [Display (Name = "Icerik")]
        public string Description { get; set; }


     
       
        
        public int AppUserId {get; set;}
   

       


    }
}
