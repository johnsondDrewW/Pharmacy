using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmDB.Models;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PharmDB.Controllers
{
    public class CategoryController : Controller
    {
        private PharmDBcontext db = new PharmDBcontext();

        // GET: /Category/
        public ActionResult Index()
        {
            var temp1 = db.Categories.Where(d => d.ParentCategory.Contains("Main.")).OrderBy(d => d.Name).ToList();
            var tempDB = new List<Category>();
            while(temp1.Count != 0)
            {
                tempDB.Add(temp1.First());

                tempDB.AddRange(GetChildren(temp1.First()));

                temp1.Remove(temp1.First());
            }

            return View(tempDB);
        }
        public List<Category> GetChildren(Category parent)
        {
            var childrenList = db.Categories.Where(d => d.ParentCategory.Contains(parent.Name+ ".")).OrderBy(d => d.Name).ToList();
            var returnList = new List<Category>();//add all childern recursivly.
            while(childrenList.Count !=0)
            {
                returnList.Add(childrenList.First());
                returnList.AddRange(GetChildren(childrenList.First()));

                childrenList.Remove(childrenList.First());
            }
            return (returnList);
        }
        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
       
        //Action result for ajax call
        [HttpPost]
        public async Task<string> Filter(string filter)
        {
            var catLst = from d in db.Categories where d.ParentCategory.Contains(filter +".") select d.Name;
            return new JavaScriptSerializer().Serialize(new SelectList(catLst));
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            var CatLst = new List<string>();
            var catQry = from d in db.Categories where d.ParentCategory.Contains("Main.")  select d.Name;
            ViewBag.CatLst = new SelectList( catQry);
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,ParentCategory,Description")] Category category)
        {
            var temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent1*DrewA*");
            var temp2 = db.Categories.FirstOrDefault(i => i.Name == "Recent2*DrewA*");
            var temp3 = db.Categories.FirstOrDefault(i => i.Name == "Recent3*DrewA*");
            if(category.ParentCategory == null)
            { 
                category.ParentCategory = "Main";
            }
            category.ParentCategory = category.ParentCategory + ".";
            if(!category.ParentCategory.Contains("Main"))
            {
                category.ParentCategory = "Main,"+category.ParentCategory;
            }
            if (ModelState.IsValid)
            {
                temp1.Description = temp2.Description;
                temp2.Description = temp3.Description;
                temp3.Description = "created the category " + category.Name;  

                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<string> tags()
        {
            var List ="";
            foreach(var Cat in db.Categories)
            {
               if (!Cat.ParentCategory.Contains("DBUtilities"))
                {
                    if (List != "")
                        List += ",";
                    List += Cat.Name;

                   /* if (Cat.ParentCategory.Contains("Main."))
                    {
                        List += ",Main";
                    }
                    else
                    {
                        var temp = Cat.ParentCategory.Split(',');
                        temp[1] = temp[1].TrimEnd('.');
                        List +=(","+ temp[1]);
                    }*/
                    
                }
            }
            return new JavaScriptSerializer().Serialize(List);
        }
        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,ParentCategory,Description")] Category category)
        {
            var tempB = db.Categories.Find(category.ID);
            var temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent1*DrewA*");
            var temp2 = db.Categories.FirstOrDefault(i => i.Name == "Recent2*DrewA*");
            var temp3 = db.Categories.FirstOrDefault(i => i.Name == "Recent3*DrewA*");
            var temp = db.Categories.FirstOrDefault(i=> i.ParentCategory.Contains( tempB.Name));
            if( temp == null||(category.Name == tempB.Name&&category.ParentCategory == tempB.ParentCategory))
            {
                if (ModelState.IsValid)
                {
                    temp1.Description = temp2.Description;
                    temp2.Description = temp3.Description;
                    temp3.Description = "Edited the category " + tempB.Name;
                                       
                    db.Entry(tempB).CurrentValues.SetValues(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("","This Category has a sub category please edit those first");
               return View();
            }
            return View(category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
         
            return View(category);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var temp1 = db.Categories.FirstOrDefault(i => i.Name == "Recent1*DrewA*");
            var temp2 = db.Categories.FirstOrDefault(i => i.Name == "Recent2*DrewA*");
            var temp3 = db.Categories.FirstOrDefault(i => i.Name == "Recent3*DrewA*");
            Category category = db.Categories.Find(id);
            var temp = db.Categories.FirstOrDefault(i=> i.ParentCategory.Contains( category.Name));
            if (temp == null)
            {
                temp1.Description = temp2.Description;
                temp2.Description = temp3.Description;
                temp3.Description = "Deleted the category " + category.Name;

                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else { 
            
                ModelState.AddModelError("","This Category has a sub category please delete those first");
                return View(category);
            }
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
