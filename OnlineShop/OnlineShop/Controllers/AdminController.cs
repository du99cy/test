using OnlineShop.Database;
using OnlineShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace OnlineShop.Controllers
{
    public class AdminController : Controller
    {
        GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<Tbl_Category> li_ca = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(ca => ca.IsDelete == false).ToList();
            return View(li_ca);
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Tbl_Category new_c)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(new_c);
            //dbMyOnlineShoppingEntities db = new dbMyOnlineShoppingEntities();
            //db.func_insert_category(new_c.CategoryName, new_c.IsActive, new_c.IsDelete);
            return RedirectToAction("Categories");
        }
        public ActionResult UpdateCategory(int categoryId)
        {
            //CategoryDetail cd;
            //if (categoryId != null)
            //{
            //    string json = JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId));
            //    cd = JsonConvert.DeserializeObject<CategoryDetail>(json);

            //}
            //else
            //{
            //    cd = new CategoryDetail();
            //}
            Tbl_Category cd = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId);
            return View(cd);
        }

        [HttpPost]
        public ActionResult UpdateCategory([Bind(Include ="CategoryId,CategoryName")]Tbl_Category c)
        {
           Tbl_Category c_new = new Tbl_Category();
           c_new = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(c.CategoryId);
            c_new.CategoryName = c.CategoryName;
            

                _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(c_new);

                
                return RedirectToAction("Categories");
            
            
        }
       

        public ActionResult Products()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetAllRecords());
        }
        public ActionResult ProductEdit(int ProductId)
        {
            ViewBag.CategoryId = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "CategoryId", "CategoryName");
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(ProductId));
        }
        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product product,HttpPostedFileBase file)
        {
            string pic = null;
            if(file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Products/"), pic);
                file.SaveAs(path);
            }
            if(product.CategoryId==null)
            {
                product.CategoryId = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(product.ProductId).CategoryId;
            }

            product.ProductImage = pic;
            product.ModifiedDate = DateTime.Now;
           
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(product);
            //ViewBag.CategoryId = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "CategoryId", "CategoryName",product.CategoryId);
            return RedirectToAction("Products");
        }
        [HttpPost]
        public ActionResult delete_ca(int id)
        {
            _unitOfWork.db.Database.ExecuteSqlCommand("delete_category @id", new SqlParameter("@id", id));
            _unitOfWork.db.SaveChanges();
            return RedirectToAction("Categories");
        }
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryId = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product new_p,HttpPostedFileBase file)
        {
            string pic=null;
            if(file!=null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Products/"), pic);
                file.SaveAs(path);
            }
            new_p.ProductImage = pic;
            new_p.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(new_p);
            //_unitOfWork.db.func_insert_procduct(new_p.ProductName, new_p.CategoryId, new_p.Price);
            //ViewBag.CategoryId = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(),"CategoryId", "CategoryName",new_p.CategoryId);
            return RedirectToAction("Products");
        }
    }
}