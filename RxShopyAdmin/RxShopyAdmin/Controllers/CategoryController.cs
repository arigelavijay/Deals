using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

using RxShopyAdmin.Models;

namespace RxShopyAdmin.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            using (var dbCntx = new dealsEntities())
            {

                Func<deal, bool> FuncDealsWhere = delegate(deal d)
                {
                    return (d.isActive == true &&
                           d.count > d.sold &&
                           d.startsOn <= DateTime.UtcNow.IndianTime() &&
                           d.endsOn >= DateTime.UtcNow.IndianTime());
                };
                
                var sqlQuery = "Select * from" +
                                "(Select B.id as id, B.name as name, ifnull(C.sequence, 1) as sequence from deal as A " +
                                "inner join category as B " +
                                "on A.categoryId = B.id " +
                                "left join categoryconfig as C " +
                                "on B.id = C.categoryId " +
                                "Where A.isActive = 1 and A.count > A.sold and A.startsOn <= now() and A.endsOn >= now() " +
                                "group by B.id, B.name) x order by sequence desc; ";
                
                var categories = dbCntx.Database.SqlQuery<CategoryConfigResult>(sqlQuery).ToList<CategoryConfigResult>();
                
                return View(categories);
            }
        }

        [HttpPost]
        public ActionResult update(string config)
        {
            var list = JsonConvert.DeserializeObject<List<CategoryConfigResult>>(config);
            

            using (var dbCntx = new dealsEntities())
            {
                var catConfigData = dbCntx.categoryconfigs
                                        .Where(x => x.isActive == true)
                                        .Select(x => x)
                                        .ToList<categoryconfig>();

                for (var i = 0; i < list.Count; i++)
                {
                    var catConfigItem = catConfigData
                                            .Where(x => x.categoryId == list[i].id)
                                            .Select(x => x)
                                            .FirstOrDefault<categoryconfig>();

                    if (catConfigItem != null)
                    {
                        catConfigItem.sequence = list[i].sequence;
                    }
                    else
                    {
                        var catConfig = new categoryconfig() 
                        {
                            categoryId = list[i].id,
                            sequence = list[i].sequence,
                            displayItems = 4,
                            isMore = true,
                            dateCreated = DateTime.UtcNow.IndianTime(),
                            isActive = true                            
                        };
                        dbCntx.categoryconfigs.Add(catConfig);                        
                    }                    
                }

                TempData["Msg"] = "Saved changes";
                dbCntx.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

    public class CategoryVm
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createdOn { get; set; }
        public bool isActive { get; set; }
    }

    public class Cat
    {
        public int id { get; set; }
        public string name { get; set; }        
    }

    public class CategoryConfigResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public int sequence { get; set; }
    }
}
