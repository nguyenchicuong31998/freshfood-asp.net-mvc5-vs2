using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class AboutsModel
    {
        FreshFoodEntities db = new FreshFoodEntities();
        public List<about> getAbouts()
        {
            return db.abouts.OrderByDescending(a=>a.start_time).ToList();
        }
        public about getAboutByStatusTrue()
        {
            var about = db.abouts.SingleOrDefault(a=>a.status == true);
            if(about == null)
            {
                return null;
            }
            return about;
        }
        public about getAboutById(int id)
        {
            var about = db.abouts.SingleOrDefault(a => a.id == id);
            if (about != null)
            {
                return about;
            }
            return null;
        }
        public bool createAbout(about about)
        {
            try
            {
                db.abouts.Add(about);
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool updateAboutById(int id, about update)
        {
            try
            {
                var currentAbout = this.getAboutById(id);
                if (currentAbout != null)
                {
                    currentAbout.content = update.content;
                    db.Entry(currentAbout).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool deleteAboutById(int id)
        {
            var about = this.getAboutById(id);
            if (about != null)
            {
                db.abouts.Remove(about);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool changeStatusById(int id)
        {
            var about = this.getAboutById(id);
            if (about != null)
            {
                //db.abouts.Remove(about);
                //db.SaveChanges();
                //return true;

            }
            return false;
        }
    }
}