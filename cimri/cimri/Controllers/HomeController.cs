using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using cimri.DAL;
using cimri.Models;
using System.Data.Entity;
using System.Linq;
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
                //ItemsAndAllCount.Items.AddRange(child.Items);
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
            all.PopularItems = db.Items.OrderByDescending(x => x.ClickCount).Take(10).ToList();
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

        public JsonResult FillRecentlyViewed(VwRecentlyItemId[] items)
        {
            if (items != null)
            {
                List<Item> RecentlyViewed = new List<Item>();
                for (int i = 0; i < items.Length; i++)
                {
                    Item temp = db.Items.Find(items[i].id);
                    if (temp != null)
                    {
                        RecentlyViewed.Add(temp);
                    }
                }

                if (RecentlyViewed.Count > 0)
                {
                    return Json(new { message = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { message = "ItemsNotFound" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { message = "Recently viewed areempty" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult NotFound()
        {
          

            return View();
        }
        [HttpPost]
        [ActionName("CatFilter")]
        public ActionResult Category(int? id, int? pageId, FiltersCont[] SpecIds)
        {


            ItemsAndAllCount.Items = new List<Item>();
            if (id != null)
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
                
                int ItemHasFilterValue = 0;
                foreach (var item in db.FilterValues)
                {
                    foreach (var itemm in db.Items.Where(x=>x.CategoryID==id))
                    {
                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count()!=0)
                        {
                            ItemHasFilterValue++;
                        }

                    }

                    using (var db = new CheapestContext())
                    {
                        var result = db.FilterValues.SingleOrDefault(b => b.id == item.id);
                        result.ItemsCount = ItemHasFilterValue;
                            db.SaveChanges();
                      
                    }
                    ItemHasFilterValue = 0;
                }
                ItemHasFilterValue = 0;
                current.Filters = current.Category.Filters.ToList();
                current.Category.Name = UsaTextInfo.ToTitleCase(current.Category.Name);
                current.CategotyItemCount = CalculateItems(current.Category).count;
                current.Subcategories = new List<VwSubcategory>();
                current.Items = new List<Item>();
               
                    //bura atacayiq
                    //Burada Filter tetbiq olunubsa countunu tuturuq
                    List<Item> buffer = new List<Item>();
                    List<int> itemspec = new List<int>();
                    List<Item> init = new List<Item>();
                    init = db.Categories.Find(30).Items.OrderByDescending(x=>x.ClickCount).ToList();
                    int SpecCount = 0;
                    int count = 0;
                    foreach (var item in SpecIds)
                    {
                        if (item.FilterValues!=null)
                        {
                            SpecCount++;
                        }
                    }

                if (SpecCount==0)
                {
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
                    if (current.Category.Children.Count != 0)
                    {
                        current.Items.AddRange(CalculateItems(current.Category).Items);
                        current.ItemsCount = current.Items.Count();
                    }
                    else

                    {
                        current.Items.AddRange(current.Category.Items);
                        current.ItemsCount = current.Items.Count();
                    }
                    current.PagesCount = (current.Items.Count - 1) / 36 + 1;
                    int itemstoskip = 0;
                    if (pageId != null)
                    {
                        itemstoskip = (pageId - 1) * 36 ?? default(int);
                        current.CurrentPage = pageId ?? default(int);

                    }
                    else
                    {
                        current.CurrentPage = 1;
                    }

                    return View("CategoryFilter", current);
                }
                else
                {
                    foreach (var item in init)
                    {
                        itemspec = item.ItemSpecs.Where(x => x.ItemID == item.id).Select(z => z.FilterValueID).ToList();
                        foreach (var it in itemspec)
                        {
                            foreach (var test in SpecIds)
                            {
                                if (test.FilterValues != null)
                                {
                                    if (test.FilterValues.Contains(it))
                                    {
                                        count++;
                                    }

                                }
                            }

                           
                            
                        }

                        if (count == SpecCount)
                        {
                            buffer.Add(item);
                        }
                        count = 0;
                    }

                    current.Items = buffer;
                    int itemstoskip = (pageId - 1) * 36 ?? default(int);
                    current.Items = current.Items.Skip(itemstoskip).OrderByDescending(x => x.ClickCount).Take(36).ToList();
                    current.ItemsCount = buffer.Count();
                    current.PagesCount = (buffer.Count - 1) / 36 + 1;
                    current.CurrentPage = pageId ?? default(int);


                    if (current.Category.Children.Count == 0)
                    {
                        current.HasChildren = false;
                    }
                    else
                    {
                        current.HasChildren = true;

                    }
                    //if (current.Items.Count == 0)
                    //{
                    //    return RedirectToAction("/NotFound");
                    //}

                    return View("CategoryFilter", current);

                }




            }

            else
            {
                
                return RedirectToAction("/NotFound");
            }
        }

        [ActionName("Category")]
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
                int ItemHasFilterValue = 0;
             foreach (var item in db.FilterValues)
                {
                    foreach (var itemm in db.Items.Where(x => x.CategoryID == id))
                    {
                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                        {
                            ItemHasFilterValue++;
                        }

                    }

                    using (var db = new CheapestContext())
                    {
                        var result = db.FilterValues.SingleOrDefault(b => b.id == item.id);
                        result.ItemsCount = ItemHasFilterValue;
                        db.SaveChanges();

                    }
                    ItemHasFilterValue = 0;
                }
                ItemHasFilterValue = 0;
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
                if (current.Category.Children.Count != 0)
                {
                    current.Items.AddRange(CalculateItems(current.Category).Items);
                    current.ItemsCount = current.Items.Count();
                }
                else

                {
                    current.Items.AddRange(current.Category.Items);
                    current.ItemsCount = current.Items.Count();
                }
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



        public ActionResult Item(int id)
        {
            VwItem current = new VwItem();
            
           current.Item= db.Items.Find(id);
            current.ItemSpecs = new List<VwItemSpec>();

            if (current.Item!=null)
            {
                using (var db = new CheapestContext())
                {
                    var result = db.Items.SingleOrDefault(b => b.id == id);
                    result.ClickCount++;
                    db.SaveChanges();

                }
                foreach (var spec in current.Item.ItemSpecs)
                {
                    VwItemSpec temp = new VwItemSpec();
                    temp.SpecGroupName = spec.Filter.FilterGroup.Name;
                    temp.SpecGroupId = spec.Filter.FilterGroup.id;
                    current.ItemSpecs.Add(temp);
                }
                //List<VwItemSpec> removeduplicates = new List<VwItemSpec>();
                
              var removeduplicates = (from duplicate in current.ItemSpecs select new  { duplicate.SpecGroupId, duplicate.SpecGroupName }).Distinct().ToList();
                current.ItemSpecs.Clear();
                foreach (var item in removeduplicates)
                {
                    VwItemSpec temp = new VwItemSpec();
                    temp.SpecGroupId = item.SpecGroupId;
                    temp.SpecGroupName = item.SpecGroupName;
                    current.ItemSpecs.Add(temp);
                }

                foreach (var item in current.ItemSpecs)
                {
                    item.Values = new List<ItemSpec>();
                    item.Values.AddRange(current.Item.ItemSpecs.Where(x => x.Filter.FilterGroup.id == item.SpecGroupId).ToList());
                }

                current.CommentOneCount = current.Item.Comments.Where(x => x.Rating == 1).Count();
                current.CommentTwoCount = current.Item.Comments.Where(x => x.Rating == 2).Count();
                current.CommentTwoCount = current.Item.Comments.Where(x => x.Rating == 3).Count();
                current.CommentFourCount = current.Item.Comments.Where(x => x.Rating == 4).Count();
                current.CommentFiveCount = current.Item.Comments.Where(x => x.Rating == 5).Count();

                double OverrallRating = 0;
                if (current.Item.Comments.Count>0)
                {
                    
                    foreach (var item in current.Item.Comments)
                    {
                        OverrallRating += item.Rating;
                    }
                  
                    OverrallRating = OverrallRating / current.Item.Comments.Count();
                    current.OverrallRating = OverrallRating;
                }
                current.SponsoredMerchs = current.Item.ItemMerches.Where(c => c.IsSponsored == true).ToList();
                current.OrdinarMerchs = current.Item.ItemMerches.Where(c => c.IsSponsored != true).OrderByDescending(x => x.PriceNormal).ToList();
                current.RelatedItems = new List<Item>();
                if (current.Item.RelatedItem1 != null)
                {
                    current.RelatedItems.Add(db.Items.Find(current.Item.RelatedItem1));
                }
                if (current.Item.RelatedItem2 != null)
                {
                    current.RelatedItems.Add(db.Items.Find(current.Item.RelatedItem2));
                }
                if (current.Item.RelatedItem3 != null)
                {
                    current.RelatedItems.Add(db.Items.Find(current.Item.RelatedItem3));
                }
                if (current.Item.RelatedItem4 != null)
                {
                    current.RelatedItems.Add(db.Items.Find(current.Item.RelatedItem4));
                }
                if (current.Item.RelatedItem5 != null)
                {
                    current.RelatedItems.Add(db.Items.Find(current.Item.RelatedItem5));
                }

                return View(current);

            }
            
            else

            {
                return RedirectToAction("NotFound");
            }
        }

        public ActionResult Generate()
        {

            return Content(Crypto.HashPassword("123"));
        }
    }
}