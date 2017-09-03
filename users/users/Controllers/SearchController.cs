using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

using users.Models;
using users.Extensions;

using Newtonsoft.Json;

namespace users.Controllers
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("{locationId}/{text}")]
        public HttpResponseMessage search(string text, int locationId)
        {
            Func<deal, bool> FuncDealsWhere = delegate(deal d)
            {
                return (d.locationId == locationId &&
                       d.isActive == true &&
                       d.count > d.sold &&
                       d.startsOn <= DateTime.UtcNow.IndianTime() &&
                       d.endsOn >= DateTime.UtcNow.IndianTime() &&
                       d.name.ToLower().Contains(text.ToLower())
                       );
            };

            Func<deal, object> FuncDealSelect = delegate(deal x)
            {
                return new {
                    dealId = x.dealId,
                    name = x.name,
                    vendorId = x.vendorId,
                    image = x.image,
                    unitPrice = x.unitPrice,
                    sellingPrice = x.sellingPrice
                };
            };

            

            var response = new HttpResponseMessage();
            try
            {
                var vendorsPortal = ConfigurationManager.AppSettings["vendorsPortal"];
                var vendorsFilesPath = ConfigurationManager.AppSettings["vendorsFilesPath"];
                using (var dbCntx = new dbEntity())
                {
                    var results = dbCntx.deals.Where(FuncDealsWhere)
                                            .Select(x => new {
                                                dealId = x.dealId,
                                                name = x.name,
                                                imageUrl = vendorsPortal + string.Format(vendorsFilesPath, x.vendorId, x.image),
                                                unitPrice = x.unitPrice,
                                                sellingPrice = x.sellingPrice
                                            })
                                            .ToList();

                    response.Content = new StringContent(JsonConvert.SerializeObject(results));
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (HttpResponseException ex)
            {
                throw new ApiDataException((int)ex.Response.StatusCode, ex.Message, ex.Response.StatusCode);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(500, ex.Message, HttpStatusCode.BadRequest);
            }

            return response;
        }
    }
}
