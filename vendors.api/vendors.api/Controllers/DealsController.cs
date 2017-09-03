using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;

using SD = System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Dynamic;
using System.Data.Objects;
using System.Linq.Expressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

using vendors.api.Models;
using vendors.api.ViewModels.Deals;
using vendors.api.Core;




namespace vendors.api.Controllers
{
    [TokenFilter]
    [RoutePrefix("api/deals")]
    public class DealsController : ApiController
    {
        [HttpGet]
        [Route("status")]
        public string status()
        {
            return "hi, this is status from deals controllers";
        }

        [HttpGet]
        [Route("{vendorId}")]
        public HttpResponseMessage deals(int vendorId)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var deals = (from row in dbCntx.deals
                                 join b in dbCntx.locations
                                 on row.locationId equals b.locationId
                                 join c in dbCntx.categories
                                 on row.categoryId equals c.id
                                 where row.vendorId == vendorId
                                 select new ViewDealsVm
                                 {
                                     id = row.dealId,
                                     locationName = b.locationName,
                                     categoryName = c.name,
                                     bannerId = row.bannerId,
                                     name = row.name,
                                     description = row.description,
                                     startsOn = row.startsOn,
                                     endsOn = row.endsOn,
                                     count = row.count,
                                     fileName = row.image                                     
                                 }).ToList<ViewDealsVm>();

