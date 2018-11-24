﻿using System;
using GreetNGroup.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UserTest
    {
        #region Required Fields
        User Dylan = new User("dylanchhin123@gmail.com", "123", "Dylan", "Chin", "Lakewood", "CA", "USA", "12/25/1996",
                                "What is your favorite book?", "Cat in the Hat", "1a2s3d4f");
        #endregion
        
        #region Pass Tests
        //Tests that are expected to pass
        //Get Methods
        [TestMethod]
        public void GetUserName_Pass()
        {
            // Arrange
            string expected = "dylanchhin123@gmail.com";
            string actual;

            // Act
            actual = Dylan.Username;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetPassword_Pass()
        {
            // Arrange
            string expected = "123";
            string actual;

            // Act
            actual = Dylan.Password;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetFirstName_Pass()
        {
            // Arrange
            string expected = "Dylan";
            string actual;

            // Act
            actual = Dylan.Firstname;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetLastName_Pass()
        {
            // Arrange
            string expected = "Chin";
            string actual;

            // Act
            actual = Dylan.Lastname;

            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetCity_Pass()
        {
            // Arrange
            string expected = "Lakewood";
            string actual;

            // Act
            actual = Dylan.Cityloc;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetState_Pass()
        {
            // Arrange
            string expected = "CA";
            string actual;

            // Act
            actual = Dylan.Stateloc;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetCountry_Pass()
        {
            // Arrange
            string expected = "USA";
            string actual;

            // Act
            actual = Dylan.Countryloc;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetDOB_Pass()
        {
            // Arrange
            string expected = "12/25/1996";
            string actual;

            // Act
            actual = Dylan.DOB;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetSecurityQuestion_Pass()
        {
            // Arrange
            string expected = "What is your favorite book?";
            string actual;

            // Act
            actual = Dylan.SecurityQ;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetSecurityAnswer_Pass()
        {
            // Arrange
            string expected = "Cat in the Hat";
            string actual;

            // Act
            actual = Dylan.SecurityA;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        //Tests that are expected to pass
        //Set Methods
        [TestMethod]
        public void SetUserName_Pass()
        {
            // Arrange
            string expected = "BobJoe@yahoo.com";
            string actual;

            // Act
            Dylan.Username = "BobJoe@yahoo.com";
            actual = Dylan.Username;
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetPassword_Pass()
        {
            // Arrange
            string expected = "qwerty";
            string actual;

            // Act
            Dylan.Password = "qwerty";
            actual = Dylan.Password;
          

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetFirstName_Pass()
        {
            // Arrange
            string expected = "Bob";
            string actual;

            // Act
            Dylan.Firstname = "Bob";
            actual = Dylan.Firstname;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetLastName_Pass()
        {
            // Arrange
            string expected = "Joe";
            string actual;

            // Act
            Dylan.Lastname = "Joe";
            actual = Dylan.Lastname;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetCity_Pass()
        {
            // Arrange
            string expected = "Las Vegas";
            string actual;

            // Act
            Dylan.Cityloc = "Las Vegas";
            actual = Dylan.Cityloc;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetID_Pass()
        {
            // Arrange
            string expected = "1a2s3d4f";
            string actual;

            // Act
            actual = Dylan.userID;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        public void SetState_Pass()
        {
            // Arrange
            string expected = "Nevada";
            string actual;

            // Act
            Dylan.Stateloc = "Nevada";
            actual = Dylan.Stateloc;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetCountry_Pass()
        {
            // Arrange
            string expected = "Canada";
            string actual;

            // Act
            Dylan.Countryloc = "Canada";
            actual = Dylan.Countryloc;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetDOB_Pass()
        {
            // Arrange
            string expected = "12/22/1996";
            string actual;

            // Act
            Dylan.DOB = "12/22/1996";
            actual = Dylan.DOB;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSecurityQuestion_Pass()
        {
            // Arrange
            string expected = "What state were you born in?";
            string actual;

            // Act
            Dylan.SecurityQ = "What state were you born in?";
            actual = Dylan.SecurityQ;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void SetSecurityAnswer_Pass()
        {
            // Arrange
            string expected = "NY";
            string actual;

            // Act
            Dylan.SecurityA = "NY";
            actual = Dylan.SecurityA;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Fail Tests
        //Get Methods
        [TestMethod]
        public void GetUserName_Fail()
        {
            // Arrange
            string expected = "winnmoo@gmail.com";
            string actual;

            // Act
            actual = Dylan.Username;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void GetFirstName_Fail()
        {
            // Arrange
            string expected = "Winn";
            string actual;

            // Act
            actual = Dylan.Firstname;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void GetLastName_Fail()
        {
            // Arrange
            string expected = "Moo";
            string actual;

            // Act
            actual = Dylan.Firstname;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void GetCity_Fail()
        {
            // Arrange
            string expected = "Westminster";
            string actual;

            // Act
            actual = Dylan.Cityloc;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void GetState_Fail()
        {
            // Arrange
            string expected = "AZ";
            string actual;

            // Act
            actual = Dylan.Stateloc;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void GetCountry_Fail()
        {
            // Arrange
            string expected = "MY";
            string actual;

            // Act
            actual = Dylan.Countryloc;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void GetDOB_Fail()
        {
            // Arrange
            string expected = "12/22/1996";
            string actual;

            // Act
            actual = Dylan.DOB;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void GetSecurityQuestion_Fail()
        {
            // Arrange
            string expected = "Who is your favorite teacher?";
            string actual;

            // Act
            actual = Dylan.SecurityQ;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void GetSecurityAnswer_Fail()
        {
            // Arrange
            string expected = "AyyLmao";
            string actual;

            // Act
            actual = Dylan.SecurityA;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }

        #endregion
        
        #region Variable Input Tests
        [TestMethod]
        public void SetUserName_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.Username = "";
            actual = Dylan.Username;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetUserName_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.Username = null;
            actual = Dylan.Username;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPassword_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.Password = "";
            actual = Dylan.Password;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetPassword_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.Password = null;
            actual = Dylan.Password;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetFirstName_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.Firstname = "";
            actual = Dylan.Firstname;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetFirstName_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.Firstname = null;
            actual = Dylan.Firstname;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetLastName_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.Lastname = "";
            actual = Dylan.Lastname;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetLastName_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.Lastname = null;
            actual = Dylan.Lastname;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetCity_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.Cityloc = "";
            actual = Dylan.Cityloc;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetCity_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.Cityloc = null;
            actual = Dylan.Cityloc;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetState_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.Stateloc = "";
            actual = Dylan.Stateloc;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetState_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.Stateloc = null;
            actual = Dylan.Stateloc;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetCountry_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.Countryloc = "";
            actual = Dylan.Countryloc;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetCountry_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.Countryloc = null;
            actual = Dylan.Countryloc;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetDOB_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.DOB = "";
            actual = Dylan.DOB;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetDOB_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.DOB = null;
            actual = Dylan.DOB;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetSecurityQuestion_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.SecurityQ = "";
            actual = Dylan.SecurityQ;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetSecurityQuestion_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.SecurityQ  = null;
            actual = Dylan.SecurityQ;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetSecurityAnswer_InputEmptyString()
        {
            // Arrange
            String expected = "";
            String actual;

            // Act
            Dylan.SecurityA = "";
            actual = Dylan.SecurityA;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetSecurityAnswer_InputNull()
        {
            // Arrange
            String expected = null;
            String actual;

            // Act
            Dylan.SecurityA = null;
            actual = Dylan.SecurityA;
            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GetID_Fail()
        {
            // Arrange
            string expected = "1b2n3m5z";
            string actual;

            // Act
            actual = Dylan.userID;

            // Assert
            Assert.AreNotEqual(expected, actual);
        }
        #endregion
    }
}
