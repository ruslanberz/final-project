using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using cimri.DAL;
using cimri.Models;
using System.Data.Entity;

namespace cimri.Controllers
{
    public class HomeController : BaseController
    {

        private readonly CheapestContext db = new CheapestContext();
        public int cnt = 0;
       public int CalculateItems(Category cat)
        {   
            cnt += cat.Items.Count();
            foreach (var child in cat.Children)
            {
                CalculateItems(child);
            }
            return cnt;
        }

        [HttpGet]
        public ActionResult Index(VwIndex index)


        {
            
            
           
            VwIndex all = new VwIndex();
            all.Static = db.Statics.FirstOrDefault();
            all.indexUpperPromo = db.indexUpperPromos.FirstOrDefault();
            all.IndexDownPromo = db.indexDownPromos.FirstOrDefault();
            all.indexSliderBigItems = db.indexSliderBigItems.Where(x => x.IsActive == true).ToList();
            all.Merches=db.Merches.OrderByDescending(m => m.Rating).Take(8).ToList();
            all.SeaarchTags = db.SeaarchTags.OrderByDescending(x => x.SearchCount).Take(20).ToList();
            all.CategoryIndexes = new List<VwCategoryIndex>();
            all.CategoryIDIndexes = db.CategoryIDIndexes.Where(x => x.IsActive == true).ToList();

            int count = 0;
            //List<Category> allCategories = db.Categories.Include(a => a.Children).ToList();
            Category loop = db.Categories.Find(12);
            count=CalculateItems(loop);
            cnt = 0;
            //do
            //{

            //    foreach (var item in loop)
            //    {
            //        count += db.Items.Where(x => x.CategoryID == item.id).Count();
            //        subloop=item.Children.ToList(); 
            //    }

            //    loop=subloop;


            //} while (loop.Count!=0);


            //foreach (var item in loop)
            //{
            //    List<Category> subloop = new List<Category>();
            //    foreach (var item2 in item.Children)
            //    {
            //        count+=item2.Items.Count;
            //        subloop.Add(item2);
            //    }

            //    loop = subloop;



            //}

            //for (int i = 0; i < loop.Count; i++)
            //{
            //    List<Category> subloop = new List<Category>();
            //    foreach (var item2 in loop[i].Children)
            //    {
            //        count += item2.Items.Count;
            //        subloop.Add(item2);
            //    }
            //    if (subloop.Count!=0)
            //    {
            //        i = 0;
            //    }
            //    loop = subloop;
            //}


            all.TESTITEMCOUNT = count;
            
            foreach (var item in all.CategoryIDIndexes)
            {
                VwCategoryIndex tmp = new VwCategoryIndex();
                tmp.Category = db.Categories.Find(item.CatIndex);
                tmp.Items= db.Items.Where(x => x.CategoryID == item.CatIndex).OrderByDescending(y => y.ClickCount).Take(4).ToList();
                all.CategoryIndexes.Add(tmp);

            }
            
            
            

            return View(all);

        }



        public ActionResult NotFound()
        {
          

            return View();
        }

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult CategoryFilter()
        {

            return View();
        }

        public ActionResult Item()
        {
            return View();
        }

        public ActionResult Generate()
        {

            return Content(Crypto.HashPassword("123"));
        }
    }
}