                    var settings = new JsonSerializerSettings {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    response.Content = new StringContent(JsonConvert.SerializeObject(deals, Formatting.None, settings));
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch(Exception ex)
            {
                response.Content = new StringContent(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        [HttpGet]
        [Route("{vendorId}/{dealId}")]
        public HttpResponseMessage getDeal(int vendorId, int dealId)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var deal = (from row in dbCntx.deals
                                join b in dbCntx.locations
                                on row.locationId equals b.locationId
                                join c in dbCntx.categories
                                on row.categoryId equals c.id
                                where row.vendorId == vendorId && row.dealId == dealId
                                select new ViewDealsVm
                                {
                                    id = row.dealId,
                                    locationId = b.locationId,
                                    locationName = b.locationName,
                                    categoryId = c.id,
                                    categoryName = c.name,
                                    bannerId = row.bannerId,
                                    name = row.name,
                                    description = row.description,
                                    startsOn = row.startsOn,
                                    endsOn = row.endsOn,
                                    count = row.count,
                                    fileName = row.image,
                                    unitPrice = row.unitPrice,
                                    sellingPrice = row.sellingPrice,
                                    discount = row.discount
                                }).FirstOrDefault<ViewDealsVm>();

                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    response.Content = new StringContent(JsonConvert.SerializeObject(deal, Formatting.None, settings));
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }
        
        [HttpPost]
        [ActionName("add")]
        [Route("add/{vendorId}")]
        public HttpResponseMessage Add(int vendorId)
        {
            var response = new HttpResponseMessage();
            var sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadedFiles/" + vendorId + "/Temp/");

            CreateDirectory(sPath);
            try
            {

                System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                int iUploadedCnt = 0;
                var deal = JsonConvert.DeserializeObject<DealsVm>(System.Web.HttpContext.Current.Request["obj"]);
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(deal.fileName);

                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];

                    if (hpf.ContentLength > 0)
                    {
                        if (!File.Exists(sPath + fileName))
                        {
                            hpf.SaveAs(sPath + fileName);
                            iUploadedCnt = iUploadedCnt + 1;
                        }
                    }
                }

                CropUploadedFile(deal, vendorId, fileName);
                using (var dbCntx = new dbEntity())
                {
                    var newdeal = new deal
                    {
                        vendorId = vendorId,
                        locationId = deal.location,
                        categoryId = deal.category,
                        bannerId = deal.bannerId,
                        name = deal.name,
                        description = deal.description,
                        startsOn = deal.startsOn,
                        endsOn = deal.endsOn,
                        count = deal.count,
                        image = fileName,
                        unitPrice = deal.unitPrice,
                        discount = deal.discount,
                        sellingPrice = deal.sellingPrice,
                        createdOn = DateTime.UtcNow.IndianTime(),
                        isActive = true,
                        sold = 0,
                        hasShipping = deal.hasShipping
                    };
                    dbCntx.deals.Add(newdeal);
                    dbCntx.SaveChanges();

                    response.Content = new StringContent(JsonConvert.SerializeObject(new { dealId = newdeal.dealId }));
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        [HttpPost]
        [Route("update/{vendorId}/{locationId}/{bannerId}/{dealId}")]
        public HttpResponseMessage update(int vendorId, int locationId, int bannerId, int dealId)
        {
            var response = new HttpResponseMessage();
            var sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadedFiles/" + vendorId + "/Temp/");
            CreateDirectory(sPath);
            try
            {
                var deal = JsonConvert.DeserializeObject<DealsVm>(System.Web.HttpContext.Current.Request["obj"]);
                using (var dbCntx = new dbEntity())
                {
                    var editDeal = (from row in dbCntx.deals
                                    where row.vendorId == vendorId && row.locationId == locationId && row.bannerId == bannerId && row.dealId == dealId
                                    select row).FirstOrDefault<deal>();

                    editDeal.description = deal.description;
                    editDeal.name = deal.name;

                    if (deal.fileName != editDeal.image)
                    {
                        var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(deal.fileName);
                        //CropUploadedFile(deal, vendorId, fileName);
                        
                        DeleteFile(System.Web.Hosting.HostingEnvironment.MapPath("~/UploadedFiles") + "\\" + vendorId + "\\" + editDeal.image);
                        editDeal.image = fileName;
                        System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
                        int iUploadedCnt = 0;

                        for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                        {
                            System.Web.HttpPostedFile hpf = hfc[iCnt];

                            if (hpf.ContentLength > 0)
                            {
                                if (!File.Exists(sPath + fileName))
                                {
                                    hpf.SaveAs(sPath + fileName);
                                    iUploadedCnt = iUploadedCnt + 1;
                                }
                            }
                        }

                        CropUploadedFile(deal, vendorId, fileName);
                    }

                    dbCntx.SaveChanges();

                    response.Content = new StringContent(JsonConvert.SerializeObject(new { dealId = editDeal.dealId }));
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        public void CropUploadedFile(DealsVm deal, int vendorId, string fileName)
        {
            try
            {
                
                var serverPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadedFiles");

                var userFilePath = serverPath + "\\" + vendorId + "\\";
                var tempFilePath = serverPath + "\\" + vendorId + "\\Temp\\";
                
                CreateDirectory(tempFilePath);

                //File.WriteAllBytes(tempFilePath + fileName, Convert.FromBase64String(deal.attachment));
                byte[] CropImage = Crop(tempFilePath + fileName, deal.dataWidth.Value, deal.dataHeight.Value, deal.dataX.Value, deal.dataY.Value);

                using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
                {
                    ms.Write(CropImage, 0, CropImage.Length);
                    using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                    {
                        CroppedImage.Save(userFilePath + fileName, CroppedImage.RawFormat);
                    }
                }

                DeleteFile(tempFilePath + fileName);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteFile(string path)
        {
            var fileInfo = new FileInfo(path);
            if(fileInfo.Exists)
                fileInfo.Delete();
        }

        public void CreateDirectory(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
                dirInfo.Create();
        }

        static byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            try
            {
                using (SD.Image OriginalImage = SD.Image.FromFile(Img))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        [HttpGet]
        [Route("locationscategories")]
        public HttpResponseMessage locationscategories()
        {
            var response = new HttpResponseMessage();

            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var locations = (from row in dbCntx.locations
                                     where row.isActive == true
                                     select new LocationsVm
                                     {
                                         id = row.locationId,
                                         name = row.locationName
                                     }).ToList<LocationsVm>();

                    var categories = (from row in dbCntx.categories
                                      where row.isActive == true
                                      select new CategoryVm
                                      {
                                          id = row.id,
                                          name = row.name
                                      }).ToList<CategoryVm>();

                    JObject data = JObject.FromObject(new {
                        locations = locations,
                        categories = categories
                    });

                    response.Content = new StringContent(JsonConvert.SerializeObject(data));
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        [HttpGet]
        [Route("dates/{locationId}/{bannerId}")]
        public HttpResponseMessage dates(int locationId, int bannerId)
        {
            var response = new HttpResponseMessage();
            Func<deal, bool> FuncWhere = delegate(deal d)
            {
                return d.locationId == locationId &&
                    d.bannerId == bannerId &&
                    EntityFunctions.TruncateTime(d.endsOn) >= EntityFunctions.TruncateTime(DateTime.UtcNow.IndianTime()) &&
                    d.isActive == true;
            };
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var deals = dbCntx.deals
                                .Where(FuncWhere)
                                .Select(x => x)
                                .ToList<deal>();

                    var datesList = new List<string>(); 
                    for (var i = 0; i < deals.Count; i++)
                    {
                        for (DateTime date = DateTime.UtcNow.IndianTime().Date; date <= deals[i].endsOn; date = date.AddDays(1))
                        {
                            if(!datesList.Contains(date.Date.ToShortDateString()))
                                datesList.Add(date.Date.ToShortDateString());
                        }
                    }
                    response.Content = new StringContent(JsonConvert.SerializeObject(datesList));
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        [HttpPost]
        [Route("pageddata/{vendorId}")]
        public HttpResponseMessage PagedData(int vendorId, ngTable obj)
        {
            var response = new HttpResponseMessage();
            Func<ViewDealsVm, Object> orderByFunc = delegate(ViewDealsVm Deal)
            {
                var _Obj = new Object();
                if (obj.sortColumn == "categoryName")
                    _Obj = Deal.categoryName;
                else if (obj.sortColumn == "locationName")
                    _Obj = Deal.locationName;
                else if (obj.sortColumn == "startsOn")
                    _Obj = Deal.startsOn;
                else if (obj.sortColumn == "endsOn")
                    _Obj = Deal.endsOn;
                else if (obj.sortColumn == "count")
                    _Obj = Deal.count;
                else if (obj.sortColumn == "name")
                    _Obj = Deal.name;

                return _Obj;
            };
            
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var deals = (from row in dbCntx.deals
                                 join b in dbCntx.locations
                                 on row.locationId equals b.locationId
                                 join c in dbCntx.categories
                                 on row.categoryId equals c.id
                                 where row.vendorId == vendorId
                                 select new ViewDealsVm
                                 {
                                     id = row.dealId,
                                     locationName = b.locationName,
                                     categoryName = c.name,
                                     bannerId = row.bannerId,
                                     name = row.name,
                                     description = row.description,
                                     startsOn = row.startsOn,
                                     endsOn = row.endsOn,
                                     count = row.count,
                                     fileName = row.image
                                 });

                    var listData = new List<ViewDealsVm>();
                    if(obj.sortType == "ASC")
                        listData = deals.OrderBy(orderByFunc).Skip(obj.offset).Take(obj.limit).ToList<ViewDealsVm>();
                    else if(obj.sortType == "DESC")
                        listData = deals.OrderByDescending(orderByFunc).Skip(obj.offset).Take(obj.limit).ToList<ViewDealsVm>();
                    

                    var count = dbCntx.deals.Where(x => x.vendorId == vendorId).Select(x => x).ToList().Count();

                    JObject result = JObject.FromObject(new {
                        data = listData,
                        total = count
                    });

                    response.Content = new StringContent(JsonConvert.SerializeObject(result));
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(ex.Message);
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }

        
    }

    public static class MyExtensions
    {
        public static IOrderedEnumerable<TSource> OrderByWithDirection<TSource, TKey> 
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool descending)
        {
            return descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
        }

        public static IOrderedQueryable<TSource> OrderByWithDirection<TSource, TKey> 
            (this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool descending)
        {
            return descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
        }
    }
}
