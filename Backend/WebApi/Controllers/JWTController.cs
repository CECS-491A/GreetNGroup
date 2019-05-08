﻿using Gucci.ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gucci.ServiceLayer.Requests;

namespace WebApi.Controllers
{
    public class JWTController : ApiController
    {
        public class TokenRequest
        {
            public string token { get; set; }
        }

        [HttpPost]
        [Route("api/jwt/isvalidtoken")]
        public IHttpActionResult IsJWTTokenValid([FromBody] TokenRequest request)
        {
            try
            {
                var _jwtService = new JWTService();
                var isJwtValid = _jwtService.IsTokenValid(request.token);
                if (!isJwtValid)
                {
                    return Content(HttpStatusCode.BadRequest, false);
                }
                return Content(HttpStatusCode.OK, true);
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, false);
            }
            
        }

        [HttpPost]
        [Route("api/jwt/check")]
        public IHttpActionResult CheckUsersClaims([FromBody] ClaimCheckRequest request)
        {
            var _jwtService = new JWTService();
            var _gngLoggerService = new LoggerService();

            var claimsToCheckResult = _jwtService.CheckUserClaims(request.JWT, request.ClaimsToCheck);
            var expirationCheckResult = _jwtService.IsTokenExpired(request.JWT);

            try
            {
                if (claimsToCheckResult.Equals("Authorized") &&
                    expirationCheckResult.Equals("NotExpired"))
                {
                    return Content(HttpStatusCode.OK, "Authorized to view content");
                }
                else if(claimsToCheckResult.Equals("Authorized") &&
                    expirationCheckResult.Equals("Expired"))
                {
                    return Content(HttpStatusCode.Forbidden, "There was a problem in checking your session, please " +
                        "try again");
                }
                else if (claimsToCheckResult.Equals("Unauthorized"))
                {
                    return Content(HttpStatusCode.Forbidden, "You are unauthorized to view this content. If this " +
                        "was a mistake, please contact an admin");
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "There was an problem in checking your session, please re-login and try again");
                }
            }
            catch (Exception e)
            {
                _gngLoggerService.LogBadRequest(_jwtService.GetUserIDFromToken(request.JWT).ToString(),
                    request.Ip, request.UrlToEnter, e.ToString());
                return Content(HttpStatusCode.BadRequest, "Service is unavailable");
            }
        }
    }
}
