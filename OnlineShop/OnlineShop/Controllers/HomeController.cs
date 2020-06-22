using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models.Home;
using OnlineShop.Database;
namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();
        public ActionResult Index(string search,int? page)
        {
            Session["session_cart"] = true;
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search,page,4));
        }
        
        public ActionResult add_to_cart(int productId)
        {
            if(Session["cart"]==null)
            {
                List<Items> li_in_cart = new List<Items>();
               
                Items i = new Items() {
                    product=db.Tbl_Product.FirstOrDefault(m=>m.ProductId==productId),
                    quantity=1
                };
                li_in_cart.Add(i);
                Session["cart"] = li_in_cart;
                
            }
            else
            {
                List<Items> li_in_cart = (List<Items>)Session["cart"];
                bool isDuplicate = false;
                for(int i=0;i<li_in_cart.Count();++i)
                {
                    if (li_in_cart[i].product.ProductId == productId)
                    {
                        li_in_cart[i].quantity += 1;
                        isDuplicate = true;
                        break;
                    }
                    
                }
                if(!isDuplicate)
                {
                    li_in_cart.Add(new Items()
                    {
                        product = db.Tbl_Product.FirstOrDefault(m => m.ProductId == productId),
                        quantity = 1
                    });
                }
                Session["cart"] = li_in_cart;
            }

            return RedirectToAction("Index");


        }
        public ActionResult remove_from_cart(int productId)
        {
            List<Items> li_in_cart = (List<Items>)Session["cart"];
            foreach(var i in li_in_cart)
            {
                if(i.product.ProductId==productId)
                {
                    li_in_cart.Remove(i);
                    break;
                }
            }
            if ((bool)Session["session_cart"] == true)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("view_product_in_cart");
        }
        public ActionResult view_product_in_cart()
        {
            Session["session_cart"] = false;
            return View();
        }
        public ActionResult increase_quantity_in_cart(int productId)
        {
            List<Items> li_in_cart = (List<Items>)Session["cart"];
            foreach(var item in li_in_cart)
            {
                if(item.product.ProductId==productId)
                {
                    item.quantity += 1;
                    break;
                }
            }
            Session["cart"] = li_in_cart;
            return RedirectToAction("view_product_in_cart");
        }
        public ActionResult decrease_quantity_in_cart(int productId)
        {
            List<Items> li_in_cart = (List<Items>)Session["cart"];
            foreach (var item in li_in_cart)
            {
                if (item.product.ProductId == productId)
                {
                    if (item.quantity > 0)
                    {
                        item.quantity -= 1;
                    }
                    break;
                }
            }
            Session["cart"] = li_in_cart;
            return RedirectToAction("view_product_in_cart");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}