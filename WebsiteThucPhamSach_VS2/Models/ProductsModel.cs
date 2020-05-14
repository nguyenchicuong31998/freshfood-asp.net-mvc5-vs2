using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using System.Data.Entity.SqlServer;
using PagedList;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class ProductsModel
    {
        FreshFoodEntities db = new FreshFoodEntities();

        public List<product> getProducts()
        {
            var products = db.products.OrderByDescending(p => p.start_time).Where(p => p.status == true).Skip(1).OrderBy(r => Guid.NewGuid()).ToList();
            return products;
        }

        public IPagedList<product> getProductsPageList(int id, string order, int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var products = from s in db.products
                           where s.status == true
                           select s;
            switch (order)
            {
                case "alpha-asc":
                    products = products.OrderBy(p => p.name);
                    break;
                case "alpha-desc":
                    products = products.OrderByDescending(p => p.name);
                    break;
                case "price-asc":
                    var listProductAsc = products.ToList();
                    var productLength = listProductAsc.Count() - 1;
                    product temporary;
                    for (int i = 0; i < productLength; i++)
                    {
                        for (int j = i + 1; j < listProductAsc.Count(); j++)
                        {
                            decimal priceA = 0;
                            decimal priceB = 0;
                            priceA = listProductAsc[i].price_promotion > 0 ? decimal.Parse(String.Format("{0:0}", listProductAsc[i].price * ((100 - listProductAsc[i].price_promotion) / 100))) : decimal.Parse(listProductAsc[i].price.ToString());
                            priceB = listProductAsc[j].price_promotion > 0 ? decimal.Parse(String.Format("{0:0}", listProductAsc[j].price * ((100 - listProductAsc[j].price_promotion) / 100))) : decimal.Parse(listProductAsc[j].price.ToString());
                            if (priceA > priceB)
                            {
                                temporary = listProductAsc[i];
                                listProductAsc[i] = listProductAsc[j];
                                listProductAsc[j] = temporary;
                            }
                        }
                    }
                    if (id != 0)
                    {
                        listProductAsc = listProductAsc.Where(p => p.menu_id == id).ToList();
                    }
                    return listProductAsc.ToPagedList(pageNumber, pageSize);
                case "price-desc":
                    var listProductDesc = products.ToList();
                    var productLengthDesc = listProductDesc.Count() - 1;
                    product temporaryDesc;
                    for (int i = 0; i < productLengthDesc; i++)
                    {
                        for (int j = i + 1; j < listProductDesc.Count(); j++)
                        {
                            decimal priceA = 0;
                            decimal priceB = 0;
                            priceA = listProductDesc[i].price_promotion > 0 ? decimal.Parse(String.Format("{0:0}", listProductDesc[i].price * ((100 - listProductDesc[i].price_promotion) / 100))) : decimal.Parse(listProductDesc[i].price.ToString());
                            priceB = listProductDesc[j].price_promotion > 0 ? decimal.Parse(String.Format("{0:0}", listProductDesc[j].price * ((100 - listProductDesc[j].price_promotion) / 100))) : decimal.Parse(listProductDesc[j].price.ToString());
                            if (priceA < priceB)
                            {
                                temporaryDesc = listProductDesc[i];
                                listProductDesc[i] = listProductDesc[j];
                                listProductDesc[j] = temporaryDesc;
                            }
                        }
                    }
                    if (id != 0)
                    {
                        listProductDesc = listProductDesc.Where(p => p.menu_id == id).ToList();
                    }
                    return listProductDesc.ToPagedList(pageNumber, pageSize);
                default:
                    products = products.OrderByDescending(p => p.start_time);
                    break;
            }
            if (id == 0)
            {
                //products = products.Skip(1);
                return products.ToPagedList(pageNumber, pageSize); 
            }
            products = products.Where(p=>p.menu_id == id);
            return products.ToPagedList(pageNumber, pageSize);
        }

        public product getProductById(int id)
        {
            return db.products.SingleOrDefault(p => p.id == id);
        }

        public List<product> getProductRelatedById(int id, int menuId)
        {
            return db.products.Where(p => p.status == true && p.menu_id == menuId && p.id != id).OrderByDescending(p => Guid.NewGuid()).Take(15).ToList();
        }

        public List<product> getProductsByNewStartTime()
        {
            return db.products.OrderByDescending(p => p.start_time).Where(p=>p.status == true).Take(12).ToList();
        }

        public List<product> getProductsByViewCount()
        {
            return db.products.OrderByDescending(p => p.view_count).Take(12).ToList();
        }

        public List<product> getFeaturedProducts()
        {
            return db.products.OrderByDescending(p => p.total_sold).Where(p => p.status == true ).Take(5).ToList();
        }

        public void updateViewByProductId(int id)
        {
            try
            {
                var product = this.getProductById(id);
                if(product != null)
                {
                    product.view_count += 1;
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }catch(Exception e)
            {
                throw e;
            }
        }
        
    }
}