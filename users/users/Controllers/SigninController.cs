using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using users.ViewModels.Signin;
using users.Models;
using users.Extensions;
using System.IO;
using users.Utilities;



namespace users.Controllers
{
    [RoutePrefix("api/signin")]
    public class SigninController : ApiController
    {
        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(registerVm reg)
        {
            var response = new HttpResponseMessage();
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dbCntx = new dbEntity())
                    {
                        var user = new user
                        {
                            firstName = reg.firstName,
                            lastName = reg.lastName,
                            userName = reg.userName,
                            email = reg.email,
                            password = reg.password,
                            createdOn = DateTime.UtcNow.IndianTime(),
                            isActive = true
                        };

                        dbCntx.users.Add(user);
                        dbCntx.SaveChanges();

                        response.Content = new StringContent(JsonConvert.SerializeObject(new {
                            id = user.id,
                            success = true
                        }));
                        response.StatusCode = HttpStatusCode.OK;
                    }
                }
                else
                {
                    var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                    response.Content = new StringContent(message);
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
        [Route("login/{guest_id}")]
        public HttpResponseMessage login(loginVm log, int guest_id)
        {
            var response = new HttpResponseMessage();
            try
            {
                if (ModelState.IsValid)
                {
                    using (var dbCntx = new dbEntity())
                    {
                        var user = (from row in dbCntx.users
                                       where row.userName.Equals(log.userName) &&
                                       row.password.Equals(log.password)
                                       select row).FirstOrDefault<user>();

                        if (user != null)
                        {
                            /* guest cart items */
                            var guestcartItems = dbCntx.usercarts
                                                .Where(
                                                    x => x.userId == guest_id && 
                                                         x.isguest == true)
                                                .Select(x => x).ToList<usercart>();


                            /* registered user cart items */
                            var userCartItems = dbCntx.usercarts
                                                .Where(x => 
                                                    x.userId == user.id && 
                                                    x.isActive == true && 
                                                    x.isguest == false)
                                                .Select(x => x).ToList<usercart>();

                            /* code for transferring guest user cart items to registered user cart items */
                            var listTemp = new List<int>();
                            for (var i = 0; i < guestcartItems.Count; i++)
                            {
                                var item = userCartItems
                                                .Where(x => x.dealId == guestcartItems[i].dealId)
                                                .Select(x => x)
                                                .FirstOrDefault<usercart>();

                                if (item != null)
                                {
                                    var dealItem = dbCntx.deals
                                                    .Where(x => x.dealId == item.dealId)
                                                    .FirstOrDefault<deal>();

                                    if ((dealItem.count - dealItem.sold) >= (item.quantity + guestcartItems[i].quantity))
                                        item.quantity = item.quantity + guestcartItems[i].quantity;
                                    else
                                        item.quantity = (dealItem.count - dealItem.sold).Value;
                                    
                                    listTemp.Add(guestcartItems[i].Id);
                                }
                                else
                                {
                                    guestcartItems[i].userId = user.id;
                                    guestcartItems[i].isguest = false;
                                }                                    
                            }

                            /* removing items from guest cart if that item present in registered user cart */
                            foreach (var i in listTemp)
                            {
                                var item = guestcartItems
                                            .Where(x => x.Id == i)
                                            .Select(x => x)
                                            .FirstOrDefault<usercart>();

                                dbCntx.usercarts.Remove(item);
                            }

                            dbCntx.SaveChanges();
                            response.Content = new StringContent(JsonConvert.SerializeObject(new
                            {
                                success = true,
                                message = "Success",
                                id = user.id,
                                userName = user.userName
                            }));                            
                        }
                        else
                        {
                            response.Content = new StringContent(JsonConvert.SerializeObject(new {                                
                                success = false,
                                message = "Invalid username or password"
                            }));                            
                        }                        
                    }
                }
                else
                {
                    var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));

                    response.Content = new StringContent(message);                                            
                }
                response.StatusCode = HttpStatusCode.OK;
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
        [Route("checkusername/{userName}")]
        public HttpResponseMessage checkusername(string userName)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var isAvail = dbCntx.users
                                        .Where(x => x.userName == userName)
                                        .Select(x => x)
                                        .Count() == 0;

                    response.Content = new StringContent(
                        JsonConvert.SerializeObject(new 
                        { 
                            isAvail = isAvail,
                            message = "Username is not available."
                        }));
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
        [Route("checkemail")]
        public HttpResponseMessage checkemail(EmailCheck obj)
        {
            var response = new HttpResponseMessage();
            try
            {
                //var obj = JsonConvert.DeserializeObject(email.ToString());
                using (var dbCntx = new dbEntity())
                {
                    var isAvail = dbCntx.users
                                        .Where(x => x.email == obj.email)
                                        .Select(x => x)
                                        .Count() == 0;

                    response.Content = new StringContent(
                        JsonConvert.SerializeObject(new 
                        { 
                            isAvail = isAvail,
                            message = "Email is not available."
                        }));
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
        [Route("forgetpassword/{username}")]
        public HttpResponseMessage ForgetPassword(string username)
        {
            var response = new HttpResponseMessage();
            try
            {
                using (var dbCntx = new dbEntity())
                {
                    var user = dbCntx.users
                                .Where(x => x.userName == username)
                                .Select(x => x)
                                .FirstOrDefault<user>();

                    JObject obj;
                    if (user != null)
                    {
                        var templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplates/ForgotPassword.txt");

                        string strText = "";
                        using (StreamReader sr = new StreamReader(templatePath))
                        {
                            while (sr.Peek() >= 0)
                            {
                                strText = sr.ReadToEnd();
                            }
                        }

                        var generator = new EmailGenerator();
                        new System.Threading.Thread(() =>
                        {
                            try
                            {
                                generator.ConfigMail(user.email, true, "Password Recovery", string.Format(strText, user.userName, user.password));
                            }
                            catch (HttpResponseException ex)
                            {
                                throw new ApiDataException((int)ex.Response.StatusCode, ex.Message, ex.Response.StatusCode);
                            }
                        }).Start();

                        obj = JObject.FromObject(new {
                            status = "Success",
                            msg = "password has sent to your registered email."
                        });
                    }
                    else
                    {
                        obj = JObject.FromObject(new {
                            status = "Not Found",
                            msg = "username doesnot exits"
                        });                        
                    }

                    response.Content = new StringContent(JsonConvert.SerializeObject(obj));
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

    public class EmailCheck
    {
        public string email { get; set; }
    }
}
