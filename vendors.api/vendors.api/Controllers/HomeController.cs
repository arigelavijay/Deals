using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using vendors.api.Models;
using vendors.api.ViewModels.Deals;

namespace vendors.api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult save(string vendorId, string name, string description, string count, string startsOn, string endsOn)
        {
            var dealId = -1;
            string actualFileName = string.Empty;
            string Message = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;                
                int size = file.ContentLength;
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/UploadedFiles")))
                        Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));

                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(actualFileName)));

                    using (var dbCntx = new dbEntity())
                    {
                        var deal = new deal
                        {
                            name = name,
                            vendorId = int.Parse(vendorId),
                            description = description,
                            count = int.Parse(count),
                            startsOn = DateTime.Parse(startsOn),
                            endsOn = DateTime.Parse(endsOn),
                            image = Path.GetFileName(actualFileName),
                            createdOn = DateTime.UtcNow.IndianTime(),
                            isActive = true
                        };

                        dbCntx.deals.Add(deal);
                        dbCntx.SaveChanges();

                        Message = "Success";
                        dealId = deal.dealId;
                        flag = true;
                    }
                    
                }
                catch (Exception ex)
                {
                    Message = "File upload failed! Please try again";
                }

            }
            return new JsonResult { Data = new { Message = Message, Status = flag, Id = dealId } };
        }
	}
}