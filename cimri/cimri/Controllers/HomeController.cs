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
using System.Data.Entity.Infrastructure;

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
            all.Categories = db.Categories.ToList();
            all.SiteSelect = db.Items.Where(x => x.SiteSelection == true).OrderByDescending(x=>x.ClickCount).ToList();

            foreach (var item in all.CategoryIDIndexes)
            {
                VwCategoryIndex tmp = new VwCategoryIndex();
                tmp.Category = db.Categories.Find(item.CatIndex);
                tmp.Items= db.Items.Where(x => x.CategoryID == item.CatIndex).OrderByDescending(y => y.ClickCount).Take(4).ToList();
                all.CategoryIndexes.Add(tmp);

            }
            
            
            

            return View(all);

        }
        [HttpPost]
        public ActionResult FillRecentlyViewed(VwRecentlyItemId[] items)
        {
            if (items != null)
            {
                VwRecent RecentlyViewed = new VwRecent();
                RecentlyViewed.Recent = new List<Item>();
                for (int i = (items.Length-1); (i >= (items.Length - 10))&&(i>=0); i--)
                {
                    Item temp = db.Items.Find(items[i].id);
                    if (temp != null)
                    {
                        RecentlyViewed.Recent.Add(temp);
                    }
                }

                if (RecentlyViewed.Recent.Count > 0)
                {
                    return View(RecentlyViewed);
                }
                else
                {
                    return View(RecentlyViewed);

                }
            }
            else
                
            {
                string ErrorText = string.Empty;
                return new HttpStatusCodeResult(500, ErrorText);

            }
        }
        [HttpPost]
        public ActionResult FillRecentlyViewedM(VwRecentlyItemId[] items)
        {
            if (items != null)
            {
                VwRecent RecentlyViewed = new VwRecent();
                RecentlyViewed.Recent = new List<Item>();
                for (int i = (items.Length - 1); (i >= (items.Length - 10)) && (i >= 0); i--)
                {
                    Item temp = db.Items.Find(items[i].id);
                    if (temp != null)
                    {
                        RecentlyViewed.Recent.Add(temp);
                    }
                }

                if (RecentlyViewed.Recent.Count > 0)
                {
                    return View(RecentlyViewed);
                }
                else
                {
                    return View(RecentlyViewed);

                }
            }
            else

            {
                string ErrorText = string.Empty;
                return new HttpStatusCodeResult(500, ErrorText);

            }
        }
        public ActionResult SearchNotFound(string search)
        {
            VwSearchNotFound srch = new VwSearchNotFound();
            srch.Request = search;


            return View(srch);
        }
        [HttpPost]
        [ActionName("CatFilter")]
        public ActionResult Category(int? id, int? pageId, FiltersCont[] SpecIds,string q, int? MinPrice,int? MaxPrice)
        {


            ItemsAndAllCount.Items = new List<Item>();
            if (id != null)
            {
                if (string.IsNullOrEmpty(q))
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

                   
                    current.Category.Name = UsaTextInfo.ToTitleCase(current.Category.Name);
                    current.CategotyItemCount = CalculateItems(current.Category).count;
                    current.Subcategories = new List<VwSubcategory>();
                    current.Items = new List<Item>();
                    //я остановился тут
                    current.Filters = db.Filters.Where(x => x.Category.id == id).ToList();
                    //bura atacayiq
                    //Burada Filter tetbiq olunubsa countunu tuturuq
                    List<Item> buffer = new List<Item>();
                    List<int> itemspec = new List<int>();
                    List<Item> init = new List<Item>();
                    if (MinPrice==0&&MaxPrice==0)
                    {
                        init = db.Categories.Find(id).Items.OrderByDescending(x => x.ClickCount).ToList();
                    }
                    else if (MaxPrice==0)
                    {
                        init = db.Categories.Find(id).Items.OrderByDescending(x => x.ClickCount).Where(x => x.ItemMerches.OrderBy(z => z.PriceNormal).FirstOrDefault().PriceNormal >= MinPrice).ToList();
                    }
                    else
                    {
                        
                        init=db.Categories.Find(id).Items.OrderByDescending(x => x.ClickCount).Where(x => x.ItemMerches.OrderBy(z=>z.PriceNormal).FirstOrDefault().PriceNormal>=MinPrice&& x.ItemMerches.OrderBy(z => z.PriceNormal).FirstOrDefault().PriceNormal <= MaxPrice).ToList();
                    }
                   
                   
                    int SpecCount = 0;
                    int count = 0;
                    foreach (var item in SpecIds)
                    {
                        if (item.FilterValues != null)
                        {
                            SpecCount++;
                          
                        }
                    }

                    foreach (var item in SpecIds)
                    {
                        if (item.FilterValues!=null)
                        {
                            foreach (var values in item.FilterValues)
                            {
                                FilterValue temp = db.FilterValues.FirstOrDefault(x => x.id == values);
                                current.Filters.FirstOrDefault(x => x.id == temp.Filter.id).FilterValues.FirstOrDefault(y => y.id == temp.id).IsChecked = true;
                            }
                        }
                    }

                    if (SpecCount == 0)
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
                            current.Items=init;

                            current.ItemsCount = current.Items.Count();
                            int ItemHasFilterValue = 0;
                            foreach (var item in db.FilterValues)
                            {
                                if (MinPrice == 0 && MaxPrice == 0)
                                {
                                    foreach (var itemm in init)
                                    {
                                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                        {
                                            ItemHasFilterValue++;
                                        }

                                    }
                                }
                                else if (MinPrice != 0 && MaxPrice != 0)
                                {
                                    foreach (var itemm in init)
                                    {
                                        itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                        if (itemm.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice && itemm.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice)
                                        {
                                            if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                            {
                                                ItemHasFilterValue++;
                                            }

                                        }

                                    }
                                }
                                else if (MinPrice != 0)
                                {
                                    foreach (var itemm in init)
                                    {
                                        itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                        if (itemm.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice)
                                        {
                                            if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                            {
                                                ItemHasFilterValue++;
                                            }

                                        }

                                    }
                                }
                                else
                                {
                                    foreach (var itemm in init)
                                    {
                                        itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                        if (itemm.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice)
                                        {
                                            if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                            {
                                                ItemHasFilterValue++;
                                            }

                                        }

                                    }

                                }

                                //using (var db = new CheapestContext())
                                //{
                                //    var result = db.FilterValues.SingleOrDefault(b => b.id == item.id);
                                //    result.ItemsCount = ItemHasFilterValue;
                                //    db.SaveChanges();

                                //}

                                current.Filters.FirstOrDefault(x => x.id == item.Filter.id).FilterValues.FirstOrDefault(x => x.id == item.id).ItemsCount = ItemHasFilterValue;
                                ItemHasFilterValue = 0;
                            }

                            ItemHasFilterValue = 0;

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

                        if (MinPrice != null && MinPrice > 0)
                        {
                            current.MinPrice = MinPrice ?? default(int);
                        }

                        if (MaxPrice != null && MaxPrice > 0)
                        {
                            current.MaxPrice = MaxPrice ?? default(int);
                        }
                        return View("CategoryFilter", current);
                    }
                    else
                    {
                        if (MinPrice != 0 || MaxPrice != 0)
                        {
                            init.Clear();
                            if (MinPrice == 0)
                            {
                                init = db.Categories.Find(id).Items.Where(x => x.ItemMerches.OrderBy(z => z.PriceNormal).FirstOrDefault().PriceNormal <= MaxPrice).ToList();
                            }
                            else if (MaxPrice == 0)
                            {

                                init = db.Categories.Find(id).Items.Where(x => x.ItemMerches.OrderBy(z => z.PriceNormal).FirstOrDefault().PriceNormal>=MinPrice).ToList();

                            }
                            else
                            {
                                init = db.Categories.Find(id).Items.Where(x => x.ItemMerches.OrderBy(z => z.PriceNormal).FirstOrDefault().PriceNormal>=MinPrice&& x.ItemMerches.OrderBy(z => z.PriceNormal).FirstOrDefault().PriceNormal<=MaxPrice).ToList();
                            }

                        }

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
                        int ItemHasFilterValue = 0;
                        foreach (var item in db.FilterValues)
                        {
                            if (MinPrice == 0 && MaxPrice == 0)
                            {
                                foreach (var itemm in buffer)
                                {
                                    if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                    {
                                        ItemHasFilterValue++;
                                    }

                                }
                            }
                            else if (MinPrice != 0 && MaxPrice != 0)
                            {
                                foreach (var itemm in buffer)
                                {
                                    itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                    if (itemm.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice && itemm.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice)
                                    {
                                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                        {
                                            ItemHasFilterValue++;
                                        }

                                    }

                                }
                            }
                            else if (MinPrice != 0)
                            {
                                foreach (var itemm in buffer)
                                {
                                    itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                    if (itemm.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice)
                                    {
                                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                        {
                                            ItemHasFilterValue++;
                                        }

                                    }

                                }
                            }
                            else {
                                foreach (var itemm in buffer)
                                {
                                    itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                    if (itemm.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice)
                                    {
                                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                        {
                                            ItemHasFilterValue++;
                                        }

                                    }

                                }

                            }

                            //using (var db = new CheapestContext())
                            //{
                            //    var result = db.FilterValues.SingleOrDefault(b => b.id == item.id);
                            //    result.ItemsCount = ItemHasFilterValue;
                            //    db.SaveChanges();

                            //}

                            current.Filters.FirstOrDefault(x=>x.id==item.Filter.id).FilterValues.FirstOrDefault(x=>x.id==item.id).ItemsCount=ItemHasFilterValue;
                            ItemHasFilterValue = 0;
                        }
                        
                        ItemHasFilterValue = 0;
                     
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
                        if (MinPrice != null && MinPrice > 0)
                        {
                            current.MinPrice = MinPrice ?? default(int);
                        }

                        if (MaxPrice != null && MaxPrice > 0)
                        {
                            current.MaxPrice = MaxPrice ?? default(int);
                        }
                        return View("CategoryFilter", current);

                    }




                }

                else
                {
                    VwCategory current = new VwCategory();
                    current.Category = db.Categories.Find(id);
                    current.Filters = db.Filters.Where(x => x.Category.id == id).ToList();
                    // Category visit increment
                    #region CategoryClick Increment
                    using (var db = new CheapestContext())
                    {
                        var result = db.Categories.SingleOrDefault(b => b.id == id);
                        if (result != null)
                        {
                            result.ClickCount++;
                            db.SaveChanges();
                        }
                    }
                    #endregion

#region SearchQuery
                    var text = q;
                    var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
                    var words = text.Split().Select(x => x.Trim(punctuation));
                    int arrl = words.Count();
                    List<int> Match = new List<int>();
                    List<int> Buffer = new List<int>();

                    Match = (from item in db.Items
                             where (words.All(n => item.Name.Contains(n) && item.Category.id == id))
                             select item.id).ToList();


                    //remove last  1 char from search words 
                    words = RemoveOneChar(words.ToArray());
                    Buffer = (from item in db.Items
                              where (words.All(n => item.Name.Contains(n) && !Match.Contains(item.id) && item.Category.id == id))
                              select item.id).ToList();
                    if (Buffer.Count > 0)
                    {
                        Match.AddRange(Buffer);
                    }

                    Buffer.Clear();

                    //remove last  2 char from search words 
                    words = RemoveOneChar(words.ToArray());
                    Buffer = (from item in db.Items
                              where (words.All(n => item.Name.Contains(n) && !Match.Contains(item.id) && item.Category.id == id))
                              select item.id).ToList();
                    if (Buffer.Count > 0)
                    {
                        Match.AddRange(Buffer);
                    }

                    Buffer.Clear();

                    //remove last  3 char from search words 
                    words = RemoveOneChar(words.ToArray());
                    Buffer = (from item in db.Items
                              where (words.All(n => item.Name.Contains(n) && !Match.Contains(item.id) && item.Category.id == id))
                              select item.id).ToList();
                    if (Buffer.Count > 0)
                    {
                        Match.AddRange(Buffer);
                    }

                    Buffer.Clear();

                    List<Item> ResultMatch = (from item in db.Items
                                              where (Match.Contains(item.id))
                                              select item).ToList();
                    #endregion
#region Initializing search Result List forfiltering
                    List<Item> init = new List<Item>();
                    if (MinPrice == 0 && MaxPrice == 0)
                    {

                        init = ResultMatch.OrderByDescending(x => x.ClickCount).ToList();
                    }
                    else if (MinPrice > 0 && MaxPrice > 0)
                    {
                        init = ResultMatch.OrderByDescending(x => x.ClickCount).Where(x => x.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice && x.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice).ToList();

                    }
                    else if (MinPrice > 0 )
                    {
                        init = ResultMatch.OrderByDescending(x => x.ClickCount).Where(x => x.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice).ToList();
                    }

                    else
                    {
                        init = ResultMatch.OrderByDescending(x => x.ClickCount).Where(x => x.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice).ToList();
                    }

                    #endregion
                    int ItemHasFilterValue = 0;
                    foreach (var item in db.FilterValues)
                    {
                        if (MinPrice == 0 && MaxPrice == 0)
                        {
                            foreach (var itemm in init)
                            {
                                if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                {
                                    ItemHasFilterValue++;
                                }

                            }
                        }
                        else if (MinPrice != 0 && MaxPrice != 0)
                        {
                            foreach (var itemm in init)
                            {
                                itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                if (itemm.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice && itemm.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice)
                                {
                                    if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                    {
                                        ItemHasFilterValue++;
                                    }

                                }

                            }
                        }
                        else if (MinPrice != 0)
                        {
                            foreach (var itemm in init)
                            {
                                itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                if (itemm.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice)
                                {
                                    if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                    {
                                        ItemHasFilterValue++;
                                    }

                                }

                            }
                        }
                        else
                        {
                            foreach (var itemm in init)
                            {
                                itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                if (itemm.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice)
                                {
                                    if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                    {
                                        ItemHasFilterValue++;
                                    }

                                }

                            }

                        }

                        //using (var db = new CheapestContext())
                        //{
                        //    var result = db.FilterValues.SingleOrDefault(b => b.id == item.id);
                        //    result.ItemsCount = ItemHasFilterValue;
                        //    db.SaveChanges();

                        //}

                        current.Filters.FirstOrDefault(x => x.id == item.Filter.id).FilterValues.FirstOrDefault(x => x.id == item.id).ItemsCount = ItemHasFilterValue;
                        ItemHasFilterValue = 0;
                    }

                    ItemHasFilterValue = 0;
                    //current.Filters = current.Category.Filters.ToList();
                    current.Category.Name = UsaTextInfo.ToTitleCase(current.Category.Name);
                    current.Subcategories = new List<VwSubcategory>();
                    current.Items = new List<Item>();
                    //bura atacayiq
                    //Burada Filter tetbiq olunubsa countunu tuturuq
                    List<Item> buffer = new List<Item>();
                    List<int> itemspec = new List<int>();
                    
                    int SpecCount = 0;
                    int count = 0;
                    foreach (var item in SpecIds)
                    {
                        if (item.FilterValues != null)
                        {
                            SpecCount++;
                        }
                    }

                    foreach (var item in SpecIds)
                    {
                        if (item.FilterValues != null)
                        {
                            foreach (var values in item.FilterValues)
                            {
                                FilterValue temp = db.FilterValues.FirstOrDefault(x => x.id == values);
                                current.Filters.FirstOrDefault(x => x.id == temp.Filter.id).FilterValues.FirstOrDefault(y => y.id == temp.id).IsChecked = true;
                            }
                        }
                    }

                    if (SpecCount == 0)
                    {
                        
                            current.Items=init;
                            current.ItemsCount = current.Items.Count();
                        
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
                        current.SearchQuery = q;
                        current.MinPrice = MinPrice??default(int);
                        current.MaxPrice = MaxPrice ?? default(int);
                        return View("CategoryFilter", current);
                    }
                    else
                    {
                        if (MinPrice!=0||MaxPrice!=0)
                        {
                            init.Clear();
                            if (MinPrice==0)
                            {
                                init = ResultMatch.Where(x => x.ItemMerches.OrderBy(z=>z.PriceNormal).FirstOrDefault(y => y.PriceNormal <= MaxPrice) != null).ToList();
                            }
                            else if (MaxPrice==0)
                            {
                               
                                    init = ResultMatch.Where(x => x.ItemMerches.OrderBy(z => z.PriceNormal).FirstOrDefault(y => y.PriceNormal >= MinPrice) != null).ToList();
                               
                            }
                            else
                            {
                                init = ResultMatch.Where(x => x.ItemMerches.OrderBy(z => z.PriceNormal).FirstOrDefault(y => y.PriceNormal >= MinPrice) != null&& x.ItemMerches.FirstOrDefault(y => y.PriceNormal <= MinPrice)!=null).ToList();
                            }

                        }
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
int ItemHasFilterValue2 = 0;
                        foreach (var item in db.FilterValues)
                        {
                            if (MinPrice == 0 && MaxPrice == 0)
                            {
                                foreach (var itemm in buffer)
                                {
                                    if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                    {
                                        ItemHasFilterValue2++;
                                    }

                                }
                            }
                            else if (MinPrice != 0 && MaxPrice != 0)
                            {
                                foreach (var itemm in buffer)
                                {
                                    itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                    if (itemm.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice && itemm.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice)
                                    {
                                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                        {
                                            ItemHasFilterValue2++;
                                        }

                                    }

                                }
                            }
                            else if (MinPrice != 0)
                            {
                                foreach (var itemm in buffer)
                                {
                                    itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                    if (itemm.ItemMerches.FirstOrDefault().PriceNormal >= MinPrice)
                                    {
                                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                        {
                                            ItemHasFilterValue2++;
                                        }

                                    }

                                }
                            }
                            else
                            {
                                foreach (var itemm in buffer)
                                {
                                    itemm.ItemMerches = itemm.ItemMerches.OrderBy(x => x.PriceNormal).ToList();
                                    if (itemm.ItemMerches.FirstOrDefault().PriceNormal <= MaxPrice)
                                    {
                                        if (itemm.ItemSpecs.Where(x => x.FilterValueID == item.id).Count() != 0)
                                        {
                                            ItemHasFilterValue2++;
                                        }

                                    }

                                }

                            }

                            //using (var db = new CheapestContext())
                            //{
                            //    var result = db.FilterValues.SingleOrDefault(b => b.id == item.id);
                            //    result.ItemsCount = ItemHasFilterValue;
                            //    db.SaveChanges();

                            //}

                            current.Filters.FirstOrDefault(x => x.id == item.Filter.id).FilterValues.FirstOrDefault(x => x.id == item.id).ItemsCount = ItemHasFilterValue2;
                            ItemHasFilterValue2 = 0;
                        }

                        ItemHasFilterValue2 = 0;
                        current.Items = buffer;
                        int itemstoskip = (pageId - 1) * 36 ?? default(int);
                        current.Items = current.Items.Skip(itemstoskip).OrderByDescending(x => x.ClickCount).Take(36).ToList();
                        current.ItemsCount = buffer.Count();
                        current.PagesCount = (buffer.Count - 1) / 36 + 1;
                        current.CurrentPage = pageId ?? default(int);
                        current.CategotyItemCount = current.Items.Count();
                        current.SearchQuery = q;
                        current.HasChildren = false;
                        if (MinPrice != null && MinPrice > 0)
                        {
                            current.MinPrice = MinPrice ?? default(int);
                        }

                        if (MaxPrice != null && MaxPrice > 0)
                        {
                            current.MaxPrice = MaxPrice ?? default(int);
                        }
                        //if (current.Items.Count == 0)
                        //{
                        //    return RedirectToAction("/NotFound");
                        //}

                        return View("CategoryFilter", current);

                    }
                }
            }

            else
            {
                
                return RedirectToAction("/NotFound");
            }
        }

        [ActionName("Category")]
        public ActionResult Category( int? id, int? pageId, string q,double? MinPrice, double? MaxPrice)
        {
           

            ItemsAndAllCount.Items = new List<Item>();
            if (id!=null)
            {
                if (string.IsNullOrEmpty(q))
                {
                    VwCategory current = new VwCategory();
                    current.Category = db.Categories.Find(id);
                    if (current.Category.Children.Count > 0)
                    {
                        current.TopFiveCatsMobile = new List<VwMobileCategoryAndItems>();
                        List<Category> calculateCat = current.Category.Children.OrderByDescending(x => x.ClickCount).Take(5).ToList();

                        foreach (var cat in calculateCat)
                        {
                            VwMobileCategoryAndItems tmp = new VwMobileCategoryAndItems();
                            tmp.Category = cat;
                            tmp.Items = CalculateItems(cat).Items.OrderByDescending(x => x.ClickCount).Take(2).ToList();
                            if (tmp.Items.Count>0)
                            {
                                current.TopFiveCatsMobile.Add(tmp);
                            }
                            ItemsAndAllCount.Items.Clear();
                            ItemsAndAllCount.count = 0;
                        }

                    }
                    if (current.Category==null)
                    {
                        return RedirectToAction("PageNotFound");
                    }
                    current.SiteSelected = db.Items.Where(x => x.SiteSelection == true).OrderByDescending(x => x.ClickCount).ToList();
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
                    foreach (var item in db.FilterValues.Where(x=>x.Filter.id!=6))
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
                    CheapestContext dbs = new CheapestContext();
                    current.Filters = dbs.Categories.Find(id).Filters.ToList();
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

                    current.Items = current.Items.Skip(itemstoskip).Take(36).OrderByDescending(x => x.ClickCount).ToList();
                    


                    if (current.Category.Children.Count == 0)
                    {
                        current.HasChildren = false;
                    }
                    else
                    {
                        current.HasChildren = true;

                    }
                    if (current.Items.Count == 0)
                    {
                        return RedirectToAction("/NotFound");
                    }


                    return View(current);


                }
                //search varsa
                else
                {
                    VwCategory current = new VwCategory();
                    current.Category = db.Categories.Find(id);
                    if (current.Category.Children.Count > 0)
                    {
                        current.TopFiveCatsMobile = new List<VwMobileCategoryAndItems>();
                        List<Category> calculateCat = current.Category.Children.OrderByDescending(x => x.ClickCount).Take(5).ToList();

                        foreach (var cat in calculateCat)
                        {
                            VwMobileCategoryAndItems tmp = new VwMobileCategoryAndItems();
                            tmp.Category = cat;
                            tmp.Items = CalculateItems(cat).Items.OrderByDescending(x => x.ClickCount).Take(2).ToList();
                            current.TopFiveCatsMobile.Add(tmp);
                        }

                    }
                    if (current.Category == null)
                    {
                        return RedirectToAction("PageNotFound");
                    }
                    current.SiteSelected = db.Items.Where(x => x.SiteSelection == true).OrderByDescending(x => x.ClickCount).ToList();
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
                    var text = q;
                    var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
                    var words = text.Split().Select(x => x.Trim(punctuation));
                    int arrl = words.Count();
                    List<int> Match = new List<int>();
                    List<int> Buffer = new List<int>();

                    Match = (from item in db.Items
                             where (words.All(n => item.Name.Contains(n)&&item.Category.id==id))
                             select item.id).ToList();


                    //remove last  1 char from search words 
                    words = RemoveOneChar(words.ToArray());
                    Buffer = (from item in db.Items
                              where (words.All(n => item.Name.Contains(n) && !Match.Contains(item.id)&&item.Category.id==id))
                              select item.id).ToList();
                    if (Buffer.Count > 0)
                    {
                        Match.AddRange(Buffer);
                    }

                    Buffer.Clear();

                    //remove last  2 char from search words 
                    words = RemoveOneChar(words.ToArray());
                    Buffer = (from item in db.Items
                              where (words.All(n => item.Name.Contains(n) && !Match.Contains(item.id)&& item.Category.id == id))
                              select item.id).ToList();
                    if (Buffer.Count > 0)
                    {
                        Match.AddRange(Buffer);
                    }

                    Buffer.Clear();

                    //remove last  3 char from search words 
                    words = RemoveOneChar(words.ToArray());
                    Buffer = (from item in db.Items
                              where (words.All(n => item.Name.Contains(n) && !Match.Contains(item.id)&& item.Category.id == id))
                              select item.id).ToList();
                    if (Buffer.Count > 0)
                    {
                        Match.AddRange(Buffer);
                    }

                    Buffer.Clear();

                    List<Item> ResultMatch = (from item in db.Items
                                              where (Match.Contains(item.id))
                                              select item).ToList();


                    int ItemHasFilterValue = 0;
                    foreach (var item in db.FilterValues)
                    {
                       
                        foreach (var itemm in ResultMatch)
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
                    current.Subcategories = new List<VwSubcategory>();
                    current.Items = new List<Item>();
                    cnt = 0;
                    current.Items=ResultMatch;
                    current.ItemsCount = current.Items.Count();
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

                    current.Items = current.Items.Skip(itemstoskip).Take(36).OrderByDescending(x => x.ClickCount).ToList();
                    current.PagesCount = (current.Items.Count - 1) / 36 + 1;
                    current.SearchQuery = q;
                    current.CategotyItemCount = current.Items.Count();
                    return View(current);
                }


            }

            else
            {
                return RedirectToAction("/NotFound");
            }
        }
        //search method to remove words endings
        public string[] RemoveOneChar(string[] words)
        {
            for (int i = 0; i < words.Count(); i++)
            {
              words[i]=words[i].Remove(words[i].Length - 1);
            }
            return words;
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

        public ActionResult Search(string q, int? pageId)
        {
            if (!string.IsNullOrEmpty(q))
            {
                VwCategory current = new VwCategory();

                current.SiteSelected = db.Items.Where(x => x.SiteSelection == true).OrderByDescending(x => x.ClickCount).ToList();
                

                

                foreach (var cat in db.Categories)
                {
                    if (db.Categories.Where(x => x.Name.ToLower() == q.ToLower()).FirstOrDefault() != null)
                    {
                        int CatId = db.Categories.Where(x => x.Name.ToLower() == q.ToLower()).FirstOrDefault().id;
                        return RedirectToAction("Category", new { id = CatId });

                    };

                }

                var text = q;
                var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
                var words = text.Split().Select(x => x.Trim(punctuation));
                int arrl = words.Count();
                List<int> Match = new List<int>();
               List<int> Buffer = new List<int>();

                Match = (from item in db.Items
                                       where (words.All(n => item.Name.Contains(n) ))
                                       select item.id).ToList();
                    
               
                //remove last  1 char from search words 
                words=RemoveOneChar(words.ToArray());
                Buffer = (from item in db.Items
                         where (words.All(n => item.Name.Contains(n)&&!Match.Contains(item.id)))
                         select item.id).ToList();
                if (Buffer.Count>0)
                {
                    Match.AddRange(Buffer);
                }

                Buffer.Clear();

                //remove last  2 char from search words 
                words = RemoveOneChar(words.ToArray());
                Buffer = (from item in db.Items
                          where (words.All(n => item.Name.Contains(n) && !Match.Contains(item.id)))
                          select item.id).ToList();
                if (Buffer.Count > 0)
                {
                    Match.AddRange(Buffer);
                }

                Buffer.Clear();

                //remove last  3 char from search words 
                words = RemoveOneChar(words.ToArray());
                Buffer = (from item in db.Items
                          where (words.All(n => item.Name.Contains(n) && !Match.Contains(item.id)))
                          select item.id).ToList();
                if (Buffer.Count > 0)
                {
                    Match.AddRange(Buffer);
                }

                Buffer.Clear();

                List<Item> ResultMatch = (from item in db.Items
                                          where (Match.Contains(item.id))
                                          select item).ToList();
                if (ResultMatch.Count>0)
                {
                    List<Category> CurrentCat = ResultMatch.Select(x => x.Category).ToList();
                    CurrentCat = CurrentCat.Distinct().ToList();
                    List<VwSubcategory> fordisplay = new List<VwSubcategory>();
                    foreach (var item in CurrentCat)
                    {
                        VwSubcategory local = new VwSubcategory();
                        local.SubCategory = item;
                        local.ItemsCount = db.Categories.Find(item.id).Items.Where(x => Match.Contains(x.id)).Count();
                        fordisplay.Add(local);
                    }
                    current.Subcategories = fordisplay;
                }


                else
                {
                    return RedirectToAction("SearchNotFound", new { search = q });
                }


                current.CategotyItemCount = ResultMatch.Count();

                current.PagesCount = (ResultMatch.Count - 1) / 36 + 1;
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

                current.Items = ResultMatch.Skip(itemstoskip).Take(36).ToList();


                current.HasChildren = true;
                current.SearchQuery = q;
                return View(current);
            }

            else return RedirectToAction("SearchNotFound",new {search=q });
        }

        public ActionResult Generate()
        {

            return Content(Crypto.HashPassword("123"));
        }
    }
}