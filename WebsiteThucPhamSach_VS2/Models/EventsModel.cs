using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Models
{
    public class EventsModel
    {
        public int id { get; set; }
        public int admin_id { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string image { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
        public string title { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
        public string content { get; set; }
        public Nullable<System.DateTime> start_time { get; set; }
        public Nullable<System.DateTime> end_time { get; set; }
        public Nullable<bool> top_hot { get; set; }
        public Nullable<bool> status { get; set; }

        FreshFoodEntities db = new FreshFoodEntities();
        public List<@event> getEvents()
        {
            return db.events.OrderByDescending(e=>e.start_time).ToList();
        }

        public List<@event> getEventsClient()
        {
            return db.events.OrderByDescending(e => e.start_time).Where(e=>e.status == true).ToList();
        }

        public List<@event> getEventByTopHot()
        {
            return db.events.OrderByDescending(e => e.start_time).Where(e=>e.top_hot == true && e.status == true).ToList();
        }

        public @event getEventById(int id)
        {
            var @event = db.events.SingleOrDefault(p => p.id == id);
            if (@event != null)
            {
                return @event;
            }
            return null;
        }

        public bool create(EventsModel newEvent)
        {
            try
            {
                @event @event = new @event();
                @event.admin_id = newEvent.admin_id;
                @event.image = newEvent.image;
                @event.title = newEvent.title;
                @event.content = newEvent.content;
                @event.start_time = DateTime.Now;
                @event.top_hot = false;
                @event.status = true;
                db.events.Add(@event);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public bool updateById(int id,EventsModel updateEvent)
        {
            try
            {
                var currentEvent = this.getEventById(id);
                if(currentEvent != null)
                {
                    currentEvent.admin_id = updateEvent.admin_id;
                    currentEvent.image = updateEvent.image;
                    currentEvent.title = updateEvent.title;
                    currentEvent.content = updateEvent.content;
                    currentEvent.start_time = DateTime.Now;
                    //currentEvent.top_hot = upd;
                    currentEvent.status = true;
                    db.Entry(currentEvent).State = System.Data.Entity.EntityState.Modified;
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



        public bool deleteById(int id)
        {
            try
            {
                var @event = this.getEventById(id);
                if(@event != null)
                {
                    db.events.Remove(@event);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool changeStatusById(int id)
        {
            try
            {
                var @event = this.getEventById(id);
                if (@event != null)
                {
                    @event.status = !@event.status;
                    db.Entry(@event).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool changeTopHotById(int id)
        {
            try
            {
                var @event = this.getEventById(id);
                if (@event != null)
                {
                    @event.top_hot = !@event.top_hot;
                    db.Entry(@event).State = System.Data.Entity.EntityState.Modified;
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

        public admin getAdminById(int id)
        {
            var admin = db.admins.SingleOrDefault(ad => ad.id == id);
            if(admin != null)
            {
                return admin;
            }
            return null;
        }
    
    }
}