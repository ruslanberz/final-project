using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using cimri.DAL;
using cimri.Models;
using System.Data.Entity;
using System.Globalization;

namespace cimri.Controllers
{
    public class HomeController : BaseController
    {
        public TextInfo UsaTextInfo = new CultureInfo("en-US", false).TextInfo;
        private readonly CheapestContext db = new CheapestContext();
        public int cnt = 0;
        public VwItemsAndAllCount ItemsAndAllCount = new VwItemsAndAllCount();
        
       public VwItemsAndAllCount CalculateItems(Category cat)
        {   
            cnt += cat.Items.Count();
            ItemsAndAllCount.Items.AddRange(cat.Items);
            foreach (var child in cat.Children)
            {
                ItemsAndAllCount.Items.AddRange(child.Items);
                CalculateItems(child);
            }
            ItemsAndAllCount.count = cnt;
            return ItemsAndAllCount;
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
            all.Brands = db.Brands.OrderByDescending(x => x.Rating).Take(8).ToList();

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

        public ActionResult Category( int? id, int? pageId)
        {
            ItemsAndAllCount.Items = new List<Item>();
            if (id!=null)
            {
                VwCategory current = new VwCategory();
                current.Category = db.Categories.Find(id);
                // Category visit increment
                using (var db = new CheapestContext())
                {
                    var result = db.Categories.SingleOrDefault(b => b.id == id);
                    if (result != null)
                    {
                        result.ClickCount++;
                        db.SaveChanges();
                    }
                }
               current.Filters = current.Category.Filters.ToList();
                current.Category.Name = UsaTextInfo.ToTitleCase(current.Category.Name);
                current.CategotyItemCount = CalculateItems(current.Category).count;
                current.Subcategories = new List<VwSubcategory>();
                current.Items = new List<Item>();
                cnt = 0;
                foreach (var item in current.Category.Children)
                {
                    VwSubcategory temp = new VwSubcategory();
                    temp.SubCategory = item;
                    temp.ItemsCount = CalculateItems(item).count;
                
                    cnt = 0;
                    current.Subcategories.Add(temp);
                   
                    ItemsAndAllCount.count = 0;
                    ItemsAndAllCount.Items.Clear();

                }
                current.Items.AddRange(CalculateItems(current.Category).Items);
                current.ItemsCount = current.Items.Count();
                current.PagesCount =(current.Items.Count-1)/36+1;
                int itemstoskip = 0;
                if (pageId!=null)
                {
                    itemstoskip = (pageId-1)*36 ?? default(int); 
                    current.CurrentPage=pageId ?? default(int);
                    
                }
                else
                {
                    current.CurrentPage = 1;
                }
                current.Items = current.Items.Skip(itemstoskip).Take(36).OrderByDescending(x => x.ClickCount).ToList();
                if (current.Category.Children.Count==0)
                {
                    current.HasChildren = false;
                }
                else
                {
                    current.HasChildren = true;

                }
                if (current.Items.Count==0)
                {
                    return RedirectToAction("/NotFound");
                }
                return View(current);
            }

            else
            {
                return RedirectToAction("/NotFound");
            }
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