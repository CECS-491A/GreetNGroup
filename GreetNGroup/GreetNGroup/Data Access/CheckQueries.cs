﻿using GreetNGroup.Account_Fields_Random_Generator;
using GreetNGroup.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreetNGroup.Data_Access
{
    public static class CheckQueries
    {
        /// <summary>
        /// Checks for duplicate usernames in the database
        /// </summary>
        /// <param name="userName">New Username</param>
        /// <param name="city">New City Location</param>
        /// <param name="state">New State Location</param>
        /// <param name="country">New Country Location</param>
        /// <param name="DOB">New Date of birth</param>
        public static void CheckDuplicates(String userName, String city, String state, String country, DateTime DOB)
        {
            try
            {
                using (var ctx = new GreetNGroupContext())
                {
                    var user = ctx.UserTables
                                  .Where(s => s.UserName == userName).Any();
                    Console.WriteLine(user);
                    if(user == false)
                    {
                        InsertUser(userName, city, state,country,DOB);
                    }
                    else
                    {
                        throw new System.ArgumentException("Name already exist", "Database");
                    }

                }
            }
            catch (Exception e)
            {
                //Log Excepetion
                //Console.WriteLine(e);
            }
        }
        /// <summary>
        /// Checks the claims of the account that is going to be deleted
        /// </summary>
        /// <param name="UID">Delete account user ID</param>
        public static void CheckDeleteClaim(string UserID)
        {
            try
            {
                using (var ctx = new GreetNGroupContext())
                {
                    var userClaims = ctx.UserClaims
                                  .Where(s => s.UserId == UserID).Count();
                    if (userClaims > 0)
                    {

                        List<string> checkClaims = DataBaseQueries.FindClaimsFromUser(UserID);
                        Boolean canDelete = ValidationManager.checkAccountEditable(checkClaims);
                        if (canDelete == true)
                        {
                            DeleteUser(UserID);
                        }
                        else
                        {
                            throw new System.ArgumentException("Account cannot be deleted", "Claim");
                        }
                        
                    }
                    else
                    {
                        throw new System.ArgumentException("user ID doesn't exist exist", "Database");
                    }

                }
            }
            catch (Exception e)
            {
                //Log Excepetion
                Console.WriteLine(e);
            }
        }
        public static void CheckEditClaim(string UserID, List<string> attributeContents)

        {
            try
            {

                var claims = DataBaseQueries.FindClaimsFromUser(UserID);
                Boolean canEdit = ValidationManager.checkAccountEditable(claims);
                if (canEdit)
                {

                    UpdateUser(UserID, attributeContents);

                }
                else
                {
                    throw new System.ArgumentException("Account cannot be updated", "Claim");
                }
   
            }
            catch (Exception e)
            {
                //Log Excepetion
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// Inserts a new user in the database given the following attributes
        /// </summary>
        /// <param name="userName">New Username</param>
        /// <param name="city">New City Location</param>
        /// <param name="state">New State Location</param>
        /// <param name="country">New Country Location</param>
        /// <param name="DOB">New Date of birth</param>
        public static void InsertUser(String userName, String city, String state, String country, DateTime DOB)
        {
            try
            {
                using (var ctx = new GreetNGroupContext())
                {
                    string UID = RandomFieldGenerator.generatePassword();
                    var newUser = new UserTable() { UserName = userName, City = city, State = state, Country = country, DoB = DOB, UserId = UID };
                    //Basic Claims everyuser should have
                    var newClaims1 = new UserClaim() { UserId = UID, ClaimId = "0001" };
                    var newClaims2 = new UserClaim() { UserId = UID, ClaimId = "0002" };
                    var newClaims3 = new UserClaim() { UserId = UID, ClaimId = "0003" };
                    ctx.UserTables.Add(newUser);
                    ctx.UserClaims.Add(newClaims1);
                    ctx.UserClaims.Add(newClaims2);
                    ctx.UserClaims.Add(newClaims3);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //Log excepetion e
            }
        }

        /// <summary>
        /// Deletes a user in the database given the following UID
        /// </summary>
        /// <param name="UID">User ID</param>
        public static void DeleteUser(String UserID)
        {
            try
            {
                using (var ctx = new GreetNGroupContext())
                {
                    var Userclaims = ctx.UserClaims
                                   .Where(s => s.UserId == UserID);
                    var user = ctx.UserTables
                                   .Where(s => s.UserId == UserID).Single();
                    ctx.UserClaims.RemoveRange(Userclaims);
                    ctx.UserTables.Remove(user);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //Log excepetion e
                Console.WriteLine(e);
            }
        }

        public static void UpdateUser(string UserID, List<string> attributeContents)
        {
            List<string> currentAttributes = new List<string>();
            var ctx = new GreetNGroupContext();

            //Try statement to fill the variables with user's current attributes
            try
            {
                using (ctx)
                {
                    var userToUpdate = ctx.UserTables
                                   .Where(s => s.UserId == UserID).Single();
                    currentAttributes.Add(userToUpdate.FirstName);
                    currentAttributes.Add(userToUpdate.LastName);
                    currentAttributes.Add(userToUpdate.UserName);
                    currentAttributes.Add(userToUpdate.City);
                    currentAttributes.Add(userToUpdate.State);
                    currentAttributes.Add(userToUpdate.Country);
                    currentAttributes.Add(userToUpdate.DoB.ToString());
                    currentAttributes.Add(userToUpdate.SecurityQuestion);
                    currentAttributes.Add(userToUpdate.SecurityAnswer);
                    currentAttributes.Add(userToUpdate.isActivated.ToString());
                }
            }
            catch (Exception e)
            {
                //log
            }
            //For loop to update the attributes with new values, if there are values to update it to
            for (int i = 0; i < attributeContents.Count; i++)
            {
                if (!attributeContents[i].Equals("."))
                {
                    currentAttributes[i] = attributeContents[i];
                }
            }
            //Try statement update the user in the database
            try
            {
                using (ctx)
                {
                    var userToUpdate = ctx.UserTables
                                   .Where(s => s.UserId == UserID).Single();
                    userToUpdate.FirstName = currentAttributes[0];
                    userToUpdate.LastName = currentAttributes[1];
                    userToUpdate.UserName = currentAttributes[2];
                    userToUpdate.City = currentAttributes[3];
                    userToUpdate.State = currentAttributes[4];
                    userToUpdate.Country = currentAttributes[5];
                    userToUpdate.DoB = Convert.ToDateTime(currentAttributes[6]);
                    userToUpdate.SecurityQuestion = currentAttributes[7];
                    userToUpdate.SecurityAnswer = currentAttributes[8];
                    userToUpdate.isActivated = currentAttributes[9].Equals("true");
                    ctx.SaveChanges();
                }
            }
            catch(Exception e)
            {
                //log
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="changeState"></param>
        public static void ChangeState(string UserID, Boolean changeState)
        {
            try
            {
                using (var ctx = new GreetNGroupContext())
                {
                    var user = ctx.UserTables
                                   .Where(s => s.UserId == UserID).Single();
                    user.isActivated = changeState;
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //Log excepetion e
                Console.WriteLine(e);
            }
        }
    }
}