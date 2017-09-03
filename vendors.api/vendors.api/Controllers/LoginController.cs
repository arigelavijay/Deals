using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;

using vendors.api.ViewModels.Login;
using vendors.api.Models;

namespace vendors.api.Controllers
{
    
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("status")]
        public string status()
        {
            return "hi, iam working...!";
        }

        [HttpGet]
        [Route("mystatus")]
        public string mystatus()
        {
            return "mystatus hi, iam working...!";
        }


        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(LoginVm log)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var vendor = (from row in dbCntx.vendors
                                  where row.vendorname == log.username && row.password == log.password
                                  select row).FirstOrDefault<vendors.api.Models.vendor>();

                    if (vendor != null)
                    {
                        var token = new authentication_token();
                        token.userid = vendor.id;
                        token.token = System.Guid.NewGuid().ToString();
                        token.created = DateTime.UtcNow.IndianTime();
                        token.expiretime = DateTime.UtcNow.IndianTime().AddMinutes(20);

                        dbCntx.authentication_token.Add(token);
                        dbCntx.SaveChanges();

                        var tokenVm = new TokenVm
                        {
                            token = token.token,
                            id = token.userid,
                            status = true
                        };

                        response.Content = new StringContent(JsonConvert.SerializeObject(tokenVm));
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Content = new StringContent("Invalid username or password");
                        response.StatusCode = HttpStatusCode.OK;
                    }
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
        [Route("register")]
        public HttpResponseMessage Register(RegisterVm register)
        {
            var response = new HttpResponseMessage();

            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var vendor = new vendor();

                    vendor.firstname = register.firstName;
                    vendor.lastname = register.lastName;
                    vendor.vendorname = register.vendorName;
                    vendor.email = register.email;
                    vendor.password = register.password;
                    vendor.createddate = DateTime.UtcNow.IndianTime();
                    vendor.isactive = true;

                    dbCntx.vendors.Add(vendor);
                    dbCntx.SaveChanges();

                    response.Content = new StringContent("Success");
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
}
