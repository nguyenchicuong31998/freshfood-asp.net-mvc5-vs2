using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class ProviderModel
    {
        FreshFoodEntities db = new FreshFoodEntities();
        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp")]
        public string name { get; set; }
        public string description { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhà cung cấp")]
        public string address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string phone_number { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string email { get; set; }
        public string link_google_map { get; set; }
        public Nullable<bool> status { get; set; }

        public List<provider> getProviders()
        {
            return db.providers.ToList();
        }

        public provider getProviderActiveById(int id)
        {
            var provider = db.providers.SingleOrDefault(p => p.id == id && p.status == true);
            return provider;
        }

        public List<provider> getProvidersActive()
        {
            return db.providers.Where(p => p.status == true).ToList();
        }
        public provider getProviderById(int id)
        {
            var provider = db.providers.SingleOrDefault(p => p.id == id);
            return provider;
        }

        public bool create(ProviderModel newProvider)
        {
            try
            {
                provider provider = new provider();
                provider.name = newProvider.name;
                provider.email = newProvider.email;
                provider.phone_number = newProvider.phone_number;
                provider.address = newProvider.address;
                provider.description = newProvider.description;
                provider.link_google_map = newProvider.link_google_map;
                provider.start_time = DateTime.Now;
                provider.status = true;
                db.providers.Add(provider);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool updateProviderById(int id, ProviderModel updateProvider)
        {
            try
            {
                var currentProvider = this.getProviderById(id);
                if (currentProvider != null)
                {
                    currentProvider.name = updateProvider.name;
                    currentProvider.email = updateProvider.email;
                    currentProvider.phone_number = updateProvider.phone_number;
                    currentProvider.address = updateProvider.address;
                    currentProvider.description = updateProvider.description;
                    currentProvider.link_google_map = updateProvider.link_google_map;
                    currentProvider.start_time = DateTime.Now;
                    db.Entry(currentProvider).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            var provider = this.getProviderById(id);
            if (provider != null)
            {
                db.providers.Remove(provider);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool changeStatusById(int id)
        {
            var provider = this.getProviderById(id);
            if (provider != null)
            {
                provider.status = !provider.status;
                db.Entry(provider).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public int totalProvider()
        {
            return db.providers.Count();
        }

        public int totalActiveProvider()
        {
            return db.providers.Where(u => u.status == true).Count();
        }
        public int totalInActiveProvider()
        {
            return db.providers.Where(u => u.status == false).Count();
        }
    }
}