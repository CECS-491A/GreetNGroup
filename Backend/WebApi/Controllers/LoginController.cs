﻿using Gucci.ManagerLayer.LoginManagement;
using Gucci.ServiceLayer.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage Login([FromBody] SSOUserRequest request)
        {
            LoginManager loginMan = new LoginManager();
            var response = loginMan.Login(request);
            if(response == "-1")
            {
                var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Invalid Session")
                };
                return httpResponse;
            }
            else
            {
                var redirect = this.Request.CreateResponse(HttpStatusCode.Moved);
                redirect.Headers.Location = new Uri("https://greetngroup.com/login/" + response);
                return redirect;
            }
        }
    }
}
