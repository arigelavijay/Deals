using RxShopyAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RxShopyAdmin.Controllers
{
    public class DealsController : Controller
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

                var categories = dbCntx.deals
                                 .Join(dbCntx.categories,
                                     a => a.categoryId,
                                     b => b.id,
                                     (a, b) => new { A = a, B = b })
                                 .Join(dbCntx.categoryconfigs,
                                    ab => ab.B.id,
                                    c => c.categoryId,
                                    (ab, c) => new { AB = ab, C = c })
                                 .Where(x => x.AB.A.isActive == true &&
                                             x.AB.A.count > x.AB.A.sold &&
                                             x.AB.A.startsOn <= DateTime.Now &&
                                             x.AB.A.endsOn >= DateTime.Now)
                                 .GroupBy(x => new { x.AB.B.id, x.AB.B.name })
                                 .Select(x => new CategoryDisplayConfig()
                                 {
                                     id = x.FirstOrDefault().AB.B.id,
                                     name = x.FirstOrDefault().AB.B.name,
                                     display = x.FirstOrDefault().C.displayItems,
                                     isMore = x.FirstOrDefault().C.isMore
                                 }).ToList<CategoryDisplayConfig>();
                /*
                var sqlQuery = "Select * from" +
                                "(Select B.id as id, B.name as name, C.displayItems as display, C.isMore as isMore from deal as A " +
                                "inner join category as B " +
                                "on A.categoryId = B.id " +
                                "left join categoryconfig as C " +
                                "on B.id = C.categoryId " +
                                "Where A.isActive = 1 and A.count > A.sold and A.startsOn <= now() and A.endsOn >= now() " +
                                "group by B.id, B.name) x ";

                var categories = dbCntx.Database.SqlQuery<CategoryDisplayConfig>(sqlQuery).ToList<CategoryDisplayConfig>();
                */
                return View(categories);
            }
        }

        public ActionResult update(List<CategoryDisplayConfig> obj)
        {
            using (var dbCntx = new dealsEntities())
            {
                var listCat = obj.Select(x => x.id).ToList<int>();
                var categoryConfigList = dbCntx.categoryconfigs
                                                    .Where(x => listCat.Contains(x.categoryId))
                                                    .Select(x => x)
                                                    .ToList<categoryconfig>();

                for (var i = 0; i < obj.Count; i++)
                {
                    var catItem = categoryConfigList.Where(x => x.categoryId == obj[i].id)
                                            .Select(x => x)
                                            .FirstOrDefault<categoryconfig>();

                    catItem.displayItems = obj[i].display;
                    catItem.isMore = obj[i].isMore;
                }
                dbCntx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }

    public class CategoryDisplayConfig
    {
        public int id { get; set; }
        public string name { get; set; }
        public int display { get; set; }
        public bool isMore { get; set; }
    }
}
