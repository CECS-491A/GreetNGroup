﻿using System;
using System.Collections.Generic;
using GreetNGroup.SiteUser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using GreetNGroup;
using GreetNGroup.Validation;

[TestClass]
public class UserManageTest
{
    UserAccount Dylan = new UserAccount("dylanchhin123@gmail.com", "Asdfg123", "Dylan", "Chin", "Lakewood", "CA", "USA", new DateTime(1996, 12, 25),
                           "What is your favorite book?", "Cat in the Hat", "1a2s3d4f", 1, true);
    UserAccount Chris = new UserAccount("chrism@gmail.com", "Tgnuj8346", "Chris", "Evans", "Los Angelos", "CA", "USA", new DateTime(1990, 6, 10),
                           "What is your favorite book?", "Charlie and the Chocolate Factory", "2d4e5f5w", 0, true);


    [TestMethod]
    public void AddAccount_ValidParameters_Pass()
    {

        //Arange
        Boolean expected = true;
        Boolean actual = false;
        //Act
        try
        {
            Dylan.AddAccount("HowdyYall@gmail.com", "Houston", "TX", "USA", new DateTime(2007, 5, 28));
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserName == "HowdyYall@gmail.com").Single();

                if (user.UserName.Equals("HowdyYall@gmail.com") && user.City.Equals("Houston") && user.State.Equals("TX") && user.Country.Equals("USA") && (DateTime.Compare(user.DoB, new DateTime(2007, 5, 28)) == 0)) 
                {
                    Console.WriteLine("Pass");
                    actual = true;
                }
                else
                {
                    actual = false;
                }
                


            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            actual = false;
        }
        //Assert
        Assert.AreEqual(actual, expected);
    }
    [TestMethod]
    public void AddAccount_UserNameExist_Fail()
    {

        //Arange
        Boolean expected = true;
        Boolean actual = true;
        //Act
       
        try
        {
            Dylan.AddAccount("HowdyYall@gmail.com", "Sacramento", "CA", "USA", new DateTime(2008, 5, 28));
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserName == "HowdyYall@gmail.com").Single();
               
                if (user.UserName.Equals("HowdyYall@gmail.com") && user.City.Equals("Sacramento") && user.State.Equals("CA") && user.Country.Equals("USA") && (DateTime.Compare(user.DoB, new DateTime(2008, 5, 28)) == 0))
                {
                    Console.WriteLine("Pass");
                    actual = true;
                }
                else
                {
                    actual = false;
                }
                


            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            actual = false;
        }
        //Assert
        Assert.AreNotEqual(actual, expected);
    }
    [TestMethod]
    public void AddAccount_WrongClaims_Fail()
    {

        //Arange
        Boolean expected = true;
        Boolean actual = true;
        //Act

        try
        {
            Chris.AddAccount("newAccount@gmail.com", "Sacramento", "CA", "USA", new DateTime(2008, 5, 28));
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserName == "newAccount@gmail.com").Count();
                if(user > 0)
                {
                    actual = true;
                }
                else
                {
                    actual = false;
                }


            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            actual = false;
        }
        //Assert
        Assert.AreNotEqual(actual, expected);
    }
    [TestMethod]
    public void AddAccount_InvalidAttributesnull_Fail()
    {

        //Arange
        Boolean expected = true;
        Boolean actual = true;
        //Act

        try
        {
            ValidationManager.checkAddAttributes(null, null, null, null, new DateTime(2008, 5, 28));
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserName == null);
                foreach (var i in user)
                {
                    if (i.UserName.Equals(null) && i.City.Equals(null) && i.State.Equals(null) && i.Country.Equals(null) && (DateTime.Compare(i.DoB, new DateTime(2008, 5, 28)) == 0))
                    {
                        Console.WriteLine("Pass");
                        actual = true;
                    }
                    else
                    {
                        actual = false;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            actual = false;
        }
        //Assert
        Assert.AreNotEqual(actual, expected);
    }
    [TestMethod]
    public void AddAccount_InvalidAttributesEmptyString_Fail()
    {

        //Arange
        Boolean expected = true;
        Boolean actual = true;
        //Act

        try
        {
            ValidationManager.checkAddAttributes("", "", "", "", new DateTime());
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserName == "");
                foreach (var i in user)
                {
                    if (i.UserName.Equals("") && i.City.Equals("") && i.State.Equals("") && i.Country.Equals("") && (DateTime.Compare(i.DoB, new DateTime(2008, 5, 28)) == 0))
                    {
                        Console.WriteLine("Pass");
                        actual = true;
                    }
                    else
                    {
                        actual = false;
                    }
                }


            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            actual = false;
        }
        //Assert
        Assert.AreNotEqual(actual, expected);
    }
    [TestMethod]
    public void deleteAccount_Pass()
    {
        //Arange
        Boolean expected = true;
        Boolean actual = false;
        //Act
        try
        {
            Dylan.DeleteAccount("test");
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                                .Where(s => s.UserName == "test@gmail.com").Count();
                if (user > 0)
                {
                    actual = false;
                }
                else
                {
                    actual = true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            actual = false;
        }
        //Assert
        Assert.AreEqual(actual, expected);
    }
    [TestMethod]
    public void deleteAccount_WrongClaims_Fail()
    {
        //Arange
        Boolean expected = true;
        Boolean actual = true;
        //Act

        Chris.DeleteAccount("test");
        using (var ctx = new GreetNGroupContext())
        {
            var user = ctx.UserTables
                            .Where(s => s.UserName == "dylanchhinn@gmail.com").Count();
            if(user > 0)
            {
                actual = false;
            }
            else
            {
                actual = true;
            }


        }

        //Assert
        Assert.AreNotEqual(actual, expected);
    }
    [TestMethod]
    public void deleteAccount_AccountWrongClaims_Fail()
    {
        //Arange
        Boolean expected = true;
        Boolean actual = true;
        //Act

        Dylan.DeleteAccount("p01q2w9o38ei4r");
        using (var ctx = new GreetNGroupContext())
        {
            var user = ctx.UserTables
                          .Where(s => s.UserName == "dylanchhinn@gmail.com").Count();
            if (user > 0)
            {
                actual = false;
            }
            else
            {
                actual = true;
            }
        }
        //Assert
        Assert.AreNotEqual(actual, expected);
    }
    [TestMethod]
    public void deleteAccount_InvalidUserIDNull_Fail()
    {
        //Arange
        Boolean expected = true;
        Boolean actual = true;
        //Act
        var check = ValidationManager.CheckDeletedAttributes(null);
        if (check == true)
        {
            actual = true;
        }
        else
        {
            actual = false;
        }

        //Assert
        Assert.AreNotEqual(actual, expected);
    }
    [TestMethod]
    public void deleteAccount_InvalidUserIDEmptyString_Fail()
    {
        //Arange
        Boolean expected = true;
        Boolean actual = true;
        //Act
        var check = ValidationManager.CheckDeletedAttributes("");
        if (check == true)
        {
            actual = true;
        }
        else
        {
            actual = false;
        }
        
        //Assert
        Assert.AreNotEqual(actual, expected);
    }

    [TestMethod]
    public void editUser_ValidString_Pass()
    {
        Boolean expected = true;
        Boolean actual = true;

        var attributesToEdit = new List<string> { "Bob", "Dylan", "bobdylan@gmail.com", "Fountain Valley", "California"
            , "United States", null, null, null };

        if (!ValidationManager.checkEditAttributes(attributesToEdit))
        {
            actual = false;
        }
        Assert.AreNotEqual(actual, expected);
    }

    [TestMethod]
    public void changeEnable_TruetoFalse_Pass()
    {
        //Arange
        Boolean expected = false;
        Boolean actual;
        //Act
        try
        {
            Dylan.ChangeEnable("p03d928ej2838fo", false);
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserId == "p03d928ej2838fo").Single();
                if (user.isActivated == false)
                {
                    actual = false;
                }
                else
                {
                    actual = true;
                }
            }
        }
        catch (Exception e)
        {
            actual = false;
        }



        //Assert
        Assert.AreEqual(actual, expected);

    }
    [TestMethod]
    public void changeEnable_FalsetoTrue_Pass()
    {
        //Arange
        Boolean expected = true;
        Boolean actual;
        //Act
        try
        {
            var attributesToEdit = new List<string> { null, null, null, null, null
            , null, null, null, "true" };
            Dylan.UpdateAccount("p0499dj238e92j2", attributesToEdit);
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserId == "p0499dj238e92j2").Single();
                if (user.isActivated == true)
                {
                    actual = true;
                }
                else
                {
                    actual = false;
                }
            }
        }
        catch (Exception e)
        {
            actual = false;
        }
        //Assert
        Assert.AreEqual(actual, expected);

    }
    [TestMethod]
    public void changeEnable_WrongClaim_Pass()
    {
        //Arange
        Boolean expected = false;
        Boolean actual;
        //Act
        try
        {
            var attributesToEdit = new List<string> { null, null, null, null, null
            , null, null, null, "false" };
            Chris.UpdateAccount("test", attributesToEdit);
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserId == "test").Single();
                if (user.isActivated == false)
                {
                    actual = false;
                }
                else
                {
                    actual = true;
                }
            }
        }
        catch (Exception e)
        {
            actual = true;
        }
        //Assert
        Assert.AreNotEqual(actual, expected);

    }
    [TestMethod]
    public void changeEnable_AccountWrongClaim_Fail()
    {
        //Arange
        Boolean expected = false;
        Boolean actual;
        //Act
        try
        {
            var attributesToEdit = new List<string> { null, null, null, null, null
            , null, null, null, "false" };
            Chris.UpdateAccount("p01dj9wjd99u3u", attributesToEdit);
            using (var ctx = new GreetNGroupContext())
            {
                var user = ctx.UserTables
                              .Where(s => s.UserId == "p01dj9wjd99u3u").Single();
                if (user.isActivated == false)
                {
                    actual = false;
                }
                else
                {
                    actual = true;
                }
            }
        }
        catch (Exception e)
        {
            actual = true;
        }
        //Assert
        Assert.AreNotEqual(actual, expected);

    }
    //TODO: Add unit tests for editing other attributes
}