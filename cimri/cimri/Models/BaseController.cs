using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cimri.DAL;
using cimri.Models;

namespace cimri.Models
{
    public class BaseController : Controller
    {
        public CheapestContext dbs = new CheapestContext();
        public List<Category> AllCategoriesClild = new List<Category>();
        public List<Category> FooterCat = new List<Category>();
        public List<Item> FooterItems =new List<Item>();
        public BaseController()
            {

             List<Category> Categories = dbs.Categories.Where(x => x.ParentCategryId == null).ToList();
            foreach (var item in Categories)
            {
                item.Children = dbs.Categories.Where(x => x.ParentCategryId == item.id).ToList();
            }

            foreach (var item in dbs.Categories.Where(x => x.ParentCategryId != null))
            {
                item.Children = dbs.Categories.Where(x => x.ParentCategryId == item.id).ToList();
               

            }
              AllCategoriesClild= Categories;
            FooterCat = dbs.Categories.OrderByDescending(x => x.ClickCount).Take(10).ToList();
            FooterItems = dbs.Items.OrderByDescending(x => x.ClickCount).Take(10).ToList();
            ViewBag.Categories = Categories;
            ViewBag.FooterCat = FooterCat;
            ViewBag.FooterItems = FooterItems;


        }

        

    }
}