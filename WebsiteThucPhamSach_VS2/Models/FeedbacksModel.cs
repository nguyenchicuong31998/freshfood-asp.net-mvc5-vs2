using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using System.ComponentModel.DataAnnotations;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class FeedbacksModel
    {

        public string title { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn đánh giá của bạn về sản phẩm này")]
        public Nullable<int> star { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung nhận xét")]
        public string comment { get; set; }

        FreshFoodEntities db = new FreshFoodEntities();
        
        public List<feedback> getFeedbacks()
        {
            return db.feedbacks.ToList();
        }

        public List<feedback> GetFeedbacksClientByProductId(int id)
        {
            return db.feedbacks.OrderByDescending(f=>f.start_time).Where(f=>f.product_id == id && f.status == true).ToList();
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