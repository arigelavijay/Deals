using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using users.Models;
using users.ViewModels.Address;
using users.Extensions;

using Newtonsoft.Json;

namespace users.Controllers
{
    [RoutePrefix("api/address")]
    public class AddressController : ApiController
    {
        [HttpGet]
        [Route("{userId}")]
        public HttpResponseMessage address(int userId)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var addresses = dbCntx.addresses
                                .Join(dbCntx.states,
                                    address => address.state,
                                    states => states.id,
                                (address, states) => new { a = address, b = states })
                                .Where(x => x.a.userId == userId)
                                .OrderByDescending(x => x.a.createdOn)
                                .Select(x => new ShowAddressVm
                                {
                                    id = x.a.id,
                                    name = x.a.name,
                                    pincode = x.a.pincode,
                                    address = x.a.address1,
                                    landmark = x.a.landmark,
                                    city = x.a.city,
                                    state = x.b.name,
                                    phone = x.a.phone,
                                    isDefault = x.a.isDefault
                                }).ToList<ShowAddressVm>();

                    response.Content = new StringContent(JsonConvert.SerializeObject(addresses));
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

        [HttpGet]
        [Route("{userId}/{id}")]
        public HttpResponseMessage GetAddressById(int userId, int id)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var address = dbCntx.addresses
                                    .Where(x =>
                                        x.userId == userId &&
                                        x.id == id)
                                    .Select(x =>
                                        new AddressVm
                                        {
                                            name = x.name,
                                            pincode = x.pincode,
                                            address = x.address1,
                                            landmark = x.landmark,
                                            city = x.city,
                                            state = x.state,
                                            phone = x.phone
                                        })
                                    .FirstOrDefault<AddressVm>();

                    /*
                    throw new HttpResponseException(new HttpResponseMessage() {
                        StatusCode = HttpStatusCode.InternalServerError
                    });*/

                    response.Content = new StringContent(JsonConvert.SerializeObject(address));
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

        [HttpPost]
        [Route("edit/{userId}/{id}")]
        public HttpResponseMessage Edit(int userId, int id, AddressVm obj)
        {
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var address = dbCntx.addresses
                                    .Where(x => 
                                            x.userId == userId &&
                                            x.id == id)
                                    .Select(x => x)
                                    .FirstOrDefault<address>();                              

                    address.name = obj.name;
                    address.pincode = obj.pincode;
                    address.address1 = obj.address;
                    address.landmark = obj.landmark;
                    address.city = obj.city;
                    address.state = obj.state;
                    address.phone = obj.phone;
                    address.createdOn = DateTime.UtcNow.IndianTime();

                    dbCntx.SaveChanges();

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

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("delete/{userId}/{id}")]
        public HttpResponseMessage delete(int userId, int id)
        {
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var address = dbCntx.addresses
                                    .Where(x =>
                                            x.userId == userId &&
                                            x.id == id)
                                    .Select(x => x)
                                    .FirstOrDefault<address>();

                    dbCntx.addresses.Remove(address);
                    dbCntx.SaveChanges();
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
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("states")]
        public HttpResponseMessage states()
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var states = dbCntx.states
                                    .Select(x => new {
                                        text = x.name,
                                        value = x.id })
                                    .ToList();

                    response.Content = new StringContent(JsonConvert.SerializeObject(states));
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

        [HttpPost]
        [Route("add/{userId}")]
        public HttpResponseMessage add(AddressVm obj, int userId)
        {

            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var address = new address
                    {
                        name = obj.name,
                        pincode = obj.pincode,
                        address1 = obj.address,
                        landmark = obj.landmark,
                        city = obj.city,
                        state = obj.state,
                        phone = obj.phone,
                        createdOn = DateTime.UtcNow.IndianTime(),
                        isActive = true,
                        userId = userId,
                        isDefault = false
                    };

                    dbCntx.addresses.Add(address);
                    dbCntx.SaveChanges();
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
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("defaultaddress/{userId}/{id}")]
        public HttpResponseMessage DefaultAddress(int userId, int id)
        {
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var address = dbCntx.addresses
                                    .Where(x =>
                                        x.userId == userId &&
                                        x.isActive == true)
                                    .Select(x => x)
                                    .ToList<address>();

                    address.ForEach(x =>
                    {
                        x.isDefault = false;
                    });

                    var defaultAddress = address.Where(x =>
                                                    x.id == id &&
                                                    x.userId == userId)
                                                 .Select(x => x)
                                                 .FirstOrDefault<address>();                    

                    defaultAddress.isDefault = true;
                    defaultAddress.createdOn = DateTime.UtcNow.IndianTime();
                    dbCntx.SaveChanges();
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
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        
    }
}
