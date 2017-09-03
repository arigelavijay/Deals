using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using users.Models;
using users.ViewModels.Location;

namespace users.Controllers
{
    [RoutePrefix("api/location")]
    public class LocationController : ApiController
    {
        [HttpGet]      
        [Route("parents")]
        [ResponseType(typeof(List<ParentVm>))]
        public IHttpActionResult ParentLocations()
        {
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var parentsList = dbCntx.parentlocations
                                            .Where(x => x.isActive == true)
                                            .GroupJoin(dbCntx.locations,
                                                a => a.id,
                                                b => b.parentId,
                                                (a, b) => new { A = a, B = b })
                                            .Select(x => new ParentVm()
                                            {                                                
                                                name = x.A.parentname,
                                                id = x.A.id,
                                                locations = x.B.Where(y => y.parentId == x.A.id)
                                                               .Select(y => 
                                                                   new LocationVm()
                                                                    { 
                                                                        id = y.locationId, 
                                                                        name = y.locationName 
                                                                    })
                                            })
                                            .ToList<ParentVm>();
                                            

                    return Json<List<ParentVm>>(parentsList);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }
    }
}
