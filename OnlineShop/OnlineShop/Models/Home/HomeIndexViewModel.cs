using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using OnlineShop.Database;
using OnlineShop.Repository;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace OnlineShop.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbMyOnlineShoppingEntities context = new dbMyOnlineShoppingEntities();
        public IPagedList<Tbl_Product> list_product { get; set; }
        public HomeIndexViewModel CreateModel(string search,int? page,int pageSize)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@search", search??(object)DBNull.Value) };
            IPagedList<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetProductBySearch @search",para).ToList().ToPagedList(page?? 1, pageSize);
            return new HomeIndexViewModel() { list_product = data };
        }
    }
}