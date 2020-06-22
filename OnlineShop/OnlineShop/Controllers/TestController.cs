using OnlineShop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class TestController : Controller
    {
        dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();
        // GET: Test
        public ActionResult Show_Category()
        {
            List<Tbl_Category> li_ca = db.Tbl_Category.ToList();
            return View(li_ca);
        }
        public ActionResult Insert_Categoty()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Insert_Categoty(Tbl_Category c)
        {
            db.func_insert_category(c.CategoryName, c.IsActive, c.IsDelete);
            return View("Show_Category");
        }
    }
}