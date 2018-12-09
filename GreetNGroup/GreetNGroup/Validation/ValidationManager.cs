﻿using GreetNGroup.Claim_Controls;
using GreetNGroup.Data_Access;
using GreetNGroup.SiteUser;
using GreetNGroup.Tokens;
using System;
using System.Collections.Generic;
using GreetNGroup;
namespace GreetNGroup.Validation
{
    public static class ValidationManager
    {
        /// <summary>
        /// Checks to see if the person who is creating an accout is allowed to
        /// </summary>
        /// <param name="userName">New user Name</param>
        /// <param name="city">New City Location</param>
        /// <param name="state">New State Location</param>
        /// <param name="country">New Country Location</param>
        /// <param name="DOB">New user's Date of birth</param>
        public static void checkAddToken(List<string> claims, String userName, String city, String state, String country, DateTime DOB)
        {
            try
            {
                string temp = "test";
                List<string> _requireAdminRights = new List<string> { "AdminRights" };
                var currentUserToken = new Token(temp);
                currentUserToken.Claims = claims;
                var canAdd = ClaimsAuthorization.VerifyClaims(currentUserToken, _requireAdminRights);
                //If they have the claims they will be able to create a new account but if they don't the function will throw an error
                if (canAdd == true)
                {
                    var attributeCheck = checkAddAttributes(userName, city, state, country, DOB);
                    if (attributeCheck == true)
                    {
                        CheckQueries.CheckDuplicates(userName, city, state, country, DOB);
                    }
                    else
                    {
                        throw new System.ArgumentException("User attributes are not formatted correctly", "Attributes");
                    }

                }
                else
                {
                    throw new System.ArgumentException("User does not have the right Claims", "Claims");
                }
            }
            catch(Exception e)
            {
                //Log
            }
            

        }
        /// <summary>
        /// Checks to see if the person who is deleting an account has the right claims
        /// </summary>
        /// <param name="claims">List of claims</param>
        /// <param name="UID">User ID </param>
        public static void CheckDeleteToken(List<string> claims, string UID)
        {
            try
            {
                Console.WriteLine("hello");
                string temp = "test";
                List<string> _requireAdminRights = new List<string> {"AdminRights" };
                var currentUserToken = new Token(temp);
                currentUserToken.Claims = claims;
                var canDelete = ClaimsAuthorization.VerifyClaims(currentUserToken, _requireAdminRights);
                
                if (canDelete == true)
                {
                    if(CheckDeletedAttributes(UID) == true)
                    {
                        CheckQueries.CheckDeleteClaim(UID);
                    }
                    
                }
                else
                {
                    throw new System.ArgumentException("User does not have the right Claims", "Claims");
                }
            }
            catch(Exception e)
            {
                //log
            }
            
        }
        /// <summary>
        /// Checks to see if the person who is editing an account has the right claims
        /// </summary>
        /// <param name="claims">List of claims</param>
        /// <param name="UID">User ID </param>
        /// <param name="changeState">The new state(activated/deactivated) of the account</param>
        public static void CheckEnableToken(List<string> claims, string UserID, Boolean changeState)
        {
            try
            {
                Console.WriteLine("hello");
                string temp = "test";
                List<string> _requireAdminRights = new List<string> {"AdminRights" };
                var currentUserToken = new Token(temp);
                currentUserToken.Claims = claims;
                var canDelete = ClaimsAuthorization.VerifyClaims(currentUserToken, _requireAdminRights);
                if (canDelete == true)
                {
                    CheckQueries.CheckStateClaim(UserID, changeState);
                }
                else
                {
                    throw new System.ArgumentException("User does not have the right Claims", "Claims");
                }
            }
            catch (Exception e)
            {
                //log
            }

        }
        public static void CheckEditToken(List<string> claims, string UserID, List<string> attributeContents)
        {
            try
            {
                Console.WriteLine("Editing User");
                string temp = "test";
                List<string> _requireAdminRights = new List<string> { "AdminRights" };
                var currentUserToken = new Token(temp);
                currentUserToken.Claims = claims;
                var canEdit = ClaimsAuthorization.VerifyClaims(currentUserToken, _requireAdminRights);
                if (canEdit)
                {
                    CheckQueries.CheckEditClaim(UserID, attributeContents);
                }
            }
            catch (Exception e)
            {
                //log
            }
        }

        /// <summary>
        /// Checks the current attributes of a new user account
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="DOB"></param>
        /// <returns>Whether the inputs are valid or not</returns>
        public static Boolean checkAddAttributes(String userName, String city, String state, String country, DateTime DOB)
        {
            if(userName.Equals(null) || city.Equals(null) || state.Equals(null) || country.Equals(null) || DOB == null)
            {
                throw new System.ArgumentException("User attributes are not correct", "Attributes");
            }
            if (userName.Equals("") || city.Equals("") || state.Equals("") || country.Equals(""))
            {
                throw new System.ArgumentException("User attributes are not correct", "Attributes");
            }
            //Validates Input
            return true;
        }
        /// <summary>
        /// Verifies the userId and makes sure its valid
        /// </summary>
        /// <param name="UID">The passed userid</param>
        /// <returns>If the input is valid or not</returns>
        public static Boolean CheckDeletedAttributes(String UID)
        {
            try
            {
                if (UID.Equals(null))
                {
                    throw new System.ArgumentException("User attributes are not correct null", "Attributes");
                }
                if (UID.Equals(""))
                {
                    throw new System.ArgumentException("User attributes are not correct emptystring", "Attributes");
                }
            }
           catch(Exception e)
            {
                //Log error
                return false;
            }
            //Validates Input
            return true;
        }
        /// <summary>
        /// Checks if the account can be edited
        /// </summary>
        /// <param name="items">List of claims the user in the database has</param>
        /// <returns>Whether or not the account can be changed</returns>
        public static Boolean checkAccountEditable (List<UserClaim> items)
        {
            Console.WriteLine("hello");
            foreach (var i in items)
            {
                if(i.ClaimId.Equals("0005"))
                {
                    return false;
                }
            }
            return true;
        }


    }
}