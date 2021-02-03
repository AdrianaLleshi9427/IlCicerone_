
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using IlCicerone.Repository;
using IlCicerone.Models;

namespace IlCicerone.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        ApplicationDbContext context = new ApplicationDbContext();
        public IPagedList<Article> ListOfArticles { get; set; }
        public IPagedList<Category> ListOfCategories { get; set; }

        public HomeIndexViewModel CreateModel(string search,int pageSize,int? page)
        {
            SqlParameter[] param=new SqlParameter[]{
                new SqlParameter("@search",search??(object)DBNull.Value)
            };
            IPagedList<Article> data = context.Database.SqlQuery<Article>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListOfArticles = data
            };
        }
    }
}