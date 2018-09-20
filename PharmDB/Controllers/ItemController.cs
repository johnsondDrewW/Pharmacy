using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmDB.Models;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using PagedList;

namespace PharmDB.Controllers
{
    public class ItemController : Controller
    {
        private PharmDBcontext db = new PharmDBcontext();

        // GET: /Item/
        public ActionResult Index(int? page)
        {
            if (page == 0) {
                page = 1;
            }
            var temp = db.Items;
            return View(temp.ToList());
        }
        public ActionResult Browse(int? id) {

            //Functions similar to var temp = (page ?? 1)
            if (!id.HasValue) {
               return Redirect(Url.Action("Browse/1", "Item"));
            }
            var temp = db.Items.OrderBy(d => d.Name);
            return View(temp.ToPagedList((int)id,10));
        }

        public ActionResult Image(int? id) {

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null) {
                return HttpNotFound();
            }
            return View(item);
        }
            

        public ActionResult Reports()
      {
          var Report = new Report();
          Report.Sources = new List<string>();
          List<String> tempL=new List<string>();
          var temp = 00.00;
          var count = 0;
         Report.numOfItems= db.Items.Count();
          foreach(var item in db.Items)
          {
              temp += (item.Value * item.Qauntity);
          }
          Report.value = temp;
          var temp2 = (from d in db.Items select d.Source).ToList();
          tempL.AddRange((temp2).Distinct());
          
          foreach(var source in tempL)
          {
              temp = 00.00;
              count = 0;
              foreach(var item in db.Items.Where(s => s.Source.Contains(source)))
              {
                  count++ ;
                  temp += (item.Value * item.Qauntity);

              }
              Report.Sources.Add("~~" + source+ " donated " + count + " at an estimated value of $" + temp );
          }
          return View(Report);
      }
        //Get /Item/SearchDetailed/Description+SearchString+Filters
        public ActionResult SearchDetailed(string SearchData, string searchField, string tag, int? page)
        {
            // var SeperatedData = SearchData.Split('+');
              
            var Search = SearchData;
            var Qry = db.Items.Where(d => d.Name.Contains(""));
            if (searchField != "" && SearchData !="")
            {
                switch (searchField)
                {
                    case "Name":
                        {
                            Qry = from d in db.Items where d.Name.Contains(Search) select d;
                            break;
                        }
                    case "Description":
                        {
                            Qry = from d in db.Items where d.Descritption.Contains(Search) select d;
                            break;
                        }
                    case "Source":
                        {
                            Qry = from d in db.Items where d.Source.Contains(Search) select d;
                            break;
                        }
                    case "Comments":
                        {
                            Qry = from d in db.Items where d.Comments.Contains(Search) select d;
                            break;
                        }


                }
            }
            if (tag != ""&& tag != null)
            {
                var SeperatedFilters = tag.Split(',');
                foreach (var filter in SeperatedFilters)
                {
                    Qry = from d in Qry where d.tags.Contains(filter) select d;
                }
            }
            if(!page.HasValue) {
                page = 1;
            }
            
            return View(Qry.OrderBy(x => x.tags).ToPagedList((int)page,10));
        }

        // GET: /Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<string> FilterN(string filter)
        {
            var catQry = from d in db.Categories where d.ParentCategory.Contains(filter + ".") select d.Name;
            return new JavaScriptSerializer().Serialize(new SelectList(catQry));
        }

        [HttpPost]
        public async Task<string> FilterD(string filter)
        {
            var CatQry = from d in db.Categories where d.ParentCategory.Contains(filter + ".") select d;
            var description = "";
            foreach(var cat in CatQry)
            {
                description += (cat.Name + ": " + cat.Description + "\n");
            }
            return new JavaScriptSerializer().Serialize(description);
        }
        // GET: /Item/Create
        public ActionResult Create()
        {
            ViewBag.Catlst = new SelectList(from d in db.Categories where d.ParentCategory.Contains("Main.") select d.Name);
            foreach(var cat in (from d in db.Categories where d.ParentCategory.Contains("Main.") select d))
            {
                ViewBag.Description += (cat.Name + ": " + cat.Description + "\n");
            }
            return View();
        }

        // POST: /Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Manufacturer,Size,Source,Value,ValueSource,StoredLocation,Qauntity,Comments,Descritption,tags")] Item item, HttpPostedFileBase image1, HttpPostedFileBase image2)
        {
            var temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent1*DrewA*");
            var temp2 = db.Categories.FirstOrDefault(i => i.Name == "Recent2*DrewA*");
            var temp3 = db.Categories.FirstOrDefault(i => i.Name == "Recent3*DrewA*");
            try
            {
                if (image1 != null)
                {
                    using (var binaryReader = new BinaryReader(image1.InputStream))
                    {
                        item.Image1 = binaryReader.ReadBytes(image1.ContentLength);
                    }
                }
                if (image2 != null)
                {
                    using (var binaryReader = new BinaryReader(image2.InputStream))
                    {
                        item.Image2 = binaryReader.ReadBytes(image2.ContentLength);
                    }
                }
                item.tags += ".";
                
                if (ModelState.IsValid)
                {
                    temp1.Description = temp2.Description;
                    temp2.Description = temp3.Description;
                    temp3.Description = "Added the item " + item.Name + ". Stored at " + item.StoredLocation + ".";
                    db.Items.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

               
            }
            catch
            {

            }
            return View(item);
        }

        // GET: /Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Manufacturer,Size,Source,Value,ValueSource,StoredLocation,Comments,Descritption,tags")] Item item, HttpPostedFileBase image1, HttpPostedFileBase image2, bool DImage1 = false, bool DImage2 = false)
        {
            var temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent1*DrewA*");
            var temp2 = db.Categories.FirstOrDefault(i => i.Name == "Recent2*DrewA*");
            var temp3 = db.Categories.FirstOrDefault(i => i.Name == "Recent3*DrewA*");
            var tempb = db.Items.Find(item.ID);
            if (image1 != null)
            {
                using (var binaryReader = new BinaryReader(image1.InputStream))
                {
                    item.Image1 = binaryReader.ReadBytes(image1.ContentLength);
                }
            }
            else
            {
                if (DImage1 == false)
                {
                    item.Image1 = tempb.Image1;
                }
            }
            if (image2 != null)
            {
                using (var binaryReader = new BinaryReader(image2.InputStream))
                {
                    item.Image2 = binaryReader.ReadBytes(image2.ContentLength);
                }
            }
            {
                if (DImage2 == false)
                {
                    item.Image2 = tempb.Image2;
                }
            }
            if (ModelState.IsValid)
            {
                temp1.Description = temp2.Description;
                temp2.Description = temp3.Description;
                temp3.Description = "Edited the item " + item.Name + ". Stored at " + item.StoredLocation + ".";
                db.Entry(tempb).CurrentValues.SetValues(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: /Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            var temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent1*DrewA*");
            var temp2 = db.Categories.FirstOrDefault(i => i.Name == "Recent2*DrewA*");
            var temp3 = db.Categories.FirstOrDefault(i => i.Name == "Recent3*DrewA*");

            temp1.Description = temp2.Description;
            temp2.Description = temp3.Description;
            temp3.Description = "Deleted the item " + item.Name + ". Stored at " + item.StoredLocation + ".";
            
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
