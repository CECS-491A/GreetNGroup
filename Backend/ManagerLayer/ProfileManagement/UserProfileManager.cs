﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Gucci.DataAccessLayer.Tables;
using Gucci.ServiceLayer.Interface;
using Gucci.ServiceLayer.Model;
using Gucci.ServiceLayer.Requests;
using Gucci.ServiceLayer.Services;
using Newtonsoft.Json;

namespace Gucci.ManagerLayer.ProfileManagement
{

    public class UserProfileManager
    {
        private readonly DateTime requiredAgeOfUser = DateTime.Now.AddYears(-18); // Must be 18 years old to use this application
        private IUserService _userService;
        private IJWTService _jwtServce;
        private RatingService _ratingService;

        public UserProfileManager()
        {
            _userService = new UserService();
            _jwtServce = new JWTService();
            _ratingService = new RatingService();
        }

        public string GetUserRating(int userID)
        {
            return _ratingService.GetRating(userID).ToString();
        }

        public HttpResponseMessage GetUser(string userID)
        {
            try
            {
                int convertedUserID = Convert.ToInt32(userID);
                if (!_userService.IsUsernameFoundById(convertedUserID))
                {
                    var httpResponseFail = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("User does not exist")
                    };
                    return httpResponseFail;
                }

                User retrievedUser = _userService.GetUserById(convertedUserID);
                if (retrievedUser == null)
                {
                    var httpResponseFail = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("User does not exist")
                    };
                    return httpResponseFail;
                }
                UserProfile up = new UserProfile
                {
                    FirstName = retrievedUser.FirstName,
                    LastName = retrievedUser.LastName,
                    UserName = retrievedUser.UserName,
                    DoB = retrievedUser.DoB.ToString("MMM dd, yyyy"),
                    City = retrievedUser.City,
                    State = retrievedUser.State,
                    Country = retrievedUser.Country,
                    EventCreationCount = retrievedUser.EventCreationCount,
                    Rating = GetUserRating(convertedUserID)
                };
                var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(up))
                };
                return httpResponse;
            }
            catch
            {
                //log
                var httpResponse = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Unable to get user")
                };
                return httpResponse;
            }
        }

        public HttpResponseMessage UpdateUserProfile(UpdateProfileRequest request)
        {
            var isSignatureTampered = _jwtServce.IsJWTSignatureTampered(request.JwtToken);
            if (isSignatureTampered){
                var httpResponseFail = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Session is invalid")
                };
                return httpResponseFail;
            }


            if(request.DoB > requiredAgeOfUser) // Check if the user is over 18
            {
                var httpResponseFail = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("This software is intended for persons over 18 years of age.")
                };
                return httpResponseFail;
            }

            List<string> userInfo = new List<string>
                {
                request.FirstName,
                request.LastName,
                request.DoB.ToString(),
                request.City,
                request.State,
                request.Country
            };
            
            foreach(string item in userInfo)
            {
                if (String.IsNullOrWhiteSpace(item))
                {
                    var httpResponseFail = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Fields cannot be null")
                    };
                    return httpResponseFail;
                }
            }

            int userID = _jwtServce.GetUserIDFromToken(request.JwtToken);
            User retrievedUser = _userService.GetUserById(userID);
            retrievedUser.FirstName = request.FirstName;
            retrievedUser.LastName = request.LastName;
            retrievedUser.DoB = request.DoB;
            retrievedUser.City = request.City;
            retrievedUser.State = request.State;
            retrievedUser.Country = request.Country;

            if (!retrievedUser.IsActivated) //Check to see if the profile is activated, if not, activate it
            {
                retrievedUser.IsActivated = true;
            }

            var isUserUpdated = _userService.UpdateUser(retrievedUser);
            if (!isUserUpdated)
            {
                var httpResponseFail = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Unable to update user")
                };
                return httpResponseFail;
            }

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Profile has been updated")
            };
            return httpResponse;
        }

        public HttpResponseMessage IsProfileActivated(string jwtToken)
        {
            try
            {
                var userIDFromToken = _jwtServce.GetUserIDFromToken(jwtToken);
                if (!_userService.IsUsernameFoundById(userIDFromToken))
                {
                    var failHttpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("false")
                    };
                    return failHttpResponse;
                }

                User retrievedUser = _userService.GetUserById(userIDFromToken);
                var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Convert.ToString(retrievedUser.IsActivated))
                };
                return httpResponse;
            }
            catch
            {
                var httpResponse = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Unable to check if user is activated at this time.")
                };
                return httpResponse;
            }
            
        }
        
        /*
        public int RateUser(RateRequest request, string rateeID)
        {
            try
            {
                int raterID = _jwtServce.GetUserIDFromToken(request.jwtToken);
                UserRating ur = new UserRating();
                ur.RatedId1 = Convert.ToInt32(rateeID);
                ur.RaterId1 = Convert.ToInt32(raterID);
                ur.Rating = Convert.ToInt32(request.rating);
                _ratingService.CreateRating(ur);
                return 1;
            }
            catch (FormatException)
            {
                //log
                return -1;
            }
        }
        */
    }
}