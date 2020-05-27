using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using System.ComponentModel.DataAnnotations;
using PagedList;
namespace WebsiteThucPhamSach_VS2.Models
{
    public class FeedbacksModel
    {

        FreshFoodEntities db = new FreshFoodEntities();
        
        public List<feedback> getFeedbacks()
        {
            return db.feedbacks.ToList();
        }

        public int totalFeedbacks()
        {
            return db.feedbacks.Where(f => f.status == true).Count();
        }

        public IPagedList<feedback> GetFeedbacksClientByProductId(int id, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var feedback = db.feedbacks.OrderByDescending(f => f.start_time).Where(f => f.product_id == id && f.status == true).ToList().ToPagedList(pageNumber, pageSize);
            return feedback;
        }
        public feedback getFeedbackById(int id)
        {
            var feedback = db.feedbacks.SingleOrDefault(c => c.id == id);
            if (feedback != null)
            {
                return feedback;
            }
            return null;
        }

        public bool create(feedback feedback)
        {
            try
            {
                db.feedbacks.Add(feedback);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string getNameUserById(int id)
        {
            var user = new UsersAdModel().getUserById(id);
            if(user == null)
            {
                return "";
            }
            return user.display_name;
        }

        public string getNameProductById(int id)
        {
            var product = new ProductsAdModel().GetProductById(id);
            if (product == null)
            {
                return "";
            }
            return product.name;
        }

        public bool deleteById(int id)
        {
            try
            {
                var feedback = this.getFeedbackById(id);
                if (feedback != null)
                {
                    db.feedbacks.Remove(feedback);
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

        public int getTotalFeedbackByProductId(int id)
        {
            int totalFeedback = db.feedbacks.Where(f => f.product_id == id && f.status == true).Count();
            return totalFeedback;
        }

        public int getTotalStarByProductId(int id)
        {
            var totalStar = db.feedbacks.Where(f => f.product_id == id).Select(f => f.star).Sum();
            //int totalStar = int.Parse(db.feedbacks.Where(f => f.product_id == id).Select(f => f.star).Sum().ToString());
            //return totalStar;
            if(totalStar == null)
            {
                return 0;
            }
            return int.Parse(totalStar.ToString());
        }


        public bool changeStatusById(int id)
        {
            try
            {
                var feedback = this.getFeedbackById(id);
                if (feedback != null)
                {
                    feedback.status = !feedback.status;
                    db.Entry(feedback).State = System.Data.Entity.EntityState.Modified;
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
    }
}