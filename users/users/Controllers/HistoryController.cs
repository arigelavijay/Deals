using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;

using users.Models;
using users.ViewModels.History;
using users.Extensions;

namespace users.Controllers
{
    [RoutePrefix("api/history")]
    public class HistoryController : ApiController
    {
        [HttpGet]
        [Route("{userId}/{dealId}")]
        public HttpResponseMessage history(int userId, int dealId)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var historyQuery = dbCntx.deals
                                    .Join(dbCntx.userhistories,
                                        a => a.dealId,
                                        b => b.dealId,
                                        (a, b) => new { A = a, B = b })
                                    .Where(x => x.B.userId == userId);                                    

                    #region
                    /*
                     First check whether user already visited the present deal.
                     If so, update the dateCreated of that object
                     Else insert new record
                    */
                    #endregion
                    var presentDeal = historyQuery
                                        .Where(x => x.B.dealId == dealId)
                                        .Select(x => x.B)
                                        .FirstOrDefault<userhistory>();

                    if (presentDeal == null)
                    {

                        var obj = new userhistory
                        {
                            userId = userId,
                            dealId = dealId,
                            dateCreated = DateTime.UtcNow.IndianTime(),
                            isActive = true
                        };

                        dbCntx.userhistories.Add(obj);
                    }
                    else
                    {
                        presentDeal.dateCreated = DateTime.UtcNow;
                    }
                    

                    
                    var history = historyQuery
                                    .Where(x => x.B.dealId != dealId)
                                    .OrderByDescending(x => x.B.dateCreated)
                                    .Select(x => new HistoryVm
                                    {
                                        dealId = x.A.dealId,
                                        description = x.A.description,
                                        name = x.A.name,
                                        dateCreated = x.B.dateCreated,
                                        image = x.A.image,
                                        vendorId = x.A.vendorId
                                    })
                                    .Take(5)
                                    .ToList<HistoryVm>();
                           

                    dbCntx.SaveChanges();

                    response.Content = new StringContent(JsonConvert.SerializeObject(history));
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
